using PE.DTO.External;
using PE.DTO.External.DBAdapter;
using PE.DTO.Internal.Adapter;
using PE.DTO.Internal.DBAdapter;
using SMF.Core.DC;
using SMF.Module.Core;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.ADP.Adapter.Communication
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
  public class ExternalAdapter : ExternalAdapterBase, Interfaces.Lite.IAdapter
  {
    private readonly ExternalAdapterHandler _handler;
    #region ctor
    public ExternalAdapter(ExternalAdapterHandler handler) : base(typeof(PE.Interfaces.Lite.IAdapter)) {
      _handler = handler;
    }
    #endregion
    #region TCP telegram sending

    public Task<DataContractBase> TcpTelegramSendAsync(DataContractBase message)
    {
      return HandleIncommingMethod(_handler.TcpTelegramSendAsync, message);
    }

    public async Task<DCDefaultExt> ProcessTestTelegramAsync(BaseExternalTelegram message)
    {
      await Task.CompletedTask;
      return new DCDefaultExt();
    }
    #endregion
    #region L3 incomming messages

    public async Task<DCWorkOrderStatusExt> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinitionExt message)
    {
      DCL3L2WorkOrderDefinition internalDc = message.ToInternal() as DCL3L2WorkOrderDefinition;

      DCWorkOrderStatus returnData = await HandleIncommingMethod(_handler.ExternalProccesWorkOrderMessageAsync, internalDc);

      DCWorkOrderStatusExt returnExtData = new DCWorkOrderStatusExt();
      returnExtData.ToExternal(returnData);

      return returnExtData;
    }

    #endregion
  }
}
