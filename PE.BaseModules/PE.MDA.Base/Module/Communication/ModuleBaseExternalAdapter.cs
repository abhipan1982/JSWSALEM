using PE.Interfaces.Modules;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;
using SMF.Module.Core;

namespace PE.MDA.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, IModuleA where T : class, IModuleA
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }

    public Task<DCAckMessage> Hello(DCHelloMessage message)
    {
      return HandleIncommingMethod(Handler.Hello, message);
    }
  }
}
