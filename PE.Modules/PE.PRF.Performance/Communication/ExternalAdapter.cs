using PE.Interfaces.Modules;
using PE.PRF.Base.Module.Communication;

namespace PE.PRF.Performance.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IPerformance>, IPerformance
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
