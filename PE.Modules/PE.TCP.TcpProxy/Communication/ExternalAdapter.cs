using PE.Interfaces.Modules;
using PE.TCP.Base.Module.Communication;

namespace PE.TCP.TcpProxy.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<ITcpProxy>, ITcpProxy
  {
    protected readonly ExternalAdapterHandler _Handler;

    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
      _Handler = handler;
    }

    #endregion ctor
  }
}
