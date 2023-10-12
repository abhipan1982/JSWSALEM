using PE.Interfaces.Modules;
using PE.MNT.Base.Module.Communication;

namespace PE.MNT.Maintenance.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IMaintenance>, IMaintenance
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
