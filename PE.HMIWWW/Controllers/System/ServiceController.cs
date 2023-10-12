using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.Services.System;

namespace PE.HMIWWW.Controllers.System
{
  public class ServiceController : BaseController
  {
    #region services

    private readonly IAccessUnitsService _accessUnitsService;

    #endregion

    #region ctor

    public ServiceController(IAccessUnitsService accessUnitsService)
    {
      _accessUnitsService = accessUnitsService;
    }

    #endregion

    #region interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> UserRightsPopulate([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _accessUnitsService.UserRightsPopulate(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Service, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetExistedAccessUnits([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _accessUnitsService.GetExistedAccessUnits(ModelState, request));
    }

    #endregion
  }
}
