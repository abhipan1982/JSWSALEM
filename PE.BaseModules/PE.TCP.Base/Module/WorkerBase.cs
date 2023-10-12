using PE.BaseInterfaces.Managers.TCP;
using SMF.Module.Core;

namespace PE.TCP.Base.Module
{
  public class WorkerBase : BaseWorker
  {
    #region managers

    protected readonly ITcpListenerProxyBaseManager TcpListenerProxyManager;

    #endregion

    #region ctor

    public WorkerBase(ITcpListenerProxyBaseManager tcpListenerProxyManager)
    {
      TcpListenerProxyManager = tcpListenerProxyManager;
    }

    #endregion

    public override void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      TcpListenerProxyManager.StartListening();
      base.ModuleStarted(sender, e);
    }

    #region properties

    #endregion
  }
}
