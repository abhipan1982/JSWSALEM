using PE.Interfaces.Modules;
using PE.PPL.Base.Module.Communication;

namespace PE.PPL.ProdPlaning.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IProdPlaning>, IProdPlaning
  {
    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
