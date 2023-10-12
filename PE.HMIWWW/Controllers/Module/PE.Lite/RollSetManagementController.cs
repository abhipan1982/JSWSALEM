using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.BaseDbEntity.Models;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module
{
  public class RollSetManagementController : BaseController
  {
    #region services

    private readonly IRollSetManagementService _rollsetManagementService;

    #endregion

    #region ctor

    public RollSetManagementController(IRollSetManagementService service)
    {
      _rollsetManagementService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RollSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollSetManagement/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRollSetDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/RollSetManagement/_AddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> AssembleRollSetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_AssemblePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> DisassembleRollSetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_DisassemblePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditRollSetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsetManagementService.GetRollSet(ModelState, id), "~/Views/Module/PE.Lite/RollSetManagement/_EditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetRollsetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetManagementService.GetRollSetList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetScheduledRollsetData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsetManagementService.GetScheduledRollSetList(ModelState, request));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetRolls(string rollSetId)
    {
      return Json(RollSetManagementService.GetRolls(rollSetId));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetGrooveTemplates()
    {
      return Json(RollSetManagementService.GetGrooveTemplates());
    }

    public static List<RLSRollSet> GetEmptyRollsetList()
    {
      return RollSetManagementService.GetEmptyRollsetList();
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetRollsWithMaterials(string upperRollId)
    {
      return Json(RollSetManagementService.GetRollsWithMaterials(upperRollId));
    }

    #region Actions

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertRollSet(VM_RollSetOverviewFull viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.InsertRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteRollSet(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.DeleteRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateRollSetStatus(VM_RollSetOverviewFull viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.UpdateRollSetStatus(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> AssembleRollSet(VM_RollSetOverviewFull viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.AssembleRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> DisassembleRollSet(VM_RollSetOverviewFull viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.DisassembleRollSet(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> ConfirmAssembleRollSet(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.ConfirmRollSetStatus(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Module_RollShop, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> CancelAssembleRollSet(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsetManagementService.CancelRollSetStatus(ModelState, viewModel));
    }

    #endregion
  }
}
