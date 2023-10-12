using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.BaseModels.DataContracts.Internal.ZPC;
using PE.Common;
using PE.Helpers;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingComponents.Collections.Concrete;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.TRK.Base.Managers.Concrete
{
  public class TrackingDispatcherManagerBase : BaseManager, ITrackingDispatcherManagerBase
  {
    protected string LabelTemplateCode;
    protected DateTime? AdjustionStartTs;
    protected readonly Dictionary<long, short> LayerWithMaterialsCounterMap;

    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;
    protected readonly ITrackingMaterialCutProviderBase MaterialCutProvider;
    protected readonly ITrackingCtrMaterialProviderBase CtrMaterialProvider;
    protected readonly ITrackingMaterialProcessingProviderBase TrackingMaterialProcessingProviderBase;
    protected readonly ITrackingGetNdrMeasurementSendOfficeBase TrackingGetNdrMeasurementSendOfficeBase;
    protected readonly ITrackingL1AdapterSendOfficeBase TrackingL1AdapterSendOfficeBase;
    protected readonly ITrackingProcessMaterialEventSendOfficeBase TrackingProcessMaterialEventSendOffice;
    protected readonly ITrackingProcessExternalSignalsSendOffice ProcessExternalSignalsSendOffice;
    protected readonly ITrackingProcessMeasurementsSendOffice TrackingProcessMeasurementsSendOffice;
    protected readonly ITrackingLabelPrinterSendOffice TrackingLabelPrinterSendOffice;
    protected readonly TrackingHandlerBase TrackingHandler;
    protected readonly Stopwatch Stopwatch = new Stopwatch();

    public TrackingDispatcherManagerBase(
      IModuleInfo moduleInfo,
      ITrackingStorageProviderBase storageProvider,
      ITrackingEventStorageProviderBase eventStorageProvider,
      ITrackingMaterialCutProviderBase materialCutProvider,
      ITrackingCtrMaterialProviderBase ctrMaterialProvider,
      ITrackingMaterialProcessingProviderBase trackingMaterialProcessingProviderBase,
      ITrackingProcessMaterialEventSendOfficeBase trackingProcessMaterialEventSendOffice,
      ITrackingGetNdrMeasurementSendOfficeBase trackingGetNdrMeasurementSendOfficeBase,
      ITrackingProcessMeasurementsSendOffice trackingProcessMeasurementsSendOffice,
      ITrackingLabelPrinterSendOffice trackingLabelPrinterSendOffice,
      TrackingHandlerBase trackingHandler,
      ITrackingL1AdapterSendOfficeBase trackingL1AdapterSendOfficeBase) : base(moduleInfo)
    {
      TrackingHandler = trackingHandler;

      StorageProvider = storageProvider;
      EventStorageProvider = eventStorageProvider;
      TrackingProcessMaterialEventSendOffice = trackingProcessMaterialEventSendOffice;
      MaterialCutProvider = materialCutProvider;
      CtrMaterialProvider = ctrMaterialProvider;
      TrackingMaterialProcessingProviderBase = trackingMaterialProcessingProviderBase;
      TrackingProcessMeasurementsSendOffice = trackingProcessMeasurementsSendOffice;
      TrackingGetNdrMeasurementSendOfficeBase = trackingGetNdrMeasurementSendOfficeBase;
      TrackingLabelPrinterSendOffice = trackingLabelPrinterSendOffice;

      LayerWithMaterialsCounterMap = new Dictionary<long, short>();
      TrackingL1AdapterSendOfficeBase = trackingL1AdapterSendOfficeBase;
    }

    public void Init()
    {
      InitQueues();
      _ = CtrMaterialProvider.CreateMaterial();
      _ = CtrMaterialProvider.CreateMaterial();
      _ = CtrMaterialProvider.CreateMaterial();
    }

    public void Start()
    {
      ProcessTrackingPointSignals();
    }

    public virtual void SetLabelTemplateCode(string newLabelCode)
    {
      LabelTemplateCode = newLabelCode;
    }

    #region private methods

    #endregion

    #region protected methods

    protected virtual async Task CreateNewLayer(PEContext ctx, DateTime operationDate, int layerAssetCode, int retryCount = 0)
    {
      try
      {
        TRKRawMaterial layerNew;
        if (StorageProvider.TrackingAreas[layerAssetCode] is not Layer layer)
          throw new InternalModuleException($"Area {layerAssetCode} is not layer.",
            AlarmDefsBase.LayerNotCreatedBecauseAreaIsNotLayerArea, layerAssetCode);

        if (ctx is not null)
          layerNew = await CreateNewLayer(ctx);
        else
        {
          await using var context = new PEContext();
          layerNew = await CreateNewLayer(context);
        }

        TrackingCollectionElementAbstractBase element = new TrackingCollectionElementBase();
        element.MaterialInfoCollection.MaterialInfos.Add(new LayerMaterialInfo(layerNew.RawMaterialId));

        layer.ChargeElement(element, operationDate);

        await ctx.SaveChangesAsync();
      }
      catch (Exception e)
      {
        NotificationController.LogException(e, $"Something went wrong while {MethodHelper.GetMethodName()}");

        if (retryCount > 0)
          await CreateNewLayer(ctx, operationDate, retryCount - 1);
      }
    }

    protected virtual async Task<TRKRawMaterial> CreateNewLayer(PEContext ctx)
    {
      return await TrackingHandler.CreateRawMaterial(ctx,
        StorageProvider.AssetsDictionary[TrackingArea.LAYER_AREA].AssetId,
          DateTime.Now, RawMaterialType.Layer);
    }

    protected virtual void InitQueues()
    {
      List<QueuePosition> queuePositions = TrackingHandler.GetQueuePositionsInit();

      var layerRelations = TrackingHandler.LayerRelationInit(queuePositions.Where(x => x.RawMaterialId.HasValue).Select(x => x.RawMaterialId.Value));
      foreach (var item in layerRelations.DistinctBy(x => x.ParentLayerRawMaterialId))
      {
        LayerWithMaterialsCounterMap.Add(item.ParentLayerRawMaterialId, (short)layerRelations.Count(x => x.ParentLayerRawMaterialId == item.ParentLayerRawMaterialId));
      }

      foreach (var configurationCollectionArea in StorageProvider.TrackingConfiguration.TrackingCollectionAreas)
      {
        InitQueue(configurationCollectionArea, queuePositions);
      }

      foreach (var configurationCtrArea in StorageProvider.TrackingConfiguration.TrackingCtrAreas)
      {
        var ctrArea = new CtrAreaBase(configurationCtrArea.AreaAssetCode,
          configurationCtrArea.AreaModeProductionFeatureCode,
          configurationCtrArea.AreaModeAdjustionFeatureCode,
          configurationCtrArea.AreaSimulationFeatureCode,
          configurationCtrArea.AreaAutomaticReleaseFeatureCode,
          configurationCtrArea.AreaEmptyFeatureCodeFeatureCode,
          configurationCtrArea.AreaCobbleDetectedFeatureCode,
          configurationCtrArea.AreaModeLocalFeatureCode,
          configurationCtrArea.AreaCobbleDetectionSelectedFeatureCode);


        StorageProvider.TrackingAreas.Add(configurationCtrArea.AreaAssetCode, ctrArea);
      }
    }

    protected virtual void InitQueue(ConfigurationCollectionAreaBase configurationCollectionArea, List<QueuePosition> queuePositions)
    {
      switch (configurationCollectionArea.AreaAssetCode)
      {
        case var value when value == TrackingArea.CHG_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new TrackingUnChargeableUnDischargeableNonPositionRelatedListBase(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;
        case var value when value == TrackingArea.FCE_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new FurnaceBase(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
             configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;

        case var value when value == TrackingArea.GREY_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new CtrGreyListArea(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;

        case var value when value == TrackingArea.ENTER_TABLE_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new TrackingNonPositionRelatedListBase(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;

        case var value when value == TrackingArea.GARRET_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new TrackingNonPositionRelatedListBase(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;

        case var value when value == TrackingArea.RAKE_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new Rake(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList()
            ));

          break;

        case var value when value == TrackingArea.BAR_WEIGHING_AREA:
          StorageProvider.TrackingAreas.Add(configurationCollectionArea.AreaAssetCode, new TrackingNonPositionRelatedListBase(
            EventStorageProvider,
            configurationCollectionArea.AreaAssetCode,
            queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList(),
            configurationCollectionArea.PositionsAmount,
            configurationCollectionArea.VirtualPositionsAmount));

          break;

        case var value when value == TrackingArea.LAYER_AREA:
          InitLayers(configurationCollectionArea, queuePositions
              .Where(p => p.AssetCode == configurationCollectionArea.AreaAssetCode)
              .ToList());
          break;
      }
    }

    protected virtual void InitLayers(ConfigurationCollectionAreaBase configurationArea, List<QueuePosition> layersPositions)
    {
      try
      {
        bool existingPositionsImported = true;

        using var ctx = new PEContext();
        if (!layersPositions.Any())
        {
          //TODO add parameter
          for (int i = 1; i <= configurationArea.PositionsAmount; i++)
          {
            var result = CreateNewLayer(ctx).GetAwaiter().GetResult();

            layersPositions.Add(new QueuePosition(i, 1, configurationArea.AreaAssetCode, result?.RawMaterialId, false,
              false));
          }
          existingPositionsImported = false;
        }
        else
        {
          foreach (QueuePosition layerPosition in layersPositions)
          {
            if (!layerPosition.RawMaterialId.HasValue)
            {
              var result = CreateNewLayer(ctx).GetAwaiter().GetResult();
              layerPosition.RawMaterialId = result.RawMaterialId;
              layerPosition.IsEmpty = false;

              existingPositionsImported = false;
            }
            else
            {
              //var materialsSum = (short)ctx.TRKLayerRawMaterialRelations.Count(x => x.ParentLayerRawMaterialId == layerPosition.RawMaterialId.Value
              //&& x.ActualRelation.HasValue && x.ActualRelation.Value);
              //LayerWithMaterialsCounterMap.Add(layerPosition.RawMaterialId.Value, materialsSum);
            }
          }
        }

        StorageProvider.TrackingAreas.Add(configurationArea.AreaAssetCode, new Layer(
             EventStorageProvider,
             configurationArea.AreaAssetCode,
             configurationArea.PositionsAmount,
             configurationArea.VirtualPositionsAmount,
             layersPositions,
             LayerWithMaterialsCounterMap,
             existingPositionsImported
             ));
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}");
        throw;
      }
    }

    protected virtual PEContext CreatePEContext()
    {
      return new PEContext();
    }

    protected virtual void ProcessTrackingPointSignals()
    {
      TaskHelper.FireAndForget(async () =>
      {
        bool isSimulation = false;

        while (true)
        {
          try
          {
            Stopwatch.Reset();
            var length = EventStorageProvider.TrackingPointSignalsToBeProcessed.Count;

            if (length > 0)
            {
              var signalEntry = EventStorageProvider.TrackingPointSignalsToBeProcessed
              .OrderBy(x => x.Value.OperationDate.Ticks)
              .ThenBy(x => x.Value.FeatureCode)
              .FirstOrDefault(x => x.Value.ProcessExpertOperationDate.AddMilliseconds(100) < DateTime.Now);

              if ((signalEntry.Key, signalEntry.Value) == default)
                await Task.Delay(100);
              else
              {
                EventStorageProvider.TrackingPointSignalsToBeProcessed.TryRemove(signalEntry.Key, out DcTrackingPointSignal signal);

                List<TrackingInstruction> trackingInstructions =
                  StorageProvider.TrackingInstructionsDictionary[signal.FeatureCode]
                  .Where(x => x.TrackingInstructionValue == signal.Value || !x.TrackingInstructionValue.HasValue)
                  .OrderBy(x => x.SeqNo)
                  .ToList();

                if (!trackingInstructions.Any())
                  continue;

                ITrackingInstructionDataContractBase result = null;
                CtrMaterialBase material = null;
                await using var ctx = CreatePEContext();

                if (StorageProvider.TrackingAreas.Any(x => x.Value is CtrAreaBase ctrArea && ctrArea.AreaSimulation.Value))
                  isSimulation = true;
                else
                  isSimulation = false;

                foreach (var instruction in trackingInstructions)
                {
                  if (!ProcessMaterialDueToSimulation(instruction, isSimulation))
                  {
                    NotificationController.Warn($"Instruction FeatureCode: {instruction.FeatureCode}" +
                      $"AreaAssetCode: {instruction.AreaAssetCode} PointAssetCode: {instruction.PointAssetCode} " +
                      $"was ignored due to simulation");

                    continue;
                  }

                  if (!ProcessInstructionDueToAdjustion(instruction, signal))
                  {
                    NotificationController.Warn($"Instruction FeatureCode: {instruction.FeatureCode}" +
                      $"AreaAssetCode: {instruction.AreaAssetCode} PointAssetCode: {instruction.PointAssetCode} " +
                      $"IsProcessedDuringAdjustment: {instruction.IsProcessedDuringAdjustment} AdjustionStartTs: {AdjustionStartTs} " +
                      $"was ignored due to adjustion");

                    continue;
                  }

                  result = await ProcessTrackingInstructionsAsync(ctx, instruction, material, signal, result, isSimulation);

                  if (result is TrackingBreakInstructionDataContractBase)
                    break;
                }

                //if (material != null && material.MaterialInfo.MaterialId == 0)
                //{
                //  NotificationController.Error($"Fatal error - no material was created while processing instruction Occupied");

                //  var rawMaterial = TrackingHandler
                //                  .CreateRawMaterial(ctx,
                //    StorageProvider.AssetsDictionary[trackingInstructions.First().AreaAssetCode].AssetId,
                //                    signal.OperationDate, isSimulation).GetAwaiter().GetResult();

                //  if (rawMaterial != null)
                //  {
                //    material.MaterialInfo.ChangeMaterialId(rawMaterial.RawMaterialId);
                //  }
                //}

                NotificationController.Debug($"Processing signal FeatureCode: {signal.FeatureCode} Value: {signal.Value} Took: {Stopwatch.ElapsedMilliseconds}ms");
              }
            }
            else
            {
              //TODO MN -refactor delay
              await Task.Delay(100);
            }
          }
          catch (Exception e)
          {
            NotificationController.LogException(e, $"Something went wrong while {MethodHelper.GetMethodName()}");
          }
        }
      });
    }

    protected bool ProcessInstructionDueToAdjustion(TrackingInstruction instruction, DcTrackingPointSignal signal)
    {
      return instruction.IsProcessedDuringAdjustment
        || !AdjustionStartTs.HasValue
        || signal.TimeStamp < AdjustionStartTs.Value;
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessWeighingStationWeightNDRInstruction(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        if (StorageProvider.TrackingAreas[instruction.AreaAssetCode] is not TrackingNonPositionRelatedListBase weighing)
        {
          NotificationController.Error($"For area {instruction.AreaAssetCode} type must be {nameof(TrackingNonPositionRelatedListBase)}");
        }
        else
        {
          var element = weighing.GetFirstElement();

          if (element == null)
          {
            NotificationController.Error($"For area {instruction.AreaAssetCode} there is no elements");
          }
          else
          {
            var sendOfficeResult = await TrackingGetNdrMeasurementSendOfficeBase.ProcessNdrMeasurementRequestAsync(new DcNdrMeasurementRequest()
            {
              ParentFeatureCode = instruction.FeatureCode,
              TimeStamp = signal.OperationDate,
              DateToTs = signal.OperationDate
            });

            if (!sendOfficeResult.OperationSuccess)
            {
              NotificationController.Error($"Something went wrong while ProcessNdrMeasurementRequestAsync");
            }
            else
            {
              if (sendOfficeResult.DataConctract.FeatureCode == 0)
              {
                NotificationController.Warn($"For parentFeatureCode: {instruction.FeatureCode} no measurement found");
              }
              else
              {
                var rawMaterialId = element.MaterialInfoCollection.MaterialInfos.First().MaterialId;

                ProcessNdrMeasurement(new DcMeasData()
                {
                  Avg = sendOfficeResult.DataConctract.Value,
                  Max = sendOfficeResult.DataConctract.Value,
                  Min = sendOfficeResult.DataConctract.Value,
                  FeatureCode = sendOfficeResult.DataConctract.FeatureCode,
                  FirstMeasurementTs = signal.OperationDate,
                  LastMeasurementTs = signal.OperationDate,
                  RawMaterialId = rawMaterialId,
                  TimeStamp = signal.OperationDate,
                  Valid = true
                });

                var rawMaterial = ctx.TRKRawMaterials
                  .Include(x => x.FKProduct)
                  .First(x => x.RawMaterialId == rawMaterialId);

                rawMaterial.LastWeight = sendOfficeResult.DataConctract.Value;
                rawMaterial.FKProduct.ProductWeight = sendOfficeResult.DataConctract.Value;
                rawMaterial.FKProduct.EnumWeightSource = WeightSource.Measured;

                await ctx.SaveChangesAsync();

                HmiRefresh(HMIRefreshKeys.CoilWeighingMonitor);
                HmiRefresh(HMIRefreshKeys.BundleWeighingMonitor);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}" +
          $"for featureCode: {instruction.FeatureCode}");
      }

      return result;
    }

    protected virtual void ProcessNdrMeasurement(DcMeasData measData)
    {
      if (measData.RawMaterialId > 0)
      {
        TaskHelper.FireAndForget(async () =>
        {
          var result = await TrackingProcessMeasurementsSendOffice.StoreSingleMeasurementAsync(measData);

          if (!result.OperationSuccess)
            NotificationController.Error($"Something went wrong while {MethodHelper.GetMethodName()}");
        });
      }
      else
        NotificationController.Error($"Missing RawMaterialId for {MethodHelper.GetMethodName()}");
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessLabelPrintInstructionAsync(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      await Task.CompletedTask;

      try
      {
        if (StorageProvider.TrackingAreas[instruction.AreaAssetCode] is not TrackingNonPositionRelatedListBase weighing)
        {
          NotificationController.Error($"For area {instruction.AreaAssetCode} type must be {nameof(TrackingNonPositionRelatedListBase)}");
        }
        else
        {
          var element = weighing.GetFirstElement();

          if (element is null)
            throw new InternalModuleException(AlarmDefsBase.UnexpectedError, $"For area {instruction.AreaAssetCode} there is no elements.");
          else
          {
            var l3MaterialId = element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId;

            if (l3MaterialId != null)
            {
              TaskHelper.FireAndForget(async () => await PrintLabelAsync(ctx, l3MaterialId.Value));
            }
          }
        }
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
      }

      return result;
    }

    protected async Task<ITrackingInstructionDataContractBase> ProcessCageClosedInstructionAsync(TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      await Task.CompletedTask;

      try
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ShapShearCageClosed, $"Cage closed in snap shear {instruction.PointAssetCode}.", instruction.PointAssetCode);
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
      }

      return result;
    }

    /// <summary>
    ///   Process tracking instructions one-by-one
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="material"></param>
    /// <param name="signal"></param>
    /// <param name="result"></param>
    /// <param name="isSimulation"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessTrackingInstructionsAsync(PEContext ctx, TrackingInstruction instruction, CtrMaterialBase material, DcTrackingPointSignal signal, ITrackingInstructionDataContractBase result, bool isSimulation, bool displayLog = true)
    {
      if (displayLog)
        NotificationController.Debug($"Received TrackingPointInstruction with parameters: signal value: {signal.Value} " +
          $"signal featureCode: {signal.FeatureCode} signal operationDate: {signal.OperationDate} " +
          $"InstructionId: {instruction.InstructionId} TrackingInstructionType: {instruction.TrackingInstructionType.Name} " +
          $"Instruction SeqNo: {instruction.SeqNo} AreaAssetCode: {instruction.AreaAssetCode} " +
          $"PointAssetCode: {instruction.PointAssetCode}");

      result = (short)instruction.TrackingInstructionType switch
      {
        var value when value == TrackingInstructionType.CreateMaterialForQueue =>
          await ProcessCreateMaterialForQueueTrackingInstructionAsync(ctx, instruction, signal),
        // Occupied instruction type should be at the first place
        var value when value == TrackingInstructionType.Occupied =>
          await ProcessOccupiedTrackingInstructionAsync(ctx, instruction, material, signal, result, isSimulation),
        var value when value == TrackingInstructionType.AssignCTRMaterialInfo =>
          await ProcessAssignCTRMaterialInfoInstructionAsync(instruction, result),
        var value when value == TrackingInstructionType.Reject =>
          await ProcessRejectTrackingInstructionAsync(ctx, instruction, result, signal),
        var value when value == TrackingInstructionType.ChargeToLayer =>
          await ProcessChargeToLayerInstructionAsync(ctx, instruction, result),
        var value when value == TrackingInstructionType.LayerFormFinished =>
          await ProcessLayerFormFinishedInstructionAsync(ctx, instruction, result),
        var value when value == TrackingInstructionType.LayerTransferred =>
          await ProcessLayerTransferredInstructionAsync(ctx, instruction, result, signal),
        var value when value == TrackingInstructionType.CreateCollectionElement =>
          await ProcessCreateCollectionElementInstructionAsync(result),
        var value when value == TrackingInstructionType.RemoveFromGrey =>
          await ProcessRemoveFromGreyInstructionAsync(instruction, result, signal),
        var value when value == TrackingInstructionType.GetLastVisitedMaterialByPointAssetCode =>
          await ProcessGetLastVisitedMaterialByPointAssetCodeInstructionAsync(instruction),
        var value when value == TrackingInstructionType.ProcessAutoChopping =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.AutoChopping),
        var value when value == TrackingInstructionType.ProcessManualChopping =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.ManualChopping),
        var value when value == TrackingInstructionType.ProcessHeadCut =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.HeadCut),
        var value when value == TrackingInstructionType.ProcessTailCut =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.TailCut),
        var value when value == TrackingInstructionType.ProcessEmergencyCut =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.EmergencyCut),
        var value when value == TrackingInstructionType.ProcessSampleCut =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.SampleCut),
        var value when value == TrackingInstructionType.ProcessDivideCut =>
          await MaterialCutProvider.ProcessMaterialCutAsync(signal, ctx, instruction, result, TypeOfCut.DivideCut),
        var value when value == TrackingInstructionType.CalculatePartialScrapForShear =>
          await ProcessCalculatePartialScrapForShearInstructionAsync(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.CreateChildMaterial =>
          await ProcessCreateChildMaterialInstructionAsync(signal.OperationDate, ctx, result, instruction.PointAssetCode, isSimulation),
        var value when value == TrackingInstructionType.LastDivideVerification =>
          await ProcessLastDivideVerificationInstructionAsync(signal.OperationDate, ctx, result, instruction.PointAssetCode, isSimulation),
        var value when value == TrackingInstructionType.CreateChildCtrMaterial =>
          await ProcessCreateChildCtrMaterialInstructionAsync(signal.OperationDate, ctx, result, instruction.PointAssetCode, isSimulation),
        var value when value == TrackingInstructionType.AssignCorrelationIdToMaterialInfo =>
          ProcessAssignCorrelationIdToMaterialInfoInstruction(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.DischargeElementByCorrelationId =>
          ProcessDischargeElementByCorrelationIdInstruction(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.SetCorrelationId =>
          ProcessSetCorrelationIdInstruction(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.WeighingStationWeightNDR =>
          await ProcessWeighingStationWeightNDRInstruction(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.PrintLabel =>
          await ProcessLabelPrintInstructionAsync(signal, ctx, instruction, result),
        var value when value == TrackingInstructionType.CageClosed =>
          await ProcessCageClosedInstructionAsync(instruction, result),
        var value when value == TrackingInstructionType.ModeAdjustion =>
          await ProcessAdjustionInstructionAsync(signal, ctx, instruction, result),

        _ => await ProcessDefaultTrackingInstructionAsync(instruction, material, signal, result),
      };

      if (instruction.ParentInstructionId.HasValue)
      {
        StorageProvider.TrackingAreas[instruction.AreaAssetCode]
          .AddChildInstructionMeta(new TrackingChildInstructionMeta()
          {
            InstructionId = instruction.InstructionId,
            OperationDate = signal.OperationDate
          });

        var minOperationDate = signal.OperationDate.AddSeconds(-instruction.ParentInstruction.TimeFilter.Value);
        if (instruction.ParentInstruction.ChildInstructions.All(x => StorageProvider.TrackingAreas[x.AreaAssetCode].IsChildInstructionProcessedInTimeFilter(x.InstructionId, minOperationDate)))
        {
          TaskHelper.FireAndForget(() =>
          {
            instruction.ParentInstruction.ChildInstructions.ForEach(x =>
            {
              StorageProvider.TrackingAreas[x.AreaAssetCode].RemoveAllChildInstructionsMetaByInstructionId(x.InstructionId);
            });
          });

          return await ProcessTrackingInstructionsAsync(ctx, instruction.ParentInstruction, material, signal, result, isSimulation);
        }
      }

      return result;
    }

    /// <summary>
    ///  Calculate material weight based on Density and Speed - DO NOT USE - POC
    /// </summary>
    /// <param name="signal"></param>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> CalculateMaterialWeightBasedOnDensityAndSpeed(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        var material = CtrMaterialProvider.GetLastVisitedMaterialByPointAssetCode(instruction.PointAssetCode.Value);

        if (material == null)
        {
          NotificationController.Warn($"{MethodHelper.GetMethodName()} for point: {instruction.PointAssetCode}" +
            $"material is not found");
        }
        else
        {
          var point = material.TrackingPoints.First(x => x.AssetCode == instruction.PointAssetCode.Value);
          var speedFeatureCode = StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].FeaturesDictionary.Values
            .First(x => x.FeatureType == FeatureType.MeasuringSpeed)
            .FeatureCode;              
          
          var rawMaterialId = material.MaterialInfo.MaterialId;

          var speedMeasurementResult = await TrackingProcessMeasurementsSendOffice
            .GetMeasurementValueAsync(
              new DcMeasurementRequest(speedFeatureCode,
                point.HeadReceivedDate.Value,
                point.TailReceivedDate.Value));

          double avgSpeed = 0d;

          if (speedMeasurementResult.OperationSuccess)
            avgSpeed = speedMeasurementResult.DataConctract.Avg ?? 0d;
          else
            NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get speed measurement");

          var materialLength = (point.TailReceivedDate.Value - point.HeadReceivedDate.Value).TotalMilliseconds / 1000 * avgSpeed;

          var rawMaterial = await ctx.TRKRawMaterials
          .Where(x => x.RawMaterialId == rawMaterialId && x.FKMaterialId != null)
          .Include(x => x.FKMaterial.FKWorkOrder.FKSteelgrade)
          .Include(x => x.FKMaterial.FKMaterialCatalogue)
          .Include(x => x.FKMaterial.FKMaterialCatalogue.FKShape)
          .FirstOrDefaultAsync();

          var density = rawMaterial.FKMaterial?.FKWorkOrder?.FKSteelgrade?.Density; // in SI [kg/m3]
          var thickness = rawMaterial.FKMaterial?.FKMaterialCatalogue.ThicknessMin; // in SI [m]
          var shape = rawMaterial.FKMaterial?.FKMaterialCatalogue.FKShape?.ShapeCode;

          if (rawMaterial == null || !density.HasValue)
            return result;

          double? weight = null;

          // For rounds (R): Long = weight / (3.14 * (section² of the OF / 4) *density of the shade)
          // For squares (C): Long = weight / (section² of the OF* density of the shade)

          switch (shape)
          {
            case "R":
              weight = materialLength * ((Math.PI * (thickness * thickness / 4d) * density));
              break;
            case "C":
              weight = materialLength * (thickness * thickness * density);
              break;
          }

          rawMaterial.LastLength = materialLength;
          rawMaterial.LastWeight = weight;

          await ctx.SaveChangesAsync();
        }
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Unexpected error while while CalculateMaterialWeightBasedOnDensityAndSpeed for point {instruction.PointAssetCode.Value}.");
        NotificationController.LogException(ex, $"Unexpected error while while CalculateMaterialWeightBasedOnDensityAndSpeed for point {instruction.PointAssetCode.Value}.");
      }

      return result;
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessAdjustionInstructionAsync(DcTrackingPointSignal signal,
      PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        TrackingInstructionRequest request = new TrackingInstructionRequest(signal.OperationDate,
          instruction.TrackingInstructionType, signal.Value,
          instruction.AreaAssetCode, instruction.PointAssetCode,
          result);

        result = StorageProvider.TrackingAreas[instruction.AreaAssetCode].ProcessInstruction(request);
        bool value = Convert.ToBoolean(signal.Value);

        if (value)
        {
          AdjustionStartTs = signal.OperationDate;
          foreach (var material in StorageProvider.Materials.Values)
          {
            material.ProcessInManualMode(signal.OperationDate);
          }
        }
        else
        {
          AdjustionStartTs = null;
        }
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}" +
         $"for featureCode: {instruction.FeatureCode}");
      }

      return result;
    }

    protected virtual ITrackingInstructionDataContractBase ProcessSetCorrelationIdInstruction(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        if (StorageProvider.TrackingAreas[instruction.AreaAssetCode] is TrackingNonPositionRelatedListWithCorrelationIdBase area)
        {
          var correlationId = new IntCorrelationId(signal.Value);
          var request = new TrackingInstructionWithCorrelationIdRequest(correlationId, signal.OperationDate,
            TrackingInstructionType.SetCorrelationId, signal.Value, instruction.AreaAssetCode, null);

          area.ProcessInstruction(request);
        }
        else
        {
          NotificationController.Error($"Area {instruction.AreaAssetCode} must be of type {nameof(TrackingNonPositionRelatedListWithCorrelationIdBase)} for {nameof(ProcessSetCorrelationIdInstruction)}");
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {nameof(ProcessSetCorrelationIdInstruction)}" +
          $"for area: {instruction.AreaAssetCode}");
        //alarm
      }

      return new TrackingBreakInstructionDataContractBase();
    }

    protected virtual ITrackingInstructionDataContractBase ProcessDischargeElementByCorrelationIdInstruction(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        var pallete = StorageProvider.TrackingAreas[instruction.AreaAssetCode] is TrackingPalleteNonPositionRelatedList
        ? StorageProvider.TrackingAreas[instruction.AreaAssetCode] as TrackingPalleteNonPositionRelatedList
        : null;
        var hook = StorageProvider.TrackingAreas[instruction.AreaAssetCode] is TrackingHookNonPositionRelatedList
          ? StorageProvider.TrackingAreas[instruction.AreaAssetCode] as TrackingHookNonPositionRelatedList
          : null;

        if (pallete == null && hook == null)
        {
          NotificationController.Error($"Area: {instruction.AreaAssetCode} is not of type: {nameof(TrackingPalleteNonPositionRelatedList)} and not of type: {nameof(TrackingHookNonPositionRelatedList)}");

          return new TrackingBreakInstructionDataContractBase();
        }

        result = null;
        foreach (var trackingArea in StorageProvider.TrackingAreas.Values
          .Where(x => x.AreaAssetCode != instruction.AreaAssetCode))
        {
          if (hook != null && trackingArea is TrackingHookNonPositionRelatedList hookToCheck)
          {
            result = hookToCheck.DischargeByCorrelationId(signal.OperationDate, hook.ActiveCorrelationId);
          }

          if (pallete != null && trackingArea is TrackingPalleteNonPositionRelatedList palleteToCheck)
          {
            result = palleteToCheck.DischargeByCorrelationId(signal.OperationDate, pallete.ActiveCorrelationId);
          }

          if (result != null)
          {
            return result;
          }
        }

        if (result == null)
        {
          NotificationController.Error($"There is no material found by Hook CorrelationId: {hook.ActiveCorrelationId} Pallete: {pallete.ActiveCorrelationId}");
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {nameof(ProcessDischargeElementByCorrelationIdInstruction)}" +
           $"for area: {instruction.AreaAssetCode}");
        //alarm
      }

      return new TrackingBreakInstructionDataContractBase();
    }

    protected virtual ITrackingInstructionDataContractBase ProcessAssignCorrelationIdToMaterialInfoInstruction(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      if (result is TrackingCollectionElementAbstractBase element)
      {
        if (StorageProvider.TrackingAreas[instruction.AreaAssetCode] is TrackingNonPositionRelatedListWithCorrelationIdBase area)
        {
          var correlationId = area.ActiveCorrelationId;
          var materialInfo = element.MaterialInfoCollection.MaterialInfos.FirstOrDefault();

          materialInfo.ChangeCorrelationId(correlationId);

          return element;
        }
        else
        {
          NotificationController.Error($"For Operation {nameof(ProcessAssignCorrelationIdToMaterialInfoInstruction)} area for assetCode: {instruction.AreaAssetCode} must be of type {nameof(TrackingNonPositionRelatedListWithCorrelationIdBase)}");
        }
      }
      else
      {
        NotificationController.Warn($"For Operation {nameof(ProcessAssignCorrelationIdToMaterialInfoInstruction)} previous operation result must be of type {nameof(MaterialInfoBase)}");
      }

      return new TrackingBreakInstructionDataContractBase();
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessCreateChildCtrMaterialInstructionAsync(DateTime operationDate,
      PEContext ctx,
      ITrackingInstructionDataContractBase result,
      int? pointAssetCode,
      bool isSimulation,
      bool updateCuttingSeqNo = true)
    {
      await Task.CompletedTask;

      try
      {
        if (result is MaterialInfoBase childMaterialInfo && childMaterialInfo.ParentMaterialId.HasValue)
        {
          var childCtrMaterial = CtrMaterialProvider.CreateMaterial();

          childCtrMaterial.SetMaterialInfo(childMaterialInfo);

          var parentCtrMaterial = GetCtrMaterialById(childMaterialInfo.ParentMaterialId.Value, out bool isFromRemove);

          if (parentCtrMaterial != null)
          {
            childCtrMaterial.SetStartDate(operationDate);
            CtrMaterialProvider.SetTrackingPointsForDividedMaterial(parentCtrMaterial, childCtrMaterial, pointAssetCode.Value, operationDate);

            StorageProvider.Materials.AddOrUpdate(childCtrMaterial.Id, childCtrMaterial, (key, value) => childCtrMaterial);

            if (!isFromRemove)
            {
              StorageProvider.Materials.AddOrUpdate(parentCtrMaterial.Id, parentCtrMaterial, (key, value) => parentCtrMaterial);
            }

            return result;
          }
          else
            NotificationController.RegisterAlarm(AlarmDefsBase.ParentRawMaterialNotFoundInPoint,
              $"{MethodHelper.GetMethodName()} for point with asset code {pointAssetCode} parent material not found", pointAssetCode);
        }
        else
          NotificationController.Error($"{MethodHelper.GetMethodName()} for point: {pointAssetCode}" +
            $"previous result is wrong");
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.RecordNotUnique, $"Unique key violation while creating child material in point with code {pointAssetCode}.");
        NotificationController.LogException(ex, $"Unique key violation while creating child material in point with code {pointAssetCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ExistingReferenceViolation, $"Existing reference key violation while creating child material in point with code {pointAssetCode}.");
        NotificationController.LogException(ex, $"Existing reference key violation while creating child material in point with code {pointAssetCode}.");
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Existing reference key violation while creating child material in point with code {pointAssetCode}.");
        NotificationController.LogException(ex, $"Existing reference key violation while creating child material in point with code {pointAssetCode}.");
      }

      return new TrackingInstructionDataContractBase();
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessCreateChildMaterialInstructionAsync(DateTime operationDate,
      PEContext ctx, ITrackingInstructionDataContractBase result, int? pointAssetCode, bool isSimulation, bool updateCuttingSeqNo = true)
    {
      try
      {
        if (result is MaterialInfoBase parentMaterialInfo)
        {
          var parentRawMaterial = await TrackingHandler.FindRawMaterialByIdAsync(ctx, parentMaterialInfo.MaterialId);

          var children = await TrackingHandler.CreateChildrenMaterials(ctx, 1, operationDate, parentRawMaterial, true, updateCuttingSeqNo);

          parentMaterialInfo.ChangeDivideAssetCode(pointAssetCode.Value);

          var rawMaterial = children.First();

          var materialInfo = new MaterialInfo(rawMaterial.RawMaterialId);

          materialInfo.ChangeParentMaterialId(parentRawMaterial.RawMaterialId);

          return materialInfo;
        }
        else
          NotificationController.Warn($"{MethodHelper.GetMethodName()} for point: {pointAssetCode}" +
            $"material is not found");
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.RecordNotUniqueDuringChildCreation, $"Unique key violation while creating child material in point with code {pointAssetCode}.", pointAssetCode);
        NotificationController.LogException(ex, $"Unique key violation while creating child material in point with code {pointAssetCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ExistingReferenceViolationDuringChildCreation, $"Unique key violation while creating child material in point with code {pointAssetCode}.");
        NotificationController.LogException(ex, $"Unique key violation while creating child material in point with code {pointAssetCode}.");
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedErrorDuringChildCreation, $"Unexpected error while creating child material in point with code {pointAssetCode}.", pointAssetCode);
        NotificationController.LogException(ex, $"Unexpected error while creating child material in point with code {pointAssetCode}.");
      }

      return new TrackingInstructionDataContractBase();
    }



    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessLastDivideVerificationInstructionAsync(DateTime operationDate,
      PEContext ctx, ITrackingInstructionDataContractBase result, int? pointAssetCode, bool isSimulation)
    {
      try
      {
        if (result is TrackingCollectionElementAbstractBase element)
        {
          var parentMaterialInfo = element.MaterialInfoCollection.MaterialInfos.First();
          if (parentMaterialInfo.DivideAssetCode.HasValue)
          {
            if (parentMaterialInfo.DivideAssetCode == pointAssetCode)
            {
              result = await ProcessCreateChildMaterialInstructionAsync(operationDate, ctx, parentMaterialInfo, pointAssetCode, isSimulation, false);

              if (result is MaterialInfoBase childMaterialInfo)
              {
                var parentCtrMaterial = GetCtrMaterialById(childMaterialInfo.ParentMaterialId.Value, out bool isFromRemove);
                if (parentCtrMaterial != null)
                {
                  parentCtrMaterial.SetMaterialInfo(childMaterialInfo);

                  if (!isFromRemove)
                  {
                    StorageProvider.Materials.AddOrUpdate(parentCtrMaterial.Id, parentCtrMaterial, (key, value) => parentCtrMaterial);
                  }
                }
                else
                {
                  NotificationController.Error($"{MethodHelper.GetMethodName()} for point: {pointAssetCode}" + $"parent material not found.");
                  //alarm
                }
              }
            }
          }

          return result;
        }
        else
        {
          NotificationController.Error($"Material not found on point with asset code {pointAssetCode}.");
          NotificationController.RegisterAlarm(AlarmDefsBase.MaterialNotFoundOnPoint,
            $"Material not found on point with asset code {pointAssetCode}.", pointAssetCode);
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.RecordNotUniqueDuringLastDivideVerification, $"Unexpected error while creating child material in point with code {pointAssetCode}.");
        NotificationController.LogException(ex, $"Unique key violation while verifying last divide in point with code {pointAssetCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ExistingReferenceViolationDuringLastDivideVerification, $"Existing reference key violation while verifying last divide in point with code {pointAssetCode}.", pointAssetCode);
        NotificationController.LogException(ex, $"Existing reference key violation while verifying last divide in point with code {pointAssetCode}.");
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedErrorDuringLastDivideVerification, $"Unexpected error while verifying last divide in point with code {pointAssetCode}.", pointAssetCode);
        NotificationController.LogException(ex, $"Unexpected error while verifying last divide in point with code {pointAssetCode}.");
      }

      return result;
    }

    protected virtual CtrMaterialBase GetCtrMaterialById(long id, out bool isFromLastRemove)
    {
      isFromLastRemove = false;

      var parentCtrMaterial = StorageProvider.Materials.Values.FirstOrDefault(x => x.MaterialInfo.MaterialId == id);

      if (parentCtrMaterial == null)
      {
        if (StorageProvider.LastRemovedMaterial?.MaterialInfo?.MaterialId == id)
        {
          parentCtrMaterial = StorageProvider.LastRemovedMaterial;
          isFromLastRemove = true;
        }
      }

      return parentCtrMaterial;
    }

    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessCalculatePartialScrapForShearInstructionAsync(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      try
      {
        var material = CtrMaterialProvider.GetLastVisitedMaterialByPointAssetCode(instruction.PointAssetCode.Value);

        if (material == null)
        {
          NotificationController.Warn($"{MethodHelper.GetMethodName()} for point: {instruction.PointAssetCode}" +
            $"material is not found");
        }
        else
        {
          var point = material.TrackingPoints.First(x => x.AssetCode == instruction.PointAssetCode.Value) as ShearTrackingPoint;
          var choppingStartDate = point.AutoChoppingStartDate ?? point.ManualChoppingStartDate;
          var choppingEndDate = point.AutoChoppingEndDate ?? point.ManualChoppingEndDate ?? signal.OperationDate;

          if (choppingStartDate is null)
            return result;

          var speedFeatureCode = StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].FeaturesDictionary.Values
            .First(x => x.FeatureType == FeatureType.MeasuringSpeed)
            .FeatureCode;

          var rawMaterialId = material.MaterialInfo.MaterialId;

          var speedMeasurementResult = await TrackingProcessMeasurementsSendOffice
            .GetMeasurementValueAsync(
              new DcMeasurementRequest(speedFeatureCode,
                point.HeadReceivedDate.Value,
                point.TailReceivedDate.Value));

          double avgSpeed = 0d;

          if (speedMeasurementResult.OperationSuccess)
            avgSpeed = speedMeasurementResult.DataConctract.Avg ?? 0d;
          else
            NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get speed measurement");

          var materialLength = (point.TailReceivedDate.Value - point.HeadReceivedDate.Value).TotalMilliseconds / 1000 * avgSpeed;

          double totalCutLength = (choppingEndDate - choppingStartDate.Value).TotalMilliseconds / 1000 * avgSpeed;


          NotificationController.Debug($"{MethodHelper.GetMethodName()}, RawMaterialId: {rawMaterialId}, PointAssetCode: {instruction.PointAssetCode}, totalCutLength: {totalCutLength}");

          double maxAutoScrap = 0.9d;

          if (totalCutLength > 0d)
          {
            var scrapPercent = materialLength == 0d
            ? maxAutoScrap
            : totalCutLength / materialLength;

            NotificationController.Debug($"{MethodHelper.GetMethodName()}, RawMaterialId: {rawMaterialId}, PointAssetCode: {instruction.PointAssetCode}, scrapPercent: {scrapPercent}");
            await TrackingMaterialProcessingProviderBase.ProcessScrapMessage(new DCL1ScrapData(rawMaterialId)
            {
              AssetId = StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].AssetId,
              ScrapPercent = scrapPercent > maxAutoScrap ? maxAutoScrap : scrapPercent,
              ScrapRemark = "[AUTO]",
              TypeOfScrap = TypeOfScrap.PartialScrap
            });
          }
          else
          {
            var cutLength = (signal.OperationDate - choppingStartDate.Value).TotalMilliseconds / 1000 * avgSpeed;

            var scrapPercent = materialLength == 0d
             ? maxAutoScrap
             : totalCutLength / materialLength;

            NotificationController.Debug($"{MethodHelper.GetMethodName()}, RawMaterialId: {rawMaterialId}, PointAssetCode: {instruction.PointAssetCode}, cutLength: {cutLength}, scrapPercent: {scrapPercent}");

            await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, TypeOfCut.AutoChopping, materialLength);

            await TrackingMaterialProcessingProviderBase.ProcessScrapMessage(new DCL1ScrapData(rawMaterialId)
            {
              AssetId = StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].AssetId,
              ScrapPercent = scrapPercent > maxAutoScrap ? maxAutoScrap : scrapPercent,
              ScrapRemark = "[AUTO]",
              TypeOfScrap = TypeOfScrap.PartialScrap
            });
          }
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.RecordNotUnique, $"Record not unique while calculating partial scrap for point {instruction.PointAssetCode.Value}.");
        NotificationController.LogException(ex, $"Unexpected error while calculating partial scrap for point {instruction.PointAssetCode.Value}.");

        NotificationController.LogException(ex);
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ExistingReferenceViolation, $"Existing reference violation while while calculating partial scrap for point {instruction.PointAssetCode.Value}.");
        NotificationController.LogException(ex, $"Unexpected error while calculating partial scrap for point {instruction.PointAssetCode.Value}.");

        NotificationController.LogException(ex);
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Unexpected error while while calculating partial scrap for point {instruction.PointAssetCode.Value}.");
        NotificationController.LogException(ex, $"Unexpected error while while calculating partial scrap for point {instruction.PointAssetCode.Value}.");
      }

      return result;
    }

    /// <summary>
    ///   Determinates when signal should be processed in simulation mode
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="isSimulation"></param>
    /// <returns>Ignore signal flag</returns>
    protected virtual bool ProcessMaterialDueToSimulation(TrackingInstruction instruction, bool isSimulation)
    {
      if (isSimulation && instruction.IgnoreIfSimulation) // test purposes
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   Process create material for queue tracking instruction
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="signal"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessCreateMaterialForQueueTrackingInstructionAsync(PEContext ctx, TrackingInstruction instruction, DcTrackingPointSignal signal)
    {
      var element = new TrackingCollectionElementBase();
      var rawMaterial = await TrackingHandler.CreateRawMaterial(
        ctx,
        StorageProvider.AssetsDictionary[instruction.AreaAssetCode].AssetId,
        signal.OperationDate,
        RawMaterialType.Material);

      element.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterial.RawMaterialId));
      return element;
    }

    /// <summary>
    ///   Process occupied tracking instruction
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="material"></param>
    /// <param name="signal"></param>
    /// <param name="result"></param>
    /// <param name="isSimulation"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessOccupiedTrackingInstructionAsync(PEContext ctx, TrackingInstruction instruction, CtrMaterialBase material, DcTrackingPointSignal signal, ITrackingInstructionDataContractBase result, bool isSimulation)
    {
      bool value = Convert.ToBoolean(signal.Value);

      NotificationController.Debug($"Processing Area: {instruction.AreaAssetCode} Point: {instruction.PointAssetCode}" +
        $"Value: {value} Date: {signal.OperationDate}");

      NotificationController.Debug($"For L1 Debug: Area: {StorageProvider.AssetsDictionary[instruction.AreaAssetCode].AssetName} " +
        $"Point: {StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].AssetName} Value: {value} Date: {signal.OperationDate}");

      material = CtrMaterialProvider.MaterialHasOccupiedTheTrackingPoint(instruction.PointAssetCode.Value,
        instruction.AreaAssetCode, value, signal.OperationDate);

      if (material == null)
      {
        NotificationController.Warn($"MaterialHasOccupiedTheTrackingPoint returned null for PointAssetCode: {instruction.PointAssetCode.Value}," +
          $"AreaAssetCode: {instruction.AreaAssetCode}, Value: {value}, Date: {signal.OperationDate.ToLongTimeString()}");
        return result;
      }

      TaskHelper.FireAndForget(() => PrintStateAsync().GetAwaiter().GetResult());
      await SetCtrMaterialIfEmpty(ctx, instruction, material, signal, isSimulation);

      if (material.MaterialInfo.MaterialId == 0 && instruction.PointAssetCode > StorageProvider.MaxAssetCodeForNonInitializedMaterialBeingUsed)
      {
        material.RemoveMaterial();
        NotificationController.Warn($"Material with no materialId for trackingPoint: {instruction.PointAssetCode} was removed because it is located after {StorageProvider.MaxAssetCodeForNonInitializedMaterialBeingUsed}");
      }

      return new TrackingCollectionElementBase(material.MaterialInfo);
    }

    protected virtual async Task SetCtrMaterialIfEmpty(PEContext ctx, TrackingInstruction instruction, CtrMaterialBase material, DcTrackingPointSignal signal, bool isSimulation)
    {
      if (isSimulation && material.MaterialInfo.MaterialId == 0)
      {
        var rawMaterial = await TrackingHandler
          .CreateRawMaterial(ctx,
            StorageProvider.AssetsDictionary[instruction.PointAssetCode.Value].AssetId,
            signal.OperationDate, RawMaterialType.Material, isSimulation);

        if (rawMaterial != null)
        {
          material.MaterialInfo.ChangeMaterialId(rawMaterial.RawMaterialId);
          material.MaterialInfo.ChangeIsDummy(true);
        }
      }
    }

    /// <summary>
    ///   Process assign CTR material info instruction from processed area
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="result"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessAssignCTRMaterialInfoInstructionAsync(TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      await Task.CompletedTask;

      if (result != null)
      {
        var material = CtrMaterialProvider.GetLastNotAssignedCtrMaterial();
        material.SetMaterialInfo((result as TrackingCollectionElementBase).MaterialInfoCollection.MaterialInfos.FirstOrDefault());
      }

      return result;
    }

    /// <summary>
    ///   Process tracking instruction not indicated exactly in switch statement
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="material"></param>
    /// <param name="signal"></param>
    /// <param name="result"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessDefaultTrackingInstructionAsync(TrackingInstruction instruction, CtrMaterialBase material, DcTrackingPointSignal signal, ITrackingInstructionDataContractBase result)
    {
      await Task.CompletedTask;

      TrackingInstructionRequest request = new TrackingInstructionRequest(signal.OperationDate,
      instruction.TrackingInstructionType, signal.Value, instruction.AreaAssetCode, instruction.PointAssetCode,
      result);

      result = StorageProvider.TrackingAreas[instruction.AreaAssetCode].ProcessInstruction(request);

      if (material != null && result is TrackingCollectionElementAbstractBase element && material.MaterialInfo.MaterialId == 0)
      {
        material.MaterialInfo.ChangeMaterialId(element
          .MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId);
      }

      return result;
    }

    protected virtual Task PrintStateAsync()
    {
      try
      {
        string returnValue = "\n";
        List<string> trackingPointIds = StorageProvider.Materials
          .Values
          .First()
          .TrackingPoints
          .OrderBy(x => x.Sequence)
          .Select(y => y.AssetCode.ToString())
          .ToList();
        returnValue += "".PadLeft(10);

        var maxIndex = trackingPointIds.Max(x => x.Length);

        for (int i = 0; i < maxIndex; i++)
        {
          for (int j = 0; j < trackingPointIds.Count; j++)
          {
            var trackingPoint = trackingPointIds[j];
            returnValue += trackingPoint.Length <= i ? "".PadLeft(1) : trackingPoint[i].ToString().PadLeft(1);
          }
          returnValue += "\n";
          returnValue += "".PadLeft(10);
        }

        returnValue += "\n";
        //returnValue += "".PadLeft(10);

        List<CtrMaterialBase> workflowDatas =
          StorageProvider.Materials.Values.Where(x => x.MaterialInfo.MaterialId > 0).ToList();
        for (int i = 0; i < workflowDatas.Count; i++)
        {
          returnValue += workflowDatas[i].MaterialInfo?.MaterialId == null
            ? "".PadLeft(10)
            : workflowDatas[i].MaterialInfo.MaterialId.ToString().PadLeft(10);
          List<TrackingPoint> trackingPoints = workflowDatas[i].TrackingPoints.OrderBy(x => x.Sequence).ToList();

          foreach (TrackingPoint point in trackingPoints)
          {
            if (point.HeadReceived && point.TailReceived)
            {
              returnValue += "=".PadLeft(1);
            }
            else if (point.HeadReceived)
            {
              returnValue += "-".PadLeft(1);
            }
            else if (point.TailReceived)
            {
              returnValue += "_".PadLeft(1);
            }
            else
            {
              returnValue += " ".PadLeft(1);
            }
          }

          returnValue += "\n";
          returnValue += "\n";
          //TrackingHelper.LogWorkflowDataInformation($"Workflow print log of workflow number {i}", _workflowDatas[i]);
        }

        returnValue += "\n";
        NotificationController.Info(returnValue);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }

      return Task.CompletedTask;
    }

    /// <summary>
    ///   Get reject location by tracking area asset code
    /// </summary>
    /// <param name="trackingAreaAssetCode"></param>
    /// <returns>Reject loaction enum defined for tracking area</returns>
    protected virtual RejectLocation GetRejectLocationByAreaAssetCode(int trackingAreaAssetCode)
    {
      try
      {
        return trackingAreaAssetCode switch
        {
          var value when value == TrackingArea.FCE_AREA => RejectLocation.AfterFurnace,
          _ => throw new ArgumentOutOfRangeException($"Cannot find reject location for area {trackingAreaAssetCode}."),
        };
      }
      catch (Exception e)
      {
        NotificationController.LogException(e, $"Something went wrong while {MethodHelper.GetMethodName()} in area {trackingAreaAssetCode}.");
      }

      return null;
    }

    /// <summary>
    ///   Get tracking area by reject location
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Tracking area enum defined for reject location</returns>
    protected virtual TrackingArea GetTrackingAreaByRejectLocation(RejectLocation location)
    {
      try
      {
        return (short)location switch
        {
          var value when value == RejectLocation.AfterFurnace => TrackingArea.FCE_AREA,
          _ => throw new ArgumentOutOfRangeException($"Cannot find tracking area for location {location}."),
        };
      }
      catch (Exception e)
      {
        NotificationController.LogException(e, $"Something went wrong while {MethodHelper.GetMethodName()} for location {location}.");
      }

      return null;
    }

    /// <summary>
    ///   Remove material from all areas, including CTR
    /// </summary>
    /// <param name="materialId"></param>
    /// <param name="trackingAreasToExclude"></param>
    /// <returns></returns>
    protected virtual void RemoveMaterialFromAllAreas(long materialId, DateTime operationDate, params TrackingArea[] trackingAreasToExclude)
    {
      CtrMaterialProvider.RemoveCtrMaterial(materialId);

      var trackingAreasWithExistingMaterial = StorageProvider.TrackingAreas.Values
        .Where(x => x is TrackingCollectionAreaBase && ((TrackingCollectionAreaBase)x).GetMaterialIds()
          .Contains(materialId))
        .ToList();

      foreach (var trackingArea in trackingAreasWithExistingMaterial)
      {
        try
        {
          if (trackingAreasToExclude.Contains((TrackingArea)trackingArea.AreaAssetCode))
          {
            NotificationController.Warn($"Material: {materialId} wasn't successfully removed from area: {trackingArea.AreaAssetCode} due to its exclude");
            continue;
          }

          (trackingArea as TrackingCollectionAreaBase)?.RemoveMaterialFromCollection(materialId, operationDate);
          NotificationController.Warn($"Material: {materialId} was successfully removed from area: {trackingArea.AreaAssetCode}");
        }
        catch (Exception e)
        {
          NotificationController.LogException(e, $"Something went wrong while remove material: {materialId} from area: {trackingArea.AreaAssetCode}");
        }
      }
    }

    /// <summary>
    ///   Find material info for reject form provided area
    /// </summary>
    /// <param name="areaAssetCode"></param>
    /// <returns>Material info</returns>
    protected virtual MaterialInfoBase GetMaterialForReject(int areaAssetCode)
    {
      try
      {
        return areaAssetCode switch
        {
          var value when value == TrackingArea.FCE_AREA => StorageProvider.Furnace.GetLastElement().MaterialInfoCollection.MaterialInfos.FirstOrDefault(),
          _ => throw new ArgumentOutOfRangeException($"Cannot find material for reject in area {areaAssetCode}."),
        };
      }
      catch (Exception e)
      {
        NotificationController.LogException(e, $"Something went wrong while {MethodHelper.GetMethodName()} in area {areaAssetCode}.");
      }

      return null;
    }

    /// <summary>
    ///   Process charge to layer tracking instruction that charges materials from layer to rake
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="result"></param>
    /// <param name="ctx"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessChargeToLayerInstructionAsync(PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      if (result is not TrackingCollectionElementAbstractBase element)
      {
        return new TrackingBreakInstructionDataContractBase();
      }

      var materials = element.MaterialInfoCollection.MaterialInfos;

      if (materials.Any())
      {
        if (StorageProvider.TrackingAreas[instruction.AreaAssetCode] is not Layer layer)
        {
          throw new InvalidOperationException($"AssetCode {TrackingArea.LAYER_AREA} is not Layer!");
        }

        var checkLayers = layer.GetLayers();
        var availableLayer = layer.GetLayers()
          .FirstOrDefault(x => x.IsForming);
        if (availableLayer is null)
        {
          availableLayer = layer.GetLayers()
            .OrderByDescending(x => x.PositionOrder)
            .First(x => !x.IsFormed);
        }

        var materialList = await TrackingHandler.GetRawMaterialsWithLayerByIds(ctx, materials.Select(x => x.MaterialId).ToList(), availableLayer.Id);
        var layerMaterial = materialList.First(x => x.EnumRawMaterialType == RawMaterialType.Layer);

        foreach (var item in materialList)
        {
          if (item.EnumRawMaterialType == RawMaterialType.Layer)
          {
            continue;
          }

          var rawMaterial = await TrackingHandler.GetRawMaterialById(ctx, item.RawMaterialId);
          var layerRelation = new TRKLayerRawMaterialRelation()
          {
            ParentLayerRawMaterialId = layerMaterial.RawMaterialId,
            ChildLayerRawMaterialId = item.RawMaterialId,
            IsActualRelation = true
          };

          ctx.TRKLayerRawMaterialRelations.Add(layerRelation);
          rawMaterial.EnumRawMaterialStatus = RawMaterialStatus.Rolled;
        }

        if (layerMaterial.EnumLayerStatus != LayerStatus.IsForming)
        {
          await TrackingHandler.ChangeLayerStatus(ctx, layerMaterial.RawMaterialId, LayerStatus.IsForming);
        }

        TaskHelper.FireAndForget(() =>
            TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(
              new DCShiftCalendarId() { ShiftCalendarId = materialList.First(x => x.EnumRawMaterialType != RawMaterialType.Layer).FKShiftCalendarId }));

        await ctx.SaveChangesAsync();

        layer.AssignMaterialsSumByRawMaterialId(layerMaterial.RawMaterialId,
          (short)ctx.TRKLayerRawMaterialRelations.Count(x => x.ParentLayerRawMaterialId == layerMaterial.RawMaterialId));
      }

      return result;
    }

    /// <summary>
    ///   Process layer form finished tracking instruction that closes layer
    /// </summary>
    /// <param name="result"></param>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessLayerFormFinishedInstructionAsync(PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result)
    {
      await ProcessLayerFormFinishedEventAsync(instruction.AreaAssetCode, null, ctx);

      return result;
    }

    /// <summary>
    ///   Process layer transfer tracking instruction that moves layer forward to bar handling area
    /// </summary>
    /// <param name="result"></param>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="signal"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessLayerTransferredInstructionAsync(PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result, DcTrackingPointSignal signal)
    {
      return await ProcessLayerTransferredEventAsync(signal.OperationDate, instruction.AreaAssetCode, null, ctx);
    }

    /// <summary>
    ///   Process remove ctr material from grey area
    /// </summary>
    /// <param name="result"></param>
    /// <param name="instruction"></param>
    /// <param name="signal"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessRemoveFromGreyInstructionAsync(TrackingInstruction instruction, ITrackingInstructionDataContractBase result, DcTrackingPointSignal signal)
    {
      await Task.CompletedTask;

      if (result is TrackingCollectionElementAbstractBase element)
      {
        StorageProvider.CtrGreyArea.RemoveMaterialFromCollection(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId, signal.OperationDate);
        return element;
      }

      return result;
    }

    /// <summary>
    ///   Process create new collection element by material info
    /// </summary>
    /// <param name="result"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessCreateCollectionElementInstructionAsync(ITrackingInstructionDataContractBase result)
    {
      await Task.CompletedTask;

      if (result is MaterialInfoBase material)
      {
        return new TrackingCollectionElementBase(material);
      }

      return new TrackingInstructionDataContractBase();
    }

    /// <summary>
    ///   Process get last visited material by point asset code
    /// </summary>
    /// <param name="instruction"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    protected virtual async Task<ITrackingInstructionDataContractBase> ProcessGetLastVisitedMaterialByPointAssetCodeInstructionAsync(TrackingInstruction instruction)
    {
      await Task.CompletedTask;

      var material = CtrMaterialProvider.GetLastVisitedMaterialByPointAssetCode(instruction.PointAssetCode.Value);

      return material.MaterialInfo;
    }

    protected virtual async Task PrintLabelAsync(PEContext ctx, long l3MaterialId)
    {
      var rawMaterial = await ctx.TRKRawMaterials.FirstOrDefaultAsync(m => m.FKMaterialId != null && m.FKMaterialId.Value == l3MaterialId);

      if (LabelTemplateCode is null)
      {
        NotificationController.Warn($"Zebra printer template code not found.");
        return;
      }

      if (rawMaterial?.FKProductId == null)
      {
        NotificationController.Error($"Product for material {l3MaterialId} not found. Cannot print label.");

        NotificationController.RegisterAlarm(AlarmDefsBase.ProductForLabelNotFound,
            $"{MethodHelper.GetMethodName()} Product for material {l3MaterialId} not found. Cannot generate label.", l3MaterialId);

        return;
      }

      var request = new DCZebraRequest
      {
        Id = rawMaterial.FKProductId.Value,
        ZebraTemplateCode = LabelTemplateCode
      };

      try
      {
        var _ = await TrackingLabelPrinterSendOffice.SendLabelPrintRequest(request);
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Unexpected error while printing label for material: {l3MaterialId}.");
        NotificationController.LogException(ex, $"Unexpected error while printing label for material: {l3MaterialId}.");
      }
    }
    #endregion

    #region public methods

    /// <summary>
    ///   Process reject tracking instruction
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="result"></param>
    /// <param name="signal"></param>
    /// <returns>ITrackingInstructionDataContractBase</returns>
    public virtual async Task<ITrackingInstructionDataContractBase> ProcessRejectTrackingInstructionAsync(PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result, DcTrackingPointSignal signal)
    {
      var material = GetMaterialForReject(instruction.AreaAssetCode);
      await RejectRawMaterialAsync(new DCRejectMaterialData
      {
        RawMaterialId = material.MaterialId,
        RejectLocation = GetRejectLocationByAreaAssetCode(instruction.AreaAssetCode),
        OutputPieces = 1,

      }, ctx, signal);

      return result;
    }

    /// <summary>
    ///   Reject raw material from tracking
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ctx"></param>
    /// <param name="signal"></param>
    /// <returns></returns>
    public virtual async Task<DataContractBase> RejectRawMaterialAsync(DCRejectMaterialData message, PEContext ctx = null, DcTrackingPointSignal signal = null)
    {
      DataContractBase result = new DataContractBase();
      bool scrapCleared = false;
      var now = DateTime.Now.ExcludeMiliseconds();

      if (signal?.OperationDate is not null)
      {
        now = signal.OperationDate;
      }

      try
      {
        if (message.RejectLocation != RejectLocation.None)
        {
          RemoveMaterialFromAllAreas(message.RawMaterialId, now);
        }

        TRKRawMaterial rawMaterial =
          await TrackingHandler.RejectRawMaterialAsync(ctx, message.RawMaterialId, message.RejectLocation, message.OutputPieces);

        scrapCleared = rawMaterial.ScrapPercent > 0.0;

        //NotificationController.RegisterAlarm(ModuleAlarmDefs.AlarmCode_MaterialManualOperationByUser, "",
        //  rawMaterial.RawMaterialName,
        //  message.HmiInitiator == null
        //    ? "UNKNOWN USER"
        //    : $"{message.HmiInitiator.UserId}({message.HmiInitiator.IpAddress})",
        //  "RejectRawMaterial",
        //  "InProcess");

        if (rawMaterial.EnumRawMaterialStatus == RawMaterialStatus.Rejected)
        {
          TaskHelper.FireAndForget(() =>
              TrackingProcessMaterialEventSendOffice.AddMillEvent(new DCMillEvent
              {
                EventType = ChildMillEventType.Reject,
                RawMaterialId = message.RawMaterialId,
                UserId = message.HmiInitiator?.UserId,
                AssetId = GetTrackingAreaByRejectLocation(message.RejectLocation),
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
              }).GetAwaiter().GetResult()
            , $"Something went wrong while AddMillEvent from RejectRawMaterial");
        }

        //NotificationController.RegisterAlarm(ModuleAlarmDefs.AlarmCode_MaterialManualOperationByUser, "",
        //  materialName,
        //  message.HmiInitiator == null
        //    ? "UNKNOWN USER"
        //    : $"{message.HmiInitiator.UserId}({message.HmiInitiator.IpAddress})",
        //  "RejectRawMaterial",
        //  "Success");

        //TaskHelper.FireAndForget(() =>
        //{
        //  PRMWorkOrder workOrder = TrackingRawMaterialHandler.GetWorkOrderByRawMaterialId(message.RawMaterialId)
        //    .GetAwaiter().GetResult();

        //  //if (workOrder != null)
        //  //  CheckIsLastRolledBillet(workOrder.WorkOrderId, message.RawMaterialId);
        //});


        TaskHelper.FireAndForget(() =>
          TrackingProcessMaterialEventSendOffice.CheckShiftsWorkOrderStatusses(
            new DCShiftCalendarId() { ShiftCalendarId = rawMaterial.FKShiftCalendarId }));

        if (scrapCleared)
        {
          //NotificationController.RegisterAlarm(); Scrap was removed
          //HmiRefresh(HMIRefreshKeys.RawMaterialDetails);
        }
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecodrNotUniqueWhileRejectingRawMaterial,
          $"Unique key violation while rejecting raw material [{message.RawMaterialId}].");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolationDuringRawMaterialReject,
          $"Existing reference key violation while rejecting raw material [{message.RawMaterialId}].");
      }
      catch (InternalModuleException ex)
      {
        NotificationController.RegisterAlarm(ex.AlarmCode, ex.Message, ex.AlarmParams);
        NotificationController.LogException(ex, ex.Message);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedErrorDuringRawMaterialReject,
          $"Unexpected error while rejecting raw material [{message.RawMaterialId}].");
      }

      return result;
    }

    /// <summary>
    ///   Process EnterTableET event
    /// </summary>
    /// <param name="queueOperation"></param>
    /// <param name="slittingFactor"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    public async Task ProcessEnterTableEventAsync(DcQueueOperation queueOperation, short slittingFactor,
      int retryCount = 0)
    {
      try
      {
        /*
        switch (queueOperation.QueueOperationType)
        {
          case QueueOperationEnum.Charge:
          {
            var lastRollingMaterial = StorageProvider.Materials.Values
              .Where(x => x.StartDate.HasValue && x.MaterialInfo.MaterialId > 0)
              .OrderBy(x => x.StartDate)
              .FirstOrDefault();

            if (lastRollingMaterial == null)
              throw new ArgumentException("There was no rolling material before");

            TrackingCollectionElementBase trackingQueueElement = new TrackingCollectionElementBase();
            var rawMaterial = await TrackingHandler.CreateRawMaterial(
              StorageProvider.AssetsDictionary[StorageProvider.EnterTable.AreaAssetCode],
              queueOperation.OperationDate, false, false, lastRollingMaterial.MaterialInfo.MaterialId);

            var materialInfo = new CbMaterialInfo(rawMaterial.RawMaterialId);
            materialInfo.ChangeSlittingFactor(slittingFactor);
            trackingQueueElement.MaterialInfoCollection.Add(materialInfo);

            StorageProvider.EnterTable.ChargeElement(trackingQueueElement, queueOperation.OperationDate);
            break;
          }
          default:
            throw new ArgumentOutOfRangeException("EnterTable area has only Charge operation");
        }*/
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "There is a problem while processing EnterTable event");

        if (retryCount > 0)
        {
          await ProcessEnterTableEventAsync(queueOperation, slittingFactor, retryCount - 1);
        }
      }

      await Task.CompletedTask;
    }

    /// <summary>
    ///   ProcessLayerTransferredEventAsync
    /// </summary>
    /// <returns></returns>
    public virtual async Task<ITrackingInstructionDataContractBase> ProcessLayerTransferredEventAsync(DateTime operationDate, int layerAssetCode, long? layerId = null, PEContext ctx = null)
    {
      ITrackingInstructionDataContractBase result = null;
      try
      {
        if (StorageProvider.TrackingAreas[layerAssetCode] is not Layer layer)
          throw new InternalModuleException($"Area {layerAssetCode} is not layer.",
            AlarmDefsBase.AssetIsNotLayer, layerAssetCode);

        if (layerId.HasValue)
        {
          result = layer.DischargePositionByLayerId(layerId.Value);
        }
        else
        {
          var dischargedLayer = layer.DischargeElement(operationDate);

          if (dischargedLayer != null && dischargedLayer is TrackingCollectionElementAbstractBase element)
          {
            layerId = element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId;
          }
          else
            throw new InternalModuleException($"Area {layerAssetCode} is not layer.",
              AlarmDefsBase.NothingDischargedFromLayer, layerAssetCode);

          result = dischargedLayer;
        }

        if (ctx is not null)
        {
          await TrackingHandler.ChangeLayerStatus(ctx, layerId.Value, LayerStatus.Transferred);
          layer.MoveForward(operationDate);
          await CreateNewLayer(ctx, operationDate, layerAssetCode);
        }
        else
        {
          await using var context = new PEContext();
          await TrackingHandler.ChangeLayerStatus(context, layerId.Value, LayerStatus.Transferred);
          layer.MoveForward(operationDate);
          await CreateNewLayer(context, operationDate, layerAssetCode);
        }

        layer.SetHasChanged(true);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while transferring layer with code {layerAssetCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while transferring layer with code {layerAssetCode}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
          $"Unexpected error while transferring layer with code {layerAssetCode}.");
      }

      return result;
    }

    /// <summary>
    ///   ProcessLayerFormFinishedEventAsync
    /// </summary>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    public async Task ProcessLayerFormFinishedEventAsync(int layerAssetCode, long? layerId = null, PEContext ctx = null)
    {
      try
      {
        if (StorageProvider.TrackingAreas[layerAssetCode] is not Layer layer)
          throw new InternalModuleException($"Area {layerAssetCode} is not layer.",
            AlarmDefsBase.AssetIsNotLayer, layerAssetCode);

        if (ctx is not null)
        {
          if (!layerId.HasValue)
          {
            var layerIds = layer.GetMaterialIds();

            await TrackingHandler.ChangeLayerStatusByCriteria(ctx, LayerStatus.IsFormed, layerIds, LayerStatus.IsForming);
          }
          else
          {
            await TrackingHandler.ChangeLayerStatus(ctx, layerId.Value, LayerStatus.IsFormed);
          }
        }
        else
        {
          await using var context = new PEContext();
          if (!layerId.HasValue)
          {
            var layerIds = layer.GetMaterialIds();

            await TrackingHandler.ChangeLayerStatusByCriteria(context, LayerStatus.IsFormed, layerIds, LayerStatus.IsForming);
          }
          else
            await TrackingHandler.ChangeLayerStatus(context, layerId.Value, LayerStatus.IsFormed);
        }

        layer.SetHasChanged(true);
      }
      catch (DbUpdateException ex) when (ex.IsDuplicateKeyViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.RecordNotUnique,
          $"Unique key violation while finishing layer with code {layerAssetCode}.");
      }
      catch (DbUpdateException ex) when (ex.IsExistingReferenceViolation())
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.ExistingReferenceViolation,
          $"Existing reference key violation while finishing layer with code {layerAssetCode}.");
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
          $"Unexpected error while finishing layer with code {layerAssetCode}.");
      }
    }

    public virtual void ReceiveTrackingPointSignals()
    {
      try
      {
        var result = TrackingL1AdapterSendOfficeBase.ResendTrackingPointSignals(new DataContractBase()).GetAwaiter().GetResult();

        if (result.OperationSuccess)
          NotificationController.Warn("Successfully received tracking point signals from L1A");
        else
          NotificationController.Error("Something went wrong while receive tracking point signals from L1A");
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }
    }
    #endregion
  }
}
