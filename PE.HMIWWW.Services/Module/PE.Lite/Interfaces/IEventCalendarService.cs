using System;
using System.Collections.Generic;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEventCalendarService
  {
    IEnumerable<VM_EventCalendar> GetEventCalendarData(long eventId, DateTime date);
    IList<VM_EventCalendar_Scheduler> GetEventCalendarSchedulerData(long eventId, DateTime date);
    List<VM_EventList> GetEventTypeList();
    DataSourceResult GetEventTypes(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    List<VM_ShiftDefinition> GetShiftDefinitions();
    DataSourceResult GetEventsByShiftId(ModelStateDictionary modelState, DataSourceRequest request, long shiftId);
    DataSourceResult GetShiftsList(ModelStateDictionary modelState, DataSourceRequest request);
  }
}
