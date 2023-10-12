using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Services.System;

namespace PE.HMIWWW.ViewComponents
{
  public class WidgetsViewComponent : ViewComponent
  {
    private readonly IWidgetConfigurationService _service;

    public WidgetsViewComponent(IWidgetConfigurationService service)
    {
      _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      var model = await _service.GetVMWidgetConfigurationsList(ViewBag.User_Id);
      return View("~/Views/Shared/Widget/_Widgets.cshtml", model);
    }
  }
}
