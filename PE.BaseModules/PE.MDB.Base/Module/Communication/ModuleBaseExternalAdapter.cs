using PE.Interfaces.Modules;
using SMF.Module.Core;

namespace PE.MDB.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, IModuleB where T : class, IModuleB
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }
  }
}
