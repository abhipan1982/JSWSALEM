using PE.BaseInterfaces.Managers.STP;
using PE.STP.Base.Module.Communication;

namespace PE.STP.Setup.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(ISetupBaseManager setupManager,
      ISetupCalculationBaseManager setupCalculationManager)
      : base(setupManager, setupCalculationManager)
    {
    }
  }
}
