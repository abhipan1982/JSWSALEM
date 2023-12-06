using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DBA.Base.Module;
using PE.DBA.DataBaseAdapter.Managers;
using PE.DbEntity.EnumClasses;
using SMF.Module.Core;
using PE.Interfaces.Managers.DBA;
using PE.DBA.DataBaseAdapter.Communication;
using PE.Interfaces.SendOffices.DBA;
using PE.DbEntity.Providers;
using PE.Interfaces.Managers.PRM;

namespace PE.DBA.DataBaseAdapter
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
      //services.AddSingleton(typeof(IContextProvider<>), typeof(DefaultContextProvider<>)); commented on 29-11-2023 by AP

      services.AddSingleton<IDbAdapterSendOffice, SendOffice>();

      services.AddSingleton<IL3DBCommunicationManager, L3DBCommunicationManager>();

      services.AddSingleton<ExternalAdapterHandler>();
    }
  }
}
