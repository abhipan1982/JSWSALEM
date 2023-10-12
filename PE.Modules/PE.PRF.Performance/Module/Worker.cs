using PE.BaseInterfaces.Managers.PRF;
using PE.PRF.Base.Module;

namespace PE.PRF.Performance.Module
{
  public class Worker : WorkerBase
  {
    public Worker(IKPICalculationBaseManager kPICalculationBaseManager) : base(kPICalculationBaseManager)
    {
    }
  }
}
