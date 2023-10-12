using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Measurements;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class MeasurementsSummaryController : BaseController
  {
    private readonly IMeasurementsService _measurementsService;
    private readonly IWorkOrderService _workOrderService;

    public MeasurementsSummaryController(IWorkOrderService workOrderService, IMeasurementsService measurementsService)
    {
      _workOrderService = workOrderService;
      _measurementsService = measurementsService;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.WorkOrderStatuses = ListValuesHelper.GetScheduleStatuses();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/MeasurementsSummary/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrderOverviewList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(
        () => _workOrderService.GetWorkOrderOverviewList(ModelState, request));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long workOrderId)
    {
      return PrepareActionResultFromVmList(() => _measurementsService.GetRawMaterialWithArea(ModelState, 0),
        "~/Views/Module/PE.Lite/MeasurementsSummary/_MeasurementsBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
  RightLevel.View)]
    public ActionResult FeaturesGridView(VM_RawMaterialInArea rawMaterialInArea)
    {
      return PartialView("~/Views/Module/PE.Lite/MeasurementsSummary/_MeasurementsGrid.cshtml", rawMaterialInArea.AreaCode.ToString());
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMaterialsListByWorkOrderId([DataSourceRequest] DataSourceRequest request,
      long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _workOrderService.GetMaterialsListByWorkOrderId(ModelState, request, workOrderId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetFeatures([DataSourceRequest] DataSourceRequest request, int areaCode)
    {
      return PrepareJsonResultFromDataSourceResult(
        () => _measurementsService.GetFeatures(ModelState, request, areaCode));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetMeasurements([DataSourceRequest] DataSourceRequest request, long featureId,
      long workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _measurementsService.GetMeasurements(ModelState, request, featureId, workOrderId));
    }

    public ActionResult MeasurementsSummaryChart()
    {
      return PartialView("~/Views/Module/PE.Lite/MeasurementsSummary/_MeasurementsSummaryChart.cshtml");
    }
  }
}
