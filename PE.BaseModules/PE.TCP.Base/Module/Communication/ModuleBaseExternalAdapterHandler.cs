using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.TCP;
using SMF.Core.DC;

namespace PE.TCP.Base.Module.Communication
{
  public class ModuleBaseExternalAdapterHandler
  {
    protected readonly ITcpSenderProxyBaseManager TcpSenderProxyManager;

    public ModuleBaseExternalAdapterHandler(ITcpSenderProxyBaseManager tcpSenderProxyManager)
    {
      TcpSenderProxyManager = tcpSenderProxyManager;
    }

    public virtual async Task<DataContractBase> SendTelegram(DataContractBase message)
    {
      await TcpSenderProxyManager.Send(message);

      return new DataContractBase();
    }
  }
}
