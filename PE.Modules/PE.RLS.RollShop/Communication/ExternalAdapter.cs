using PE.Interfaces.Modules;
using PE.RLS.Base.Module.Communication;

namespace PE.RLS.RollShop.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IRollShop>, IRollShop
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
