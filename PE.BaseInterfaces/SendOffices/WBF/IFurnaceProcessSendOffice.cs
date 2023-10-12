using System.Threading.Tasks;
using SMF.Core.DC;
using SMF.Core.Communication;
using PE.BaseModels.DataContracts.Internal.MVH;

namespace PE.BaseInterfaces.SendOffices.WBF
{
  public interface IFurnaceProcessBaseSendOffice
  {
    Task<SendOfficeResult<DCMeasDataAggregated>> GetFurnaceMeasurementsAsync(DCMeasRequest dataToSend);
    Task<SendOfficeResult<DataContractBase>> SendFurnaceMeasurementsAsync(DCMeasDataAggregated dataToSend);
  }
}
