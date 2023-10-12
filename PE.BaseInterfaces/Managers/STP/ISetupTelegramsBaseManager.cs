using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.STP
{
  public interface ISetupTelegramsBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSendTelegramSetupAsync(DCTelegramSetupId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateTelegramValueAsync(DCTelegramSetupValue message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateNewVersionOfTelegramAsync(DCTelegramSetupId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupAsync(DCTelegramSetupId message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RequestUpdateSetupFromL1Async(DCTelegramSetupId message);

    Task<DataContractBase> UpdateSetupFromL1Async(DCCommonSetupStructure message);

    Task<List<DCCommonSetupStructure>> ProcessRequestPESetupsAsync(DataContractBase message);
  }
}
