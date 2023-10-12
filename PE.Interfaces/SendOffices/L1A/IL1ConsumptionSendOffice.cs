using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.L1A
{
  public interface IL1ConsumptionSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasDataAggregated dataToSend);
  }
}
