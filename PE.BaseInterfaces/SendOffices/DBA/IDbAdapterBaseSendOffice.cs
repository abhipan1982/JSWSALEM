using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.DBA;
using SMF.Core.Communication;

namespace PE.BaseInterfaces.SendOffices.DBA
{
  public interface IDbAdapterBaseSendOffice
  {
    Task<SendOfficeResult<DCWorkOrderStatus>> SendWorkOrderDataToAdapterAsync(DCL3L2WorkOrderDefinition dataToSend);
  }
}
