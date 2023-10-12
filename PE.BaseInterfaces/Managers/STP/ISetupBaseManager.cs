using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.STP
{
  public interface ISetupBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupAsync(DCSetupListOfParameres message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSetupParametersAsync(DCSetupListOfParameres message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSetupValueAsync(DCSetupValue message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupAsync(DCSetupListOfParameres message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CopySetupAsync(DCSetupListOfParameres message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupsToL1Async(DCCommonSetupStructure message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupConfigurationAsync(DCSetupConfiguration message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditSetupConfigurationAsync(DCSetupConfiguration message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupConfigurationAsync(DCSetupConfiguration message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupConfigurationAsync(DCSetupConfiguration message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CloneSetupConfigurationAsync(DCSetupConfiguration message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupConfigurationVersionAsync(DCSetupConfiguration message);
  }
}
