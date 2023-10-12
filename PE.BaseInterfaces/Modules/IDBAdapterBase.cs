using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.DC;
using SMF.Core.Interfaces;

// ReSharper disable once CheckNamespace
namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  // ReSharper disable once InconsistentNaming
  public interface IDBAdapterBase : IBaseModule
  {
    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> SendBackMsgToL3(DCWorkOrderStatusExt dataToSend);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2WorkOrderDefinition dcWorkOrderDefinition);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ResetWorkOrderReportAsync(DCL2L3WorkOrderReport dcWorkOrderReport);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ResetProductReportAsync(DCL2L3ProductReport dcCoilReport);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderReport(DCL2L3WorkOrderReport dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductReport(DCL2L3ProductReport dc);
  }
}
