using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Measurements;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MeasurementAnalysisController : BaseController
  {
    private readonly IMeasurementAnalysisService _measurementAnalysisService;
    private readonly IMeasurementsService _measurementsService;

    public MeasurementAnalysisController(IMeasurementAnalysisService service, IMeasurementsService measurementsService)
    {
      _measurementAnalysisService = service;
      _measurementsService = measurementsService;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      return View("~/Views/Module/PE.Lite/MeasurementAnalysis/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public ActionResult Comparison()
    {
      return View("~/Views/Module/PE.Lite/MeasurementAnalysisComp/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public async Task<ActionResult> GetMaterialMeasurementsBody(long rawMaterialId)
    {
      await Task.CompletedTask;
      return await PrepareActionResultFromVmList(
        () => _measurementsService.GetRawMaterialWithArea(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/MeasurementAnalysis/_MeasurementAnalysisBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<JsonResult> GetMeasurement([DataSourceRequest] DataSourceRequest request, long featureId, long rawMaterialId)
    {
      return Json(await _measurementAnalysisService.GeMaterialMeasurement(ModelState, rawMaterialId, featureId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public async Task<ActionResult> GetFeatureTabsView()
    {
      await Task.CompletedTask;
      return await PrepareActionResultFromVmList(
        () => _measurementsService.GetRawMaterialWithArea(ModelState, 0),
        "~/Views/Module/PE.Lite/MeasurementAnalysisComp/_AssetTabs.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public ActionResult GetFeaturesGridView(VM_RawMaterialInArea rawMaterialInArea)
    {
      return PartialView("~/Views/Module/PE.Lite/MeasurementAnalysisComp/_FeatureGrid.cshtml", rawMaterialInArea.AreaCode.ToString());
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public ActionResult GetMaterialGridView()
    {
      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      return PartialView("~/Views/Module/PE.Lite/MeasurementAnalysisComp/_MaterialGrid.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public ActionResult GetWorkOrderGridView()
    {
      return PartialView("~/Views/Module/PE.Lite/MeasurementAnalysisComp/_WorkOrderGrid.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
    public Task<JsonResult> GetFeatures([DataSourceRequest] DataSourceRequest request, int areaCode, bool? lengthRelated)
    {
      if(lengthRelated != null)
        return PrepareJsonResultFromDataSourceResult(
          () => _measurementAnalysisService.GetFeaturesByType(ModelState, request, areaCode, lengthRelated.Value));

      return PrepareJsonResultFromDataSourceResult(
       () => _measurementsService.GetFeatures(ModelState, request, areaCode));
    }

    public Task<JsonResult> GetMeasurements(long[] rawMaterialIds, long[] featureIds, bool timeNormalization)
    {
      return PrepareJsonResultFromVmAsync(() => _measurementAnalysisService.GetMeasurementComparison(rawMaterialIds, featureIds, timeNormalization));
    }

  }
}
