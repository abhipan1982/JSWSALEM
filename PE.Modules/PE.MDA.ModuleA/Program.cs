using Microsoft.Extensions.DependencyInjection;
using PE.MDA.Base.Interfaces;
using PE.MDA.Base.Module;
using PE.MDA.ModuleA.Communication;
using PE.MDA.ModuleA.Managers;
using SMF.Module.Core;

namespace PE.MDA.ModuleA
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>();
    }

    public override void RegisterServices(ServiceCollection services)
    {
       base.RegisterServices(services);

       services.AddSingleton<ExternalAdapterHandler>();
       services.AddSingleton<IHelloManagerBase, HelloManager>();
    }
  }
}
