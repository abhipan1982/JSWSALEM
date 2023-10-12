using System;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.System;
using Kendo.Mvc.Extensions;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MaintenanceController : BaseController
  {
    private readonly IMaintenanceService _service;

    public MaintenanceController(IMaintenanceService service)
    {
      _service = service;
    }

    //#region Views

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult Index()
    //{
    //  ViewBag.AssetTreeData = _service.GetAssetTreeData();
    //  ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/Maintenance/Index.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public ActionResult IndexInventory()
    //{
    //  ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return View("~/Views/Module/PE.Lite/Maintenance/IndexInventory.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ComponentElementDetails(long componentId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PrepareActionResultFromVm(() => _service.GetComponentDetails(ModelState, componentId),
    //    "~/Views/Module/PE.Lite/Maintenance/_ComponentBody.cshtml");
    //}

    //public ActionResult GetAssetTreeListData([DataSourceRequest] DataSourceRequest request)
    //{
    //  TreeDataSourceResult result = _service.GetAssetTreeListData().ToTreeDataSourceResult(request,
    //    e => e.Id,
    //    e => e.ParentId,
    //    e => e
    //  );

    //  return Json(result);

    //  //return Json(_service.GetAssetTreeData2(true));
    //}

    //public ActionResult GetAssetOnlyTreeListData([DataSourceRequest] DataSourceRequest request)
    //{
    //  TreeDataSourceResult result = _service.GetAssetOnlyTreeListData().ToTreeDataSourceResult(request,
    //    e => e.Id,
    //    e => e.ParentId,
    //    e => e
    //  );

    //  return Json(result);
    //}

    //public ActionResult GetAllComponentsForDeviceTreeList([DataSourceRequest] DataSourceRequest request, long deviceId)
    //{
    //  TreeDataSourceResult result = _service.GetComponentTreeListData(deviceId).ToTreeDataSourceResult(request,
    //    e => e.Id,
    //    e => e.ParentId,
    //    e => e
    //  );

    //  return Json(result);

    //  //return Json(_service.GetAssetTreeData2(true));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetails(long elementId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.SeverityStatusesList = ListValuesHelper.GetSeverityStatuses();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.DeviceStatuses = ListValuesHelper.GetDeviceStatuses();
    //  return PrepareActionResultFromVm(() => _service.GetMaintenanceDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/Maintenance/InstalledView/_MaintenanceBody.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetailsInventory(long elementId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.SeverityStatusesList = ListValuesHelper.GetSeverityStatuses();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.DeviceStatuses = ListValuesHelper.GetDeviceStatuses();
    //  return PrepareActionResultFromVm(() => _service.GetMaintenanceDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/Maintenance/InventoryView/_MaintenanceBody.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ElementDetailsEmpty(long elementId)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.IncidentTypesList = ListValuesHelper.GetIncidentTypesList();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PrepareActionResultFromVm(() => _service.GetMaintenanceDetails(ModelState, elementId),
    //    "~/Views/Module/PE.Lite/Maintenance/_MaintenanceDetailsEmpty.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ShowIncidentDetails(long id)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.ActionTypesList = ListValuesHelper.GetActionTypesList();
    //  return PrepareActionResultFromVm(() => _service.GetIncidentDetails(ModelState, id),
    //    "~/Views/Module/PE.Lite/Maintenance/_IncidentBody.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ShowDeviceGroupDetails(string name)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PrepareActionResultFromVm(() => _service.GetDeviceGroupDetails(ModelState, name),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceGroupBody.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<ActionResult> ShowDeviceGroupDetailsById(long id)
    //{
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PrepareActionResultFromVm(() => _service.GetDeviceGroupDetailsById(ModelState, id),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceGroupBody.cshtml");
    //}

    //#endregion

    //#region Popups

    ////Device
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddDevicePopupAsync()
    //{
    //  ViewBag.DeviceStatusesList = ListValuesHelper.GetDeviceStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.AssetTreeData = _service.GetAssetTreeData();
    //  ViewBag.AssetTreeWithoutDevicesData = _service.GetAssetTreeData(false);
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyDevice(),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> EditDevicePopupAsync(long id)
    //{
    //  ViewBag.DeviceStatusesList = ListValuesHelper.GetDeviceStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.AssetTreeData = _service.GetAssetTreeData();
    //  ViewBag.AssetTreeWithoutDevicesData = _service.GetAssetTreeData(false);
    //  return PreparePopupActionResultFromVm(() => _service.GetDevice(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceEdit.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> InstallDevicePopupAsync(long id)
    //{
    //  ViewBag.DeviceStatusesList = ListValuesHelper.GetDeviceStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.AssetTreeData = _service.GetAssetTreeData();
    //  ViewBag.AssetTreeWithoutDevicesData = _service.GetAssetTreeData(false);
    //  return PreparePopupActionResultFromVm(() => _service.GetDevice(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceInstall.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> UninstallDevicePopupAsync(long id)
    //{
    //  ViewBag.DeviceStatusesList = ListValuesHelper.GetDeviceStatuses();
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.DeviceGroupsList = ListValuesHelper.GetDeviceGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  ViewBag.AssetTreeData = _service.GetAssetTreeData();
    //  ViewBag.AssetTreeWithoutDevicesData = _service.GetAssetTreeData(false);
    //  return PreparePopupActionResultFromVm(() => _service.GetDevice(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_DeviceUninstall.cshtml");
    //}

    ////Incident
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddNewIncidentPopupAsync()
    //{
    //  ViewBag.IncidentsTypeList = _service.GetIncidentsTypeList();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyIncident(),
    //    "~/Views/Module/PE.Lite/Maintenance/_IncidentAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyIncidentDetailsPopupAsync(long id)
    //{
    //  ViewBag.IncidentsTypeList = _service.GetIncidentsTypeList();
    //  return PreparePopupActionResultFromVm(() => _service.GetIncident(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_IncidentModify.cshtml");
    //}
    ////Component

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddComponentPopupAsync()
    //{
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyComponent(),
    //    "~/Views/Module/PE.Lite/Maintenance/_ComponentAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyComponentPopupAsync(long id)
    //{
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PreparePopupActionResultFromVm(() => _service.GetComponent(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_ComponentModify.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> AddActionPopupAsync()
    //{
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PreparePopupActionResultFromVm(() => _service.GetEmptyAction(),
    //    "~/Views/Module/PE.Lite/Maintenance/_ActionAdd.cshtml");
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.Update)]
    //public Task<ActionResult> ModifyActionPopupAsync(long id)
    //{
    //  ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    //  ViewBag.ComponentGroupsList = ListValuesHelper.GetComponentGroupsList();
    //  ViewBag.SuppliersList = ListValuesHelper.GetSupplierList();
    //  ViewBag.NoYes = ListValuesHelper.GetTextFromYesNoStatus();
    //  return PreparePopupActionResultFromVm(() => _service.GetAction(id),
    //    "~/Views/Module/PE.Lite/Maintenance/_ActionModify.cshtml");
    //}

    //#endregion

    //#region Data

    //public ActionResult ComponentSchema(long deviceId)
    //{
    //  return Json(_service.ComponentsInDevice(deviceId));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetSubComponents([DataSourceRequest] DataSourceRequest request, long componentId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() =>
    //    _service.GetSubComponentsForComponent(ModelState, request, componentId));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetAllIncidentsForDevice([DataSourceRequest] DataSourceRequest request, long deviceId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetIncidentsForDevice(ModelState, request, deviceId));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetAllActionsForIncidentList([DataSourceRequest] DataSourceRequest request, long incidentId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() =>
    //    _service.GetAllActionsForIncidentList(ModelState, request, incidentId));
    //}


    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetAllComponentsForDevice([DataSourceRequest] DataSourceRequest request, long deviceId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() =>
    //    _service.GetAllComponentsForDevice(ModelState, request, deviceId));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetAllIncidentsForComponentList([DataSourceRequest] DataSourceRequest request,
    //  long componentId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() =>
    //    _service.GetAllIncidentsForComponentList(ModelState, request, componentId));
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetSubDeviceGroups([DataSourceRequest] DataSourceRequest request, long deviceGroupId)
    //{
    //  return PrepareJsonResultFromDataSourceResult(
    //    () => _service.GetSubDeviceGroups(ModelState, request, deviceGroupId));
    //}

    //public async Task<ActionResult> GetIncidentTypeDetails([DataSourceRequest] DataSourceRequest request,
    //  long incidentTypeId)
    //{
    //  DataSourceResult result = _service.GetIncidentTypeDetails(ModelState, request, incidentTypeId);
    //  return await PrepareJsonResultFromDataSourceResult(() => result);
    //}

    //public async Task<ActionResult> GetIncidentsForDevice([DataSourceRequest] DataSourceRequest request, long deviceId,
    //  DateTime startDateTime, DateTime endDateTime)
    //{
    //  DataSourceResult result =
    //    _service.GetIncidentsBetweenDatesList(ModelState, request, deviceId, startDateTime, endDateTime);
    //  return await PrepareJsonResultFromDataSourceResult(() => result);
    //}

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance,
    //  RightLevel.View)]
    //public Task<JsonResult> GetDeviceSearchList([DataSourceRequest] DataSourceRequest request)
    //{
    //  return PrepareJsonResultFromDataSourceResult(() => _service.GetDeviceOverviewList(ModelState, request));
    //}

    //#endregion

    //#region Actions

    ////Device
    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteDevice(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteDevice(ModelState, viewModel));
    //}

    ////Component
    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteComponent(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteComponent(ModelState, viewModel));
    //}

    ////Incident
    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteIncident(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteIncident(ModelState, viewModel));
    //}

    ////Action
    //[HttpPost]
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_System,
    //  RightLevel.Delete)]
    //public Task<JsonResult> DeleteAction(VM_LongId viewModel)
    //{
    //  return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteAction(ModelState, viewModel));
    //}

    //#endregion
  }
}
