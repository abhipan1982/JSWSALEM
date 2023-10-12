using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.STP
{
  public interface ISetupTelegramSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendTelegramSetupDataAsync(DCCommonSetupStructure dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendSetupDataRequestToL1Async(DCCommonSetupStructure dataToSend);
  }
}
