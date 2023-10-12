using Microsoft.Extensions.DependencyInjection;
using PE.ADP.Adapter.Communication;
using PE.ADP.Managers;
using PE.Interfaces.Interfaces.SendOffice;
using PE.Interfaces.Managers;
using SMF.Module.Core;
using SMF.Module.Core.Interfaces;
using System.Threading.Tasks;

namespace PE.ADP.Adapter
{
  internal class Program : IModule
  {
    private static Task Main(string[] args)
    {
      return ModuleController.StartModuleAsync<Program>();
    }

    public void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<ExternalAdapterHandler>();

      services.AddSingleton<IAdapterL3SendOffice, SendOffice>();
      services.AddSingleton<IAdapterL1SendOffice, SendOffice>();
      services.AddSingleton<IAdapterTcpTelegramCommunicationSendOffice, SendOffice>();

      services.AddSingleton<IL3CommunicationManager, L3CommunicationManager>();
      services.AddSingleton<IL1CommunicationManager, L1CommunicationManager>();
      services.AddSingleton<ITcpTelegramCommunicationManager, TcpTelegramCommunicationManager>();

    }
  }
}
