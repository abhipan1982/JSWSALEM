using PE.Interfaces.Modules;
using PE.QEX.Base.Module.Communication;

namespace PE.QEX.QualityExpert.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IQualityExpert>, IQualityExpert
  {
    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
