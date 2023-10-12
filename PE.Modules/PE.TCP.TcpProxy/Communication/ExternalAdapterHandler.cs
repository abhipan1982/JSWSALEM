using PE.BaseInterfaces.Managers.TCP;
using PE.Interfaces.Managers.TCP;
using PE.TCP.Base.Managers;
using PE.TCP.Base.Module.Communication;

namespace PE.TCP.TcpProxy.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    //AP on 07072023
    private readonly ITcpSenderProxyBaseManager _tcpListenerProxyManager;

    public ExternalAdapterHandler(ITcpSenderProxyBaseManager tcpSenderProxyBaseManager, ITcpSenderProxyBaseManager tcpSenderProxyManager):base(tcpSenderProxyBaseManager)
    {
      //AP on 07072023
      _tcpListenerProxyManager = tcpSenderProxyManager;
    }
  }
}
