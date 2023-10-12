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
using PE.HMIWWW.ViewModel.Module.Lite.RollType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RollTypeController : BaseController
  {
    #region services

    private readonly IRollTypeService _rollTypeService;

    #endregion

    #region ctor

    public RollTypeController(IRollTypeService service)
    {
      _rollTypeService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/RollType/Index.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddRollTypeDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/RollType/_AddPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditRollTypeDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _rollTypeService.GetRollType(ModelState, id), "~/Views/Module/PE.Lite/RollType/_EditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetRollTypeData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollTypeService.GetRollTypeList(ModelState, request));
    }
    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertRollType(VM_RollType viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.InsertRollType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteRollType(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.DeleteRollType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollTypes, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateRollType(VM_RollType viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _rollTypeService.UpdateRollType(ModelState, viewModel));
    }
    #endregion
  }
}
