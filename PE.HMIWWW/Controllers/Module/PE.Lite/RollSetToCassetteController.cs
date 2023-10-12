using System.Collections.Generic;
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
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System,
    RightLevel.View)]
  public class RollsetToCassetteController : BaseController
  {
    #region services

    private readonly IRollsetToCassetteService _rollsetToCassetteService;

    #endregion

    #region ctor

    public RollsetToCassetteController(IRollsetToCassetteService service)
    {
      _rollsetToCassetteService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.Arrang = ListValuesHelper.GetCassetteArrangement();
      ViewBag.RollTypes = ListValuesHelper.GetRollWithTypes();
      ViewBag.RSetHistoryStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.CassReadyNew = ListValuesHelper.GetCassetteReadyNewWithInitValue();
      ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.CassType = ListValuesHelper.GetCassetteType();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteType();
      ViewBag.CassetteStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.GrooveConditionSelectList = new SelectList(ListValuesHelper.GetGrooveConditionEnum().ToList(), "Value", "Text");
    }

    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollSetToCassette/Index.cshtml");
    }

    public JsonResult Filtering_RollSets(string text, long cassetteTypeId)
    {
      IList<VM_RollSetShort> rollSets = _rollsetToCassetteService.GetRollSetListByText(text, cassetteTypeId);
      return Json(rollSets);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditGroovesForRollSetDialog(long rollsetId)
    {
      ViewBag.GrooveConditionSelectList = new SelectList(ListValuesHelper.GetGrooveConditionEnum().ToList(), "Value", "Text");
      return PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetRollSetDisplay(ModelState, rollsetId), "~/Views/Module/PE.Lite/RollSetToCassette/_EditGroovesForRollSetPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> EditGroovesForRollSet(VM_RollsetDisplay viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.EditGroovesForRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> AssembleCassetteAndRollsetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetCassetteOverviewWithPositions(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_AssemblePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> CassetteInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteInfoCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> RollSetInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetToCassetteService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/RollSetToCassette/_RollSetToCassetteInfoRollSetPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetAvailableCassettesData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableCassettesList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetAvailableInterCassettesData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableInterCassettesList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetAvailableRollSetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetAvailableRollSetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetScheduledRollSetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetScheduledRollSetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetReadyRollSetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetToCassetteService.GetReadyRollSetList(ModelState, request));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetCassetteRSWith3RollsList([DataSourceRequest] DataSourceRequest request, long cassetteType, short rollsetType)
    {
      return Json(RollsetToCassetteService.GetCassetteRSWith3RollsList(cassetteType));
    }

    #region Actions

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> ConfirmRsReadyForMounting(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.ConfirmRsReadyForMounting(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> AssembleRollSetAndCassette(VM_CassetteOverviewWithPositions viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.AssembleRollSetAndCassette(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UnloadRollset(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.UnloadRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetToCassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> CancelPlan(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetToCassetteService.CancelPlan(ModelState, viewModel));
    }

    #endregion
  }
}
