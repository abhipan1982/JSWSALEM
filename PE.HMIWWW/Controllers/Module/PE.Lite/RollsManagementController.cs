using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module
{
  public class RollsManagementController : BaseController
  {
    #region services

    private readonly IRollsManagementService _rollsManagementService;

    #endregion

    #region ctor

    public RollsManagementController(IRollsManagementService service)
    {
      _rollsManagementService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RollTypes = ListValuesHelper.GetRollWithTypes();
      ViewBag.RollStatus = ListValuesHelper.GetRollStatus();
      ViewBag.RollScrapReasons = ListValuesHelper.GetRollScrapReasons();

      ViewBag.WarningThreshold = GetParameter("RollWearWarningThreshold");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollsManagement/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> AddRollDialog()
    {
      ViewBag.Limits = await _rollsManagementService.GetRollLimits();
      return PartialView("~/Views/Module/PE.Lite/RollsManagement/_AddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditRollDialog(long id)
    {
      ViewBag.Limits = _rollsManagementService.GetRollLimits();
      return PreparePopupActionResultFromVm(() => _rollsManagementService.GetRoll(ModelState, id), "~/Views/Module/PE.Lite/RollsManagement/_EditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> ScrapRollDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollsManagementService.GetRoll(ModelState, id), "~/Views/Module/PE.Lite/RollsManagement/_ScrapPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetRollData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollsManagementService.GetRollsList(ModelState, request));
    }
    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertRoll(VM_RollsWithTypes viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.InsertRoll(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteRoll(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.DeleteRoll(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateRoll(VM_RollsWithTypes viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.UpdateRoll(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollsManagement, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> ScrapRoll(VM_RollsWithTypes viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollsManagementService.ScrapRoll(ModelState, viewModel));
    }
    #endregion
  }
}
