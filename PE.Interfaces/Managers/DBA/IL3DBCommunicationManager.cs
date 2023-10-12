using PE.BaseInterfaces.Managers;
using PE.BaseInterfaces.Managers.DBA;
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
    Task<DCL3L2BatchData> UpdateBatchDataAsync(DCL3L2BatchData batchData);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCL3L2BatchData> CreateBatchDataAsync(DCL3L2BatchData batchData);

    [FaultContract(typeof(ModuleMessage))]
    Task UpdateBatchDataWithTimeoutAsync();
  }
}
