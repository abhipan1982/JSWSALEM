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
  public class BundleWeighingMonitorController : BaseController
  {
    private readonly IBundleWeighingMonitorService _service;

    public BundleWeighingMonitorController(IBundleWeighingMonitorService service)
    {
      _service = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.InspectionResultList = ListValuesHelper.GetInspectionResultList();
      ViewBag.WorkOrderStatusList = ListValuesHelper.GetWorkOrderStatusesList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BundleWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/BundleWeighingMonitor/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BundleWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<ActionResult> GetMaterialOnWeight(long? rawMaterialId)
    {
      var data = await _service.GetRawMaterialOnWeightAsync(ModelState, rawMaterialId);
      return PartialView("~/Views/Module/PE.Lite/BundleWeighingMonitor/_MaterialOnWeight.cshtml", data);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BundleWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<JsonResult> GetWorkOrdersBeforeWeightList([DataSourceRequest] DataSourceRequest request)
    {
      var data = await _service.GetWorkOrdersBeforeWeightListAsync(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => data);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BundleWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialsAfterWeightList([DataSourceRequest] DataSourceRequest request, long? rawMaterialId)
    {
      var data = await _service.GetRawMaterialsAfterWeightListAsync(ModelState, request, rawMaterialId);
      return await PrepareJsonResultFromDataSourceResult(() => data);
    }
  }
}
