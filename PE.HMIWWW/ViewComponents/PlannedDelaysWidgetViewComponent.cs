using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;

namespace PE.HMIWWW.ViewComponents
{
  public class PlannedDelaysWidgetViewComponent : ViewComponent
  {
    private readonly IDelaysService _service;

    public PlannedDelaysWidgetViewComponent(IDelaysService service)
    {
      _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      var model = await _service.GetUpcomingPlannedDelays(ModelState);
      return View("~/Views/Shared/Widget/_PlannedDelaysWidget.cshtml", model);
    }
  }
}
