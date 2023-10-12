using PE.MDA.Base.Interfaces;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;

namespace PE.MDA.Base.Module.Communication
{
  public class ModuleBaseExternalAdapterHandler
  {
    private readonly IHelloManagerBase _helloManagerBase;

    public ModuleBaseExternalAdapterHandler(IHelloManagerBase helloManagerBase)
    {
      _helloManagerBase = helloManagerBase;
    }

    public virtual Task<DCAckMessage> Hello(DCHelloMessage message)
    {
        return _helloManagerBase.ProcessHello(message);
    }
  }
}
