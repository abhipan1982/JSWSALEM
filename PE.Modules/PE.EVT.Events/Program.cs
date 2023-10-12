using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.EVT.Base.Module;
using PE.EVT.Events.Communication;
using PE.EVT.Events.Managers;
using PE.Interfaces.Managers.EVT;
using SMF.Module.Core;

namespace PE.EVT.Events
{
  internal class Program : ProgramBase
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>(1000);
    }

    public override void RegisterServices(ServiceCollection services)
    {
      EnumInitializator.Init();
      base.RegisterServices(services);

      services.AddSingleton<ExternalAdapterHandler>();
      services.AddSingleton<IShiftManager, ShiftManager>();
    }
  }
}
