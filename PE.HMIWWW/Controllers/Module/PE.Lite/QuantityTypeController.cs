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
  public class QuantityTypeController : BaseController
  {
    private readonly IQuantityTypeService _service;

    public QuantityTypeController(IQuantityTypeService service)
    {
      _service = service;
    }

    //#region View

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/QuantityType/Index.cshtml");
    //}

    //#endregion

    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetQuantityTypeList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetQuantityTypeList(ModelState, request));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  return PrepareActionResultFromVm(() => _service.GetQuantityTypeDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/QuantityType/_QuantityTypeBody.cshtml");
    //}

    //#endregion

    //#region Popups

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewQuantityTypePopupAsync()
    //{
    //  ViewBag.UnitsList = ListValuesHelper.GetUnitsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyQuantityType(),
    //    "~/Views/Module/PE.Lite/QuantityType/_QuantityTypeAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyQuantityTypePopupAsync(long id)
    //{
    //  ViewBag.UnitsList = ListValuesHelper.GetUnitsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetQuantityType(id),
    //    "~/Views/Module/PE.Lite/QuantityType/_QuantityTypeEdit.cshtml");
    //}

    //#endregion

    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertQuantityType(VM_QuantityType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.InsertQuantityType(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateQuantityType(VM_QuantityType viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.UpdateQuantityType(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteQuantityType(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.DeleteQuantityType(ModelState, viewModel));
    //}

    //#endregion
  }
}
