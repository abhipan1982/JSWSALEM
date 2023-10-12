using PE.Interfaces.Modules;
using PE.STP.Base.Module.Communication;

namespace PE.STP.Setup.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<ISetup>, ISetup
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
