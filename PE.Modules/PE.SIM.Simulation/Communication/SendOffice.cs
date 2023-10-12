namespace PE.SIM.Simulation.Communication
{
  public class SendOffice : ModuleSendOfficeBase, ISimulationSendOffice
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

    public Task<SendOfficeResult<DataContractBase>> SendL1AggregatedMeasDataToAdapterAsync(DCMeasData dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreSingleMeasurementAsync(dataToSend));
      ;
    }

    public Task<SendOfficeResult<DataContractBase>> SendL1SampleMeasDataToAdapterAsync(DCMeasDataSample dataToSend)
    {
      string targetModuleName = PE.Core.Modules.MvHistory.Name;
      IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.StoreSingleSampleMeasurementAsync(dataToSend));
      ;
    }
  }
}
