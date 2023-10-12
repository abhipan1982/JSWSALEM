using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.QEX.Base.Module;
using PE.QEX.QualityExpert.Communication;
using SMF.Module.Core;

namespace PE.QEX.QualityExpert
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
