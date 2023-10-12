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
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CassetteTypeController : BaseController
  {
    #region services

    private readonly ICassetteTypeService _cassetteTypeService;

    #endregion

    #region ctor

    public CassetteTypeController(ICassetteTypeService service)
    {
      _cassetteTypeService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/CassetteType/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCassetteTypeDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/CassetteType/_AddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditCassetteTypeDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _cassetteTypeService.GetCassetteType(ModelState, id), "~/Views/Module/PE.Lite/CassetteType/_EditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetCassetteTypeData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _cassetteTypeService.GetCassetteTypeList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertCassetteType(VM_CassetteType viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.InsertCassetteType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteCassetteType(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.DeleteCassetteType(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CassetteType, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateCassetteType(VM_CassetteType viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteTypeService.UpdateCassetteType(ModelState, viewModel));
    }
    #endregion
  }
}
