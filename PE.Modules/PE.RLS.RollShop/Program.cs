using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.EnumClasses;
using PE.RLS.Base.Module;
using PE.RLS.RollShop.Communication;
using SMF.Module.Core;

namespace PE.RLS.RollShop
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

      var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false)
      .Build();

      services.AddDbContext<PEContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("PEContext")), ServiceLifetime.Scoped);

      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
