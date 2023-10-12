using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.External;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.External.TCP.Telegrams;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IAdapterBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCWorkOrderStatusExt> ExternalProccesWorkOrderMessageAsync(DCL3L2WorkOrderDefinitionExt message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    [ServiceKnownTypeAttribute(typeof(DCTCPTestCommunicationTelegramExt))]
    Task<DCDefaultExt> ProcessTestTelegramAsync(BaseExternalTelegram message);
  }
}
