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
  public class IncidentTypeController : BaseController
  {
    private readonly IIncidentTypeService _service;

    public IncidentTypeController(IIncidentTypeService service)
    {
      _service = service;
    }

    //#region Views

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/IncidentType/Index.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.SeverityStatusesList = ListValuesHelper.GetSeverityStatuses();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PrepareActionResultFromVm(() => _service.GetIncidentTypeDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeBody.cshtml");
    //}

    //#endregion

    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetIncidentTypeList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetIncidentTypeList(ModelState, request));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetRecommendedActionsList([DataSourceRequest] DataSourceRequest request,
    //  long incidentTypeId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() =>
    //    _service.GetRecommendedActionsList(ModelState, request, incidentTypeId));
    //}

    //#endregion

    //#region Popups

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewIncidentTypePopupAsync()
    //{
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyIncidentType(),
    //    "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyIncidentTypePopupAsync(long id)
    //{
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.SeverityStatusesList = ListValuesHelper.GetSeverityStatuses();
    //  return PreparePopupActionResultFromVm(() => _service.GetIncidentType(id),
    //    "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeEdit.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewRecommendedActionPopupAsync()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyRecommendedAction(),
    //    "~/Views/Module/PE.Lite/IncidentType/RecommendedActions/_RecommendedActionAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyRecommendedActionPopupAsync(long id)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetRecommendedAction(id),
    //    "~/Views/Module/PE.Lite/IncidentType/RecommendedActions/_RecommendedActionEdit.cshtml");
    //}

    //#endregion

    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertIncidentType(VM_IncidentType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.InsertIncidentType(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateIncidentType(VM_IncidentType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.UpdateIncidentType(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteIncidentType(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.DeleteIncidentType(ModelState, viewModel));
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
