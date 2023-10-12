using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite;

public class HistorianController : BaseController
{
  private readonly IHistorianService _historianService;

  public HistorianController(IHistorianService historianService)
  {
    _historianService = historianService;
  }

  [SmfAuthorization(Constants.SmfAuthorization_Controller_RawMaterial, Constants.SmfAuthorization_Module_ProdManager,
    RightLevel.View)]
  public ActionResult Index()
  {
    return View("~/Views/Module/PE.Lite/Historian/Index.cshtml");
  }

  public Task<JsonResult> GetMeasurements(long[] featureIds, DateTime startDate, DateTime endDate)
  {
    return PrepareJsonResultFromVmAsync(() => _historianService.GetMeasurements(ModelState, featureIds, startDate, endDate));
  }

  public Task<JsonResult> GetMaterials(long[] rawMaterialIds)
  {
    throw new NotImplementedException();
  }
}
