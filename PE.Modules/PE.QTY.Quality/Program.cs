using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.QTY.Base.Module;
using PE.QTY.Quality.Communication;
using SMF.Module.Core;

namespace PE.QTY.Quality
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
