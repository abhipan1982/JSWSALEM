using System;
using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class EventCalendarService : BaseService, IEventCalendarService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public EventCalendarService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public IEnumerable<VM_EventCalendar> GetEventCalendarData(long eventId, DateTime date)
    {
      List<VM_EventCalendar> result = new List<VM_EventCalendar>();
      IEnumerable<V_Event> list = _hmiContext.V_Events
        .AsNoTracking()
        .Where(x => x.EventTypeId == eventId && x.Month == date.Month && x.Year == date.Year).AsEnumerable();

      foreach (V_Event item in list)
      {
        result.Add(new VM_EventCalendar(item));
      }
      return result;
    }

    public IList<VM_EventCalendar_Scheduler> GetEventCalendarSchedulerData(long eventId, DateTime date)
    {
      IList<VM_EventCalendar_Scheduler> result = new List<VM_EventCalendar_Scheduler>();

      DateTime now = DateTime.Now;
      IList<V_Event> list;

      using (HmiContext ctx = new HmiContext())
      {

        if (now.Month == date.Month && now.Year == date.Year)
        {
          list = ctx.V_Events
          .AsNoTracking()
          .Where(x => x.EventTypeId == eventId && ((x.Month == date.Month && x.Year == date.Year) || x.EventEndTs == null)).ToList();
        } else {
          list = ctx.V_Events
          .AsNoTracking()
          .Where(x => x.EventTypeId == eventId && x.Month == date.Month && x.Year == date.Year).ToList();
        }



        foreach (V_Event item in list)
        {
          result.Add(new VM_EventCalendar_Scheduler(item));
        }
      }

      return result;
    }

    public DataSourceResult GetEventsByShiftId(ModelStateDictionary modelState, DataSourceRequest request, long shiftId)
    {
      EVTShiftCalendar shift = _peContext.EVTShiftCalendars.SingleOrDefault(x => x.ShiftCalendarId == shiftId);
      DateTime startTime = shift.StartTime ?? shift.PlannedStartTime;
      DateTime endTime = shift.EndTime ?? shift.PlannedEndTime;

      IQueryable<EVTEvent> result = _peContext.EVTEvents
        .Include(x => x.FKEventType)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKAsset)
        .Where(x => x.EventStartTs >= startTime && x.EventStartTs <= endTime);

      return result.ToDataSourceLocalResult(request, modelState, data => new VM_EventDetails(data));
    }

    public List<VM_EventList> GetEventTypeList()
    {
      List<VM_EventList> result = new List<VM_EventList>();
      IQueryable<EVTEventType> dbList = _peContext.EVTEventTypes.AsQueryable();
      foreach (EVTEventType item in dbList)
      {
        result.Add(new VM_EventList(item));
      }

      return result;
    }

    public DataSourceResult GetEventTypes(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      IQueryable<EVTEventType> eventsTypes = _peContext.EVTEventTypes.AsQueryable();
      return eventsTypes.ToDataSourceLocalResult(request, modelState, data => new VM_EventList(data));
    }

    public List<VM_ShiftDefinition> GetShiftDefinitions()
    {
      List<VM_ShiftDefinition> result = new List<VM_ShiftDefinition>();
      IQueryable<EVTShiftDefinition> dbList = _peContext.EVTShiftDefinitions.AsQueryable();
      foreach (EVTShiftDefinition item in dbList)
      {
        result.Add(new VM_ShiftDefinition(item));
      }

      return result;
    }

    public DataSourceResult GetShiftsList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<EVTShiftCalendar> shiftsList = _peContext.EVTShiftCalendars
        .Include(x => x.FKShiftDefinition)
        .Where(x => x.PlannedStartTime <= DateTime.Now)
        .AsQueryable();
      return shiftsList.ToDataSourceLocalResult(request, modelState, data => new VM_ShiftOverview(data));

    }
  }
}
