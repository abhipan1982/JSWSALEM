using PE.BaseInterfaces.Managers.QEX;
using PE.QEX.Base.Module.Communication;

namespace PE.QEX.QualityExpert.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IQualityExpertBaseManager qualityExpertBaseManager)
      : base(qualityExpertBaseManager)
    {
    }
  }
}
