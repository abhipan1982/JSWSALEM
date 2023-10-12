using PE.BaseInterfaces.Managers.PRF;
using PE.PRF.Base.Module.Communication;

namespace PE.PRF.Performance.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IKPICalculationBaseManager kpiCalculationBaseManager)
      : base(kpiCalculationBaseManager)
    {
    }
  }
}
