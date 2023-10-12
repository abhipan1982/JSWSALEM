using System.Threading.Tasks;
using PE.DTO.Internal.Adapter;
using PE.Interfaces.Managers;
using SMF.Core.DC;

namespace PE.ADP.Adapter.Communication
{
  public class ExternalAdapterHandler
  {
    private readonly IL3CommunicationManager _l3CommunicationManager;
    private readonly IL1CommunicationManager _l1CommunicationManager;
    private readonly ITcpTelegramCommunicationManager _tcpTelegramCommunicationManager;

    public ExternalAdapterHandler(IL3CommunicationManager l3CommunicationManager, IL1CommunicationManager l1CommunicationManager, ITcpTelegramCommunicationManager tcpTelegramCommunicationManager)
    {
      _l3CommunicationManager = l3CommunicationManager;
      _l1CommunicationManager = l1CommunicationManager;
      _tcpTelegramCommunicationManager = tcpTelegramCommunicationManager;
    }

    //L3 methods
    internal Task<DCWorkOrderStatus> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinition message)
    {
      // return result
      return _l3CommunicationManager.ProccesExtWorkOrderMessageAsync(message);
    }
    //internal async Task<DataContractBase> ExternalSendTelegramSetupByteDataAsync(DCTelegramSetup message)
    //{
    //  // return result
    //  return await _l1CommunicationManager.ProcessSendTelegramSetupByteDataAsync(message);
    //}

    internal Task<DataContractBase> TcpTelegramSendAsync(DataContractBase message)
    {
      return _tcpTelegramCommunicationManager.ProcessTcpTelegramSendAsync(message);
    }
  }
}
