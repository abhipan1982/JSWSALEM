using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IDBAdapter : IDBAdapterBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCL3L2BatchData> CreateBatchDataAsync(DCL3L2BatchData dcBatchDataDefinition);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCL3L2BatchData> UpdateBatchDataAsync(DCL3L2BatchData dcWorkOrderDefinition);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteWorkOrderDefinitionAsync(DCL3L2BatchData dcWorkOrderDefinition);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ResetWorkOrderReportAsync(DCL3L2BatchData dcWorkOrderReport);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ResetProductReportAsync(DCL3L2BatchData dcCoilReport);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateWorkOrderReport(DCL3L2BatchData dc);

    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateProductReport(DCL3L2BatchData dc);
  }
}
