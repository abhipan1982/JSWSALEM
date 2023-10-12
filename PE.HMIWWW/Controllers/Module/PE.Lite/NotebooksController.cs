using System.Web;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class NotebooksController : BaseController
  {
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Notebooks, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index(string url)
    {
      ViewBag.NotebookUrl = HttpUtility.UrlDecode(url);

      return View("~/Views/Module/PE.Lite/Notebooks/Index.cshtml");
    }
  }
}
