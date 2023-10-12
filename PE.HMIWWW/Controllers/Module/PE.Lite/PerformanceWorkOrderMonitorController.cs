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
  public class PerformanceWorkOrderMonitorController : BaseController
  {
    private readonly IWorkOrderService _workOrderService;
    private readonly IKPIService _kpiService;
    public PerformanceWorkOrderMonitorController(IWorkOrderService workOrderService, IKPIService kpiService)
    {
      _workOrderService = workOrderService;
      _kpiService = kpiService;
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
      return View("~/Views/Module/PE.Lite/PerformanceWorkOrderMonitor/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetWorkOrderOverviewList([DataSourceRequest] DataSourceRequest request)
    {
      ViewBag.WorkOrderStatuses = ListValuesHelper.GetWorkOrderStatusesList();
      return PrepareJsonResultFromDataSourceResult(() => _workOrderService.GetWorkOrderOverviewList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult ElementDetails(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/PerformanceWorkOrderMonitor/_WorkOrderMonitorBody.cshtml", workOrderId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetWorkOrderKPIs(long workOrderId)
    {
      return PrepareActionResultFromVm(
        () => _kpiService.GetWorkOrderBasedKPIs(workOrderId), "Views/Module/PE.Lite/KPI/_KPI.cshtml");
    }
  }
}
