using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EFCoreExtensions;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseDbEntity.Providers;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseInterfaces.SendOffices.PRM;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Common;
using PE.Core;
using PE.Helpers;
using PE.PRM.Base.Handlers;
using PE.PRM.Base.Handlers.Models;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.Module.Core;
using SMF.Module.Parameter;
using PE.Interfaces.Managers.PRM;
using PE.PRM.Base.Managers;

namespace PE.PRM.Managers
{
  public class WorkOrderManager : BaseManager,IWorkOrderManager 
  {
    #region fields

    protected readonly IProdManagerWorkOrderBaseSendOffice SendOffice;
    protected readonly IContextProvider<PEContext> Context;
    protected internal bool UseExistingMaterialCatalogueForProcessingTransferObject;
    protected internal bool UseExistingProductCatalogueForProcessingTransferObject;
    private readonly Nito.AsyncEx.AsyncLock _mutex = new Nito.AsyncEx.AsyncLock();

    #region handlers

    protected readonly ProductCatalogueHandlerBase ProductCatalogueHandler;
    protected readonly HeatHandlerBase HeatHandler;
    protected readonly SteelgradeHandlerBase SteelgradeHandler;
    protected readonly MaterialCatalogueHandlerBase MaterialCatalogueHandler;
    protected readonly MaterialHandlerBase MaterialHandler;
    protected readonly WorkOrderHandlerBase WorkOrderHandler;
    protected readonly CustomerHandler CustomerHandler;
    protected readonly BilletYardHandlerBase BilletYardHandler;
    protected readonly WorkOrderTransferValidationHandler WorkOrderTransferValidationHandler;

    #endregion

    #endregion

    #region ctor

    public WorkOrderManager(IModuleInfo moduleInfo, IProdManagerWorkOrderBaseSendOffice sendOffice, IContextProvider<PEContext> context) :
      base(moduleInfo)
    {
      SendOffice = sendOffice;
      Context = context;
      ProductCatalogueHandler = new ProductCatalogueHandlerBase();
      HeatHandler = new HeatHandlerBase();
      SteelgradeHandler = new SteelgradeHandlerBase();
      MaterialCatalogueHandler = new MaterialCatalogueHandlerBase();
      MaterialHandler = new MaterialHandlerBase();
      WorkOrderHandler = new WorkOrderHandlerBase();
      CustomerHandler = new CustomerHandler();
      BilletYardHandler = new BilletYardHandlerBase();
      WorkOrderTransferValidationHandler = new WorkOrderTransferValidationHandler();

      if (!UnitTestDetector.IsInUnitTest)
      {
        ModuleController.ParametersChanged += ModuleController_ParametersChanged;

        UseExistingMaterialCatalogueForProcessingTransferObject =
          ParameterController.GetParameterStatic("MaterialCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
        UseExistingProductCatalogueForProcessingTransferObject =
          ParameterController.GetParameterStatic("ProductCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
      }
    }
    #endregion

    #region func

    protected virtual void ModuleController_ParametersChanged(object sender, ParametersChangedEventArgs e)
    {
      UseExistingMaterialCatalogueForProcessingTransferObject =
        ParameterController.GetParameterStatic("MaterialCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
      UseExistingProductCatalogueForProcessingTransferObject =
        ParameterController.GetParameterStatic("ProductCatalogueIsUsed").ValueInt.GetValueOrDefault() == 1;
    }

    public virtual async Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition dc)
    {
      DCWorkOrderStatus backMsg = new DCWorkOrderStatus { Counter = dc.Counter };

      if (!WorkOrderTransferValidationHandler.ValidateWorkOrderDefinition(dc, backMsg, UseExistingMaterialCatalogueForProcessingTransferObject, UseExistingProductCatalogueForProcessingTransferObject, out ValidatedWorkOrderBase workOrderData))
      {
        backMsg.Status = CommStatus.ValidationError;
        return backMsg;
      }

      // Work order creating
      try
      {
        await using var ctx = Context.Create();
        var dbValidationResult = await ValidateTransferDataOnDatabase(ctx, workOrderData, backMsg);

        if (!dbValidationResult.IsValid)
        {
          return dbValidationResult.DcWorkOrderStatus;
        }

        MVHAsset reception = await BilletYardHandler.GetReception(ctx);
        List<PRMMaterial> materials = MaterialHandler.CreateMaterials(reception,
          workOrderData.WorkOrderName,
          workOrderData.NumberOfBillets,
          dbValidationResult.MaterialCatalogue,
          dbValidationResult.Heat,
          workOrderData.BilletWeight,
          workOrderData.InputThickness,
          workOrderData.InputLength,
          workOrderData.InputWidth);

        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByNameAsync(ctx, workOrderData.WorkOrderName);

        if (workOrder != null) // if work order with this name don't exists
        {
          NotificationController.Info($"Work order: {workOrder.WorkOrderName} exists");
          if (workOrder.EnumWorkOrderStatus <= WorkOrderStatus.Scheduled) // if work order can be updated
          {
            MaterialHandler.DeleteOldMaterialsAfterWoUpdate(ctx, workOrder.WorkOrderId);
            workOrder = await WorkOrderHandler.UpdateWorkOrderAsync(ctx,
              dbValidationResult.ProductCatalogue.ProductCatalogueId,
              dbValidationResult.Customer,
              dbValidationResult.MaterialCatalogue,
              materials,
              dbValidationResult.Heat,
              dbValidationResult.Steelgrade,
              dbValidationResult.ParentWorkOrder,
              workOrderData);

            NotificationController.Info("[WO:{workorder}] Work order updating...", workOrder.WorkOrderName);
          }
          else
          {
            NotificationController.RegisterAlarm(AlarmDefsBase.WorkOrderNotUpdatable,
              $"Work order [{workOrder.WorkOrderName}] status does not allow it to update", workOrder.WorkOrderName);
            NotificationController.Warn(
              $"Work order {workOrder.WorkOrderName} [{workOrder.WorkOrderId}] status {workOrder.EnumWorkOrderStatus.Value} does not allow it to update", workOrder.WorkOrderId);
            backMsg.Status = CommStatus.ValidationError;
            return backMsg;
          }
        }
        else
        {
          workOrder = WorkOrderHandler.CreateWorkOrder(ctx,
            workOrderData.AmISimulated,
            workOrderData.TargetWorkOrderWeight,
            materials,
            dbValidationResult.Steelgrade,
            dbValidationResult.Heat,
            dbValidationResult.MaterialCatalogue,
            dbValidationResult.ProductCatalogue.ProductCatalogueId,
            dbValidationResult.Customer,
            workOrderData.L3CreatedTs,
            workOrderData.WorkOrderName,
            workOrderData.ExternalWorkOrderName,
            workOrderData.OrderDeadline,
            workOrderData.BundleWeightMin,
            workOrderData.BundleWeightMax,
            workOrderData.TargetWorkOrderWeightMin,
            workOrderData.TargetWorkOrderWeightMax);

          workOrder.FKHeat = dbValidationResult.Heat;
          workOrder.FKParentWorkOrder = dbValidationResult.ParentWorkOrder;
          ctx.PRMWorkOrders.Add(workOrder);

          NotificationController.Info($"Creating work order {workOrder.WorkOrderName}...");
        }

        backMsg.Status = CommStatus.OK;
        var saveChangesWithValidationResult = await ctx.SaveChangesWithValidationAsync();

        if (!string.IsNullOrWhiteSpace(saveChangesWithValidationResult.ErrorValidationMessages))
        {
          backMsg.Status = CommStatus.ValidationError;
          backMsg.BackMessage += saveChangesWithValidationResult.ErrorValidationMessages;

          return backMsg;
        }

        HmiRefresh(HMIRefreshKeys.WorkOrder);

        //--------------******************** S I M U L A T I O N ******************** ---------------------------------------
        if (workOrderData.AmISimulated)
        {
          SendOfficeResult<DataContractBase> result =
            await SendOffice.AutoScheduleWorkOrderAsync(new DCWorkOrderToSchedule
            {
              WorkOrderId = workOrder.WorkOrderId
            });

          if (result.OperationSuccess)
          {
            NotificationController.Info("[WO:{workorder}] Forwarding work order to schedule module - success",
              workOrder.WorkOrderName);
          }
          else
          {
            NotificationController.Error(
              "Forwarding work order to schedule module - error. Work order will not be scheduled automatically");
          }
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating work order {dc.WorkOrderName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating work order {dc.WorkOrderName}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotCreated,
          $"Unexpected error while creating work order {dc.WorkOrderName}.", dc.WorkOrderName);
      }

      return backMsg;
    }

    public virtual async Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        WorkOrderHandler.CreateWorkOrderByUser(ctx, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
        NotificationController.Info($"Work order {dc.WorkOrderName} has been created by user.");
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating work order {dc.WorkOrderName}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating work order {dc.WorkOrderName}.");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotCreated,
          $"Unexpected error while creating work order {dc.WorkOrderName}.", dc.WorkOrderName);
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.WorkOrderId);
        if (workOrder is null)
          throw new InternalModuleException($"Work order {dc.WorkOrderName} [{dc.WorkOrderId}] was not found.",
            AlarmDefsBase.WorkOrderNotFound, dc.WorkOrderName);

        WorkOrderHandler.UpdateWorkOrderByUser(ctx, workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while updating work order {dc.WorkOrderName} [{dc.WorkOrderId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while updating work order {dc.WorkOrderName} [{dc.WorkOrderId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotUpdated,
          $"Unexpected error while updating work order {dc.WorkOrderName} [{dc.WorkOrderId}].", dc.WorkOrderName);
      }

      return result;
    }

    public virtual async Task<DataContractBase> SendWorkOrderReportAsync(DCWorkOrderConfirmation dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        //if (dc.IsEndOfWorkShop)
        //{
        //  SendOfficeResult<DataContractBase> sendEndOfWorkShopResult = await SendOffice.SendEndOfWorkShop(new DataContractBase());

        //  if (!sendEndOfWorkShopResult.OperationSuccess)
        //    throw new Exception("SendEndOfWorkShop failed");
        //  //TODO MB Mill Event on end of workshop
        //  //else
        //  //TaskHelper.FireAndForget(() => _sendOffice.AddMillEvent(new DCMillEvent { EventType = MillEventType.FA, UserId = dc.HmiInitiator.UserId }).GetAwaiter().GetResult());
        //}

        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdWithDeepIncludeAsync(ctx, dc.Id);

        List<TRKRawMaterial> rawMaterials = await ctx.TRKRawMaterials
          .Where(x => x.FKMaterial.FKWorkOrderId == workOrder.WorkOrderId)
          .OrderBy(x => x.RawMaterialCreatedTs)
          .ToListAsync();
        EVTShiftCalendar shift = await ctx.EVTShiftCalendars
          .Include(x => x.FKCrew)
          .Include(x => x.FKShiftDefinition)
          .Where(x => x.ShiftCalendarId == rawMaterials
          .First().FKShiftCalendarId)
          .SingleAsync();

        DCL2L3WorkOrderReport report = new DCL2L3WorkOrderReport
        {
          CommStatus = CommStatus.New,
          CreatedTs = DateTime.Now,
          UpdatedTs = DateTime.Now,
          WorkOrderName = workOrder.WorkOrderName,
          IsWorkOrderFinished = workOrder.EnumWorkOrderStatus == WorkOrderStatus.Finished,
          MaterialName = workOrder?.FKMaterialCatalogue?.MaterialCatalogueName,
          InputWidth = (workOrder?.PRMMaterials?.FirstOrDefault(x => x.MaterialWidth.HasValue)?.MaterialWidth ?? 0) * 1000,
          InputThickness = (workOrder?.PRMMaterials?.FirstOrDefault()?.MaterialThickness ?? 0) * 1000,
          ProductName = workOrder.FKProductCatalogue?.ProductCatalogueName,
          OutputWidth = (workOrder.FKProductCatalogue.Width ?? 0) * 1000,
          OutputThickness = (workOrder?.FKProductCatalogue?.Thickness ?? 0) * 1000,
          HeatName = workOrder.FKHeat.HeatName,
          SteelgradeCode = workOrder.FKSteelgrade.SteelgradeCode,
          TargetWorkOrderWeight = workOrder.TargetOrderWeight,
          ProductsNumber = (short)workOrder.PRMProducts.Count(),
          TotalProductsWeight = (int)workOrder.PRMProducts.Select(x => x.ProductWeight).DefaultIfEmpty(0).Sum(),
          TotalRawMaterialWeight = (int)rawMaterials.Select(x => x.LastWeight).DefaultIfEmpty(0).Sum(),
          RawMaterialsPlanned = (short)rawMaterials.Count(),
          RawMaterialsCharged = (short)rawMaterials.Count(x => x.EnumRawMaterialStatus >= RawMaterialStatus.Charged),
          RawMaterialsDischarged = (short)rawMaterials.Count(x => x.EnumRawMaterialStatus >= RawMaterialStatus.Discharged),
          RawMaterialsRolled = (short)rawMaterials.Count(x => x.EnumRawMaterialStatus >= RawMaterialStatus.Rolled && x.EnumRawMaterialStatus < RawMaterialStatus.Rejected && x.EnumTypeOfScrap != TypeOfScrap.Scrap),
          RawMaterialsScrapped = (short)rawMaterials.Count(x => x.EnumRawMaterialStatus == RawMaterialStatus.Scrap),
          RawMaterialsRejected = (short)rawMaterials.Count(x => x.EnumRawMaterialStatus == RawMaterialStatus.Rejected),
          NumberOfPiecesRejectedInLocation1 = (short)rawMaterials.Count(x => x.EnumRejectLocation == RejectLocation.AfterFurnace),
          WeightOfPiecesRejectedInLocation1 = rawMaterials.Where(x => x.EnumRejectLocation == RejectLocation.AfterFurnace).Select(x => x.LastWeight).DefaultIfEmpty(0).Sum() ?? 0,
          WorkOrderStartTs = workOrder.WorkOrderStartTs,
          WorkOrderEndTs = workOrder.WorkOrderEndTs,
          DelayDuration = (int)workOrder.EVTEvents.Where(x => x.FKEventTypeId == EventType.Checkpoint1Delay && x.EventEndTs.HasValue).Sum(x => (x.EventEndTs.Value - x.EventStartTs).TotalSeconds),
          ShiftName = shift.FKShiftDefinition.ShiftCode,
          CrewName = shift.FKCrew.CrewName
        };

        SendOfficeResult<DataContractBase> sendOfficeResult = await SendOffice.CreateWorkOrderReportAsync(report);

        if (sendOfficeResult.OperationSuccess)
        {
          if (!dc.IsEndOfWorkShop)
          {
            TaskHelper.FireAndForget(() =>
            {
              _ = SendOffice.SendRemoveWorkOrderFromScheduleAsync(dc).GetAwaiter().GetResult();
              HmiRefresh(HMIRefreshKeys.Schedule);
              HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
            });

            workOrder.IsSentToL3 = true;
            await ctx.SaveChangesAsync();
          }

          HmiRefresh(HMIRefreshKeys.WorkOrder);
          HmiRefresh(HMIRefreshKeys.L3TransferTableWorkOrderReports);
        }
        else
          throw new InternalModuleException($"Work order [{dc.Id}] was not sent to tranfer table.",
            AlarmDefsBase.WorkOrderNotFound);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while sending work order [{dc.Id}] to transfer table.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while sending work order [{dc.Id}] to transfer table.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderReportCreateError,
          $"Unexpected error while sending work order [{dc.Id}] to transfer table.");
      }

      return result;
    }

    public virtual async Task<DataContractBase> SendProductReportAsync(DCRawMaterial dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();

        var data = await (from rm in ctx.TRKRawMaterials
                          .Include(x => x.FKProduct)
                          .Include(x => x.FKProduct.FKWorkOrder)
                          .Include(x => x.FKProduct.FKWorkOrder.FKHeat)
                          .Include(x => x.FKProduct.FKWorkOrder.FKHeat.FKSteelgrade)
                          .Include(x => x.QTYQualityInspection)
                          .Include(x => x.FKProduct.FKWorkOrder.FKProductCatalogue)
                          .Include(x => x.FKProduct.FKWorkOrder.FKProductCatalogue.FKProductCatalogueType)
                          join m in ctx.PRMMaterials
                          on rm.FKMaterialId equals m.MaterialId
                           into ms
                          from m in ms.DefaultIfEmpty()
                          join s in ctx.EVTShiftCalendars
                          .Include(x => x.FKShiftDefinition)
                          on rm.FKShiftCalendarId equals s.ShiftCalendarId
                           into ss
                          from s in ss
                          where rm.RawMaterialId == dc.Id
                          select new { RawMaterial = rm, Material = m, ShiftCalendar = s }).FirstAsync();

        var rawMaterial = data.RawMaterial;
        var material = data.Material;
        var shiftCalendar = data.ShiftCalendar;
        var product = rawMaterial.FKProduct;

        if (rawMaterial.FKProductId is null)
          throw new InternalModuleException($"Cannot create product report for billet {rawMaterial.RawMaterialName} [{dc.Id}] because product cannot be found.",
            AlarmDefsBase.CoilReportNotCreatedBecauseProductNotFound, rawMaterial.RawMaterialName);

        var report = new DCL2L3ProductReport
        {
          CommStatus = CommStatus.New,
          ShiftName = shiftCalendar.FKShiftDefinition.ShiftCode,
          WorkOrderName = product.FKWorkOrder?.WorkOrderName ?? "",
          SteelgradeCode = product.FKWorkOrder?.FKHeat?.FKSteelgrade?.SteelgradeCode ?? "",
          HeatName = product.FKWorkOrder?.FKHeat?.HeatName ?? "",
          SequenceInWorkOrder = material is not null ? material.SeqNo :
            (product.FKWorkOrder is null ? (short)0 : Convert.ToInt16(product.ProductName[(product.ProductName.LastIndexOf('_') + 1)..])),
          ProductName = product.ProductName,
          ProductType = product.FKWorkOrder?.FKProductCatalogue?.FKProductCatalogueType?.ProductCatalogueTypeCode ??
            (await ctx.PRMProductCatalogueTypes.SingleAsync(x => x.IsDefault)).ProductCatalogueTypeCode,
          OutputWeight = product.ProductWeight,
          OutputWidth = product.FKWorkOrder?.FKProductCatalogue?.Width ?? 0,
          OutputThickness = product?.FKWorkOrder?.FKProductCatalogue?.Thickness ?? 0,
          OutputPieces = product.BarsCounter,
          InspectionResult = rawMaterial.QTYQualityInspection?.EnumInspectionResult ?? InspectionResult.Undefined
        };

        var sendOfficeResult = await SendOffice.CreateProductReportAsync(report);

        if (!sendOfficeResult.OperationSuccess)
          throw new InternalModuleException($"Material {rawMaterial.RawMaterialName} [{dc.Id}] was not sent to tranfer table.", AlarmDefsBase.ProductReportForMaterialNotCreated, report.ProductName);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while sending product related to material [{dc.Id}] to transfer table.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while sending product related to material [{dc.Id}] to transfer table.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ProductReportNotCreated,
          $"Unexpected error while sending product related to material [{dc.Id}] to transfer table.");
      }

      return result;
    }

    public async Task<DataContractBase> CheckShiftsWorkOrderStatusses(DCShiftCalendarId dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = new PEContext();
        List<long> workOrderIds = await ctx.PPLSchedules
          .Select(x => x.FKWorkOrderId).ToListAsync();

        List<PRMWorkOrder> workOrders = await ctx.PRMWorkOrders
          .Include(x => x.FKProductCatalogue)
          .Include(x => x.FKProductCatalogue.FKProductCatalogueType)
          .Include(x => x.PRMProducts)
          .Where(x => workOrderIds
          .Any(y => y == x.WorkOrderId))
          .ToListAsync();

        DateTime now = DateTime.Now;

        foreach (PRMWorkOrder workOrder in workOrders)
        {
          // For cancelled workOrders there could not be assigned Delay
          if (workOrder.EnumWorkOrderStatus == WorkOrderStatus.Cancelled)
          {
            List<EVTEvent> delaysWithCancelledWorkOrder = await ctx.EVTEvents
              .Include(x => x.FKEventType)
              .Where(x => !x.IsPlanned &&
                (x.FKEventType.EventTypeCode == (short)ChildMillEventType.Checkpoint1Delay || x.FKEventType.EventTypeCode == (short)ChildMillEventType.Checkpoint1DelayMicroStop) &&
                x.FKWorkOrderId.HasValue && x.FKWorkOrderId.Value == workOrder.WorkOrderId)
              .ToListAsync();

            foreach (EVTEvent delay in delaysWithCancelledWorkOrder)
            {
              delay.FKWorkOrderId = null;
            }
          }

          if (workOrder.EnumWorkOrderStatus >= WorkOrderStatus.Finished)
          {
            continue;
          }

          int materialsCount = 0;
          var rawMaterials = new List<TRKRawMaterial>();

          await ctx.PRMMaterials
            .Where(x => x.FKWorkOrderId == workOrder.WorkOrderId)
            .Include(x => x.TRKRawMaterials)
            .ForEachAsync(x =>
            {
              materialsCount++;
              foreach (var item in x.TRKRawMaterials)
              {
                rawMaterials.Add(item);
              }
            });

          var productCatalogueTypeCode = workOrder.FKProductCatalogue.FKProductCatalogueType.ProductCatalogueTypeCode.ToUpper();
          var shouldBeFinished = false;

          shouldBeFinished = productCatalogueTypeCode switch
          {
            var value when value.Equals(Constants.Bar.ToUpper()) => await ShouldWorkOrderWithBarsBeFinishedAsync(rawMaterials, workOrder, materialsCount),
            var value when value.Equals(Constants.WireRod.ToUpper()) => await ShouldWorkOrderWithCoilsBeFinishedAsync(rawMaterials, workOrder, materialsCount),
            var value when value.Equals(Constants.Garret.ToUpper()) => await ShouldWorkOrderWithGarretsBeFinishedAsync(rawMaterials, workOrder, materialsCount),
            _ => await ShouldWorkOrderWithOtherProductsBeFinishedAsync(rawMaterials, workOrder, materialsCount),
          };

          if (shouldBeFinished)
          {
            workOrder.EnumWorkOrderStatus = WorkOrderStatus.Finished;
            workOrder.WorkOrderStartTs ??= now;
            workOrder.WorkOrderEndTs ??= now;
          }

          //TaskHelper.FireAndForget(() =>
          //  SendOffice.AddMillEvent(new DCMillEvent { EventType = ChildEventTypeEnum.WOE, WorkOrderId = workOrder.WorkOrderId }).GetAwaiter().GetResult()
          //  , $"Something went wrong while CheckShiftsWorkOrderStatusses");
        }

        await ctx.SaveChangesAsync();

        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while CheckShiftsWorkOrderStatusses");
      }

      return result;
    }

    public async Task<DataContractBase> ProcessWorkOrderStatus(DCRawMaterial arg)
    {
      try
      {
        await using var ctx = new PEContext();
        TRKRawMaterial rawMaterial = await ctx.TRKRawMaterials
          .Include(x => x.FKMaterial)
          .FirstOrDefaultAsync(x => x.RawMaterialId == arg.Id && x.FKMaterialId.HasValue);

        if (rawMaterial?.FKMaterial?.FKWorkOrderId != null)
        {
          PRMWorkOrder workOrder = await ctx.PRMWorkOrders.FirstAsync(x => x.WorkOrderId == rawMaterial.FKMaterial.FKWorkOrderId);

          if (arg.AssetCode == TrackingArea.RM_AREA && workOrder.EnumWorkOrderStatus < WorkOrderStatus.InRealization)
          {
            workOrder.EnumWorkOrderStatus = WorkOrderStatus.InRealization;
          }

          if (arg.AssetCode == TrackingArea.FCE_AREA && workOrder.EnumWorkOrderStatus < WorkOrderStatus.InRealization)
          {
            List<long> materialIdsToCheck = await ctx.PRMMaterials.Where(x => x.FKWorkOrderId == rawMaterial.FKMaterial.FKWorkOrderId).Select(x => x.MaterialId).ToListAsync();
            MVHAsset furnaceAsset = await ctx.MVHAssets.FirstAsync(x => x.AssetCode == TrackingArea.FCE_AREA.Value);
            bool allMaterialsAssigned = true;
            bool allMaterialsCharged = true;

            foreach (long materialId in materialIdsToCheck)
            {
              TRKRawMaterial rawMaterialToCheck = await ctx.TRKRawMaterials
                .FirstOrDefaultAsync(x => x.FKMaterialId.HasValue && x.FKMaterialId.Value == materialId);

              if (rawMaterialToCheck == null)
              {
                allMaterialsAssigned = false;
                break;
              }
              else
              {
                if (allMaterialsCharged && rawMaterialToCheck.EnumRawMaterialStatus < RawMaterialStatus.Charged)
                {
                  allMaterialsCharged = false;
                }
              }
            }

            if (!allMaterialsAssigned || !allMaterialsCharged)
            {
              workOrder.EnumWorkOrderStatus = WorkOrderStatus.Charging;
            }

            if (allMaterialsAssigned && allMaterialsCharged)
            {
              workOrder.EnumWorkOrderStatus = WorkOrderStatus.Charged;
            }
          }

          await ctx.SaveChangesAsync();

          HmiRefresh(HMIRefreshKeys.Schedule);
          HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
        }
        else
        {
          NotificationController.Warn("Material has not been assign to L3 material");
        }
      }
      catch (Exception ex)
      {
        NotificationController.Error($"[CRITICAL] fun {MethodBase.GetCurrentMethod().ReflectedType.Name} failed");
        NotificationController.LogException(ex, "Something went wrong while ProcessWorkOrderStatus");
      }

      return new DataContractBase();
    }

    public virtual async Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc)
    {
      DataContractBase result = new DataContractBase();
      await using var ctx = new PEContext();

      try
      {
        if (dc.NoOfmaterials == null || (dc.NoOfmaterials.HasValue && dc.NoOfmaterials <= 0))
          throw new InternalModuleException($"Test work order not created because number of materials {dc.NoOfmaterials} is wrong.",
            AlarmDefsBase.TestOrderNotCreatedDueToWrongMaterialsNumber);

        PRMWorkOrder parentWorkOrder = await ctx.PRMWorkOrders
          .Include(x => x.FKSteelgrade)
          .Include(x => x.FKHeat)
          .Include(x => x.FKMaterialCatalogue)
          .Include(x => x.FKCustomer)
          .Where(x => x.WorkOrderId == dc.WorkOrderId.Value)
          .FirstAsync();

        var steelgrade = parentWorkOrder.FKSteelgrade ?? await ctx.PRMSteelgrades.FirstOrDefaultAsync(x => x.IsDefault);

        var heat = HeatHandler.CreateTestHeat(parentWorkOrder.FKHeat);
        ctx.PRMHeats.Add(heat);

        double weight = (double)(dc.PlannedWeight / dc.NoOfmaterials);

        var now = DateTime.Now;

        MVHAsset reception = await BilletYardHandler.GetReception(ctx);

        List<PRMMaterial> materials = MaterialHandler.CreateTestMaterials(reception,
          "TestOrder" + now.ToString("yyyyMMddHHmmss"),
          (short)dc.NoOfmaterials,
          parentWorkOrder.FKMaterialCatalogue, heat, weight);

        PRMWorkOrder workOrder = WorkOrderHandler.CreateWorkOrder(ctx,
          true,
          dc.PlannedWeight,
          materials,
          steelgrade,
          heat,
          parentWorkOrder.FKMaterialCatalogue,
          parentWorkOrder.FKProductCatalogueId,
          parentWorkOrder.FKCustomer,
          now,
          null,
          null,
          now,
          dc.PlannedWeight,
          dc.PlannedWeight
        );

        ctx.PRMWorkOrders.Add(workOrder);

        await ctx.SaveChangesAsync();

        TaskHelper.FireAndForget(async () => await AutoScheduleWorkOrderAsync(workOrder.WorkOrderId));

      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while creating test work order based on exisiting work order [{dc.WorkOrderId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while creating test work order based on exisiting work order [{dc.WorkOrderId}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        switch (ex.Source)
        {
          case "CreateTestHeat":
            {
              NotificationController.Error(ex.Source + " error");
              break;
            }
          case "CreateTestMaterial":
          case "CreateTestSchedule":
            {
              NotificationController.Error(ex.Source + " missing argument Heat or WorkOrder or DCTestSchedule");
              break;
            }
        }

        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while creating test work order based on exisiting work order [{dc.WorkOrderId}].");
      }

      return result;
    }

    #endregion

    private async Task<WorkOrderValidationResultBase> ValidateTransferDataOnDatabase(PEContext ctx,
      ValidatedWorkOrderBase workOrderData, DCWorkOrderStatus backMsg)
    {
      var validator = new WorkOrderValidatorBase(ctx,
        SteelgradeHandler,
        ProductCatalogueHandler,
        CustomerHandler,
        MaterialCatalogueHandler,
        HeatHandler,
        WorkOrderHandler,
        workOrderData,
        UseExistingProductCatalogueForProcessingTransferObject,
        UseExistingMaterialCatalogueForProcessingTransferObject);

      return await validator
        .Initialize(backMsg)
        .AddValidatorAsync(validator.ValidateSteelGrade)
        .AddValidatorAsync(validator.ValidateOrderDeadline)
        .AddValidatorAsync(validator.ValidateProductCatalogue)
        .AddValidatorAsync(validator.ValidateOrCreateMaterialCatalogue)
        .AddValidatorAsync(validator.ValidateOrCreateHeat)
        .AddValidatorAsync(validator.ValidateOrCustomer)
        .AddValidatorAsync(validator.ValidateParentWorkOrder)
        .AddValidatorAsync(validator.ValidateTargetWorkOrderWeight)
        .RunAsync();
    }

    public virtual async Task<DataContractBase> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.Id);
        if (workOrder is null)
          throw new InternalModuleException($"Work order [{dc.Id}] was not found.", AlarmDefsBase.RecordNotFound);

        WorkOrderHandler.UpdateCanceledWorkOrderByUser(workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.WorkOrder);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while uncanceling work order [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while uncanceling work order [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while uncanceling work order [{dc.Id}].");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.Id);
        if (workOrder is null)
          throw new InternalModuleException($"Work order [{dc.Id}] was not found.", AlarmDefsBase.RecordNotFound);

        WorkOrderHandler.UpdateCanceledWorkOrderByUser(workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.WorkOrder);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while canceling work order [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while canceling work order [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while canceling work order [{dc.Id}].");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.Id);
        if (workOrder is null)
          throw new InternalModuleException($"Work order [{dc.Id}] was not found.", AlarmDefsBase.RecordNotFound);

        WorkOrderHandler.UpdateBlockedWorkOrderByUser(workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.WorkOrder);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while unblocking work order [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while unblocking work order [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while unblocking work order [{dc.Id}].");
      }

      return result;
    }

    public virtual async Task<DataContractBase> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = Context.Create();
        PRMWorkOrder workOrder = await WorkOrderHandler.GetWorkOrderByIdAsync(ctx, dc.Id);
        if (workOrder is null)
          throw new InternalModuleException($"Work order [{dc.Id}] was not found.", AlarmDefsBase.RecordNotFound);

        WorkOrderHandler.UpdateBlockedWorkOrderByUser(workOrder, dc);

        await ctx.SaveChangesAsync();
        HmiRefresh(HMIRefreshKeys.WorkOrder);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while blocking work order [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while blocking work order [{dc.Id}].");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while blocking work order [{dc.Id}].");
      }

      return result;
    }

    public virtual async Task<DataContractBase> EndOfWorkOrderAsync(WorkOrderId dc)
    {
      DataContractBase result = new DataContractBase();

      try
      {
        await using var ctx = new PEContext();
        using (await _mutex.LockAsync())
        {
          var materials = await MaterialHandler.GetRawMaterialsByWorkOrderIdAsync(ctx, dc.Id);
          materials.ForEach(x =>
          {
            if (x.EnumRawMaterialStatus == RawMaterialStatus.Rolled)
              x.EnumRawMaterialStatus = RawMaterialStatus.Finished;
          });

          await ctx.SaveChangesAsync();

          TaskHelper.FireAndForget(() => CheckShiftsWorkOrderStatusses(new DCShiftCalendarId()).GetAwaiter().GetResult(),
            "Error while EndOfWorkOrderAsync");
        }

        HmiRefresh(HMIRefreshKeys.Schedule);
        HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while ending work order [{dc.Id}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while ending work order [{dc.Id}].");
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.WorkOrderNotEnded,
          $"Unexpected error while ending work order [{dc.Id}].");
      }

      return result;
    }

    protected virtual async Task AutoScheduleWorkOrderAsync(long workOrderId)
    {
      var sendOfficeResult = await SendOffice.AutoScheduleWorkOrderAsync(new() { WorkOrderId = workOrderId });

      if (sendOfficeResult.OperationSuccess)
        NotificationController.Info("[WO:{workorder}] Forwarding work order to schedule module - success",
         workOrderId);
      else
        NotificationController.Error(
          "Forwarding work order to schedule module - error. Work order will not be scheduled automatically");
    }

    protected virtual async Task<bool> ShouldWorkOrderWithBarsBeFinishedAsync(List<TRKRawMaterial> rawMaterials, PRMWorkOrder workOrder, int materialsCount)
    {
      await Task.CompletedTask;

      if (materialsCount != rawMaterials.Select(x => x.FKMaterialId.Value).Distinct().Count())
        return false;

      if (!rawMaterials.Any(x => x.EnumRawMaterialStatus < RawMaterialStatus.Finished))
        return true;

      return false;
    }

    protected virtual async Task<bool> ShouldWorkOrderWithCoilsBeFinishedAsync(List<TRKRawMaterial> rawMaterials, PRMWorkOrder workOrder, int materialsCount)
    {
      await Task.CompletedTask;

      if (materialsCount != rawMaterials.Select(x => x.FKMaterialId.Value).Distinct().Count())
        return false;

      if (rawMaterials.Any(x => x.EnumRawMaterialStatus < RawMaterialStatus.Rolled))
        return false;

      foreach (var item in rawMaterials)
      {
        if (item.EnumRawMaterialStatus != RawMaterialStatus.Divided
          && item.EnumRawMaterialStatus != RawMaterialStatus.Rejected
          && item.EnumRawMaterialStatus != RawMaterialStatus.Scrap)
        {
          if (item.EnumRawMaterialStatus >= RawMaterialStatus.Rolled && !item.FKProductId.HasValue)
          {
            NotificationController.RegisterAlarm(AlarmDefsBase.CoilWorkOrderCannotBeFinishedDueToLackOfProduct,
              $"Coil {item.RawMaterialName} is in status {item.EnumRawMaterialStatus.Value} but it has no product created. Work order {workOrder.WorkOrderName} cannot be finished.",
              workOrder.WorkOrderName, item.RawMaterialName);
            return false;
          }
        }
      }

      return true;
    }

    protected virtual async Task<bool> ShouldWorkOrderWithGarretsBeFinishedAsync(List<TRKRawMaterial> rawMaterials, PRMWorkOrder workOrder, int materialsCount)
    {
      await Task.CompletedTask;

      if (materialsCount != rawMaterials.Select(x => x.FKMaterialId.Value).Distinct().Count())
        return false;

      if (rawMaterials.Any(x => x.EnumRawMaterialStatus < RawMaterialStatus.Rolled))
        return false;

      foreach (var item in rawMaterials)
      {
        if (item.EnumRawMaterialStatus != RawMaterialStatus.Divided
          && item.EnumRawMaterialStatus != RawMaterialStatus.Rejected
          && item.EnumRawMaterialStatus != RawMaterialStatus.Scrap)
        {
          if (item.EnumRawMaterialStatus >= RawMaterialStatus.Rolled && !item.FKProductId.HasValue)
          {
            NotificationController.RegisterAlarm(AlarmDefsBase.BarInCoilWorkOrderCannotBeFinishedDueToLackOfProduct,
              $"Bar in coil {item.RawMaterialName} is in status {item.EnumRawMaterialStatus.Value} but it has no product created. Work order {workOrder.WorkOrderName} cannot be finished.",
              workOrder.WorkOrderName, item.RawMaterialName);
            return false;
          }
        }
      }

      return true;
    }

    protected virtual async Task<bool> ShouldWorkOrderWithOtherProductsBeFinishedAsync(List<TRKRawMaterial> rawMaterials, PRMWorkOrder workOrder, int materialsCount)
    {
      await Task.CompletedTask;

      if (materialsCount != rawMaterials.Select(x => x.FKMaterialId.Value).Distinct().Count())
        return false;

      if (!rawMaterials.Any(x => x.EnumRawMaterialStatus < RawMaterialStatus.Rolled))
        return true;

      return false;
    }
  }
}
