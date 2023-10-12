using System.Collections.Generic;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IDispatcherManager : IManagerBase
  {
    /// <summary>
    ///   Init
    /// </summary>
    /// <returns></returns>
    Task InitAsync();

    /// <summary>
    ///   Stop
    /// </summary>
    /// <returns></returns>
    Task StopAsync();

    /// <summary>
    ///   Material Has Occupied The TrackingPoint
    /// </summary>
    /// <param name="trackingPoint"></param>
    /// <param name="workflowArea"></param>
    /// <param name="placeOccupied"></param>
    /// <returns></returns>
    Task MaterialHasOccupiedTheTrackingPoint(int trackingPoint, int workflowArea, bool placeOccupied);

    /// <summary>
    ///   Process Furnace event
    /// </summary>
    /// <param name="eventTypes"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessFurnaceEventAsync(List<FurnaceOperation> eventTypes, int retryCount = 0);

    /// <summary>
    ///   Process ChargingArea event
    /// </summary>
    /// <param name="eventTypes"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessChargingAreaEventAsync(List<QueueOperation> eventTypes, int retryCount = 0);

    /// <summary>
    ///   Process ReformingArea event
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessReformingAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    ///   Process EnterTableET event
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="slittingFactor"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessEnterTableETEventAsync(SimpleQueueOperation eventType, short slittingFactor, int retryCount = 0);

    /// <summary>
    ///   Process InsulatedCorridorArea event
    /// </summary>
    /// <param name="eventTypes"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessInsulatedCorridorAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    ///   Process GarretArea event
    /// </summary>
    /// <param name="eventTypes"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessGarretAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    ///   Process Storage event
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessStorageOperationAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessConveyorAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessBandingAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessWeighingAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessAuxiliariesAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessTransportAreaEventAsync(SimpleQueueOperation eventType, int retryCount = 0);

    /// <summary>
    ///   ProcessApronChargeEvent
    /// </summary>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessApronChargeEvent(int retryCount = 0);

    /// <summary>
    ///   ProcessRakeCycleEvent
    /// </summary>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessRakeCycleEvent(int retryCount = 0);

    /// <summary>
    ///   ProcessLayerTransferredEventAsync
    /// </summary>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessLayerTransferredEventAsync(int retryCount = 0);

    /// <summary>
    ///   ProcessLayerFormFinishedEventAsync
    /// </summary>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessLayerFormFinishedEventAsync(int retryCount = 0);

    /// <summary>
    ///   ProcessBarsAmountChangedEventAsync
    /// </summary>
    /// <param name="eventArgsBarsAmount"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessBarsAmountChangedEventAsync(short eventArgsBarsAmount, int retryCount = 0);

    /// <summary>
    ///   ProcessManualLayerTransferAsync
    /// </summary>
    /// <param name="dc"></param>
    /// <param name="retryCount"></param>
    /// <returns></returns>
    Task ProcessManualLayerTransferAsync(DataContractBase dc, int retryCount = 0);
  }
}
