using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.BaseDbEntity.Models;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;
using PE.HMIWWW.ViewModel.System;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Controllers.System
{
  public class ShiftCalendarController : BaseController
  {
    #region services

    private readonly IShiftCalendarService _shiftCalendarService;

    #endregion

    #region ctor

    public ShiftCalendarController(IShiftCalendarService service)
    {
      _shiftCalendarService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
      
      ViewBag.ShiftLayouts = ListValuesHelper.GetShiftLayouts();
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> ShiftCalendarData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _shiftCalendarService.GetShiftCalendarsList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> UpdateShiftCalendarElement(VM_ShiftCalendarElement viewModel)
    {
      return PrepareJsonResultFromVmAsync(() =>
        _shiftCalendarService.UpdateShiftCalendarElement(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteShiftCalendarElement([DataSourceRequest] DataSourceRequest request,
      VM_LongId viewModel)
    {
      return PrepareJsonResultFromVmAsync(() =>
        _shiftCalendarService.DeleteShiftCalendarElement(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> GenerateShiftCalendarsForNextWeek()
    {
      return PrepareJsonResultFromVmAsync(() => _shiftCalendarService.GenerateShiftCalendarForNextWeek(ModelState));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> EditShiftCalendarDialog(VM_LongId viewModel)
    {
      return PreparePopupActionResultFromVm(
        () => _shiftCalendarService.GetShiftCalendarElement(ModelState,
          new VM_ShiftCalendarElement {ShiftCalendarId = viewModel.Id}), "EditShiftCalendarDialog");
    }

    public static List<EVTShiftDefinition> GetShiftDefinitionsList()
    {
      return ShiftCalendarService.GetShiftDefinitionsList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public ActionResult AddShiftCalendarDialog(VM_StringId viewModel)
    {
      VM_ShiftCalendarElement vModel = new VM_ShiftCalendarElement();
      vModel.Start = DateTime.Parse(viewModel.Id);
      vModel.End = DateTime.Parse(viewModel.Id);

      return PartialView("AddShiftCalendarDialog", vModel);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> InsertShiftCalendar(VM_ShiftCalendarElement viewModel)
    {
      return PrepareJsonResultFromVmAsync(() => _shiftCalendarService.InsertShiftCalendar(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> PrepareCrewData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _shiftCalendarService.PrepareCrewData(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
  RightLevel.Update)]
    public ActionResult GenerateShiftDialog()
    {
      VM_ShiftGenerate vModel = new VM_ShiftGenerate();

      vModel.From = DateTime.Today.AddDays(1);
      vModel.To = vModel.From;

      return PartialView("_GenerateShiftDialog", vModel);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
    RightLevel.View)]
   public ActionResult GenerateShiftLayoutForms(IList<VM_ShiftDay> list)
    {
      return PartialView("_ShiftDayForm", list);
    }
    
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ShiftCalendar, Constants.SmfAuthorization_Module_System,
     RightLevel.Update)]
    public async Task<ActionResult> GenerateShifts(IList<VM_ShiftDay> list)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _shiftCalendarService.GenerateShifts(ModelState, list));
    }

  }
}
