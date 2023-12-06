using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.DC;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IDBAdapter : IDBAdapterBase
  {

    //AV@
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition);



    //AV@
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition);

  //AV@
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition);
  }
}
