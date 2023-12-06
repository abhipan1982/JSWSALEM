using PE.BaseInterfaces.Managers;
using PE.BaseInterfaces.Managers.DBA;
using PE.Models.DataContracts.External.DBA;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PE.Interfaces.Managers.DBA
{
  public interface IL3DBCommunicationManager:IManagerBase
  {
    //AP on 27072023 ... JSWSALEM standard

    //Task UpdateTransferTableCommStatusesAsync(DCBatchDataStatus dataToSend);
    [FaultContract(typeof(ModuleMessage))]
    Task TransferBatchDataFromTransferTableToAdapterAsync();

    [FaultContract(typeof(ModuleMessage))]
    Task<DCL3L2BatchDataDefinition> UpdateBatchDataDefinitionAsync(DCL3L2BatchDataDefinitionExt batchData);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCL3L2BatchDataDefinition> CreateBatchDataAsync(DCL3L2BatchDataDefinitionExt batchData);

    [FaultContract(typeof(ModuleMessage))]
    Task UpdateBatchDataWithTimeoutAsync();

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DCL3L2BatchData> ProcessBatchDataAsync(DCL3L2BatchData batchData);



    //AV@
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionExtMOD workOrderDefinition);


    //AV@
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionExtMOD workOrderDefinition);


    //AV@
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderDefinitionAsyncEXT(DCL3L2WorkOrderDefinitionExtMOD workOrderDefinition);


  }
}
