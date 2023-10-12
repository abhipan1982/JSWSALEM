using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class RecommendedActionController : BaseController
  {
    private readonly IRecommendedActionService _service;

    public RecommendedActionController(IRecommendedActionService service)
    {
      _service = service;
    }

    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetRecommendedActionList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetRecommendedActionsList(ModelState, request));
    //}

    //#endregion

    //#region View

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/RecommendedAction/Index.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  return PrepareActionResultFromVm(() => _service.GetRecommendedActionDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/RecommendedAction/_RecommendedActionBody.cshtml");
    //}

    //#endregion

    //#region Popups

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewRecommendedActionPopupAsync()
    //{
    //  ViewBag.RecommendedActionsList = ListValuesHelper.GetRecommendedActionsList();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyRecommendedAction(),
    //    "~/Views/Module/PE.Lite/RecommendedAction/_RecommendedActionAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyRecommendedActionPopupAsync(long id)
    //{
    //  ViewBag.RecommendedActionsList = ListValuesHelper.GetRecommendedActionsList();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetRecommendedAction(id),
    //    "~/Views/Module/PE.Lite/RecommendedAction/_RecommendedActionEdit.cshtml");
    //}

    //#endregion


    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertRecommendedAction(VM_RecommendedAction viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.InsertRecommendedAction(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateRecommendedAction(VM_RecommendedAction viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.UpdateRecommendedAction(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteRecommendedAction(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.DeleteRecommendedAction(ModelState, viewModel));
    //}

    //#endregion
  }
}
