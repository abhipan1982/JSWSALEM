using PE.BaseInterfaces.Managers.EVT;
using PE.EVT.Base.Module;

namespace PE.EVT.Events.Module
{
  public class Worker : WorkerBase
  {
    public Worker(IDelayProcessManagerBase delayProcessManager, 
      IShiftManagerBase shiftManager,
      IMillEventManagerBase millEventManager, 
      IDelayManagerBase delayManager,
      ICrewManagerBase crewManager) 
      : base(delayProcessManager, shiftManager, millEventManager, delayManager, crewManager)
    {
    }
  }
}
