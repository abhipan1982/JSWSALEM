using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.System
{
  public class LimitController : BaseController
  {
    #region services

    private readonly ILimitService _limitService;

    #endregion

    #region ctor

    public LimitController(ILimitService service)
    {
      _limitService = service;
    }

    #endregion

    #region view interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> LimitData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _limitService.GetLimits(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Limit, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> Update([DataSourceRequest] DataSourceRequest request, VM_Limit viewModel)
    {
      return PrepareJsonResultFromVm(() => _limitService.UpdateLimit(ModelState, viewModel));
    }

    #endregion
  }
}
