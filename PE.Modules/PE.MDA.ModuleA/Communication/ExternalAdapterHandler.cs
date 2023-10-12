using PE.MDA.Base.Interfaces;
using PE.MDA.Base.Module.Communication;

namespace PE.MDA.ModuleA.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IHelloManagerBase helloManagerBase) : base(helloManagerBase)
    {
    }
  }
}
