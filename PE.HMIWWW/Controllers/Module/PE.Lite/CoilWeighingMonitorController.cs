using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CoilWeighingMonitorController : BaseController
  {
    private readonly ICoilWeighingMonitorService _service;

    public CoilWeighingMonitorController(ICoilWeighingMonitorService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CoilWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/CoilWeighingMonitor/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CoilWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<ActionResult> GetMaterialOnWeight(long? rawMaterialId)
    {
      var data = await _service.GetRawMaterialOnWeightAsync(ModelState, rawMaterialId);
      return PartialView("~/Views/Module/PE.Lite/CoilWeighingMonitor/_MaterialOnWeight.cshtml", data);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CoilWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialsBeforeWeightList([DataSourceRequest] DataSourceRequest request, DCMaterialPosition materialPosition)
    {
      var data = await _service.GetRawMaterialsBeforeWeightListAsync(ModelState, request, materialPosition);
      return await PrepareJsonResultFromDataSourceResult(() => data);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_CoilWeighingMonitor,
      Constants.SmfAuthorization_Module_Tracking, RightLevel.View)]
    public async Task<JsonResult> GetRawMaterialsAfterWeightList([DataSourceRequest] DataSourceRequest request, DCMaterialPosition materialPosition)
    {
      var data = await _service.GetRawMaterialsAfterWeightListAsync(ModelState, request, materialPosition);
      return await PrepareJsonResultFromDataSourceResult(() => data);
    }
  }
}
