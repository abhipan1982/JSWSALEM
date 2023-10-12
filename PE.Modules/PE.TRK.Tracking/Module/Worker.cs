using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Module;
using SMF.Core.Interfaces;

namespace PE.TRK.Tracking.Module
{
  public class Worker : WorkerBase
  {
    public Worker(ITrackingConfigurationManagerBase configurationManager, ITrackingDispatcherManagerBase dispatcherManager, ITrackingManagerBase trackingManager, ITrackingEventHandlingManagerBase trackingEventHandlingManager, IParameterService parameterService) : base(configurationManager, dispatcherManager, trackingManager, trackingEventHandlingManager, parameterService)
    {
    }
  }
}
