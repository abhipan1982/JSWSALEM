using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Constants;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class BypassController : BaseController
  {
    private readonly IBypassService _service;

    public BypassController(IBypassService service)
    {
      _service = service;
    }

    [SmfAuthorization(CustomConstants.SmfAuthorization_Controller_Bypass, Constants.SmfAuthorization_Module_Tracking,
     RightLevel.View)]
    public Task<JsonResult> GetActiveBypasses()
    {
      var result = _service.GetActiveBypasses();

      return Task.FromResult(Json(result));
    }

    [SmfAuthorization(CustomConstants.SmfAuthorization_Controller_Bypass, Constants.SmfAuthorization_Module_Tracking,
    RightLevel.View)]
    public ActionResult GetActiveBypassesWidgetView()
    {
      return PartialView("~/Views/Shared/Widget/_ActiveBypassesWidget.cshtml",
      _service.GetActiveBypasses());
    }
  }
}
