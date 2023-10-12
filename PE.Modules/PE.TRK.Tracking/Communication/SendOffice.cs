using SMF.Module.Core;

namespace PE.TRK.Tracking.Communication
{
  internal class SendOffice : ModuleSendOfficeBase
  {
    //public Task<SendOfficeResult<DCL1L3MaterialConnector>> SendRequestForL3MaterialAsync(
    //  DCFeatureRelatedOperationData data)
    //{
    //  string targetModuleName = PE.Core.Modules.ProdPlaning.Name;
    //  IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
    //  return HandleModuleSendMethod(targetModuleName, () => client.RequestL3MaterialAsync(data));
    //}

    //public Task<SendOfficeResult<DCProductData>> SendRequestToCreateProductAsync(
    //  DTO.Internal.ProdManager.DCRawMaterialData data)
    //{
    //  string targetModuleName = PE.Core.Modules.ProdManager.Name;
    //  IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.ProcessProductionEndAsync(data));
    //}

    //public Task<SendOfficeResult<DataContractBase>> SendHeadEnterToDLSAsync(DTO.Internal.Events.DCDelayEvent data)
    //{
    //  string targetModuleName = PE.Core.Modules.Events.Name;
    //  IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.ProcessHeadEnterAsync(data));
    //}

    //public Task<SendOfficeResult<DataContractBase>> TriggerKPICalculation(DCGenerateWorkOrderKPI data)
    //{
    //  string targetModuleName = PE.Core.Modules.Performance.Name;
    //  IPerformance client = InterfaceHelper.GetFactoryChannel<IPerformance>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.GenerateWorkOrderKPIAsync(data));
    //}

    //public Task<SendOfficeResult<DataContractBase>> SendTailLeavesToDLSAsync(DTO.Internal.Events.DCDelayEvent data)
    //{
    //  string targetModuleName = PE.Core.Modules.Events.Name;
    //  IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.ProcessTailLeavesAsync(data));
    //}

    //public Task<SendOfficeResult<DataContractBase>> SendRemoveFinishedOrdersFromScheduleAsync(DataContractBase data)
    //{
    //  string targetModuleName = PE.Core.Modules.ProdPlaning.Name;
    //  IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
    //  return HandleModuleSendMethod(targetModuleName, () => client.RemoveFinishedOrdersFromScheduleAsync(data));
    //}

    //public Task<SendOfficeResult<DataContractBase>> SendLastMaterialPosition(DataContractBase dc)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = PE.Core.Modules.Hmiexe.Name;
    //  IHmi client = InterfaceHelper.GetFactoryChannel<IHmi>(targetModuleName);

    //  //call method on remote module
    //  return HandleModuleSendMethod(targetModuleName, () => client.LastMaterialPositionRequestMessageAsync(dc));
    //}

    //public Task<SendOfficeResult<DataContractBase>> ProcessMaterialDischargeFromFurnaceEventAsync(DCLongIdRequest dc)
    //{
    //  string targetModuleName = PE.Core.Modules.WalkingBeamFurnace.Name;
    //  IWalkingBeamFurnace client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnace>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.ProcessMaterialDischargeEventAsync(dc));
    //}

    //public Task<SendOfficeResult<DataContractBase>> AccumulateRollsUsageAsync(DCRollsAccu dataToSend)
    //{
    //  string targetModuleName = PE.Core.Modules.RollShop.Name;
    //  IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

    //  //call method on remote module
    //  return HandleModuleSendMethod(targetModuleName, () => client.AccumulateRollsUsageAsync(dataToSend));
    //}

    //public Task<SendOfficeResult<DataContractBase>> AccumulateEquipmentUsageAsync(DCEquipmentAccu dataToSend)
    //{
    //  string targetModuleName = PE.Core.Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleModuleSendMethod(targetModuleName, () => client.AccumulateEquipmentUsageAsync(dataToSend));
    //}

    //public Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent)
    //{
    //  string targetModuleName = PE.Core.Modules.Events.Name;
    //  IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.AddMillEvent(millEvent));
    //}

    //public Task<SendOfficeResult<DataContractBase>> AssignRawMaterialQualityAsync(
    //  DCRawMaterialQuality dCRawMaterialQuality)
    //{
    //  string targetModuleName = PE.Core.Modules.Quality.Name;
    //  IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

    //  return HandleModuleSendMethod(targetModuleName, () => client.AssignRawMaterialQualityAsync(dCRawMaterialQuality));
    //}

    ////public async Task<SendOfficeResult<DataContractBase>> SendTriggerToWBMAsync(DCTriggerData dcTriggerData)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.WalkingBeamFurnace.Name;
    ////  IWalkingBeamFurnace client = InterfaceHelper.GetFactoryChannel<IWalkingBeamFurnace>(targetModuleName);

    ////  //call method on remote module
    ////  if ((client as IClientChannel).State == CommunicationState.Opened)
    ////  {
    ////    return await HandleModuleSendMethod(targetModuleName, () => client.ProcessTriggerAsync(dcTriggerData));
    ////  }
    ////  else
    ////  {
    ////    DataContractBase returnValue = new DataContractBase();

    ////    return new SendOfficeResult<DataContractBase>(returnValue, false);
    ////  }
    ////}

    ////public async Task<SendOfficeResult<DataContractBase>> SendTriggerToTRKAsync(DCTriggerData dcTriggerData)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.Tracking.Name;
    ////  ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

    ////  //call method on remote module
    ////  if ((client as IClientChannel).State == CommunicationState.Opened)
    ////  {
    ////    return await HandleModuleSendMethod(targetModuleName, () => client.ProcessTriggerAsync(dcTriggerData));
    ////  }
    ////  else
    ////  {
    ////    DataContractBase returnValue = new DataContractBase();

    ////    return new SendOfficeResult<DataContractBase>(returnValue, false);
    ////  }
    ////}

    ////public async Task<SendOfficeResult<DataContractBase>> SendTriggerToDLSAsync(DCTriggerData dcTriggerData)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.Delay.Name;
    ////  IDelay client = InterfaceHelper.GetFactoryChannel<IDelay>(targetModuleName);

    ////  if ((client as IClientChannel).State == CommunicationState.Opened)
    ////  {
    ////    return await HandleModuleSendMethod(targetModuleName, () => client.ProcessTrigger(dcTriggerData));
    ////  }
    ////  else
    ////  {
    ////    DataContractBase returnValue = new DataContractBase();

    ////    return new SendOfficeResult<DataContractBase>(returnValue, false);
    ////  }
    ////}
    ////public async Task<SendOfficeResult<DataContractBase>> SendTriggerToPPLAsync(DCTriggerData dcTriggerData)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.ProdPlaning.Name;
    ////  IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);
    ////  return await HandleModuleSendMethod(targetModuleName, () => client.ProcessTrigger(dcTriggerData));

    ////}

    ////public async Task<SendOfficeResult<DataContractBase>> ChangeMaterialStatus(DCNewMaterialStatus dataToSend)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.MvHistory.Name;
    ////  IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

    ////  return await HandleModuleSendMethod(targetModuleName, () => client.ChangeMaterialStatusAsync(dataToSend));
    ////}
    ////public async Task<SendOfficeResult<DataContractBase>> CreateProductAfterProductionEnd(DCMaterialProductionEnd dataToSend)
    ////{
    ////  string targetModuleName =  PE.Core.Modules.MvHistory.Name;
    ////  IMVHistory client = InterfaceHelper.GetFactoryChannel<IMVHistory>(targetModuleName);

    ////  return await HandleModuleSendMethod(targetModuleName, () => client.ProcessProductionEndAsync(dataToSend));
    ////}
  }
}
