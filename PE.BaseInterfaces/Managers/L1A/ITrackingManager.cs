using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.AbstractionModels.L1A;
using PE.BaseModels.ConcreteModels.L1A;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface ITrackingManager : IManagerBase
  {
    /// <summary>
    ///   InitAsync
    /// </summary>
    /// <returns></returns>
    Task InitAsync();

    /// <summary>
    ///   StopAsync
    /// </summary>
    /// <returns></returns>
    Task StopAsync(int retryCount = 3);

    /// <summary>
    ///   Method for Sending measurements
    /// </summary>
    /// <param name="trackingPoints"></param>
    /// <returns></returns>
    Task SendMeasurements(long materialId, bool isLastPass, bool isReversed, int passNumber,
      TrackingStepBase trackingStep, List<TrackingPointBase> trackingPoints);

    /// <summary>
    ///   Method for sending tracking events
    /// </summary>
    /// <param name="materialId"></param>
    /// <param name="isLastPass"></param>
    /// <param name="isReversed"></param>
    /// <param name="passNumber"></param>
    /// <returns></returns>
    Task SendTrackingEvent(long? materialId, bool isLastPass, bool isReversed, int passNumber, int assetCode,
      bool isArea, TrackingEventType eventType, DateTime triggerDate, double? length = null, double? weight = null,
      double? temperature = null, bool isRejected = false);

    /// <summary>
    ///   Method for printing tracking line
    /// </summary>
    /// <returns></returns>
    Task PrintStateAsync();

    /// <summary>
    ///   Get material method
    /// </summary>
    /// <returns></returns>
    Task<MaterialInfoBase> GetMaterialInfoAsync(bool checkFurnace);

    /// <summary>
    ///   GetMaterialInfoFromQueueToMillAsync
    /// </summary>
    /// <returns></returns>
    MaterialInfoBase GetMaterialInfoFromQueueToMillAsync();

    /// <summary>
    ///   Preparing measurements and adding it to proper trackingPoint
    /// </summary>
    /// <param name="trackingData"></param>
    void PrepareMeasurements(TrackingDataBase trackingData);

    /// <summary>
    ///   Send materials position to HMI
    /// </summary>
    /// <returns></returns>
    Task SendMaterialPosition(long elapsedMillis);

    Task ProcessQueueMoveForward(DCUpdateArea dc);

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<DataContractBase> ProcessL1TrackingVisualizationOperation(DCTrackingVisualizationAction message);

    Task ProcessQueueMoveBackward(DCUpdateArea dc);
    Task ProcessFurnaceUnCharge(DataContractBase dc);

    /// <summary>
    /// </summary>
    /// <param name="assetCode"></param>
    /// <param name="cutLength"></param>
    /// <param name="typeOfCut"></param>
    /// <returns></returns>
    Task SendCutMessage(int assetCode, double cutLength, TypeOfCut typeOfCut, short slittingFactor = 1);

    /// <summary>
    /// </summary>
    /// <param name="queuePositions"></param>
    /// <returns></returns>
    Task ProcessL1TrackingQueuePositionChange(List<QueuePosition> queuePositions, short assetCode);

    Task ProcessFurnaceCharge(DataContractBase dc);
    Task ProcessFurnaceUnDischargeFromStorage(DataContractBase dc);
    Task ProcessReplaceMaterialPosition(DCMoveMaterial dc);
    Task ProcessRemoveMaterial(DCRemoveMaterial dc);
    Task ProcessFurnaceUnDischargeFromReformingArea(DataContractBase dc);

    Task SendSingleMeasurement(long? materialId, int measurementId, double measurementValue, DateTime measurementDate,
      bool isLastPass, bool isReversed, int passNumber);

    Task<DataContractBase> ProcessFurnaceDischargeToStorage(DataContractBase dc);
    Task<DataContractBase> ProcessFurnaceDischargeForReject(DataContractBase dc);
    Task ProcessChargeWOOnFurnaceLastPosition(DataContractBase dc);
    Task<DataContractBase> ProcessRejectRawMaterial(DCRejectMaterialData dc);
  }
}
