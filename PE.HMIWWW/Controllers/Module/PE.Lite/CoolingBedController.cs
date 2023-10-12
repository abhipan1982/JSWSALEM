using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Core.Controllers;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CoolingBedController : BaseController
  {
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/CoolingBed/Index.cshtml");
    }
  }
}
