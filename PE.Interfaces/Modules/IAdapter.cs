using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.External;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.External.TCP.Telegrams;
using PE.Models.DataContracts.External.TCP.Telegrams;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IAdapter : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCBatchDataStatus> ExternalProccesBatchDataMessageAsync(DCL3L2BatchData message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    [ServiceKnownTypeAttribute(typeof(DCTCPChargingBedTelegramExt))]
    Task<DCDefaultExt> ProcessTestTelegramAsync(DataContractBase message);
  }
}
