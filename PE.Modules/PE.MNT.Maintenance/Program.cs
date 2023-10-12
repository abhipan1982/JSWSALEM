using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.MNT.Base.Module;
using PE.MNT.Maintenance.Communication;
using SMF.Module.Core;

namespace PE.MNT.Maintenance
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
