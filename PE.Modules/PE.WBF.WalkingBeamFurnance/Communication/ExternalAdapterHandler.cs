using PE.BaseInterfaces.Managers.WBF;
using PE.WBF.Base.Module.Communication;

namespace PE.WBF.WalkingBeamFurnance.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IFurnaceProcessManagerBase furnaceProcessManager,
      IFurnaceStateManagerBase furnaceStateManager) : base(furnaceProcessManager, furnaceStateManager)
    {
    }
  }
}
