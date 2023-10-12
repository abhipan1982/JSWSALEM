using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class TrackingManagementController : BaseController
  {
    private readonly ITrackingManagementService _service;
    private readonly IParameterService _parameterService;

    public TrackingManagementController(ITrackingManagementService service, IParameterService parameterService)
    {
      _service = service;
      _parameterService = parameterService;
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement, Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/TrackingManagement/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public Task<JsonResult> GetTrackingOverview([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetTrackingOverview(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> UpdateMaterialPosition(VM_ReplaceMaterialPosition newLocation)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateMaterialPosition(ModelState, newLocation));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public ActionResult RemoveRawMaterialPopup(VM_RemoveMaterial materialCoordinates)
    {
      return PartialView("~/Views/Module/PE.Lite/TrackingManagement/_RemoveRawMaterialPopup.cshtml", materialCoordinates);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> DeleteMaterial(VM_RemoveMaterial materialCoordinates)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteMaterial(ModelState, materialCoordinates));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> MoveAllMaterialsInAreaUp(VM_MoveMaterials area)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.MoveMaterialsUp(ModelState, area));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> MoveAllMaterialsInAreaDown(VM_MoveMaterials area)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.MoveMaterialsDown(ModelState, area));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement, Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> ChargeIntoChargingGrid()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.ChargeIntoChargingGrid(ModelState));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> UnchargeFromChargingGrid()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UnchargeFromChargingGrid(ModelState));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> ChargeIntoFurnace(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.ChargeIntoFurnace(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> UnchargeFromFurnace(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UnchargeFromFurnace(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> DischargeForRolling(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DischargeForRolling(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> UnDischargeFromRolling(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UnDischargeFromRolling(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> DischargeForReject(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DischargeForReject(ModelState, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> CloseWorkShop()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.EndOfWorkShop(ModelState));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public ActionResult ChargeMaterialOnFurnaceExitPopup()
    {
      ViewBag.WorkOrderList = _service.GetWorkOrderToChargeList();
      return PartialView("~/Views/Module/PE.Lite/TrackingManagement/_ChargeMaterialOnFurnaceExitPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> ChargeMaterialOnFurnaceExitAsync(long workOrderId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.ChargeMaterialOnFurnaceExitAsync(ModelState, workOrderId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> FinishLayer(long layerId, int areaCode)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.FinishLayerAsync(ModelState, layerId, areaCode));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> TransferLayer(long layerId, int areaCode)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.TransferLayerAsync(ModelState, layerId, areaCode));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> StartLaneCommunication()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _parameterService.UpdateParameterValueByParameterName(ModelState, "TRK_LineStatus", 0));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> StopLaneCommunication()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _parameterService.UpdateParameterValueByParameterName(ModelState, "TRK_LineStatus", 1));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> StartSlowProduction()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _parameterService.UpdateParameterValueByParameterName(ModelState, "DLS_Mode", 1));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_TrackingManagement,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.Update)]
    public Task<JsonResult> StopSlowProduction()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _parameterService.UpdateParameterValueByParameterName(ModelState, "DLS_Mode", 0));
    }

    public JsonResult Filtering_WorkOrders(string text)
    {
      return Json(_service.GetWorkOrderToChargeList(text));
    }
  }
}
