using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.BaseDbEntity.EnumClasses;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Visualization;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class VisualizationController : BaseController
  {
    #region members

    private readonly IVisualizationService _visualizationService;

    #endregion

    #region ctor

    public VisualizationController(IVisualizationService service)
    {
      _visualizationService = service;
    }

    #endregion

    // GET: Visualization
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Visualization/Index.cshtml");
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
    }


    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<JsonResult> RequestLastMaterialPosition()
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.RequestLastMaterialPosition(ModelState));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrdersInRealizationList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _visualizationService.GetWorkOrdersInRealizationList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrdersPlannedList([DataSourceRequest] DataSourceRequest request)
    {
      //ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() =>
        _visualizationService.GetWorkOrdersPlannedList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public ActionResult GetMeasurementsList([DataSourceRequest] DataSourceRequest request)
    {
      return PartialView("~/Views/Module/PE.Lite/Measurements/_MeasurementsBody.cshtml");
    }

    public async Task<ActionResult> GetQueueAreasView(List<long> materialsInAreas, List<long> materialsInFurnace, int selected)
    {
      await Task.CompletedTask;
      return await PrepareActionResultFromVmList(() => _visualizationService.GetQueueAreas(ModelState, materialsInAreas, materialsInFurnace, selected),
        "~/Views/Module/PE.Lite/Visualization/_MaterialsInArea.cshtml");
    }

    public Task<ActionResult> GetLayerViewById(long layerId)
    {
      return PrepareActionResultFromVmList(() => _visualizationService.GetLayerById(ModelState, layerId),
        "~/Views/Module/PE.Lite/Visualization/_MaterialsInLayers.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrdersProducedList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _visualizationService.GetWorkOrdersProducedList(ModelState, request));
    }

    public Task<JsonResult> GetMaterialsInArea([DataSourceRequest] DataSourceRequest request, List<VM_RawMaterialOverview> materials)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _visualizationService.GetMaterialsInArea(ModelState, request, materials));
    }

    public Task<JsonResult> GetMaterialsInLayer([DataSourceRequest] DataSourceRequest request, long layerId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _visualizationService.GetMaterialsInLayer(ModelState, request, layerId));
    }

    public Task<JsonResult> GetMaterialsInFurnace([DataSourceRequest] DataSourceRequest request, List<VM_Furnace> materials)
    {
      return PrepareJsonResultFromDataSourceResult(
        () => _visualizationService.GetMaterialInFurnace(ModelState, request, materials));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<JsonResult> GetTrackingMaterialDetails(long? rawMaterialId)
    {
      return TaskPrepareJsonResultFromVm<VM_TrackingMaterialOverview, Task<VM_TrackingMaterialOverview>>(() =>
        _visualizationService.GetTrackingMaterialDetails(ModelState, rawMaterialId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.Update)]
    public async Task<ActionResult> RemoveFromTrackingAction(long rawMaterialId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.TrackingRemoveAction(ModelState, rawMaterialId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.Update)]
    public async Task<ActionResult> MaterialReadyAction(VM_RawMaterialGenealogy data)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.TrackingReadyAction(ModelState, data));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.Update)]
    public async Task<ActionResult> ProductUndoAction(long rawMaterialId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.ProductUndoAction(ModelState, rawMaterialId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> ScrapAction(VM_Scrap scrapData)
    {
      long rawMaterialId = scrapData.RawMaterialId;
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.TrackingScrapAction(ModelState, scrapData));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public async Task<ActionResult> UnscrapAction(long rawMaterialId)
    {
      VM_Scrap scrapModel = new VM_Scrap { RawMaterialId = rawMaterialId, ScrapPercent = 0 };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.TrackingScrapAction(ModelState, scrapModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<ActionResult> PartialScrapPopup(VM_LongId viewModel)
    {
      ViewBag.AssetList = _visualizationService.GetAssets();
      return PreparePopupActionResultFromVm(() => _visualizationService.GetRawMaterialPartialScrap(viewModel.Id),
        "~/Views/Module/PE.Lite/Visualization/_PartialScrapPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<ActionResult> ScrapPopup(VM_LongId viewModel)
    {
      ViewBag.AssetList = _visualizationService.GetAssets();
      return PreparePopupActionResultFromVm(() => _visualizationService.GetRawMaterialPartialScrap(viewModel.Id),
        "~/Views/Module/PE.Lite/Visualization/_ScrapPopup.cshtml");
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_Quality, RightLevel.Update)]
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> AssignRawMaterialPartialScrap(VM_Scrap rawMaterialPartialScrap)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.TrackingScrapAction(ModelState, rawMaterialPartialScrap));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public Task<ActionResult> RejectRawMaterialPopup(VM_LongId viewModel)
    {
      ViewBag.RejectLocations = ListValuesHelper.GetRejectLocationList();
      return PreparePopupActionResultFromVm(() => _visualizationService.GetRawMaterialRejection(viewModel.Id),
        "~/Views/Module/PE.Lite/Visualization/_RejectRawMaterialPopup.cshtml");
      //return await PreparePopupActionResultFromVm(() => new VM_RawMaterialRejection(viewModel.Id), "~/Views/Module/PE.Lite/Visualization/_RejectRawMaterialPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.Update)]
    public async Task<ActionResult> RejectRawMaterial(VM_RawMaterialRejection rawMaterial)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.RejectRawMaterial(ModelState, rawMaterial));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public async Task<ActionResult> UnRejectAction(long rawMaterialId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _visualizationService.RejectRawMaterial(ModelState,
          new VM_RawMaterialRejection { RawMaterialId = rawMaterialId, EnumRejectLocation = RejectLocation.None }));
    }
  }
}
