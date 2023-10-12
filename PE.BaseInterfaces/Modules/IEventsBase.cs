using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IEventsBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventCatalogueAsync(DCEventCatalogue delayCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventCatalogueAsync(DCEventCatalogue dcDelayCatalogue);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventCatalogueAsync(DCEventCatalogue dcDelayCatalogue);

    [OperationContract]
    Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent);

    [OperationContract]
    Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayAsync(DCDelay delay);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateDelayAsync(DCDelay delay);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delay);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateShiftCalendarElementAsync(DCShiftCalendarElement dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteShiftCalendarElement(DCShiftCalendarId dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCShiftCalendarId> InsertShiftCalendar(DCShiftCalendarElement dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> GenerateShiftCalendarForNextWeek(DataContractBase dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventCatalogueCategoryAsync(DCEventCatalogueCategory dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventCatalogueCategoryAsync(DCEventCatalogueCategory dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventCatalogueCategoryAsync(DCEventCatalogueCategory dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateEventCategoryGroupAsync(DCEventGroup dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddEventCategoryGroupAsync(DCEventGroup dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteEventCategoryGroupAsync(DCEventGroup dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddMillEvent(DCMillEvent dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EndOfWorkShop(DataContractBase dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteCrew(DCCrewId dc);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> InsertCrew(DCCrewElement dc);    

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateCrew(DCCrewElement dc);
  }
}
