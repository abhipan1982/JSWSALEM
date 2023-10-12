namespace PE.L1A.L1Adapter.Communication
{
  public class ExternalAdapterHandler
  {
    #region ctor

    public ExternalAdapterHandler(IL1SetupManager l1SetupManager, IL1SignalManager l1SignalManager,
      ITrackingManager trackingManager, IDispatcherManager dispatcherManager)
    {
      _level1SetupManager = l1SetupManager;
      _level1TrackingManager = l1SignalManager;
      _trackingManager = trackingManager;
      _dispatcherManager = dispatcherManager;
    }

    #endregion

    internal Task<DataContractBase> SendSetupDataRequestToL1AdapterAsync(DCCommonSetupStructure message)
    {
      // return result
      return _level1SetupManager.RequestSetupFromL1Async(message);
    }

    internal Task<DataContractBase> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure message)
    {
      // return result
      return _level1SetupManager.SendSetupToL1Async(message);
    }

    internal async Task<DataContractBase> ProcessL1TrackingVisualizationOperation(DCTrackingVisualizationAction message)
    {
      await _trackingManager.ProcessL1TrackingVisualizationOperation(message);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessQueueMoveForward(DCUpdateArea dc)
    {
      await _trackingManager.ProcessQueueMoveForward(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessQueueMoveBackward(DCUpdateArea dc)
    {
      await _trackingManager.ProcessQueueMoveBackward(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessFurnaceUnCharge(DataContractBase dc)
    {
      await _trackingManager.ProcessFurnaceUnCharge(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessFurnaceUnDischargeFromStorage(DataContractBase dc)
    {
      await _trackingManager.ProcessFurnaceUnDischargeFromStorage(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessFurnaceCharge(DataContractBase dc)
    {
      await _trackingManager.ProcessFurnaceCharge(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessFurnaceUnDischargeFromReformingArea(DataContractBase dc)
    {
      await _trackingManager.ProcessFurnaceUnDischargeFromReformingArea(dc);

      return new DataContractBase();
    }

    internal Task<DataContractBase> ProcessFurnaceDischargeToStorage(DataContractBase dc)
    {
      return _trackingManager.ProcessFurnaceDischargeToStorage(dc);
    }

    internal async Task<DataContractBase> ProcessRemoveMaterial(DCRemoveMaterial dc)
    {
      await _trackingManager.ProcessRemoveMaterial(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> ProcessReplaceMaterialPosition(DCMoveMaterial dc)
    {
      await _trackingManager.ProcessReplaceMaterialPosition(dc);

      return new DataContractBase();
    }

    internal Task<DataContractBase> ProcessFurnaceDischargeForReject(DataContractBase dc)
    {
      return _trackingManager.ProcessFurnaceDischargeForReject(dc);
    }

    internal async Task<DataContractBase> ProcessChargeWOOnFurnaceLastPosition(DataContractBase dc)
    {
      await _trackingManager.ProcessChargeWOOnFurnaceLastPosition(dc);

      return new DataContractBase();
    }

    internal async Task<DataContractBase> TransferLayer(DataContractBase dc)
    {
      await _dispatcherManager.ProcessManualLayerTransferAsync(dc);

      return new DataContractBase();
    }

    internal Task<DataContractBase> ProcessRejectRawMaterial(DCRejectMaterialData dc)
    {
      return _trackingManager.ProcessRejectRawMaterial(dc);
    }

    #region members

    private readonly IL1SetupManager _level1SetupManager;
    private readonly IL1SignalManager _level1TrackingManager;
    private readonly ITrackingManager _trackingManager;
    private readonly IDispatcherManager _dispatcherManager;

    #endregion
  }
}
