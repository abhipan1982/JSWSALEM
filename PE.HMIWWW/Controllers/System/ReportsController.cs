using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Resources;

namespace PE.HMIWWW.Controllers.System
{
  public class ReportsController : BaseController
  {
    private readonly IConfiguration _configuration;

    public ReportsController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public ActionResult Index(string id)
    {
      ViewBag.Page_Title = ResourceController.GetPageTitleValue(HttpContext.Request.RouteValues["controller"].ToString(), id);
      ViewBag.SSRSReportUrl = _configuration.GetValue<string>("SSRSReportPresenterUrl") + $"?id={id}";

      return View();
    }
  }
}
