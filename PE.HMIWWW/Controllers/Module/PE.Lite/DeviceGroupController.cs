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
  public class DeviceGroupController : BaseController
  {
    private readonly IDeviceGroupService _service;

    public DeviceGroupController(IDeviceGroupService service)
    {
      _service = service;
    }

    //#region Views

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/DeviceGroup/Index.cshtml");
    //}

    //#endregion


    //#region Data

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetDeviceGroupList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetDeviceGroupList(ModelState, request));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PrepareActionResultFromVm(() => _service.GetDeviceGroupDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/DeviceGroup/_DeviceGroupBody.cshtml");
    //}

    //#endregion

    //#region Popups

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewDeviceGroupPopupAsync()
    //{
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyDeviceGroup(),
    //    "~/Views/Module/PE.Lite/DeviceGroup/_DeviceGroupAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyDeviceGroupDetailsPopupAsync(long id)
    //{
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  return PreparePopupActionResultFromVm(() => _service.GetDeviceGroup(id),
    //    "~/Views/Module/PE.Lite/DeviceGroup/_DeviceGroupEdit.cshtml");
    //}

    //#endregion

    //#region Actions

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> InsertDeviceGroup(VM_DeviceGroup viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(
    //    () => _service.InsertDeviceGroup(ModelState, viewModel));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> UpdateDeviceGroup(VM_DeviceGroup viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(
    //    () => _service.UpdateDeviceGroup(ModelState, viewModel));
    //}

    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteDeviceGroup(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(
    //    () => _service.DeleteDeviceGroup(ModelState, viewModel));
    //}

    //#endregion
  }
}
