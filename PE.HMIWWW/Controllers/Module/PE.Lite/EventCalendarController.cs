using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  [SmfAuthorization(Constants.SmfAuthorization_Controller_EventCalendar,
    Constants.SmfAuthorization_Module_EventCalendar, RightLevel.View)]
  public class EventCalendarController : BaseController
  {
    private readonly IEventCalendarService _eventCalendarService;

    public EventCalendarController(IEventCalendarService service)
    {
      _eventCalendarService = service;
    }

    // GET: EventCalendar
    public ActionResult Index()
    {
      // return View("~/Views/Module/PE.Lite/EventCalendar/Index.cshtml", _eventCalendarService.GetShiftDefinitions());
      return View("~/Views/Module/PE.Lite/EventCalendar/EventCalendar.cshtml");
    }

    public ActionResult Events()
    {
      return View("~/Views/Module/PE.Lite/EventCalendar/EventList.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_EventCalendar,
      Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public ActionResult ElementDetails(long shiftId)
    {
      return PartialView("~/Views/Module/PE.Lite/EventCalendar/_EventDetails.cshtml", shiftId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EventCalendar,
      Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public Task<JsonResult> GetEventsByShiftId([DataSourceRequest] DataSourceRequest request, long shiftId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _eventCalendarService.GetEventsByShiftId(ModelState, request, shiftId));
    }

    public JsonResult GetEventCalendarSchedulerData([DataSourceRequest] DataSourceRequest request, long? eventId, DateTime? date)
    {
      if (eventId != null && date != null) {
        return Json(_eventCalendarService.GetEventCalendarSchedulerData(eventId.Value, date.Value).ToDataSourceResult(request));
      }
;      return Json(new List<VM_EventCalendar_Scheduler>().ToDataSourceResult(request));
    }

    public JsonResult EventCalendarData(long eventId, DateTime date)
    {
      IEnumerable<VM_EventCalendar> result = _eventCalendarService.GetEventCalendarData(eventId, date);
      return Json(result);
    }

    public JsonResult GetEventTypeList()
    {
      return Json(_eventCalendarService.GetEventTypeList());
    }

    public async Task<ActionResult> GetEventTypes([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _eventCalendarService.GetEventTypes(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_EventCalendar,
      Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public Task<JsonResult> GetShiftsList([DataSourceRequest] DataSourceRequest request)
    {
      //ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() => _eventCalendarService.GetShiftsList(ModelState, request));
    }
  }
}
