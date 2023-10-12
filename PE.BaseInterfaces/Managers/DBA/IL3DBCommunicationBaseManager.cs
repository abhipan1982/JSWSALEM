using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.DBA
{
  public interface IL3DBCommunicationBaseManager : IManagerBase
  {
    //Task UpdateTransferTableCommStatusesAsync(DCWorkOrderStatusExt dataToSend);
    [FaultContract(typeof(ModuleMessage))]
    Task TransferWorkOrderDataFromTransferTableToAdapterAsync();

    [FaultContract(typeof(ModuleMessage))]
    Task UpdateWorkOrdesWithTimeoutAsync();

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt workOrderDefinition);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt workOrderDefinition);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinitionExt workOrderDefinition);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReportExt dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReportExt dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc);
  }
}
