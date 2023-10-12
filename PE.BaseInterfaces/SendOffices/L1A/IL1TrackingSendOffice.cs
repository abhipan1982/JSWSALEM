using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.L1A
{
  public interface IL1TrackingSendOffice
  {
    Task<SendOfficeResult<WorkOrderId>> SendL1MaterialIdRequestAsync(WorkOrderId dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1MeasDataToAdapterAsync(DcMeasData dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1TrackingEventAsync(DCTrackingEvent dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1MaterialPositionAsync(DCMaterialPosition dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasDataAggregated dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1ScrapInfoToAdapterAsync(DCL1ScrapData dataToSend);

    Task<SendOfficeResult<DataContractBase>> SendL1CutDataAsync(DCL1CutData dataToSend);

    Task<SendOfficeResult<WorkOrderId>> SendDivisionMaterialMessageAsync(DCL1MaterialDivision message);

    //Task<SendOfficeResult<DataContractBase>> ProcessMaterialChangeInFurnaceEventAsync(DataContractBase dc);
    Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent);

    Task<SendOfficeResult<DataContractBase>> RejectRawMaterial(DCRejectMaterialData dc);

    Task<SendOfficeResult<LayerId>> ProcessLayerCreationRequest(LayerId request);

    Task<SendOfficeResult<DCMaterialsCountResult>> GetMaterialsCountByLayerId(LayerId request);

    Task<SendOfficeResult<DataContractBase>> SetLayerForMaterialsByMaterialIds(
      DCAppendRawMaterialsToLayerRequest request);

    Task<SendOfficeResult<DataContractBase>> ProcessLayerFormFinished(LayerId dCLongIdRequest);

    Task<SendOfficeResult<DataContractBase>> ProcessLayerTransferredEventAsync(LayerId dCLongIdRequest);

    Task<SendOfficeResult<DataContractBase>> ProcessMaterialChargeEventAsync(WorkOrderId dc);

    Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeEventAsync(WorkOrderId dc);
  }
}
