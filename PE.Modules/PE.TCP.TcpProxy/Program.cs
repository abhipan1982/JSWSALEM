using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PE.DbEntity.EnumClasses;
using PE.Interfaces.Managers.DBA;
using PE.Interfaces.Managers.TCP;
using PE.Interfaces.SendOffices.TCP;
using PE.TCP.Base.Managers;
using PE.TCP.Base.Module;
using PE.TCP.TcpProxy.Communication;
using PE.TCP.TcpProxy.Managers;
using SMF.Module.Core;

namespace PE.TCP.TcpProxy
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

      services.AddSingleton<ITcpProxySendOffice, SendOffice>();

      //services.AddSingleton<ITcpSenderProxyManager, TcpSenderProxyManager>();

      services.AddSingleton<ITcpListenerProxyManager, TcpListenerProxyManager>();

      services.AddSingleton<ExternalAdapterHandler>();      
    }
  }
}
