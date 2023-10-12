using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Signalr.Hubs;

namespace PE.HMIWWW.Controllers.System;

public class SessionsController : BaseController
{
  [HttpGet]
  [SmfAuthorization(Constants.SmfAuthorization_Controller_UserAccountAdministration,
    Constants.SmfAuthorization_Module_System, RightLevel.View)]
  public ActionResult Index()
  {
    return View(HmiHub.UserConnections.AsEnumerable());
  }
}
