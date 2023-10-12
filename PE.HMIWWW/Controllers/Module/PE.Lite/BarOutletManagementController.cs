using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BarOutletManagementController : BaseController
  {
    private readonly IBarOutletManagementService _service;

    public BarOutletManagementController(IBarOutletManagementService service)
    {
      _service = service;
    }

    //[SmfAuthorization(Constants.SmfAuthorization_Controller_BarOutletManagementarHandling, Constants.SmfAuthorization_Module_Tracking,
    //RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/BarOutletManagement/Index.cshtml");
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
    }
  }
}
