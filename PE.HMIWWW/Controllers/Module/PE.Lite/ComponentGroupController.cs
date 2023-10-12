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
  public class ComponentGroupController : BaseController
  {
    private readonly IComponentGroupService _service;

    public ComponentGroupController(IComponentGroupService service)
    {
      _service = service;
    }

    //#region Views

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/ComponentGroup/Index.cshtml");
    //}

    //#endregion

    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetComponentGroupList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetComponentGroupList(ModelState, request));
    //}

    //#endregion

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewComponentGroupPopupAsync()
    //{
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyComponentGroup(),
    //    "~/Views/Module/PE.Lite/ComponentGroup/_ComponentGroupAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyComponentGroupPopupAsync(long id)
    //{
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetComponentGroup(id),
    //    "~/Views/Module/PE.Lite/ComponentGroup/_ComponentGroupEdit.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  return PrepareActionResultFromVm(() => _service.GetComponentGroupDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/ComponentGroup/_ComponentGroupBody.cshtml");
    //}

    //#region Popups

    //#endregion

    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertComponentGroup(VM_ComponentGroup viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.InsertComponentGroup(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateComponentGroup(VM_ComponentGroup viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.UpdateComponentGroup(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteComponentGroup(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
    //    _service.DeleteComponentGroup(ModelState, viewModel));
    //}

    //#endregion
  }
}
