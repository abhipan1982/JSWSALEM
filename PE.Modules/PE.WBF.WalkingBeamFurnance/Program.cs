using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.WBF.Base.Module;
using PE.WBF.WalkingBeamFurnance.Communication;
using SMF.Module.Core;

namespace PE.WBF.WalkingBeamFurnance
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(60 * 1000);
    }

    public override void RegisterServices(ServiceCollection services)
    {
      EnumInitializator.Init();
      base.RegisterServices(services);
      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
