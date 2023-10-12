using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ChargingController : BaseController
  {
    private readonly IChargingService _chargingService;

    public ChargingController(IChargingService chargingService)
    {
      _chargingService = chargingService;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Charging/Index.cshtml");
    }

    public ActionResult GetChargingFullList()
    {
      return PartialView("~/Views/Module/PE.Lite/Charging/_ChargingBody.cshtml");
    }

    public List<VM_RawMaterialOverview> GetChargingRawMateralsList([DataSourceRequest] DataSourceRequest request, List<long> materialsInAreas)
    {
      return _chargingService.GetQueueAreas(ModelState, materialsInAreas);
    }
  }
}
