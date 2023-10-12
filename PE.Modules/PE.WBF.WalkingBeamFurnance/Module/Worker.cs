using PE.BaseInterfaces.Managers.WBF;
using PE.WBF.Base.Module;

namespace PE.WBF.WalkingBeamFurnance.Module
{
  public class Worker : WorkerBase
  {
    #region ctor

    public Worker(IFurnaceProcessManagerBase furnaceProcessManager) : base(furnaceProcessManager)
    {
    }

    #endregion

    
  }
}
