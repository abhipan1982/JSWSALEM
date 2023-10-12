using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.PEContext;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Managers;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.TRK.Base.Providers.Concrete
{

  public class TrackingMaterialCutProviderBase : ITrackingMaterialCutProviderBase
  {
    protected readonly TrackingHandlerBase TrackingHandler;
    protected readonly ITrackingCtrMaterialProviderBase CtrMaterialProvider;
    protected readonly ITrackingStorageProviderBase StorageProvider;
    protected readonly ITrackingProcessMeasurementsSendOffice TrackingProcessMeasurementsSendOffice;

    public TrackingMaterialCutProviderBase(ITrackingCtrMaterialProviderBase ctrMaterialProvider,
      TrackingHandlerBase trackingHandler,
      ITrackingStorageProviderBase storageProvider, 
      ITrackingProcessMeasurementsSendOffice trackingProcessMeasurementsSendOffice)
    {
      CtrMaterialProvider = ctrMaterialProvider;
      TrackingHandler = trackingHandler;
      StorageProvider = storageProvider;
      TrackingProcessMeasurementsSendOffice = trackingProcessMeasurementsSendOffice;
    }

    /// <summary>
    /// ProcessMaterialCutAsync
    /// </summary>
    /// <param name="signal"></param>
    /// <param name="ctx"></param>
    /// <param name="instruction"></param>
    /// <param name="result"></param>
    /// <param name="typeOfCut"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual async Task<ITrackingInstructionDataContractBase> ProcessMaterialCutAsync(DcTrackingPointSignal signal, PEContext ctx, TrackingInstruction instruction, ITrackingInstructionDataContractBase result, TypeOfCut typeOfCut)
    {
      try
      {
        if (instruction.PointAssetCode is null)
          NotificationController.Error($"For Operation {MethodHelper.GetMethodName()} {nameof(instruction.PointAssetCode)} cannot be null");
        else
        {
          var material = CtrMaterialProvider.GetMaterialByPlaceOccupied(instruction.PointAssetCode.Value);

          if (material is null)
            NotificationController.Error($"For Operation {MethodHelper.GetMethodName()} {nameof(instruction.PointAssetCode)}: {instruction.PointAssetCode} cannot find material");
          else
          {
            var point = material.TrackingPoints.First(x => x.AssetCode == instruction.PointAssetCode.Value);

            if (point is not ShearTrackingPoint shearTrackingPoint)
              NotificationController.Error($"For Operation {MethodHelper.GetMethodName()} {nameof(instruction.PointAssetCode)}: {instruction.PointAssetCode} must be of type {nameof(ShearTrackingPoint)}");
            else
            {
              switch (typeOfCut)
              {
                case var value when value == TypeOfCut.ManualChopping:
                  await ProcessManualChopping(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.AutoChopping:
                  await ProcessAutoChopping(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.HeadCut:
                  await ProcessHeadCut(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.TailCut:
                  await ProcessTailCut(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.EmergencyCut:
                  await ProcessEmergencyCut(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.SampleCut:
                  await ProcessSampleCut(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                case var value when value == TypeOfCut.DivideCut:
                  await ProcessDivideCut(signal, material, shearTrackingPoint, ctx, instruction, typeOfCut);
                  break;
                default:
                  break;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while {MethodHelper.GetMethodName()}" +
          $"For point {instruction.PointAssetCode} with TypeOfCut {typeOfCut.Name}");
        NotificationController.RegisterAlarm(AlarmDefsBase.MaterialCutFailed,
          $"Something went wrong while {MethodHelper.GetMethodName()} For point {instruction.PointAssetCode} with TypeOfCut {typeOfCut.Name}");
      }

      return new TrackingInstructionDataContractBase();
    }

    #region DivideCut
    protected virtual async Task ProcessDivideCut(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleDivideCut(signal, point, material, ctx, typeOfCut);
          break;
        default:
          break;
      }
    }

    protected virtual async Task HandleDivideCut(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      //Not available in current version of standard
      //var divideCutLengthFeatureCode = await GetDivideCutLengthFeatureCode(ctx, point.AssetCode);
      //var cutLengthMeasurementResult = await TrackingProcessMeasurementsSendOffice.GetMeasurementValueAsync(new DcMeasurementRequest(divideCutLengthFeatureCode, signal.TimeStamp, signal.TimeStamp));

      double cutLength = 0d;

      //if (cutLengthMeasurementResult.OperationSuccess)
      //{
      //  cutLength = cutLengthMeasurementResult.DataConctract.Avg ?? 0d;
      //}
      //else
      //{
      //  NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get cut length measurement");
      //}

      await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);

    }

    protected virtual async Task<int> GetDivideCutLengthFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringHeadCutLength);

      return feature.FeatureCode;
    }
    #endregion

    #region HeadCut
    protected virtual async Task ProcessHeadCut(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleHeadCut(signal, point, material, ctx, typeOfCut);
          break;
        default:
          break;
      }
    }

    protected virtual async Task HandleHeadCut(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      var headCutLengthFeatureCode = await GetHeadCutLengthFeatureCode(ctx, point.AssetCode);
      var cutLengthMeasurementResult = await TrackingProcessMeasurementsSendOffice.GetMeasurementValueAsync(new DcMeasurementRequest(headCutLengthFeatureCode, signal.OperationDate, signal.OperationDate));

      double cutLength = 0d;

      if (cutLengthMeasurementResult.OperationSuccess)
      {
        cutLength = cutLengthMeasurementResult.DataConctract.Avg ?? 0d;
      }
      else
      {
        NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get cut length measurement");
      }

      await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
    }

    protected virtual async Task<int> GetHeadCutLengthFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringHeadCutLength);

      return feature.FeatureCode;
    }
    #endregion

    #region TailCut
    protected virtual async Task ProcessTailCut(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleTailCut(signal, point, material, ctx, typeOfCut);
          break;
        default:
          break;
      }
    }

    protected virtual async Task HandleTailCut(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      var tailCutLengthFeatureCode = await GetTailCutLengthFeatureCode(ctx, point.AssetCode);
      var cutLengthMeasurementResult = await TrackingProcessMeasurementsSendOffice.GetMeasurementValueAsync(new DcMeasurementRequest(tailCutLengthFeatureCode, signal.OperationDate, signal.OperationDate));

      double cutLength = 0d;

      if (cutLengthMeasurementResult.OperationSuccess)
      {
        cutLength = cutLengthMeasurementResult.DataConctract.Avg ?? 0d;
      }
      else
      {
        NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get cut length measurement");
      }

      await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
    }

    protected virtual async Task<int> GetTailCutLengthFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringTailCutLength);

      return feature.FeatureCode;
    }
    #endregion

    #region EmergencyCut
    protected virtual async Task ProcessEmergencyCut(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleEmergencyCut(signal, point, material, ctx, typeOfCut);
          break;
        default:
          break;
      }
    }

    protected virtual async Task HandleEmergencyCut(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      //Not available in current version of Standard
      //var emergencyCutLengthFeatureCode = await GetEmergencyCutLengthFeatureCode(ctx, point.AssetCode);
      //var cutLengthMeasurementResult = await TrackingProcessMeasurementsSendOffice.GetMeasurementValueAsync(new DcMeasurementRequest(emergencyCutLengthFeatureCode, signal.TimeStamp, signal.TimeStamp));

      double cutLength = 0d;

      //if (cutLengthMeasurementResult.OperationSuccess)
      //{
      //  cutLength = cutLengthMeasurementResult.DataConctract.Avg ?? 0d;
      //}
      //else
      //{
      //  NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get cut length measurement");
      //}

      await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
    }

    protected virtual async Task<int> GetEmergencyCutLengthFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringHeadCutLength);

      return feature.FeatureCode;
    }
    #endregion

    #region SampleCut
    protected virtual async Task ProcessSampleCut(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleSampleCut(signal, point, material, ctx, typeOfCut);
          break;
        default:
          break;
      }
    }

    protected virtual async Task HandleSampleCut(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      //Not available in current version of standard
      //var sampleCutLengthFeatureCode = await GetSampleCutLengthFeatureCode(ctx, point.AssetCode);
      //var cutLengthMeasurementResult = await TrackingProcessMeasurementsSendOffice.GetMeasurementValueAsync(new DcMeasurementRequest(sampleCutLengthFeatureCode, signal.TimeStamp, signal.TimeStamp));

      double cutLength = 0d;

      //if (cutLengthMeasurementResult.OperationSuccess)
      //{
      //  cutLength = cutLengthMeasurementResult.DataConctract.Avg ?? 0d;
      //}
      //else
      //{
      //  NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get cut length measurement");
      //}

      await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
    }

    protected virtual async Task<int> GetSampleCutLengthFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringHeadCutLength);

      return feature.FeatureCode;
    }
    #endregion

    #region ManualChopping
    protected virtual async Task ProcessManualChopping(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleManualChoppingSetToTrue(signal, point);
          break;
        case var value when value == 0:
          await HandleManualChoppingSetToFalse(signal, point, material, ctx, typeOfCut);
          break;
        default:
          throw new ArgumentOutOfRangeException($"Wrong value for {MethodHelper.GetMethodName()}");
      }
    }

    protected virtual Task HandleManualChoppingSetToTrue(DcTrackingPointSignal signal, ShearTrackingPoint point)
    {
      point.ManualChoppingStartDate = signal.OperationDate;

      return Task.CompletedTask;
    }

    protected virtual async Task HandleManualChoppingSetToFalse(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      if (point.ManualChoppingStartDate is null)
      {
        NotificationController.Warn($"{MethodHelper.GetMethodName()} {nameof(point.ManualChoppingStartDate)} is null");
      }
      else
      {
        point.ManualChoppingEndDate = signal.OperationDate;
        var speedFeatureCode = await GetSpeedFeatureCode(ctx, point.AssetCode);
        var speedMeasurementResult = await TrackingProcessMeasurementsSendOffice
          .GetMeasurementValueAsync(
            new DcMeasurementRequest(speedFeatureCode,
              point.ManualChoppingStartDate.Value,
              signal.OperationDate));

        double avgSpeed = 0d;

        if (speedMeasurementResult.OperationSuccess)
        {
          avgSpeed = speedMeasurementResult.DataConctract.Avg ?? 0d;
        }
        else
        {
          NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get speed measurement");
        }

        var cutLength = (signal.OperationDate - point.ManualChoppingStartDate.Value).TotalMilliseconds / 1000 * avgSpeed;

        await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
      }
    }


    #endregion

    #region Auto chopping
    protected virtual async Task ProcessAutoChopping(DcTrackingPointSignal signal, CtrMaterialBase material, ShearTrackingPoint point, PEContext ctx, TrackingInstruction instruction, TypeOfCut typeOfCut)
    {
      switch (signal.Value)
      {
        case var value when value == 1:
          await HandleAutoChoppingSetToTrue(signal, point);
          break;
        case var value when value == 0:
          await HandleAutoChoppingSetToFalse(signal, point, material, ctx, typeOfCut);
          break;
        default:
          throw new ArgumentOutOfRangeException($"Wrong value for {MethodHelper.GetMethodName()}");
      }
    }

    protected virtual Task HandleAutoChoppingSetToTrue(DcTrackingPointSignal signal, ShearTrackingPoint point)
    {
      point.AutoChoppingStartDate = signal.OperationDate;

      return Task.CompletedTask;
    }

    protected virtual async Task HandleAutoChoppingSetToFalse(DcTrackingPointSignal signal, ShearTrackingPoint point, CtrMaterialBase material, PEContext ctx, TypeOfCut typeOfCut)
    {
      if (point.AutoChoppingStartDate is null)
      {
        NotificationController.Warn($"{MethodHelper.GetMethodName()} {nameof(point.AutoChoppingStartDate)} is null");
      }
      else
      {
        point.AutoChoppingEndDate = signal.OperationDate;
        var speedFeatureCode = await GetSpeedFeatureCode(ctx, point.AssetCode);
        var speedMeasurementResult = await TrackingProcessMeasurementsSendOffice
          .GetMeasurementValueAsync(
            new DcMeasurementRequest(speedFeatureCode,
              point.AutoChoppingStartDate.Value,
              signal.OperationDate));

        double avgSpeed = 0d;

        if (speedMeasurementResult.OperationSuccess)
        {
          avgSpeed = speedMeasurementResult.DataConctract.Avg ?? 0d;
        }
        else
        {
          NotificationController.Warn($"{MethodHelper.GetMethodName()} cannot get speed measurement");
        }
        var cutLength = (signal.OperationDate - point.AutoChoppingStartDate.Value).TotalMilliseconds / 1000 * avgSpeed;

        await TrackingHandler.AddMaterialCutAsync(ctx, signal.OperationDate, StorageProvider.AssetsDictionary[point.AssetCode].AssetId, material.MaterialInfo.MaterialId, typeOfCut, cutLength);
      }
    }
    #endregion

    protected virtual async Task<int> GetSpeedFeatureCode(PEContext ctx, int assetCode)
    {
      var assetId = StorageProvider.AssetsDictionary[assetCode].AssetId;
      var feature = await TrackingHandler.GetFeatureByAssetIdAndFeatureType(ctx, assetId, FeatureType.MeasuringSpeed);

      return feature.FeatureCode;
    }
  }
}
