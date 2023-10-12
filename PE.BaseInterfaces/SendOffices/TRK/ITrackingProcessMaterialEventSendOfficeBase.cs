using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PRF;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.TRK
{
  public interface ITrackingProcessMaterialEventSendOfficeBase
  {
    Task<SendOfficeResult<DCL1L3MaterialConnector>> SendRequestForL3MaterialAsync(DCMaterialRelatedOperationData data);

    Task<SendOfficeResult<DCProductData>> SendRequestToCreateCoilAsync(DCCoilData data);

    Task<SendOfficeResult<DCProductData>> SendRequestToCreateBundleAsync(DCBundleData data);

    Task<SendOfficeResult<DataContractBase>> SendHeadEnterToDLSAsync(DCDelayEvent data);

    Task<SendOfficeResult<DataContractBase>> SendTailLeavesToDLSAsync(DCDelayEvent data);

    //Task<SendOfficeResult<DataContractBase>> SendRemoveFinishedOrdersFromScheduleAsync(DataContractBase data);

    //Task<SendOfficeResult<DataContractBase>> SendLastMaterialPosition(DataContractBase dc);

    Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeFromFurnaceEventAsync(WorkOrderId dc);
    Task<SendOfficeResult<DataContractBase>> ProcessMaterialChargeEventAsync(WorkOrderId dc);

    Task<SendOfficeResult<DataContractBase>> AccumulateRollsUsageAsync(DCRollsAccu dataToSend);

    Task<SendOfficeResult<DataContractBase>> AccumulateEquipmentUsageAsync(DCEquipmentAccu dataToSend);

    Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent);

    Task<SendOfficeResult<DataContractBase>> ProcessWorkOrderStatus(DCRawMaterial message);

    Task<SendOfficeResult<DataContractBase>> CheckShiftsWorkOrderStatusses(DCShiftCalendarId dCShiftCalendarId);

    Task<SendOfficeResult<DataContractBase>> CalculateWorkOrderKPIsAsync(DCCalculateKPI message);

    Task<SendOfficeResult<DataContractBase>> AssignRawMaterialQualityAsync(DCRawMaterialQuality dCRawMaterialQuality);

    Task<SendOfficeResult<DataContractBase>> SendProductReportAsync(DCRawMaterial dc);
  }
}
