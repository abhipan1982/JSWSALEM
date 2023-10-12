using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using PE.HMIWWW.ViewModel.Module.Lite.Event;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDelaysService
  {
    VM_EventCatalogue GetEventCatalogue(ModelStateDictionary modelState, long id);

    Task<DataSourceResult> GetEventCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);
    
    Task<DataSourceResult> GetEventCatalogueListByEventData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, short eventTypeCode, string eventCategoryGroupCode, string eventCatalogueCategoryCode);

    Task<VM_Base> AddEventCatalogueAsync(ModelStateDictionary modelState, VM_EventCatalogue eventCatalogue);
    Task<VM_Base> UpdateEventCatalogueAsync(ModelStateDictionary modelState, VM_EventCatalogue eventCatalogue);
    Task<VM_Base> DeleteEventCatalogueAsync(ModelStateDictionary modelState, VM_EventCatalogue eventCatalogue);
    IList<VM_EventCatalogueCategory> GetEventCategories();

    IList<VM_EventCatalogue> GetEventCataloguesForParentSelector();

    VM_Delay GetDelay(ModelStateDictionary modelState, long id);
    VM_DelayDivision GetDelayDivision(ModelStateDictionary modelState, long id);
    DataSourceResult GetDelayList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> UpdateDelayAsync(ModelStateDictionary modelState, VM_Delay delay);
    Task<VM_Base> CreateDelayAsync(ModelStateDictionary modelState, VM_Delay delay);
    IList<VM_EventCatalogue> GetEventCatalogue();
    IList<VM_EventCatalogueCategory> GetEventCatalogueCategory();
    IList<VM_Asset> GetAssets();
    IList<VM_ShiftWorkOrderSimpleData> GetWorkOrders();
    Task<VM_Base> DivideDelayAsync(ModelStateDictionary modelState, VM_DelayDivision delay);

    DataSourceResult GetDelayBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime);

    DataSourceResult GetDelaysPlannedBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime);

    DataSourceResult GetDelaysUnplannedBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime);


    Dictionary<string, int>[] GetDelaysSummary(DateTime startDateTime, DateTime endDateTime);
    Task<bool> ValidateEventCode(string code);
    Task<bool> ValidateEventName(string name);
    DataSourceResult GetPlannedDelays(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    Task<IList<VM_Delay>> GetUpcomingPlannedDelays(ModelStateDictionary modelState);

    DataSourceResult GetDelaysOverviewByShiftIdAndWorkOrderId(ModelStateDictionary modelState,
      DataSourceRequest request, VM_ShiftWorkOrderModel model);

    DataSourceResult GetActiveDelay(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    DataSourceResult GetEventCatalogueSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    List<short> GetParentEventCodes();
    Task<IList<VM_EventCatalogue>> FilterEventCatalogue(long? eventCatalogueCategoryId);
  }
}
