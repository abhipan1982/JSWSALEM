using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BarHandlingController : BaseController
  {
    #region members

    private readonly IBarHandlingService _barHandlingService;

    #endregion

    #region ctor

    public BarHandlingController(IBarHandlingService service)
    {
      _barHandlingService = service;
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_BarHandling, Constants.SmfAuthorization_Module_Tracking,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/BarHandling/Index.cshtml");
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
    }
  }
}
