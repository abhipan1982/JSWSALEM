using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class GrindingTurningController : BaseController
  {
    #region services

    private readonly IGrindingTurningService _grindingTurningService;

    #endregion

    #region ctor

    public GrindingTurningController(IGrindingTurningService service)
    {
      _grindingTurningService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.RSetStatusShort = ListValuesHelper.GetRollSetStatusShortList();
      ViewBag.RSetHistoryStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RollsAvail = ListValuesHelper.GetRollsReadyWithTypes();
      ViewBag.RollsetAvail = ListValuesHelper.GetRollsetEmpty();
      ViewBag.GroovesList = ListValuesHelper.GetGrooveTemplatesList();
      ViewBag.GrooveListShorts = ListValuesHelper.GetGrooveTemplatesShortList();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/GrindingTurning/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> ConfigRollSetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_ConfigRollSetPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> ConfirmRollSetDialog(long id)
    {
      ViewBag.GrooveConditionSelectList = new SelectList(ListValuesHelper.GetGrooveConditionEnum().ToList(), "Value", "Text");
      return PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_ConfirmRollSetPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> TurningInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_InfoPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> TurningForConfirmInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_ScheduledInfoPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> RollSetHistoryPopupDialog(long id)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(id);
      return PreparePopupActionResultFromVm(() => _grindingTurningService.GetRollSetHistoryActual(ModelState, id), "~/Views/Module/PE.Lite/GrindingTurning/_HistoryRollSetPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> GetRollSetHistoryById(long id)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(id);
      VM_RollSetTurningHistory model = _grindingTurningService.GetRollSetHistoryById(ModelState, id);
      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/GrindingTurning/_RollSetHistoryDetails.cshtml", model);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetPlannedRollsetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _grindingTurningService.GetPlannedRollsetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetScheduledRollsetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _grindingTurningService.GetScheduledRollsetList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfigRollSetSubmit(VM_RollsetDisplay viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.UpdateGroovesToRollSetDisplay(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfirmRollSetSubmit(VM_RollsetDisplay viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.ConfirmUpdateGroovesToRollSetDisplay(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> CancelRollset(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.CancelRollSetStatus(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrindingTurning, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> ConfirmRollset(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grindingTurningService.ConfirmRollSetStatus(ModelState, viewModel));
    }

    public JsonResult GetGrooveList()
    {
      return Json(_grindingTurningService.GetGrooveList());
    }

    public JsonResult GetGrooveTemplate(long id)
    {
      return Json(_grindingTurningService.GetGrooveTemplate(id));
    }
    #endregion
  }
}
