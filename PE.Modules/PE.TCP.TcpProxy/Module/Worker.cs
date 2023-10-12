using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.TCP;
using PE.Interfaces.Managers.TCP;
using PE.TCP.Base.Module;
using SMF.Module.Core;

namespace PE.TCP.TcpProxy.Module
{
  public class Worker : BaseWorker
  {
    #region managers
    private readonly ITcpListenerProxyManager _tcpListenerProxyManager;

    public Worker(ITcpListenerProxyManager tcpListenerProxyManager)
    {
      _tcpListenerProxyManager = tcpListenerProxyManager;
    }
    #endregion

    public override async void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      await Task.CompletedTask;
    }

    public override async void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      _tcpListenerProxyManager.StartListening();
      base.ModuleStarted(sender, e);
      await Task.CompletedTask;
    }
  }
}
