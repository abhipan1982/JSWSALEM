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
  public class ActionTypeController : BaseController
  {
    private readonly IActionTypeService _service;

    public ActionTypeController(IActionTypeService service)
    {
      _service = service;
    }

    //#region View

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/ActionType/Index.cshtml");
    //}

    //#endregion

    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetActionTypeList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetActionTypeList(ModelState, request));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  return PrepareActionResultFromVm(() => _service.GetActionTypeDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/ActionType/_ActionTypeBody.cshtml");
    //}

    //#endregion

    //#region Popups

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewActionTypePopupAsync()
    //{
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyActionType(),
    //    "~/Views/Module/PE.Lite/ActionType/_ActionTypeAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyActionTypePopupAsync(long id)
    //{
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PreparePopupActionResultFromVm(() => _service.GetActionType(id),
    //    "~/Views/Module/PE.Lite/ActionType/_ActionTypeEdit.cshtml");
    //}

    //#endregion

    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertActionType(VM_ActionType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.InsertActionType(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateActionType(VM_ActionType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.UpdateActionType(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteActionType(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.DeleteActionType(ModelState, viewModel));
    //}

    //#endregion

    //#region Views
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public ActionResult Index()
    //{
    //	ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //	return View("~/Views/Module/PE.Lite/ActionType/Index.cshtml");
    //}
    //#endregion

    //#region Actions
    //#endregion

    //#region Data
    //#endregion

    //#region Popups
    //#endregion
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public Task<JsonResult> GetIncidentTypeList([DataSourceRequest] DataSourceRequest request)
    //{
    //	return PrepareJsonResultFromDataSourceResult(() => _service.GetIncidentTypeList(ModelState, request));
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    //public Task<ActionResult> AddNewIncidentTypePopupAsync()
    //{
    //	return PreparePopupActionResultFromVm(() => _service.GetEmptyIncidentType(), "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeAdd.cshtml");
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    //public Task<ActionResult> ModifyIncidentTypePopupAsync(long id)
    //{
    //	return PreparePopupActionResultFromVm(() => _service.GetIncidentType(id), "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeEdit.cshtml");
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    //public Task<ActionResult> AddNewRecommendedActionPopupAsync()
    //{
    //	ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //	ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //	ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //	return PreparePopupActionResultFromVm(() => _service.GetEmptyRecommendedAction(), "~/Views/Module/PE.Lite/IncidentType/RecommendedActions/_RecommendedActionAdd.cshtml");
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    //public Task<ActionResult> ModifyRecommendedActionPopupAsync(long id)
    //{
    //	ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //	ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //	ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //	return PreparePopupActionResultFromVm(() => _service.GetRecommendedAction(id), "~/Views/Module/PE.Lite/IncidentType/RecommendedActions/_RecommendedActionEdit.cshtml");
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long ElementId)
    //{
    //	ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //	ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //	ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //	return PrepareActionResultFromVm(() => _service.GetIncidentTypeDetails(ModelState, ElementId), "~/Views/Module/PE.Lite/IncidentType/_IncidentTypeBody.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public Task<JsonResult> GetRecommendedActionsList([DataSourceRequest] DataSourceRequest request, long incidentTypeId)
    //{
    //	ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //	ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //	return PrepareJsonResultFromDataSourceResult(() => _service.GetRecommendedActionsList(ModelState, request, incidentTypeId));
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    //public Task<JsonResult> GetAllIncidentsForTypeList([DataSourceRequest] DataSourceRequest request, long incidentTypeId)
    //{
    //	ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //	ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //	return PrepareJsonResultFromDataSourceResult(() => _service.GetAllIncidentsForTypeList(ModelState, request, incidentTypeId));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    //public Task<JsonResult> DeleteRecommendedAction(VM_LongId viewModel)
    //{
    //	return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteRecommendedAction(ModelState, viewModel));
    //}
  }
}
