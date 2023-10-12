using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Core.Controllers;


namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class PerformanceTimeMonitorController : BaseController
  {
    private readonly IKPIService _kpiService;

    public PerformanceTimeMonitorController(IKPIService kpiService)
    {
      _kpiService = kpiService;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WorkOrder, 
      Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public Task<ActionResult> Index()
    {
      return PrepareActionResultFromVm(
        () => _kpiService.GetTimeBasedKPIs(), "~/Views/Module/PE.Lite/PerformanceTimeMonitor/Index.cshtml");
    }
  }
}
