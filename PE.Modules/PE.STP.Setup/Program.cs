using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.STP.Base.Module;
using PE.STP.Setup.Communication;
using SMF.Module.Core;

namespace PE.STP.Setup
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>();
    }

    public override void RegisterServices(ServiceCollection services)
    {
      EnumInitializator.Init();
      base.RegisterServices(services);

      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
