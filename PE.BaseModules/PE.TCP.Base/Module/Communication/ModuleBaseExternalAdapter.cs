using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.TCP.Base.Module.Communication
{
  public abstract class ModuleBaseExternalAdapter<T> : ExternalAdapterBase<T>, ITcpProxyBase where T : class, ITcpProxyBase
  {
    protected readonly ModuleBaseExternalAdapterHandler Handler;

    #region ctor

    public ModuleBaseExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base()
    {
      Handler = handler;
    }

    #endregion

    public virtual Task<DataContractBase> SendTelegramAsync(DataContractBase message)
    {
      //TODO check why HandleExternalMethod is sync
      return HandleExternalMethod(message, () => Handler.SendTelegram(message).GetAwaiter().GetResult());
    }
  }
}
