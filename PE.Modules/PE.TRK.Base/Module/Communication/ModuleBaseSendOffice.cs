using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PRF;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.BaseModels.DataContracts.Internal.WBF;
using PE.BaseModels.DataContracts.Internal.ZPC;
using PE.Core;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.TRK.Base.Module.Communication
{
  public class ModuleBaseSendOffice : ModuleSendOfficeBase,
    ITrackingProcessMeasurementsSendOffice,
    ITrackingFurnaceSendOffice,
    ITrackingProcessMaterialEventSendOfficeBase,
    ITrackingHmiSendOfficeBase,
    ITrackingGetNdrMeasurementSendOfficeBase,
    ITrackingProcessQualityExpertTriggersSendOffice,
    ITrackingLabelPrinterSendOffice,
    ITrackingSendMillControlDataSendOffice,
    ITrackingL1AdapterSendOfficeBase
  {
    public Task<SendOfficeResult<DataContractBase>> ProcessMaterialChangeInFurnaceEventAsync(DCFurnaceRawMaterials data)
    {
      string targetModuleName = Modules.WalkingBeamFurnace.Name;
      IWalkingBeamFurnaceBase client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnaceBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialChangeInFurnaceEventAsync(data));
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeEventAsync(DCFurnaceMaterialDischarge data)
    {
      string targetModuleName = Modules.WalkingBeamFurnace.Name;
      IWalkingBeamFurnaceBase client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnaceBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialDischargeEventAsync(data));
    }

    public virtual Task<SendOfficeResult<DataContractBase>> SendProcessMeasurementRequestAsync(DcRelatedToMaterialMeasurementRequest data)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessMeasurementRequestAsync(data));
    }

    public virtual Task<SendOfficeResult<DataContractBase>> SendProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessAggregatedMeasurementRequestAsync(data));
    }

    public virtual Task<SendOfficeResult<DcMeasurementResponse>> GetMeasurementValueAsync(DcMeasurementRequest data)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessGetMeasurementValueAsync(data));
    }

    public Task<SendOfficeResult<DCL1L3MaterialConnector>> SendRequestForL3MaterialAsync(DCMaterialRelatedOperationData data)
    {
      string targetModuleName = Modules.ProdPlaning.Name;
      IProdPlaningBase client = InterfaceHelper.GetFactoryChannel<IProdPlaningBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.RequestL3MaterialAsync(data));
    }

    public Task<SendOfficeResult<DataContractBase>> AccumulateEquipmentUsageAsync(DCEquipmentAccu dataToSend)
    {
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenanceBase client = InterfaceHelper.GetFactoryChannel<IMaintenanceBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.AccumulateEquipmentUsageAsync(dataToSend));
    }

    public Task<SendOfficeResult<DataContractBase>> AccumulateRollsUsageAsync(DCRollsAccu dataToSend)
    {
      string targetModuleName = Modules.RollShop.Name;
      IRollShopBase client = InterfaceHelper.GetFactoryChannel<IRollShopBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.AccumulateRollsUsageAsync(dataToSend));
    }

    public Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent)
    {
      string targetModuleName = Core.Constants.SmfAuthorization_Module_Events;
      IEventsBase client = InterfaceHelper.GetFactoryChannel<IEventsBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.AddMillEvent(millEvent));
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeFromFurnaceEventAsync(WorkOrderId dc)
    {
      throw new System.NotImplementedException();
    }

    public Task<SendOfficeResult<DataContractBase>> SendHeadEnterToDLSAsync(DCDelayEvent data)
    {
      string targetModuleName = Modules.Events.Name;
      IEventsBase client = InterfaceHelper.GetFactoryChannel<IEventsBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessHeadEnterAsync(data));
    }

    public Task<SendOfficeResult<DCProductData>> SendRequestToCreateCoilAsync(DCCoilData data)
    {
      string targetModuleName = Modules.ProdManager.Name;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessCoilProductionEndAsync(data));
    }

    public Task<SendOfficeResult<DCProductData>> SendRequestToCreateBundleAsync(DCBundleData data)
    {
      string targetModuleName = Modules.ProdManager.Name;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessBundleProductionEndAsync(data));
    }

    public Task<SendOfficeResult<DataContractBase>> SendTailLeavesToDLSAsync(DCDelayEvent data)
    {
      string targetModuleName = Modules.Events.Name;
      IEventsBase client = InterfaceHelper.GetFactoryChannel<IEventsBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessTailLeavesAsync(data));
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessMaterialChargeEventAsync(WorkOrderId dc)
    {
      throw new System.NotImplementedException();
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessWorkOrderStatus(DCRawMaterial message)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessWorkOrderStatus(message));
    }

    public Task<SendOfficeResult<DataContractBase>> SendProductReportAsync(DCRawMaterial message)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.SendProductReportAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> CheckShiftsWorkOrderStatusses(DCShiftCalendarId message)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.CheckShiftsWorkOrderStatusses(message));
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1MaterialPositionAsync(DCMaterialPosition dataToSend)
    {
      string targetModuleName = Modules.Hmiexe.Name;
      IHmiModuleBase client = InterfaceHelper.GetFactoryChannel<IHmiModuleBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.L1MaterialPositionMessageAsync(dataToSend));
    }

    public Task<SendOfficeResult<DataContractBase>> LastMaterialPositionRequestMessageAsync(DataContractBase message)
    {
      string targetModuleName = Modules.Hmiexe.Name;
      IHmiModuleBase client = InterfaceHelper.GetFactoryChannel<IHmiModuleBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.LastMaterialPositionRequestMessageAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> CalculateWorkOrderKPIsAsync(DCCalculateKPI message)
    {
      string targetModuleName = Modules.Performance.Name;
      IPerformanceBase client = InterfaceHelper.GetFactoryChannel<IPerformanceBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.CalculateWorkOrderKPIsAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> AssignRawMaterialQualityAsync(DCRawMaterialQuality dCRawMaterialQuality)
    {
      string targetModuleName = Modules.Quality.Name;
      IQualityBase client = InterfaceHelper.GetFactoryChannel<IQualityBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.AssignRawMaterialQualityAsync(dCRawMaterialQuality));
    }

    public Task<SendOfficeResult<DcNdrMeasurementResponse>> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest message)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessNdrMeasurementRequestAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> StoreSingleMeasurementAsync(DcMeasData dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistoryBase client = InterfaceHelper.GetFactoryChannel<IMVHistoryBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreSingleMeasurementAsync(dataToSend));
    }

    public virtual Task<SendOfficeResult<DataContractBase>> ProcessMaterialAreaExitEventAsync(DCAreaRawMaterial dataToSend)
    {
      string targetModuleName = PE.Core.Modules.QualityExpert.Name;
      IQualityExpertBase client = InterfaceHelper.GetFactoryChannel<IQualityExpertBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialAreaExitEventAsync(dataToSend));
    }

    public Task<SendOfficeResult<DCZebraPrinterResponse>> SendLabelPrintRequest(DCZebraRequest request)
    {
      string targetModuleName = Modules.ZebraPrinter.Name;
      IZebraPCBase client = InterfaceHelper.GetFactoryChannel<IZebraPCBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.PrintLabelAsync(request));
    }

    public Task<SendOfficeResult<DataContractBase>> SendMillControlDataAsync(DCMillControlMessage data)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.SendMillControlData(data));
    }

    public Task<SendOfficeResult<DataContractBase>> ResendTrackingPointSignals(DataContractBase data)
    {
      string targetModuleName = Modules.L1Adapter.Name;
      IL1AdapterBase client = InterfaceHelper.GetFactoryChannel<IL1AdapterBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ResendTrackingPointSignals(data));
    }
  }
}
