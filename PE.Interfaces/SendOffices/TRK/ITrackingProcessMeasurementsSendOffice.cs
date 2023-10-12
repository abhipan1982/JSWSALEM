using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.WBF;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.TRK
{
  public interface ITrackingProcessMeasurementsSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendProcessMeasurementRequestAsync(DcRelatedToMaterialMeasurementRequest data);

    Task<SendOfficeResult<DataContractBase>> SendProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data);

    Task<SendOfficeResult<DcMeasurementResponse>> GetMeasurementValueAsync(DcMeasurementRequest data);

    Task<SendOfficeResult<DataContractBase>> StoreSingleMeasurementAsync(DcMeasData dataToSend);
  }

  public interface ITrackingGetNdrMeasurementSendOfficeBase
  {
    Task<SendOfficeResult<DcNdrMeasurementResponse>> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest message);
  }
  
  public interface ITrackingL1AdapterSendOfficeBase
  {
    Task<SendOfficeResult<DataContractBase>> ResendTrackingPointSignals(DataContractBase message);
  }

  public interface ITrackingSendMillControlDataSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> SendMillControlDataAsync(DCMillControlMessage data);
  }

    public interface ITrackingProcessExternalSignalsSendOffice
  {
    Task<SendOfficeResult<DcReadTagActualValuesResponse>> SendReadTagsActualValuesAsync(DcReadTagActualValuesRequest data);
  }

  public interface ITrackingProcessQualityExpertTriggersSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> ProcessMaterialAreaExitEventAsync(DCAreaRawMaterial dataToSend);
  }

  public interface ITrackingFurnaceSendOffice
  {
    Task<SendOfficeResult<DataContractBase>> ProcessMaterialChangeInFurnaceEventAsync(DCFurnaceRawMaterials data);
    Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeEventAsync(DCFurnaceMaterialDischarge data);
  }

  public interface ITrackingHmiSendOfficeBase
  {
    Task<SendOfficeResult<DataContractBase>> SendL1MaterialPositionAsync(DCMaterialPosition dataToSend);
    Task<SendOfficeResult<DataContractBase>> LastMaterialPositionRequestMessageAsync(DataContractBase message);
  }
}
