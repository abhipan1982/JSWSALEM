using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.BaseModels.DataContracts.Internal.WBF;
using PE.Common;
using PE.Helpers;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.Module.Core;

namespace PE.TRK.Base.Managers.Concrete
{
  public class TrackingEventHandlingManagerBase : BaseManager, ITrackingEventHandlingManagerBase
  {
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;
    protected readonly ITrackingProcessMeasurementsSendOffice ProcessMeasurementsSendOffice;
    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingFurnaceSendOffice FurnaceSendOffice;
    protected readonly ITrackingProcessMaterialEventSendOfficeBase TrackingProcessMaterialEventSendOffice;
    protected readonly ITrackingProcessQualityExpertTriggersSendOffice TrackingProcessQualityExpertTriggersSendOffice;
    protected readonly TrackingRawMaterialHandlerBase TrackingRawMaterialHandler;

    public TrackingEventHandlingManagerBase(IModuleInfo moduleInfo,
      ITrackingEventStorageProviderBase eventStorageProvider,
      ITrackingProcessMeasurementsSendOffice processMeasurementsSendOffice,
      ITrackingStorageProviderBase storageProvider,
      ITrackingFurnaceSendOffice furnaceSendOffice,
      ITrackingProcessMaterialEventSendOfficeBase trackingProcessMaterialEventSendOffice,
      TrackingRawMaterialHandlerBase trackingRawMaterialHandler,
      ITrackingProcessQualityExpertTriggersSendOffice trackingProcessQualityExpertTriggersSendOffice)
      : base(moduleInfo)
    {
      EventStorageProvider = eventStorageProvider;
      ProcessMeasurementsSendOffice = processMeasurementsSendOffice;
      StorageProvider = storageProvider;
      FurnaceSendOffice = furnaceSendOffice;
      TrackingProcessMaterialEventSendOffice = trackingProcessMaterialEventSendOffice;
      TrackingRawMaterialHandler = trackingRawMaterialHandler;
      TrackingProcessQualityExpertTriggersSendOffice = trackingProcessQualityExpertTriggersSendOffice;
    }

    public void Init()
    {
      // Not used in current version of Standard
      //ProcessMeasurementEvents();
      ProcessAggregatedMeasurementEvents();
      ProcessTrackingQueuePositionChangeEvents();
      ProcessTrackingPointEvents();
    }

    protected virtual void ProcessMeasurementEvents()
    {
      TaskHelper.FireAndForget(() =>
      {
        while (true)
        {
          try
          {
            if (!EventStorageProvider.MeasurementEventsToBeProcessed.IsEmpty)
            {
              EventStorageProvider.MeasurementEventsToBeProcessed
                .TryDequeue(out DcRelatedToMaterialMeasurementRequest request);

              if (request == null)
                continue;

              var result = ProcessMeasurementsSendOffice.SendProcessMeasurementRequestAsync(request).GetAwaiter().GetResult();

              if (!result.OperationSuccess)
                NotificationController.Warn($"Something went wrong while {MethodHelper.GetMethodName()} for MaterialId: {request.MaterialId} FeatureCode: {request.FeatureCode}");
            }
            else
              Task.Delay(20).GetAwaiter().GetResult();
          }
          catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
              $"Unique key violation while processing measurement.");
          }
          catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
              $"Existing reference key violation while processing measurement.");
          }
          catch (InternalModuleException ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
              ex.Message, ex.AlarmParams);
          }
          catch (Exception ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
              $"Unexpected error while processing measurement.");
          }
        }
      });
    }

    protected virtual void ProcessAggregatedMeasurementEvents()
    {
      TaskHelper.FireAndForget(() =>
      {
        while (true)
        {
          try
          {
            if (!EventStorageProvider.AggregatedMeasurementsToBeProcessed.IsEmpty)
            {
              EventStorageProvider.AggregatedMeasurementsToBeProcessed
                .TryDequeue(out DcAggregatedMeasurementRequest request);

              if (request == null)
                continue;

              var result = ProcessMeasurementsSendOffice.SendProcessAggregatedMeasurementRequestAsync(request)
                .GetAwaiter().GetResult();

              if (!result.OperationSuccess)
                NotificationController.Warn($"Something went wrong while {MethodHelper.GetMethodName()} for MaterialId: {request.MaterialId} AreaAssetCode: {request.AreaAssetCode}");
              else
              {
                TriggerQEEvaluation(request);
                ProcessFurnaceExitTemperature(request);
              }
            }
            else
              Task.Delay(20).GetAwaiter().GetResult();
          }
          catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
              $"Unique key violation while processing measurement.");
          }
          catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
              $"Existing reference key violation while processing measurement.");
          }
          catch (InternalModuleException ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
              ex.Message, ex.AlarmParams);
          }
          catch (Exception ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
              $"Unexpected error while processing measurement.");
          }
        }
      });
    }

    protected virtual void TriggerQEEvaluation(DcAggregatedMeasurementRequest request)
    {
      TaskHelper.FireAndForget(() =>
      {
        var qeResult = TrackingProcessQualityExpertTriggersSendOffice.ProcessMaterialAreaExitEventAsync(
          new DCAreaRawMaterial
          {
            RawMaterialId = request.MaterialId,
            AssetId = StorageProvider.AssetsDictionary[request.AreaAssetCode].AssetId,
          })
        .GetAwaiter().GetResult();

        if (!qeResult.OperationSuccess)
          NotificationController.Warn($"Something went wrong while ProcessMaterialAreaExitEventAsync for MaterialId: {request.MaterialId} AreaAssetCode: {request.AreaAssetCode}");
      });
    }

    protected virtual void ProcessFurnaceExitTemperature(DcAggregatedMeasurementRequest request)
    {
      TaskHelper.FireAndForget(() =>
      {
        if (request.AreaAssetCode == StorageProvider.DischargeTemperatureAreaAssetCode)
        {
          using var ctx = new PEContext();

          var measurement = ctx.MVHMeasurements.FirstOrDefault(m =>
            m.FKFeature.FeatureCode == StorageProvider.DischargeTemperatureFeatureCode &&
            m.FKRawMaterialId == request.MaterialId);

          var rawMaterial = ctx.TRKRawMaterials.FirstOrDefault(m => m.RawMaterialId == request.MaterialId);

          if (rawMaterial != null && measurement != null)
          {
            rawMaterial.FurnaceExitTemperature = measurement.ValueAvg;
          }

          ctx.SaveChanges();
        }
      });
    }

    protected void ProcessTrackingPointEvents()
    {
      TaskHelper.FireAndForget(() =>
      {
        while (true)
        {
          try
          {
            if (!EventStorageProvider.TrackingPointEventsToBeProcessed.IsEmpty)
            {
              EventStorageProvider.TrackingPointEventsToBeProcessed
                .TryDequeue(out TrackingEventArgs request);

              if (request == null || request.MaterialId == 0)
                continue;

              ProcessTrackingTriggers(request);
            }
            else
              Task.Delay(100).GetAwaiter().GetResult();
          }
          catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
              $"Unique key violation while processing tracking point.");
          }
          catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
              $"Existing reference key violation while processing tracking point.");
          }
          catch (InternalModuleException ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
              ex.Message, ex.AlarmParams);
          }
          catch (Exception ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
              $"Unexpected error while processing tracking point.");
          }
        }
      });
    }

    protected virtual void ProcessTrackingTriggers(TrackingEventArgs request)
    {
      bool isDummyMaterial = IsMaterialDummy(request.MaterialId);

      if (isDummyMaterial)
      {
        NotificationController.Warn($"Tracking trigger will be ignored because material: {request.MaterialId} is dummy");
        return;
      }

      AssignL3Material(request);
      ProcessFurnaceEvent(request);
      UpdateRawMaterialStatus(request);
      ProcessRoughingMillEvent(request);
      ProcessRodMillEvent(request);
      ProcessGarretMillEvent(request);
      SetRawMaterialRollingDate(request);
      ProcessCoilWeighingEvent(request);
      ProcessBundleWeighingEvent(request);
      ProcessCalculateWorkOrderKPIsEvent(request);
    }

    protected virtual bool IsMaterialDummy(long materialId)
    {
      try
      {
        using PEContext ctx = new PEContext();
        return ctx.TRKRawMaterials.FirstOrDefault(x => x.RawMaterialId == materialId)?.IsDummy ?? true;
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }

      return true;
    }

    protected virtual void SetRawMaterialRollingDate(TrackingEventArgs request)
    {
      TaskHelper.FireAndForget(async () => await SetRawMaterialRollingDateAsync(request));
    }

    public virtual async Task SetRawMaterialRollingDateAsync(TrackingEventArgs request)
    {
      if (request.AssetCode == TrackingArea.FCE_AREA && request.EventType == TrackingEventType.Exit)
      {
        using var ctx = new PEContext();
        var rawMaterial = await ctx.TRKRawMaterials.FirstAsync(r => r.RawMaterialId == request.MaterialId);
        rawMaterial.RollingStartTs = request.TriggerDate;
        await ctx.SaveChangesAsync();
      }

      if (IsAreaGarret(request.AssetCode) && request.EventType == TrackingEventType.Enter)
      {
        using var ctx = new PEContext();
        var rawMaterial = await ctx.TRKRawMaterials.FirstAsync(r => r.RawMaterialId == request.MaterialId);
        rawMaterial.RollingEndTs = request.TriggerDate;
        await ctx.SaveChangesAsync();
      }

      if (request.AssetCode == EventStorageProvider.LayingHeadAssetCode && request.EventType == TrackingEventType.Exit)
      {
        using var ctx = new PEContext();
        var rawMaterial = await ctx.TRKRawMaterials.FirstAsync(r => r.RawMaterialId == request.MaterialId);
        rawMaterial.RollingEndTs = request.TriggerDate;
        await ctx.SaveChangesAsync();
      }

      if (request.AssetCode == TrackingArea.ENTER_TABLE_AREA && request.EventType == TrackingEventType.Enter)
      {
        using var ctx = new PEContext();
        var rawMaterial = await ctx.TRKRawMaterials.FirstAsync(r => r.RawMaterialId == request.MaterialId);
        rawMaterial.RollingEndTs = request.TriggerDate;
        await ctx.SaveChangesAsync();
      }
    }

    protected virtual void ProcessCoilWeighingEvent(TrackingEventArgs request)
    {
      TaskHelper.FireAndForget(async () => await SendCoilReportOnWeighingExitAsync(request));
    }

    protected virtual void ProcessBundleWeighingEvent(TrackingEventArgs request)
    {
      TaskHelper.FireAndForget(async () => await SendBundleReportOnWeighingExitAsync(request));
    }

    public virtual async Task SendCoilReportOnWeighingExitAsync(TrackingEventArgs request)
    {
      await Task.CompletedTask;

      if (IsAreaCoilWeighing(request.AssetCode) && request.EventType == TrackingEventType.Exit)
      {
        TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice.SendProductReportAsync(
            new DCRawMaterial(request.MaterialId)),
            $"Something went wrong while sending coil report for material {request.MaterialId}");
      }
    }

    public virtual async Task SendBundleReportOnWeighingExitAsync(TrackingEventArgs request)
    {
      if (IsAreaBundleWeighing(request.AssetCode) && request.EventType == TrackingEventType.Exit)
      {
        try
        {
          await using var ctx = new PEContext();
          var rawMaterial = await TrackingRawMaterialHandler.FindRawMaterialWithProductByIdAsync(ctx, request.MaterialId);

          if (rawMaterial is null)
            throw new InternalModuleException($"Cannot find raw material [{request.MaterialId}] while processing bundle weighing exit event.", AlarmDefsBase.UnexpectedError);

          if (rawMaterial.FKProduct is null)
            throw new InternalModuleException($"Cannot find product related to raw material {rawMaterial.RawMaterialName} [{request.MaterialId}] while processing bundle weighing exit event.",
              AlarmDefsBase.UnexpectedError);

          TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice.SendProductReportAsync(
            new DCRawMaterial(rawMaterial.RawMaterialId)),
            $"Something went wrong while sending bundle report for material {rawMaterial.RawMaterialId}.");
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex);
        }
      }
    }

    public virtual void UpdateRawMaterialStatus(TrackingEventArgs request)
    {
      try
      {
        using var ctx = new PEContext();
        TRKRawMaterial rawMaterial =
          TrackingRawMaterialHandler.FindRawMaterialByIdAsync(ctx, request.MaterialId).GetAwaiter().GetResult();

        NotificationController.Debug(
          $"Setting status for raw material {rawMaterial.RawMaterialId}, last status: {rawMaterial.EnumRawMaterialStatus}");

        RawMaterialStatus newStatus = rawMaterial.EnumRawMaterialStatus;

        newStatus = GetRawMaterialStatus(request);

        if (newStatus > rawMaterial.EnumRawMaterialStatus)
        {
          rawMaterial.EnumRawMaterialStatus = newStatus;
          NotificationController.Debug(
            $"Setting status for raw material {rawMaterial.RawMaterialId}, new status: {rawMaterial.EnumRawMaterialStatus}");
          ctx.SaveChanges();

          TaskHelper.FireAndForget(() =>
            TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(
              new DCShiftCalendarId() { ShiftCalendarId = rawMaterial.FKShiftCalendarId }));

          //TaskHelper.FireAndForget(() =>
          //    AssignQuality(rawMaterial.RawMaterialId, InspectionResult.Good).GetAwaiter().GetResult()
          //  , $"Something went wrong while AssignQuality for Material: {rawMaterial.RawMaterialId}");
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating raw material status.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating raw material status.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
          $"Unexpected error while updating raw material status.");
      }
    }

    protected virtual void ProcessGarretMillEvent(TrackingEventArgs request)
    {
      if (request.EventType == TrackingEventType.Enter && IsAreaGarret(request.AssetCode))
      {
        var rawMaterial = TrackingRawMaterialHandler.FindRawMaterialByIdAsync(null, request.MaterialId).GetAwaiter().GetResult();
        if (rawMaterial != null && !rawMaterial.FKProductId.HasValue)
        {
          TaskHelper.FireAndForget(() =>
          {
            ProductCreationAsync(request.MaterialId, request.TriggerDate.ExcludeMiliseconds(), request.AssetCode).ConfigureAwait(false).GetAwaiter().GetResult();
            TrackingProcessMaterialEventSendOffice.SendProductReportAsync(new DCRawMaterial(request.MaterialId));
          });
        }
      }
    }

    protected virtual void ProcessRodMillEvent(TrackingEventArgs request)
    {
      if (request.AssetCode == EventStorageProvider.LayingHeadAssetCode)
      {
        TaskHelper.FireAndForget(() =>
        {
          var result = TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent()
          {
            EventType = request.EventType == TrackingEventType.Exit
              ? ChildMillEventType.Checkpoint1Out
              : ChildMillEventType.Checkpoint1In,
            AssetId = StorageProvider.AssetsDictionary[EventStorageProvider.LayingHeadAssetCode].AssetId,
            DateStart = request.TriggerDate.ExcludeMiliseconds(),
            DateEnd = request.TriggerDate.ExcludeMiliseconds(),
            RawMaterialId = request.MaterialId
          }).GetAwaiter().GetResult();

          if (!result.OperationSuccess)
            NotificationController.Warn($"Something went wrong while AddMillEvent for {EventStorageProvider.LayingHeadAssetCode}, MaterialId: {request.MaterialId}, EventType: {request.EventType}");
        });

        if (request.EventType == TrackingEventType.Exit)
        {
          PRMWorkOrder workOrder = TrackingRawMaterialHandler.GetWorkOrderByRawMaterialId(request.MaterialId)
                  .GetAwaiter().GetResult();

          TRKRawMaterial rawMaterial = TrackingRawMaterialHandler.FindRawMaterialByIdAsync(null, request.MaterialId).GetAwaiter().GetResult();

          if (rawMaterial != null && !rawMaterial.FKProductId.HasValue)
          {
            TaskHelper.FireAndForget(() => ProductCreationAsync(request.MaterialId, request.TriggerDate.ExcludeMiliseconds(), request.AssetCode).ConfigureAwait(false).GetAwaiter().GetResult());
            TaskHelper.FireAndForget(() => CallDelayTailLeaveAsync(request.MaterialId, request.TriggerDate.ExcludeMiliseconds()).ConfigureAwait(false).GetAwaiter().GetResult());
            TaskHelper.FireAndForget(() => ProcessDelayPointLeaveAsync(request.MaterialId).ConfigureAwait(false).GetAwaiter().GetResult());
          }
        }
        else
          TaskHelper.FireAndForget(() => CallDelayHeadEntersAsync(request.MaterialId, request.TriggerDate.ExcludeMiliseconds()).ConfigureAwait(false).GetAwaiter().GetResult());
      }
    }

    protected virtual void ProcessCalculateWorkOrderKPIsEvent(TrackingEventArgs request)
    {
      if (request.IsArea && request.AssetCode == TrackingArea.TRANSPORT_AREA && request.EventType == TrackingEventType.Enter)
      {
        //Product weight will be set on WeightNDR
        //UpdateProductWeight(request.MaterialId, request.Weight.Value).ConfigureAwait(false).GetAwaiter().GetResult();

        TaskHelper.FireAndForget(async () => await CalculateWorkOrderKPIsAsync(request.MaterialId));
      }
    }

    protected virtual void AssignL3Material(TrackingEventArgs request)
    {
      if (request.AssetCode == TrackingArea.CHG_AREA && request.EventType == TrackingEventType.Enter)
      {
        L3MaterialAssignment(request.MaterialId).GetAwaiter().GetResult();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
    }

    protected virtual void ProcessRoughingMillEvent(TrackingEventArgs request)
    {
      if (request.AssetCode == TrackingArea.RM_AREA && request.EventType == TrackingEventType.Enter)
      {
        TaskHelper.FireAndForget(() =>
        {
          try
          {
            SendOfficeResult<DataContractBase> result = TrackingProcessMaterialEventSendOffice
              .ProcessWorkOrderStatus(
                new DCRawMaterial(request.MaterialId) { AssetCode = request.AssetCode })
              .GetAwaiter().GetResult();
          }
          catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
              $"Unique key violation while processing roughing mill event.");
          }
          catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
              $"Existing reference key violation while processing roughing mill event.");
          }
          catch (InternalModuleException ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
              ex.Message, ex.AlarmParams);
          }
          catch (Exception ex)
          {
            ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
              $"Unexpected error while processing roughing mill event.");
          }
        });
      }
    }

    protected virtual void ProcessFurnaceEvent(TrackingEventArgs request)
    {
      if (request.AssetCode == TrackingArea.FCE_AREA)
      {
        if (request.EventType == TrackingEventType.Enter)
        {
          TaskHelper.FireAndForget(() =>
          {
            TrackingProcessMaterialEventSendOffice
              .AddMillEvent(
                new DCMillEvent
                {
                  EventType = ChildMillEventType.Charge,
                  RawMaterialId = request.MaterialId,
                  AssetId = StorageProvider.AssetsDictionary[request.AssetCode].AssetId,
                  DateStart = request.TriggerDate,
                  DateEnd = request.TriggerDate,
                })
              .ConfigureAwait(false).GetAwaiter().GetResult();
          }, "Something went wrong while sending Furnace charge operation to EVT module");

          TaskHelper.FireAndForget(() =>
              TrackingProcessMaterialEventSendOffice
                .ProcessWorkOrderStatus(
                  new DCRawMaterial(request.MaterialId) { AssetCode = request.AssetCode })
                .ConfigureAwait(false).GetAwaiter().GetResult()
            , $"Something went wrong while ProcessWorkOrderStatus for MaterialId: {request.MaterialId}, AssetCode: {request.AssetCode}");
        }
        else if (request.EventType == TrackingEventType.UnEnter)
        {
          TaskHelper.FireAndForget(() =>
          {
            TrackingProcessMaterialEventSendOffice
              .AddMillEvent(
                new DCMillEvent
                {
                  EventType = ChildMillEventType.Uncharge,
                  RawMaterialId = request.MaterialId,
                  AssetId = StorageProvider.AssetsDictionary[request.AssetCode].AssetId,
                  DateStart = request.TriggerDate,
                  DateEnd = request.TriggerDate,
                })
              .ConfigureAwait(false).GetAwaiter().GetResult();
          }, "Something went wrong while sending Furnace uncharge operation to EVT module");
        }
        else if (request.EventType == TrackingEventType.Exit)
        {
          TaskHelper.FireAndForget(() =>
          {
            TrackingProcessMaterialEventSendOffice
              .AddMillEvent(
                new DCMillEvent
                {
                  EventType = ChildMillEventType.Discharge,
                  RawMaterialId = request.MaterialId,
                  AssetId = StorageProvider.AssetsDictionary[request.AssetCode].AssetId,
                  DateStart = request.TriggerDate,
                  DateEnd = request.TriggerDate,
                })
              .ConfigureAwait(false).GetAwaiter().GetResult();
          }, "Something went wrong while sending Furnace discharge operation to EVT module");

          TaskHelper.FireAndForget(() =>
              FurnaceSendOffice
                .ProcessMaterialDischargeEventAsync(
                  new DCFurnaceMaterialDischarge()
                  {
                    DischargingTime = request.TriggerDate,
                    RawMaterialId = request.MaterialId,
                  })
                .ConfigureAwait(false).GetAwaiter().GetResult()
            , "Something went wrong while sending Furnace discharge operation to WBF module");
        }
        else if (request.EventType == TrackingEventType.UnExit)
        {
          TaskHelper.FireAndForget(() =>
          {
            TrackingProcessMaterialEventSendOffice
              .AddMillEvent(
                new DCMillEvent
                {
                  EventType = ChildMillEventType.Undischarge,
                  RawMaterialId = request.MaterialId,
                  AssetId = StorageProvider.AssetsDictionary[request.AssetCode].AssetId,
                  DateStart = request.TriggerDate,
                  DateEnd = request.TriggerDate,
                })
              .ConfigureAwait(false).GetAwaiter().GetResult();
          }, "Something went wrong while sending Furnace undischarge operation to EVT module");
        }
      }
    }

    protected virtual async Task CalculateWorkOrderKPIsAsync(long materialId)
    {
      try
      {
        var workOrder = await TrackingRawMaterialHandler.GetWorkOrderByRawMaterialId(materialId);

        if (workOrder != null)
        {
          var result = await TrackingProcessMaterialEventSendOffice.CalculateWorkOrderKPIsAsync(new() { WorkOrderId = workOrder.WorkOrderId });

          if (result.OperationSuccess)
            NotificationController.Debug($"Successful triggering KPI calculation module for materialId: {materialId}");
          else
            NotificationController.Error($"Error during triggering KPI calculation module for materialId: {materialId}");
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while calculating KPI for raw material [{materialId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while calculating KPI for raw material [{materialId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
          $"Unexpected error while calculating KPI for raw material [{materialId}].");
      }
    }

    protected void ProcessTrackingQueuePositionChangeEvents()
    {
      TaskHelper.FireAndForget(() =>
      {
        while (true)
        {
          try
          {
            if (!EventStorageProvider.TrackingQueuePositionChangeEvents.IsEmpty)
            {
              EventStorageProvider.TrackingQueuePositionChangeEvents
                .TryDequeue(out TrackingQueuePositionChangeEventArgs eventArg);

              if (eventArg == null)
              {
                NotificationController.Warn("TrackingQueuePositionChangeEvents - Dequeue failed");
                continue;
              }


              ProcessTrackingQueuePositionChange(eventArg.QueuePositions, eventArg.AssetCode);

              ModuleController.HmiRefresh(HMIRefreshKeys.TrackingManagement).ConfigureAwait(false).GetAwaiter().GetResult();

            }
            else
              Task.Delay(100).GetAwaiter().GetResult();
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}");
          }
        }
      });
    }

    /// <summary>
    /// Indicates if area is garret
    /// </summary>
    /// <param name="areaAssetCode"></param>
    /// <returns>True or false</returns>
    protected virtual bool IsAreaGarret(int areaAssetCode)
    {
      return areaAssetCode switch
      {
        var value when value == TrackingArea.GARRET_AREA => true,
        _ => false,
      };
    }

    /// <summary>
    /// Indicates if area is coil weighing
    /// </summary>
    /// <param name="areaAssetCode"></param>
    /// <returns>True or false</returns>
    protected virtual bool IsAreaCoilWeighing(int areaAssetCode)
    {
      return areaAssetCode switch
      {
        var value when value == TrackingArea.COIL_WEIGHING_AREA => true,
        _ => false,
      };
    }

    /// <summary>
    /// Indicates if area is coil weighing
    /// </summary>
    /// <param name="areaAssetCode"></param>
    /// <returns>True or false</returns>
    protected virtual bool IsAreaBundleWeighing(int areaAssetCode)
    {
      return areaAssetCode switch
      {
        var value when value == TrackingArea.BAR_WEIGHING_AREA => true,
        _ => false,
      };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queuePositions"></param>
    /// <returns></returns>
    protected virtual void ProcessTrackingQueuePositionChange(List<QueuePosition> queuePositions, int assetCode)
    {
      long? assetId = null;

      if (queuePositions.Any())
      {
        assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;

        if (assetCode == TrackingArea.FCE_AREA)
        {
          TaskHelper.FireAndForget(() =>
          {
            DCFurnaceRawMaterials result = new DCFurnaceRawMaterials();
            result.RawMaterials.AddRange(
              queuePositions
                .Where(x => !x.IsVirtualPosition && x.RawMaterialId.HasValue)
                .Select(x => new FurnaceRawMaterial()
                {
                  ChargingTime = x.ChargeDate ?? DateTime.Now,
                  RawMaterialId = x.RawMaterialId.Value,
                  Position = (short)x.PositionSeq
                })
                .ToList());

            FurnaceSendOffice.ProcessMaterialChangeInFurnaceEventAsync(result).GetAwaiter().GetResult();

          }, "Something went wrong while send ProcessMaterialChangeInFurnaceEventAsync operation to WBF");
        }

        if (!assetId.HasValue)
          throw new Exception($"For assetCode: {assetCode} there is no assetId");
      }

      try
      {
        ProcessTrackingQueuePositions(queuePositions, assetCode, assetId);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Exception while processing L1 Tracking Queue Position Change. AssetCode: {assetCode}, AssetId: {assetId}");
      }
    }

    protected virtual void ProcessTrackingQueuePositions(List<QueuePosition> queuePositions, int assetCode, long? assetId)
    {
      using (PEContext ctx = new PEContext())
      {
        if (!queuePositions.Any())
        {
          ctx.Database.ExecuteSqlRaw("delete from TRKRawMaterialLocations where AssetCode = {0}", assetCode);
        }
        else
        {
          ctx.Database.ExecuteSqlRaw("delete from TRKRawMaterialLocations where FKAssetId = {0}", assetId.Value);


          List<TRKRawMaterialLocation> locations = queuePositions.Select(qp => new TRKRawMaterialLocation()
          {
            FKAssetId = assetId.Value,
            AssetCode = assetCode,
            OrderSeq = (short)qp.OrderSeq,
            PositionSeq = (short)qp.PositionSeq,
            FKRawMaterialId = qp.RawMaterialId,
            IsVirtual = qp.IsVirtualPosition,
            IsOccupied = qp.RawMaterialId.HasValue,
            FKCtrAssetId = qp.CtrAssetCode.HasValue ? StorageProvider.AssetsDictionary[qp.CtrAssetCode.Value].AssetId : (long?)null,
            CorrelationId = qp.CorrelationId,
            EnumAreaType = AreaType.Undefined
          }).ToList();

          //NotificationController.Debug("{Locations}", queuePositions);
          Stopwatch w1 = new Stopwatch();

          w1.Start();
          DataTable dataTable = GenerateDataTable(locations);
          SqlParameter parameter = new SqlParameter("@LineItems", dataTable);
          parameter.TypeName = "dbo.TRK_RML_Row";
          ctx.Database.ExecuteSqlRaw("exec SPAddRawMaterialLocations @LineItems", parameter);
          w1.Stop();
          NotificationController.Debug("Elapsed time for SPAddRawMaterialLocations:" + w1.ElapsedMilliseconds);
        }
      }
    }

    protected virtual DataTable GenerateDataTable(List<TRKRawMaterialLocation> materialLocations)
    {
      DataTable result = new DataTable();

      result.Columns.Add("FkAssetId");
      result.Columns.Add("AssetCode");
      result.Columns.Add("PositionSeq");
      result.Columns.Add("OrderSeq");
      result.Columns.Add("FkRawMaterialId");
      result.Columns.Add("EnumAreaType");
      result.Columns.Add("IsVirtual");
      result.Columns.Add("IsOccupied");
      result.Columns.Add("FKCtrAssetId");
      result.Columns.Add("CorrelationId");

      foreach (TRKRawMaterialLocation materialLocation in materialLocations)
      {
        result.Rows.Add(
          materialLocation.FKAssetId,
          materialLocation.AssetCode,
          materialLocation.PositionSeq,
          materialLocation.OrderSeq,
          materialLocation.FKRawMaterialId,
          materialLocation.EnumAreaType.Value,
          materialLocation.IsVirtual,
          materialLocation.IsOccupied,
          materialLocation.FKCtrAssetId,
          materialLocation.CorrelationId);
      }

      return result;
    }

    protected virtual async Task UpdateProductWeight(long basId, double value)
    {
      try
      {
        using (PEContext ctx = new PEContext())
        {
          TRKRawMaterial rawMaterial = await ctx.TRKRawMaterials
            .Include(x => x.FKProduct)
            .FirstOrDefaultAsync(x => x.RawMaterialId == basId);

          if (rawMaterial != null && rawMaterial.FKProductId.HasValue)
          {
            rawMaterial.FKProduct.ProductWeight = value;
            rawMaterial.LastWeight = value; //TODO

            await ctx.SaveChangesAsync().ConfigureAwait(false);
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}");
      }
    }

    protected virtual async Task L3MaterialAssignment(long materialId)
    {
      try
      {
        using var ctx = new PEContext();

        var rawMaterial = await ctx.TRKRawMaterials.Include(m => m.FKProduct).FirstAsync(m => m.RawMaterialId == materialId);

        if (!rawMaterial.FKMaterialId.HasValue)
        {
          var result = await TrackingProcessMaterialEventSendOffice.SendRequestForL3MaterialAsync(
            new DCMaterialRelatedOperationData() { RawMaterialId = rawMaterial.RawMaterialId });

          if (result.OperationSuccess)
          {
            NotificationController.Debug($"Assigning L3 material ({result.DataConctract?.PRMMaterialId}) for Raw Material " +
              $"({result.DataConctract?.RawMaterialId}) BasId: {materialId}");

            //assign material
            if (result.DataConctract?.PRMMaterialId != null)
            {
              var prmMaterial = await ctx.PRMMaterials.FirstOrDefaultAsync(x => x.MaterialId == result.DataConctract.PRMMaterialId);

              TrackingRawMaterialHandler.AssignL3Material(rawMaterial, prmMaterial);

              await ctx.SaveChangesAsync();
            }
          }
          else
          {
            NotificationController.Error($"Error during assigning L3 material for BasId: {materialId}");
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Error during assigning L3 material for BasId: {materialId}");
        NotificationController.Error(
          $"[CRITICAL] fun {MethodHelper.GetMethodName()} failed");
      }
    }

    protected virtual RawMaterialStatus GetRawMaterialStatus(TrackingEventArgs message)
    {
      switch (message.EventType)
      {
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.FCE_AREA:
          return RawMaterialStatus.Charged;
        case var eventType when eventType == TrackingEventType.Exit && message.AssetCode == TrackingArea.FCE_AREA:
          return RawMaterialStatus.Discharged;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.RM_AREA:
          return RawMaterialStatus.InMill;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.ROD_AREA:
          return RawMaterialStatus.InFinalProduction;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == EventStorageProvider.LayingHeadAssetCode:
          return RawMaterialStatus.Rolled;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.TRANSPORT_AREA:
          return RawMaterialStatus.InTransport;
        case var eventType when eventType == TrackingEventType.Exit && message.AssetCode == TrackingArea.TRANSPORT_AREA:
          return RawMaterialStatus.Finished;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.RAKE_AREA:
          return RawMaterialStatus.OnCoolingBed;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.ENTER_TABLE_AREA:
          return RawMaterialStatus.InFinalProduction;
        case var eventType when eventType == TrackingEventType.Enter && message.AssetCode == TrackingArea.GARRET_AREA:
          return RawMaterialStatus.Rolled;
      }

      return RawMaterialStatus.Unassigned;
    }

    /// <summary>
    /// Trigger rollshop and maintanance actions when material leaves tracking point
    /// </summary>
    /// <param name="materialId"></param>
    /// <returns>True or false</returns>
    protected virtual async Task ProcessDelayPointLeaveAsync(long materialId)
    {
      try
      {
        await using var ctx = new PEContext();

        double weightWithCoeff;
        double? coeff = null;
        double materialWeight = 0;
        TRKRawMaterial rolledMaterial = null;
        var rawMaterialWithParent = await (from rm in ctx.TRKRawMaterials
                                           join pmr in ctx.TRKRawMaterialRelations
                                             .Include(x => x.ParentRawMaterial)
                                            on rm.RawMaterialId equals pmr.ChildRawMaterialId
                                            into pmrs
                                           from pmr in pmrs.DefaultIfEmpty()
                                           where rm.RawMaterialId == materialId
                                           select new { RawMaterial = rm, ParentRawMaterial = pmr }).SingleAsync();

        var rawMaterial = rawMaterialWithParent.RawMaterial;

        if (rawMaterial.IsAfterDelayPoint)
          return;

        var parentRawMaterial = rawMaterialWithParent?.ParentRawMaterial?.ParentRawMaterial;

        if (parentRawMaterial is not null)
        {
          materialWeight = (parentRawMaterial.LastWeight == 0 || !parentRawMaterial.LastWeight.HasValue ?
          parentRawMaterial.FKMaterial?.MaterialWeight ?? 0
          : parentRawMaterial.LastWeight.Value) * StorageProvider.WeightLossFactor;
          coeff = await TrackingRawMaterialHandler.FindRawMaterialWearCoeffByIdAsync(ctx, parentRawMaterial.RawMaterialId);

          if (!parentRawMaterial.IsAfterDelayPoint)
            rolledMaterial = parentRawMaterial;

          parentRawMaterial.IsAfterDelayPoint = true;
        }
        else
        {
          materialWeight = (rawMaterial.LastWeight == 0 || !rawMaterial.LastWeight.HasValue ?
          rawMaterial.FKMaterial?.MaterialWeight ?? 0
          : rawMaterial.LastWeight.Value) * StorageProvider.WeightLossFactor;
          coeff = await TrackingRawMaterialHandler.FindRawMaterialWearCoeffByIdAsync(ctx, rawMaterial.RawMaterialId);

          if (!rawMaterial.IsAfterDelayPoint)
            rolledMaterial = rawMaterial;
        }

        rawMaterial.IsAfterDelayPoint = true;

        if (rolledMaterial is not null)
        {
          await EquipmentUsageAsync(rolledMaterial, materialWeight);

          if (coeff is not null)
            weightWithCoeff = materialWeight * (double)coeff;
          else
            weightWithCoeff = materialWeight;
          await RollsUsageAsync(rolledMaterial, materialWeight, weightWithCoeff);
        }

        await ctx.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    protected virtual async Task CallDelayTailLeaveAsync(long basId, DateTime date)
    {
      SendOfficeResult<DataContractBase> result =
        await TrackingProcessMaterialEventSendOffice.SendTailLeavesToDLSAsync(new DCDelayEvent() { RawMaterialId = basId, Date = date });

      if (result.OperationSuccess)
        NotificationController.Debug($"Successful triggering delay module for BasId: {basId}");
      else
        NotificationController.Error($"Error during triggering delay module for BasId: {basId}");
    }

    protected virtual async Task CallDelayHeadEntersAsync(long basId, DateTime date)
    {
      SendOfficeResult<DataContractBase> result =
        await TrackingProcessMaterialEventSendOffice.SendHeadEnterToDLSAsync(new DCDelayEvent() { RawMaterialId = basId, Date = date });

      if (result.OperationSuccess)
        NotificationController.Debug($"Successful triggering delay module for BasId: {basId}");
      else
        NotificationController.Error($"Error during triggering delay module for BasId: {basId}");
    }

    protected virtual async Task ProductCreationAsync(long materialId, DateTime date, int assetCode)
    {
      SendOfficeResult<DCProductData> result = new SendOfficeResult<DCProductData>();

      try
      {
        using PEContext ctx = new PEContext();
        TRKRawMaterial rawMaterial = await TrackingRawMaterialHandler.FindRawMaterialByIdAsync(ctx, materialId);
        if (rawMaterial.IsDummy)
          NotificationController.Warn($"Cannot create a product for dummy material: {materialId}");
        else if (rawMaterial.EnumTypeOfScrap == TypeOfScrap.Scrap)
          NotificationController.Warn($"Cannot create a product for scrapped material: {materialId}");
        else if (rawMaterial.FKProductId.HasValue)
          NotificationController.Warn($"Cannot create a product for material: {materialId} with product");
        else
        {
          DCMaterialProductionEnd messageToSend =
          new DCMaterialProductionEnd() { RawMaterialId = rawMaterial.RawMaterialId };

          NotificationController.Debug($"Requesting product creation for RawMaterial {messageToSend.RawMaterialId}");
          result = await TrackingProcessMaterialEventSendOffice.SendRequestToCreateCoilAsync(new DCCoilData()
          {
            RawMaterialId = rawMaterial.RawMaterialId,
            FKMaterialId = rawMaterial.FKMaterialId,
            OverallWeight = (rawMaterial.LastWeight == 0 || !rawMaterial.LastWeight.HasValue
              ? rawMaterial.FKMaterial?.MaterialWeight ??
                0
              : rawMaterial.LastWeight.Value) *
              StorageProvider.WeightLossFactor,
            Date = date
          });

          if (result.OperationSuccess)
          {
            TaskHelper.FireAndForget(() =>
            {
              var result = TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent()
              {
                EventType = ChildMillEventType.ProductCreate,
                AssetId = StorageProvider.AssetsDictionary[assetCode].AssetId,
                DateStart = date,
                DateEnd = date,
                RawMaterialId = rawMaterial.RawMaterialId
              }).GetAwaiter().GetResult();

              if (!result.OperationSuccess)
                NotificationController.Warn($"Something went wrong while AddMillEvent for {assetCode}, MaterialId: {rawMaterial.RawMaterialId}, EventType: {ChildMillEventType.ProductCreate}");
            });

            try
            {
              //assign product to raw material
              rawMaterial.FKProductId = result.DataConctract.ProductId;
              rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rolled;
              rawMaterial.ProductCreatedTs = date;
              await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
            {
              ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
                $"Unique key violation while creating product for raw material [{materialId}].");
            }
            catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
            {
              ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
                $"Existing reference key violation while creating product for raw material [{materialId}].");
            }
            catch (InternalModuleException ex)
            {
              ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
                ex.Message, ex.AlarmParams);
            }
            catch (Exception ex)
            {
              ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
                $"Unexpected error while creating product for raw material [{materialId}].");
            }
          }
          else
            NotificationController.Error("Send Raw Material Data to PRM - error");

          TaskHelper.FireAndForget(() => TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(new DCShiftCalendarId()
          {
            ShiftCalendarId = rawMaterial.FKShiftCalendarId
          }));
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    /// <summary>
    /// Send product weight to Maintenence to recalculate accumulated weight on equipment
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <param name="materialWeight"></param>
    /// <returns>True or false</returns>
    protected virtual async Task EquipmentUsageAsync(TRKRawMaterial rawMaterial, double materialWeight)
    {
      try
      {
        if (rawMaterial.IsDummy)
          NotificationController.Warn($"Cannot accumulate equipment usage for dummy material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.EnumTypeOfScrap == TypeOfScrap.Scrap)
          NotificationController.Warn($"Cannot accumulate equipment usage for scrapped material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.CuttingSeqNo > 1)
          NotificationController.Warn($"Cannot accumulate equipment usage for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] because is has been already done for parent");
        else
        {
          var result = await TrackingProcessMaterialEventSendOffice.AccumulateEquipmentUsageAsync(new DCEquipmentAccu()
          {
            MaterialWeight = materialWeight
          });

          if (result.OperationSuccess)
            NotificationController.Debug($"Forwarding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight {materialWeight} to Maintenance - success");
          else
            throw new InternalModuleException($"Something went wrong while adding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight to active equipment.",
              AlarmDefsBase.FailedToSendAccumulatedEquipmentUsageForMaterial, rawMaterial.RawMaterialName, materialWeight);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    /// <summary>
    /// Send product weight to Rollshop to recalculate accumulated weight on rolls
    /// </summary>
    /// <param name="rawMaterial"></param>
    /// <param name="materialWeight"></param>
    /// <param name="materialWeightWithCoeff"></param>
    /// <returns>True or false</returns>
    protected virtual async Task RollsUsageAsync(TRKRawMaterial rawMaterial, double materialWeight, double materialWeightWithCoeff)
    {
      try
      {
        if (rawMaterial.IsDummy)
          NotificationController.Warn($"Cannot accumulate rolls usage for dummy material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.EnumTypeOfScrap == TypeOfScrap.Scrap)
          NotificationController.Warn($"Cannot accumulate rolls usage for scrapped material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}]");
        else if (rawMaterial.CuttingSeqNo > 1)
          NotificationController.Warn($"Cannot accumulate rolls usage for material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] because is has been already done for parent");
        else
        {
          var result = await TrackingProcessMaterialEventSendOffice.AccumulateRollsUsageAsync(new DCRollsAccu()
          {
            MaterialWeight = materialWeight,
            MaterialWeightWithCoeff = materialWeightWithCoeff
          });

          if (result.OperationSuccess)
            NotificationController.Debug($"Forwarding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight {materialWeight} and weight with coefficient {materialWeightWithCoeff} to RollShop - success");
          else
            throw new InternalModuleException($"Something went wrong while adding material {rawMaterial.RawMaterialName} [{rawMaterial.RawMaterialId}] weight and weight with coefficient to rolls.",
              AlarmDefsBase.FailedToSendAccumulatedRollsUsageForMaterial, rawMaterial.RawMaterialName, materialWeight, materialWeightWithCoeff);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }

    protected virtual async Task ProcessLHEvent(TrackingEventArgs message)
    {
      try
      {
        TRKRawMaterial rawMaterial = await TrackingRawMaterialHandler
          .FindRawMaterialByIdAsync(null, message.MaterialId).ConfigureAwait(false);

        double weight = rawMaterial.LastWeight ?? rawMaterial.FKMaterial?.MaterialWeight ?? 0;

        //await AccumulateEquipmentUsage(weight).ConfigureAwait(false);
        //await AccumulateRollsUsage(message, weight).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          $"Something went wrong while {MethodHelper.GetMethodName()} by materialId: {message.MaterialId}");
      }
    }
  }
}
