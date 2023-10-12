using Microsoft.Extensions.DependencyInjection;
using PE.BaseInterfaces.Managers.TCP;
using PE.BaseInterfaces.SendOffices.TCP;
using PE.TCP.Base.Managers;
using PE.TCP.Base.Module.Communication;
using SMF.Module.Core.Interfaces;

namespace PE.TCP.Base.Module
{
  public abstract class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<ITcpProxyBaseSendOffice, ModuleBaseSendOffice>();

      services.AddSingleton<ITcpSenderProxyBaseManager, TcpSenderProxyBaseManager>();
      services.AddSingleton<ITcpListenerProxyBaseManager, TcpListenerProxyBaseManager>();

      services.AddSingleton<ModuleBaseExternalAdapterHandler>();
    }
  }
}
