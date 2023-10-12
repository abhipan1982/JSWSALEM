using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Measurements;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MeasurementsController : BaseController
  {
    private readonly IMeasurementsService _measurementsService;

    public MeasurementsController(IMeasurementsService service)
    {
      _measurementsService = service;
    }

    // GET: Measurements
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.RawMaterialStatuses = ListValuesHelper.GetRawMaterialStatusesList();
      return View("~/Views/Module/PE.Lite/Measurements/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public async Task<ActionResult> GetMeasurementsBody(long rawMaterialId)
    {
      await Task.CompletedTask;
      return await PrepareActionResultFromVmList(
        () => _measurementsService.GetRawMaterialWithArea(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/Measurements/_MeasurementsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization,
      Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public async Task<JsonResult> GeMaterialMeasurements(long? rawMaterialId)
    {
      return Json(await _measurementsService.GeMaterialMeasurements(ModelState, rawMaterialId));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurmentsByRawMaterialId([DataSourceRequest] DataSourceRequest request, long areaCode,
      long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _measurementsService.GetMeasurementsByRawMaterialId(ModelState, request, areaCode, rawMaterialId));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurements([DataSourceRequest] DataSourceRequest request, long featureId,
      long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _measurementsService.GetMeasurements(ModelState, request, featureId, workOrderId));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Furnace, Constants.SmfAuthorization_Module_MeasValueHistory,
      RightLevel.View)]
    public JsonResult GetFurnaceTemperatures()
    {
      IList<VM_Temperature> temperatures = _measurementsService.GetFurnaceTemperatures(ModelState);
      return Json(temperatures);
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> MeasurementsGridView(VM_RawMaterialInArea rawMaterialInArea)
    {
      return PrepareActionResultFromVm(() => rawMaterialInArea,
        "~/Views/Module/PE.Lite/Measurements/_MeasurementsGrid.cshtml");
    }
  }
}
