using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.L1A
{
  public interface IL1SignalSendOfficeBase
  {
    Task<SendOfficeResult<DataContractBase>> SendL1TrackingPointToTrackingAsync(DcTrackingPointSignal signalToSend);
    Task<SendOfficeResult<DataContractBase>> SendAggregatedL1TrackingPointToTrackingAsync(DcAggregatedTrackingPointSignal signalToSend);
    //Task<SendOfficeResult<DataContractBase>> SendL1MeasDataToAdapterAsync(DCMeasData dataToSend);
  }
}
