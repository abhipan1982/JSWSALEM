using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.L1A
{
  public interface IL1SetupSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendL1SetupToAdapterAsync(DCCommonSetupStructure dataToSend);

    Task<SendOfficeResult<List<DCCommonSetupStructure>>> RequestPESetupsAsync(DataContractBase message);
  }
}
