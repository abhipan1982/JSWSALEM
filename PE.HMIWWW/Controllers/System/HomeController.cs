using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PE.Common;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Parameter;

namespace PE.HMIWWW.Controllers
{
  public class HomeController : BaseController
  {
    #region ctor

    #endregion

    //[HttpPost]
    //public async Task<JsonResult> ModuleOperationRequest(VM_LongId viewModel)
    //{
    //  return await PrepareJsonResultFromVm(() => _service.ModuleOperationRequest(ModelState, viewModel));
    //}

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Constants()
    {
      Dictionary<string, object> constants = typeof(HMIRefreshKeys)
        .GetFields()
        .ToDictionary(x => x.Name, x => x.GetValue(null));

      string json = JsonConvert.SerializeObject(constants);

      return new JavaScriptResult("var HmiRefreshKeys = " + json + ";");
    }
    
    public ActionResult GetPublishDate()
    {
      return Content(ApplicationInformation.CompileDate.ToString("s"));
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult SetCulture(string culture, string returnUrl)
    {
      Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );

      return RedirectToLocal(returnUrl);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    //[SmfAuthorization("CONTROLLER_Schedule", "MODULE_NAME", RightLevel.View)]
    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      return View();
      // Modules.hmiexe.Name;
    }

    [HttpPost]
    public ActionResult ProcessForm()
    {
      //_service.SendHmiOperationRequest(PrepareInitiator(), Modules.hmiexe.Name, 100);
      return View("Contact");
    }

    #region Private

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }

      return RedirectToAction("Index", "Home");
    }

    #endregion

    #region services

    // private IHomeService _service;

    #endregion
  }
}
