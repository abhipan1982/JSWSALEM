using System;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.QEX;
using PE.BaseModels.DataContracts.Internal.QTY;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.DataContracts.Internal.STP;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.BaseModels.DataContracts.Internal.YRD;
using PE.BaseModels.DataContracts.Internal.ZPC;
using PE.Core;
using PE.Interfaces.Modules;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.Models.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.DBA;

namespace PE.HMIWWW.Core.Communication
{
  public class HmiSendOffice : HmiSendOfficeBase
  {
    public static readonly int Timeout = 4000;

    static HmiSendOffice()
    {
    }

    #region HMI

    public static Task<SendOfficeResult<DataContractBase>> RequestLastMaterialPosition(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Hmiexe.Name;
      IHmi client = InterfaceHelper.GetFactoryChannel<IHmi>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.LastMaterialPositionRequestMessageAsync(dc));
    }

    #endregion

    #region Setup

    public static Task<SendOfficeResult<DataContractBase>> CreateSetupAsync(DCSetupListOfParameres dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateSetupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateSetupParametersAsync(DCSetupListOfParameres dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateSetupParametersAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateSetupValueAsync(DCSetupValue dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateSetupValueAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CopySetupAsync(DCSetupListOfParameres dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CopySetupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteSetupAsync(DCSetupListOfParameres dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteSetupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendSetupsToL1Async(DCCommonSetupStructure dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.SendSetupsToL1Async(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CalculateSetupAsync(DCCommonSetupStructure dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CalculateSetupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateSetupConfigurationAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateSetupConfigurationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> EditSetupConfigurationAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EditSetupConfigurationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteSetupConfigurationAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteSetupConfigurationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendSetupConfigurationAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.SendSetupConfigurationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CloneSetupConfigurationAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CloneSetupConfigurationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateSetupConfigurationVersionAsync(DCSetupConfiguration dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Setup.Name;
      ISetup client = InterfaceHelper.GetFactoryChannel<ISetup>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateSetupConfigurationVersionAsync(dc));
    }

    #endregion

    #region Adapter

    public static Task<SendOfficeResult<DataContractBase>> L1ScrapAction(DCL1ScrapData dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ProcessScrapMessageAsync(dc));
    }

    #endregion

    #region events

    public static Task<SendOfficeResult<DataContractBase>> UpdateEventCatalogueAsync(DCEventCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEventCatalogueAsync(dcDelayCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> AddEventCatalogueAsync(DCEventCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddEventCatalogueAsync(dcDelayCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> EditDefectAsync(DCDefect dcDefect)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EditDefectAsync(dcDefect));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteEventCatalogueAsync(
      DCEventCatalogue dcDelayCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteEventCatalogueAsync(dcDelayCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDelayAsync(DCDelay dcDelay)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateDelayAsync(dcDelay));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateDelayAsync(DCDelay dcDelay)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateDelayAsync(dcDelay));
    }

    public static Task<SendOfficeResult<DataContractBase>> DivideDelayAsync(DCDelayToDivide dcDelaytoDivide)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DivideDelayAsync(dcDelaytoDivide));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateEventCatalogueCategoryAsync(DCEventCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEventCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> AddEventCatalogueCategoryAsync(
      DCEventCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddEventCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteEventCatalogueCategoryAsync(
      DCEventCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteEventCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateEventCategoryGroupAsync(DCEventGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEventCategoryGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> AddEventCategoryGroupAsync(DCEventGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddEventCategoryGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteEventCategoryGroupAsync(DCEventGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteEventCategoryGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendUpdateDefectGroupAsync(DCDefectGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateDefectGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendAddDefectGroupAsync(DCDefectGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddDefectGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteDefectGroupAsync(DCDefectGroup dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteDefectGroupAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDefectCatalogueCategoryAsync(
      DCDefectCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateDefectCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendAddDefectCatalogueCategoryAsync(
      DCDefectCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddDefectCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteDefectCatalogueCategoryAsync(
      DCDefectCatalogueCategory dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteDefectCatalogueCategoryAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateShiftCalendarElementAsync(DCShiftCalendarElement dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateShiftCalendarElementAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteShiftCalendarElement(DCShiftCalendarId dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteShiftCalendarElement(dc));
    }

    public static Task<SendOfficeResult<DCShiftCalendarId>> InsertShiftCalendar(DCShiftCalendarElement dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertShiftCalendar(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> GenerateShiftCalendarForNextWeek(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.GenerateShiftCalendarForNextWeek(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEndOfWorkShop(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EndOfWorkShop(dc));
    }

    #endregion

    #region Historian

    public static Task<SendOfficeResult<DcRawMeasurementResponse>> GetRawMeasurementsAsync(DcAggregatedMeasurementRequest dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.L1Adapter.Name;
      IL1Adapter client = InterfaceHelper.GetFactoryChannel<IL1Adapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ProcessGetRawMeasurementsAsync(dc));
    }

    #endregion

    #region schedule

    public static Task<SendOfficeResult<DataContractBase>> MoveItemInScheduleAsync(DCWorkOrderToSchedule data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.MoveItemInScheduleAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddWorkOrderToScheduleAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdPlaning.Name;
      IProdPlaning client = InterfaceHelper.GetFactoryChannel<IProdPlaning>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RemoveItemFromScheduleAsync(data));
    }    
    
    public static Task<SendOfficeResult<DataContractBase>> EndOfWorkOrderAsync(WorkOrderId data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EndOfWorkOrderAsync(data));
    }

    #endregion

    #region prodManager

    public static Task<SendOfficeResult<DataContractBase>> SendCreateSteelgradeAsync(DCSteelgradeEXT dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateSteelgradeAsyncEXT(dcSteelgrade));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendSteelgradeAsync(DCSteelgradeEXT dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateSteelgradeAsyncEXT(dcSteelgrade));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteSteelgradeAsync(DCSteelgradeEXT dcSteelgrade)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteSteelgradeAsyncEXT(dcSteelgrade));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendScrapGroupAsync(DCScrapGroup dcScrapgroup)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateScrapGroupAsync(dcScrapgroup));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendCreateScrapGroupAsync(DCScrapGroup dcScrapgroup)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateScrapGroupAsync(dcScrapgroup));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteScrapGroupAsync(DCScrapGroup dcScrapgroup)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteScrapGroupAsync(dcScrapgroup));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendUpdateSteelFamilyAsync(DCSteelFamilyEXT dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateSteelFamilyAsyncEXT(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendCreateSteelFamilyAsync(DCSteelFamilyEXT dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateSteelFamilyAsyncEXT(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteSteelFamilyAsync(DCSteelFamilyEXT dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteSteelFamilyAsyncEXT(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendWorkOrderAsync(DCWorkOrderEXT dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      if (dcWorkOrder.WorkOrderId == 0)
      {
        return HandleHMISendMethod(targetModuleName, () => client.CreateWorkOrderAsyncEXT(dcWorkOrder));
      }

      return HandleHMISendMethod(targetModuleName, () => client.UpdateWorkOrderAsyncEXT(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateWorkOrderDefinitionAsync(
      DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.DBAdapter.Name;
      IDBAdapter client = InterfaceHelper.GetFactoryChannel<IDBAdapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateWorkOrderDefinitionAsyncEXT(dcWorkOrderDefinition));//AV@
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateWorkOrderDefinitionAsync(
      DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.DBAdapter.Name;
      IDBAdapter client = InterfaceHelper.GetFactoryChannel<IDBAdapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateWorkOrderDefinitionAsyncEXT(dcWorkOrderDefinition));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteWorkOrderDefinitionAsync(
      DCL3L2WorkOrderDefinitionMOD dcWorkOrderDefinition)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.DBAdapter.Name;
      IDBAdapter client = InterfaceHelper.GetFactoryChannel<IDBAdapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteWorkOrderDefinitionAsyncEXT(dcWorkOrderDefinition));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateMaterialAsync(DCMaterialEXT dcMaterial)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateMaterialAsyncEXT(dcMaterial));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateMaterialAsync(DCMaterialEXT dCMaterial)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateMaterialAsyncEXT(dCMaterial));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteWorkOrderAsync(DCWorkOrderEXT dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteWorkOrderAsyncEXT(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendCreateHeatAsync(DCHeatEXT dcHeat)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateHeatAsyncEXT(dcHeat));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEditHeatAsync(DCHeatEXT dcHeat)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EditHeatAsyncEXT(dcHeat));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendCreateMaterialCatalogueAsync(
      DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateMaterialCatalogueAsyncEXT(dcMaterialCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendMaterialCatalogueAsync(
      DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateMaterialCatalogueAsyncEXT(dcMaterialCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteMaterialCatalogueAsync(
      DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteMaterialCatalogueAsyncEXT(dcMaterialCatalogue));
    }
    //Avijit 17082023
    public static Task<SendOfficeResult<DataContractBase>> SendCreateProductCatalogueAsync(
      DCProductCatalogueEXT dcProductCatalogue)//Av@
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateProductCatalogueEXTAsync(dcProductCatalogue));
    }
    //Avijit 17082023


    //Av@210823
    public static Task<SendOfficeResult<DataContractBase>> SendProductCatalogueAsync(
      DCProductCatalogueEXT dcProductCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateProductCatalogueEXTAsync(dcProductCatalogue));
    }
    //Av@210823
    public static Task<SendOfficeResult<DataContractBase>> SendDeleteProductCatalogueAsync(
      DCProductCatalogueEXT dcProductCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteProductCatalogueEXTAsync(dcProductCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> EditMaterialNumberAsync(DCWorkOrderMaterials dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EditMaterialNumberAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendResetWorkOrderReportAsync(
      DCL2L3WorkOrderReport dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.DBAdapter.Name;
      IDBAdapter client = InterfaceHelper.GetFactoryChannel<IDBAdapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ResetWorkOrderReportAsync(dataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendResetProductReportAsync(
      DCL2L3ProductReport dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.DBAdapter.Name;
      IDBAdapter client = InterfaceHelper.GetFactoryChannel<IDBAdapter>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ResetProductReportAsync(dataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendWorkOrderReportAsync(DCWorkOrderConfirmation dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.SendWorkOrderReportAsync(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> AddTestScheduleAsync(DCTestSchedule dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddTestWorkOrderToScheduleAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleHMISendMethod(targetModuleName, () => client.UpdateCanceledWorkOrderAsync(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleHMISendMethod(targetModuleName, () => client.UpdateUnCanceledWorkOrderAsync(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleHMISendMethod(targetModuleName, () => client.UpdateBlockedWorkOrderAsync(dcWorkOrder));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock dcWorkOrder)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ProdManager.Name;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleHMISendMethod(targetModuleName, () => client.UpdateUnBlockedWorkOrderAsync(dcWorkOrder));
    }

    #endregion

    #region measured values history

    public static Task<SendOfficeResult<DataContractBase>> RejectRawMaterial(DCRejectMaterialData dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RejectRawMaterial(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> RemoveMaterialFromTracking(DCRemoveMaterial dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RemoveMaterialFromTracking(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> HardRemoveMaterialFromTracking(DCHardRemoveMaterial dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.HardRemoveMaterialFromTracking(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> MaterialReady(DCMaterialReady dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.MarkAsReady(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> ProductUndo(DCProductUndo dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ProductUndo(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateBundleAsync(DCBundleData dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateBundleAsync(dc));
    }

    #endregion

    #region Rollshop

    public static Task<SendOfficeResult<DataContractBase>> InsertRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertRollAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateRollAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> ScrapRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ScrapRollAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteRollAsync(DCRollData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteRollAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> InsertRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertRollTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateRollTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteRollTypeAsync(DCRollTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteRollTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> InsertGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertGrooveTemplateAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateGrooveTemplateAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteGrooveTemplateAsync(DCGrooveTemplateData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteGrooveTemplateAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> InsertRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertRollSetAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> AssembleRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AssembleRollSetAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateRollSetStatusAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> ConfirmRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ConfirmRollSetStatusAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DisassembleRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DisassembleRollSetAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteRollSetAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteRollSetAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> InsertCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertCassetteTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateCassetteTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteCassetteTypeAsync(DCCassetteTypeData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteCassetteTypeAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> InsertCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertCassetteAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateCassetteAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DeleteCassetteAsync(DCCassetteData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteCassetteAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> DismountCassetteAsync(DCCassetteData dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DismountCassetteAsync(dataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> RollChangeActionAsync(DCRollChangeOperationData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RollChangeActionAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> RollSetToCassetteAction(DCRollSetToCassetteAction data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RollSetToCassetteAction(data));
    }


    public static Task<SendOfficeResult<DataContractBase>> CancelRollSetStatusAsync(DCRollSetData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CancelRollSetStatusAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateGroovesToRollSetAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateGroovesDataToRollSetAsync(DCRollSetGrooveSetup dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateGroovesDataToRollSetAsync(dataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateGroovesStatusesAsync(DCRollSetGrooveSetup data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateGroovesStatusesAsync(data));
    }

    public static Task<SendOfficeResult<DataContractBase>> UpdateStandConfigurationAsync(DCStandConfigurationData data)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.RollShop.Name;
      IRollShop client = InterfaceHelper.GetFactoryChannel<IRollShop>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateStandConfigurationAsync(data));
    }

    #endregion

    #region Maintenance

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupCreateRequest(
      DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateEquipmentGroupAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupUpdateRequest(
      DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEquipmentGroupAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentGroupDeleteRequest(
      DCEquipmentGroup entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteEquipmentGroupAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentCreateRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateEquipmentAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentUpdateRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEquipmentAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentStatusUpdateRequest(
      DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateEquipmentStatusAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentDeleteRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteEquipmentAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendEquipmentCloneRequest(DCEquipment entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Maintenance.Name;
      IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CloneEquipmentAsync(entryDataContract));
    }

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertRecommendedAction(
    //  DCRecommendedAction entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertRecommendedActionAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateRecommendedAction(
    //  DCRecommendedAction entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateRecommendedActionAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteRecommendedAction(
    //  DCRecommendedAction entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteRecommendedActionAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertDeviceGroup(DCDeviceGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertDeviceGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateDeviceGroup(DCDeviceGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateDeviceGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteDeviceGroup(DCDeviceGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteDeviceGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertComponentGroup(DCComponentGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertComponentGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateComponentGroup(DCComponentGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateComponentGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteComponentGroup(DCComponentGroup entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteComponentGroupAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertIncidentType(DCIncidentType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertIncidentTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateIncidentType(DCIncidentType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateIncidentTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteIncidentType(DCIncidentType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteIncidentTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertActionType(DCActionType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertActionTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateActionType(DCActionType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateActionTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteActionType(DCActionType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteActionTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendInsertQuantityType(DCQuantityType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.InsertQuantityTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendUpdateQuantityType(DCQuantityType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.UpdateQuantityTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteQuantityType(DCQuantityType entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteQuantityTypeAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteAction(DCAction entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteActionAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteDevice(DCDevice entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteDeviceAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteIncident(DCIncident entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteIncidentAsync(entryDataContract));
    //}

    //public static Task<SendOfficeResult<DataContractBase>> SendDeleteComponent(DCComponent entryDataContract)
    //{
    //  //prepare target module name and interface
    //  string targetModuleName = Modules.Maintenance.Name;
    //  IMaintenance client = InterfaceHelper.GetFactoryChannel<IMaintenance>(targetModuleName);

    //  //call method on remote module
    //  return HandleHMISendMethod(targetModuleName, () => client.DeleteComponentAsync(entryDataContract));
    //}

    #endregion

    #region Quality

    public static Task<SendOfficeResult<DataContractBase>> DeleteDefectAsync(DCDefect entryDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteDefectAsync(entryDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendAddDefectCatalogue(DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AddDefectCatalogueAsync(dcDefectCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDefectCatalogue(DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateDefectCatalogueAsync(dcDefectCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteDefectCatalogue(
      DCDefectCatalogue dcDefectCatalogue)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteDefectCatalogueAsync(dcDefectCatalogue));
    }

    public static Task<SendOfficeResult<DataContractBase>> AssignQualityAsync(DCQualityAssignment dCQualityAssignment)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AssignQualityAsync(dCQualityAssignment));
    }

    public static Task<SendOfficeResult<DataContractBase>> AssignRawMaterialQualityAsync(
      DCRawMaterialQuality qualityDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AssignRawMaterialQualityAsync(qualityDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> AssignRawMaterialFinalQuality(
      DCRawMaterialQuality qualityDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AssignRawMaterialFinalQuality(qualityDataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> EditRawMaterialQualityAsync(
      DCRawMaterialQuality qualityDataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Quality.Name;
      IQuality client = InterfaceHelper.GetFactoryChannel<IQuality>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.EditRawMaterialQualityAsync(qualityDataContract));
    }

    #endregion

    #region QualityExpert

    public static Task<SendOfficeResult<DataContractBase>> ForceRatingValue(DCRatingForce dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.QualityExpert.Name;
      IQualityExpert client = InterfaceHelper.GetFactoryChannel<IQualityExpert>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ForceRatingValueAsync(dataContract));
    }

    public static Task<SendOfficeResult<DataContractBase>> ToggleCompensation(DCCompensationTrigger dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.QualityExpert.Name;
      IQualityExpert client = InterfaceHelper.GetFactoryChannel<IQualityExpert>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ToggleCompensationAsync(dataContract));
    }

    #endregion

    #region Label printer

    public static Task<SendOfficeResult<DCZebraPrinterResponse>> PrintLabel(DCZebraRequest dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ZebraPrinter.Name;
      IZebraPC client = InterfaceHelper.GetFactoryChannel<IZebraPC>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.PrintLabelAsync(dataContract));
    }

    public static Task<SendOfficeResult<DCZebraImageResponse>> RequestLabelPreview(DCZebraRequest dataContract)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.ZebraPrinter.Name;
      IZebraPC client = InterfaceHelper.GetFactoryChannel<IZebraPC>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RequestPreviewForHmiAsync(dataContract));
    }

    #endregion

    #region Tracking

    public static Task<SendOfficeResult<DataContractBase>> SendUpdateMaterialPositionInTracking(
      DCMoveMaterial materialPosition)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ReplaceMaterialPosition(materialPosition));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendMoveMaterialsInAreaUp(DCUpdateArea area)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CollectionMoveBackward(area));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendMoveMaterialsInAreaDown(DCUpdateArea area)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CollectionMoveForward(area));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFurnaceDischargeForRolling(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DischargeForRolling(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFurnaceUnDischargeFromRolling(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UnDischargeFromRolling(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFurnaceDischargeForReject(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.FurnaceDischargeForReject(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendChargingGridCharge(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ChargingGridCharge(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendChargingGridUnCharge(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ChargingGridUnCharge(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFurnaceCharge(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.FurnaceCharge(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFurnaceUnCharge(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.FurnaceUnCharge(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> TransferLayer(DataContractBase dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.TransferLayer(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendChargeMaterialOnFurnaceExitAsync(DCChargeMaterialOnFurnaceExit dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      // fill HMI related DC data
      //InitHmiDataContract(dataContract);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ChargeMaterialOnFurnaceExitAsyncAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendFinishLayerAsync(DCLayer dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      // fill HMI related DC data
      //InitHmiDataContract(dataContract);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.FinishLayerAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendTransferLayerAsync(DCLayer dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      // fill HMI related DC data
      //InitHmiDataContract(dataContract);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.TransferLayerAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendUnassignMaterial(DCMaterialUnassign dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UnassignMaterial(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendAssignMaterial(DCMaterialAssign dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Tracking.Name;
      ITracking client = InterfaceHelper.GetFactoryChannel<ITracking>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.AssignMaterial(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendDeleteCrew(DCCrewId dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DeleteCrew(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> SendInsertCrew(DCCrewElement dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.InsertCrew(dc));
    }    
    
    public static Task<SendOfficeResult<DataContractBase>> SendUpdateCrew(DCCrewElement dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Events.Name;
      IEvents client = InterfaceHelper.GetFactoryChannel<IEvents>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UpdateCrew(dc));
    }
    #endregion

    #region Yards

    public static Task<SendOfficeResult<DataContractBase>> TransferHeatIntoLocationAsync(DCTransferHeatLocation dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.TransferHeatIntoLocationAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> TransferHeatIntoChargingGridAsync(
      DCTransferHeatToChargingGrid dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.TransferHeatIntoChargingGridAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateMaterialInReceptionAsync(
      DCCreateMaterialInReception dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateMaterialInReceptionAsync(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> ScrapMaterials(DCScrapMaterial dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ScrapMaterials(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> UnscrapMaterials(DCUnscrapMaterial dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.UnscrapMaterials(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> CreateHeatWithMaterials(
      DCCreateMaterialWithHeatInReception dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.CreateHeatWithMaterials(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> DispatchWorkOrder(DCWorkOrderToDispatch dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.DispatchWorkOrder(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> RelocateProducts(DCProductRelocation dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.RelocateProducts(dc));
    }

    public static Task<SendOfficeResult<DataContractBase>> ReorderLocationSeq(DCProductYardLocationOrder dc)
    {
      //prepare target module name and interface
      string targetModuleName = Modules.Yards.Name;
      IYards client = InterfaceHelper.GetFactoryChannel<IYards>(targetModuleName);

      //call method on remote module
      return HandleHMISendMethod(targetModuleName, () => client.ReorderLocationSeq(dc));
    }

    #endregion
  }
}
