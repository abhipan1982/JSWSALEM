using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ISetupBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupAsync(DCSetupListOfParameres dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSetupParametersAsync(DCSetupListOfParameres dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSetupValueAsync(DCSetupValue dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CopySetupAsync(DCSetupListOfParameres dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupAsync(DCSetupListOfParameres dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupsToL1Async(DCCommonSetupStructure dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateSetupAsync(DCCommonSetupStructure dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupConfigurationAsync(DCSetupConfiguration dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditSetupConfigurationAsync(DCSetupConfiguration dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSetupConfigurationAsync(DCSetupConfiguration dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendSetupConfigurationAsync(DCSetupConfiguration dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CloneSetupConfigurationAsync(DCSetupConfiguration dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSetupConfigurationVersionAsync(DCSetupConfiguration dc);
  }
}
