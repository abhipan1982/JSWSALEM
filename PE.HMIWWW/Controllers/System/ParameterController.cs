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
  public class ParameterController : BaseController
  {
    #region services

    private readonly IParameterService _parameterService;

    #endregion

    #region ctor

    public ParameterController(IParameterService service)
    {
      _parameterService = service;
    }

    #endregion

    #region view interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> ParameterData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _parameterService.GetParameters(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Parameter, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> Update(VM_Parameter viewModel)
    {
      return PrepareJsonResultFromVm(() => _parameterService.UpdateParameter(ModelState, viewModel));
    }

    #endregion
  }
}
