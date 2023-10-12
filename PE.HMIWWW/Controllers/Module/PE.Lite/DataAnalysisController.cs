using System;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DataAnalysisController : BaseController
  {
    private readonly IDataAnalysisService _service;

    public DataAnalysisController(IDataAnalysisService service)
    {
      _service = service;
    }

    // GET: DataAnalysis
    //public ActionResult Index()
    //{
    //  return View("~/Views/Module/PE.Lite/DataAnalysis/Index.cshtml");
    //}

    // GET: DataAnalysis/WorkOrder
    public ActionResult WorkOrders()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisWorkOrder.cshtml");
    }

    // GET: DataAnalysis/Delays
    public ActionResult Delays()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisDelay.cshtml");
    }

    // GET: DataAnalysis/ProductionSummary
    public ActionResult ProductionSummary()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisProduction.cshtml");
    }

    // GET: DataAnalysis/KPIAnalysis
    public ActionResult KpiAnalysis()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisKPI.cshtml");
    }

    // GET: DataAnalysis/MillUtilization
    public ActionResult MillUtilization()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisMillUtilization.cshtml");
    }

    // GET: DataAnalysis/Defects
    public ActionResult Defects()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisDefects.cshtml");
    }

    // GET: DataAnalysis/Scraps
    public ActionResult Scraps()
    {
      return View("~/Views/Module/PE.Lite/DataAnalysis/DataAnalysisScraps.cshtml");
    }


    public Task<JsonResult> GetDelaysDataList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetDelaysDataList(ModelState, request));
    }

    public Task<JsonResult> GetWorkOrdersDataList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrdersDataList(ModelState, request));
    }

    public Task<JsonResult> GetDefectsDataList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetWorkOrdersDataList(ModelState, request));
    }


    [HttpPost]
    public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
    {
      return File(Convert.FromBase64String(base64), contentType, fileName);
    }

    [HttpPost]
    public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
    {
      return File(Convert.FromBase64String(base64), contentType, fileName);
    }

    public PartialViewResult DataChartView()
    {
      return PartialView("~/Views/Module/PE.Lite/DataAnalysis/_DataAnalysisChart.cshtml");
    }
  }
}
