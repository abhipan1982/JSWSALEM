using PE.Interfaces.Modules;
using PE.MDA.Base.Module.Communication;

namespace PE.MDA.ModuleA.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IModuleA>, IModuleA
  {
    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }
  }
}
