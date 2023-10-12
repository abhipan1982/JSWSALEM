using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.L1A.Base.Module;
using PE.L1A.L1Adapter.Communication;
using SMF.Module.Core;
using SMF.Startup;

namespace PE.L1A.L1Adapter
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(60000);
    }

    public override void RegisterServices(ServiceCollection services)
    {
      EnumInitializator.Init();
      //OpcLicenseHelper.Init();
      base.RegisterServices(services);

      services.AddSingleton<ExternalAdapterHandler>();
     
    }
  }
}
