using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class KPIController : BaseController
  {
    private readonly IKPIService _service;

    public KPIController(IKPIService service)
    {
      _service = service;
    }

    public ActionResult KPIChart(long kpiValueId)
    {
      return PartialView("~/Views/Module/PE.Lite/KPI/_KPIChart.cshtml", kpiValueId);
    }

    public async Task<ActionResult> GetKPIValues(long kpiValueId)
    {
      return await PrepareJsonResultFromVmAsync(() => _service.GetKPIValuesAsync(kpiValueId));
    }
  }
}
