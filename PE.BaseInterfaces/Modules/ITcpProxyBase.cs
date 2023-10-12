using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.TCP;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITcpProxyBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    [ServiceKnownTypeAttribute(typeof(DCTCPTestCommunicationTelegram))]
    Task<DataContractBase> SendTelegramAsync(DataContractBase message);
  }
}
