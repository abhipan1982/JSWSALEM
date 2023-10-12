using System;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ConsumptionMonitoringController : BaseController
  {
    private readonly IConsumptionMonitoringService _consumptionMonitoringService;

    public ConsumptionMonitoringController(IConsumptionMonitoringService service)
    {
      _consumptionMonitoringService = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ConsumptionMonitoring/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<JsonResult> GetFeaturesList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _consumptionMonitoringService.GetFeaturesList(ModelState, request));
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> GetFeatureDetails(long featureId)
    {
      return PrepareActionResultFromVm(() => _consumptionMonitoringService.GetFeatureDetails(ModelState, featureId),
        "~/Views/Module/PE.Lite/ConsumptionMonitoring/_MeasurementBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Products, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    [HttpPost]
    public async Task<ActionResult> GetMeasurementData([DataSourceRequest] DataSourceRequest request, long featureId,
      DateTime dateFrom, DateTime dateTo)
    {
      DataSourceResult measurements =
        _consumptionMonitoringService.GetMeasurementData(ModelState, request, featureId, dateFrom, dateTo);

      await Task.CompletedTask;

      return Json(measurements.Data);
    }
  }
}
