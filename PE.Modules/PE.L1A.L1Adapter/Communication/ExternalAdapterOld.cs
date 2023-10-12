using PE.Interfaces.Modules;

namespace PE.L1A.L1Adapter.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, IL1Adapter
  {
    private readonly ExternalAdapterHandler _handler;

    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(typeof(IL1Adapter))
    {
      _handler = handler;
    }

    #endregion

    public Task<DataContractBase> SendSetupDataRequestToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      return HandleIncommingMethod(_handler.SendSetupDataRequestToL1AdapterAsync, dataToSend);
    }

    public Task<DataContractBase> SendSetupDataToL1AdapterAsync(DCCommonSetupStructure dataToSend)
    {
      return HandleIncommingMethod(_handler.SendSetupDataToL1AdapterAsync, dataToSend);
    }

    public Task<DataContractBase> ProcessL1TrackingVisualizationOperation(DCTrackingVisualizationAction message)
    {
      return HandleIncommingMethod(_handler.ProcessL1TrackingVisualizationOperation, message);
    }

    /// <summary>
    ///   Move forward operation
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> QueueMoveForward(DCUpdateArea dc)
    {
      return HandleIncommingMethod(_handler.ProcessQueueMoveForward, dc);
    }

    /// <summary>
    ///   Move backward operation
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> QueueMoveBackward(DCUpdateArea dc)
    {
      return HandleIncommingMethod(_handler.ProcessQueueMoveBackward, dc);
    }

    /// <summary>
    ///   Return operation from furnace to charging area
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceUnCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceUnCharge, dc);
    }

    /// <summary>
    ///   Return operation to furnace from charging area
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceCharge(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceCharge, dc);
    }

    /// <summary>
    ///   Return operation to storage from furnace
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceDischargeToStorage(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceDischargeToStorage, dc);
    }

    /// <summary>
    ///   Return operation to reforming area from furnace
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceDischargeForReject(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceDischargeForReject, dc);
    }

    /// <summary>
    ///   Return operation from storage to furnace
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceUnDischargeFromStorage(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceUnDischargeFromStorage, dc);
    }

    /// <summary>
    ///   Return operation from reforming area to furnace
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> FurnaceUnDischargeFromReformingArea(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessFurnaceUnDischargeFromReformingArea, dc);
    }

    /// <summary>
    ///   Charge WO On Furnace Last Position
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> ChargeWOOnFurnaceLastPosition(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessChargeWOOnFurnaceLastPosition, dc);
    }

    /// <summary>
    ///   Transfer Layer manually
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> TransferLayer(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.TransferLayer, dc);
    }

    /// <summary>
    ///   Removing material from tracking
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> RemoveMaterial(DCRemoveMaterial dc)
    {
      return HandleIncommingMethod(_handler.ProcessRemoveMaterial, dc);
    }

    /// <summary>
    ///   Replacing material position by materialId
    /// </summary>
    /// <param name="dc"></param>
    /// <returns></returns>
    public Task<DataContractBase> ReplaceMaterialPosition(DCMoveMaterial dc)
    {
      return HandleIncommingMethod(_handler.ProcessReplaceMaterialPosition, dc);
    }

    public Task<DataContractBase> RejectRawMaterial(DCRejectMaterialData dc)
    {
      return HandleIncommingMethod(_handler.ProcessRejectRawMaterial, dc);
    }
  }
}
