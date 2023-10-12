namespace PE.L1A.L1Adapter.Communication
{
  public class SendOffice : ModuleSendOfficeBase, IL1SetupSendOffice, IL1SignalSendOffice, IL1ConsumptionSendOffice,
    IL1TrackingSendOffice
  {
    public Task<SendOfficeResult<DCRequestMaterial>> SendL1MaterialIdRequestAsync(DCRawMaterialRequest dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessL1BaseIdRequestAsync(dataToSend));
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1CutDataAsync(DCL1CutData dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessCutMessageAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DCRequestMaterial>> SendL1DivisionAsync(DCL1MaterialDivision dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessDivisionMaterialMessageAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1ScrapInfoToAdapterAsync(DCL1ScrapData dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessScrapMessageAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1MeasDataToAdapterAsync(DCMeasData dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreSingleMeasurementAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1TrackingEventAsync(DCTrackingEvent dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessL1TrackingEventMessageAsync(dataToSend));
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1SampleMeasDataToAdapterAsync(DCMeasDataSample dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreSingleMeasurementAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1AggregatedMeasDataToAdapterAsync(
      DCMeasDataAggregated dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreMultipleMeasurementsAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1SetupToAdapterAsync(DCCommonSetupStructure dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.UpdateSetupFromL1Async(dataToSend));
      ;
    }

    public Task<SendOfficeResult<List<DCCommonSetupStructure>>> RequestPeSetupsAsync(DataContractBase message)
    {
      string targetModuleName = PE.Core.Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessRequestPESetupsAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1MaterialPositionAsync(DCMaterialPosition dataToSend)
    {
      string targetModuleName = PE.Core.Modules.Hmiexe.Name;
      IHmi client = InterfaceHelper.GetFactoryChannel<IHmi>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.L1MaterialPositionMessageAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DCRequestMaterial>> SendDivisionMaterialMessageAsync(DCL1MaterialDivision message)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessDivisionMaterialMessageAsync(message));
    }

    public Task<SendOfficeResult<DataContractBase>> RejectRawMaterial(DCRejectMaterialData dc)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.RejectRawMaterial(dc));
    }

    public async Task<SendOfficeResult<DataContractBase>> ProcessMaterialChargeEventAsync(DCLongIdRequest dc)
    {
      string targetModuleName = PE.Core.Modules.WalkingBeamFurnace.Name;
      IWalkingBeamFurnace client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnace>(targetModuleName);

      return await HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialChargeEventAsync(dc));
    }

    public async Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeEventAsync(DCLongIdRequest dc)
    {
      string targetModuleName = PE.Core.Modules.WalkingBeamFurnace.Name;
      IWalkingBeamFurnace client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnace>(targetModuleName);

      return await HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialDischargeEventAsync(dc));
    }

    public Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent)
    {
      string targetModuleName = PE.Core.Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.AddMillEvent(millEvent));
    }

    public Task<SendOfficeResult<DCRequestMaterial>> ProcessLayerCreationRequest(DCRawMaterialRequest request)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessLayerCreationRequestAsync(request));
    }

    public Task<SendOfficeResult<DCMaterialsCountResult>> GetMaterialsCountByLayerId(DCLongIdRequest request)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.GetMaterialsCountByLayerId(request));
    }

    public Task<SendOfficeResult<DataContractBase>> SetLayerForMaterialsByMaterialIds(
      DCAppendRawMaterialsToLayerRequest request)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.SetLayerForMaterialsByMaterialIds(request));
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessLayerFormFinished(DCLongIdRequest request)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessLayerFormFinished(request));
    }

    public Task<SendOfficeResult<DataContractBase>> ProcessLayerTransferredEventAsync(DCLongIdRequest request)
    {
      string targetModuleName = PE.Core.Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessLayerTransferredEventAsync(request));
    }
  }
}
