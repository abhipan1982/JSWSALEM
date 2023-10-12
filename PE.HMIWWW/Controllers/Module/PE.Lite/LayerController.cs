using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class LayerController : BaseController
  {
    private readonly ILayerService _layerService;

    public LayerController(ILayerService service)
    {
      _layerService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.LayerStatuses = ListValuesHelper.GetLayerStatusesList();
      return View("~/Views/Module/PE.Lite/Layer/Index.cshtml");
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetLayerSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _layerService.GetLayerSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long rawMaterialId)
    {
      return PrepareActionResultFromVm(() => _layerService.GetLayerDetails(ModelState, rawMaterialId),
        "~/Views/Module/PE.Lite/Layer/_LayerBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> MeasurementDetails(long measurementId)
    {
      return PrepareActionResultFromVm(() => _layerService.GetMeasurementDetails(ModelState, measurementId),
        "~/Views/Module/PE.Lite/Layer/_MeasurementsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> HistoryDetails(long rawMaterialStepId)
    {
      return PrepareActionResultFromVm(() => _layerService.GetHistoryDetails(ModelState, rawMaterialStepId),
        "~/Views/Module/PE.Lite/Layer/_HistoryBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurmentsByRawMaterialId([DataSourceRequest] DataSourceRequest request,
      long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _layerService.GetMeasurmentsByRawMaterialId(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetHistoryByRawMaterialId([DataSourceRequest] DataSourceRequest request, long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _layerService.GetHistoryByRawMaterialId(ModelState, request, rawMaterialId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Layer, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetChildrenByRawMaterialId([DataSourceRequest] DataSourceRequest request,
      long rawMaterialId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _layerService.GetChildrenByRawMaterialId(ModelState, request, rawMaterialId));
    }
  }
}
