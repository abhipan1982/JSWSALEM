using System.Threading.Tasks;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.Communication;

namespace PE.Interfaces.SendOffices.DBA
{
  public interface IDbAdapterSendOffice
  {
    Task<SendOfficeResult<DCBatchDataStatus>> SendBatchDataToAdapterAsync(DCL3L2BatchDataDefinition dataToSend);
  }
}
