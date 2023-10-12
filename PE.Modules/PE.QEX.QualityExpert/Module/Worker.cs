using PE.BaseInterfaces.Managers.QEX;
using PE.QEX.Base.Module;

namespace PE.QEX.QualityExpert.Module
{
  public class Worker : WorkerBase
  {
    public Worker(IQualityExpertBaseManager qualityExpertManager) : base(qualityExpertManager)
    {
      
    }
  }
}
