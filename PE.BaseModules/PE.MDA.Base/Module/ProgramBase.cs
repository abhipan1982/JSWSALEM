using Microsoft.Extensions.DependencyInjection;
using PE.MDA.Base.Interfaces;
using PE.MDA.Base.Managers;
using SMF.Module.Core.Interfaces;

namespace PE.MDA.Base.Module
{
  public class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<IHelloManagerBase, HelloManagerBase>();
    }
  }
}
