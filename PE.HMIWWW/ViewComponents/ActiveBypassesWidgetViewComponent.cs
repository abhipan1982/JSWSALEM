using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Constants;

namespace PE.HMIWWW.ViewComponents
{
  public class ActiveBypassesWidgetViewComponent : ViewComponent
  {
    private readonly IBypassService _service;

    public ActiveBypassesWidgetViewComponent(IBypassService service)
    {
      _service = service;
    }

    [SmfAuthorization(CustomConstants.SmfAuthorization_Controller_Bypass, Constants.SmfAuthorization_Module_Tracking,
    RightLevel.View)]
    public async Task<IViewComponentResult> InvokeAsync()
    {
      var model =  _service.GetActiveBypasses();
      return View("~/Views/Shared/Widget/_ActiveBypassesWidget.cshtml", model);
    }
  }
}
