using PE.Interfaces.Modules;
using PE.MDB.Base.Module.Communication;

namespace PE.MDB.ModuleB.Communication
{
  internal class ExternalAdapter : ModuleBaseExternalAdapter<IModuleB>, IModuleB
  {
    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }
  }
}
