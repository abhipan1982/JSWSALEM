using Microsoft.Extensions.DependencyInjection;
using PE.MDB.Base.Module;
using PE.MDB.ModuleB.Communication;
using SMF.Module.Core;

namespace PE.MDB.ModuleB
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(3000);
    }

    public override void RegisterServices(ServiceCollection services)
    {
      base.RegisterServices(services);

      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
