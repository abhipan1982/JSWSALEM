using PE.Interfaces.Modules;
using PE.YRD.Base.Module.Communication;

namespace PE.YRD.Yards.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IYards>, IYards
  {
    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
