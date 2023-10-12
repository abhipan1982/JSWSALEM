using PE.HMI.Base.Module.Communication;
using PE.Interfaces.Modules;

namespace PE.HMI.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IHmi>, IHmi
  {
    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
