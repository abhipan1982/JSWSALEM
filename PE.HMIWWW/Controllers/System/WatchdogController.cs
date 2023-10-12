using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;

namespace PE.HMIWWW.Controllers.System
{
  public class WatchdogController : BaseController
  {
    #region services

    private readonly IWatchdogService _service;

    #endregion

    #region ctor

    public WatchdogController(IWatchdogService service)
    {
      _service = service;
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.Delete)]
    public Task<JsonResult> Kill(string moduleName)
    {
      return PrepareJsonResultFromVm(() => _service.Kill(ModelState, moduleName));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.Delete)]
    public Task<JsonResult> Stop(string moduleName)
    {
      return PrepareJsonResultFromVm(() => _service.Stop(ModelState, moduleName));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.Delete)]
    public Task<JsonResult> Initialize(string moduleName)
    {
      return PrepareJsonResultFromVm(() => _service.Initialize(ModelState, moduleName));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.Delete)]
    public Task<JsonResult> SetProcessUnderWd(string moduleName)
    {
      return PrepareJsonResultFromVm(() => _service.SetProcessUnderWd(ModelState, moduleName));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Watchdog, Constants.SmfAuthorization_Controller_Watchdog,
      RightLevel.Delete)]
    public Task<JsonResult> UnSetProcessUnderWd(string moduleName)
    {
      return PrepareJsonResultFromVm(() => _service.UnSetProcessUnderWd(ModelState, moduleName));
    }
  }
}
