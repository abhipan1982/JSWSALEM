using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.System
{
  public class WidgetConfigurationController : BaseController
  {
    #region services

    private readonly IWidgetConfigurationService _widgetService;

    #endregion

    public WidgetConfigurationController(IWidgetConfigurationService service)
    {
      _widgetService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.UserId = ViewBag.User_Id;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddWidgetConfigurationDialog()
    {
      return PartialView("AddWidgetConfigurationDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditWidgetConfigurationDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _widgetService.GetWidgetConfiguration(ModelState, id),
        "EditWidgetConfigurationDialog");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetWidgetsList([DataSourceRequest] DataSourceRequest request)
    {
      string userId = ViewBag.UserId;
      return PrepareJsonResultFromDataSourceResult(() => _widgetService.GetWidgetsList(ModelState, request, userId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<ActionResult> EditWidgetAssigmentDialog(long widgetId)
    {
      string userId = ViewBag.UserId;
      return PreparePopupActionResultFromVm(
        () => _widgetService.GetWidgetConfigurationByUser(ModelState, widgetId, userId),
        "EditWidgetConfigurationDialog");
    }

    #region actions

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertWidgetConfiguration(VM_WidgetConfigurations viewModel)
    {
      return PrepareJsonResultFromVm(() => _widgetService.InsertWidgetConfiguration(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateWidgetConfiguration(VM_WidgetConfigurations viewModel)
    {
      return PrepareJsonResultFromVm(() => _widgetService.UpdateWidgetConfiguration(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateWidgetConfigurationForUser(VM_WidgetConfigurations viewModel)
    {
      string userId = ViewBag.UserId;
      return PrepareJsonResultFromVm(() =>
        _widgetService.UpdateWidgetConfigurationForUser(ModelState, viewModel, userId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, VM_LongId viewModel)
    {
      return PrepareJsonResultFromVm(() => _widgetService.DeleteWidgetConfiguration(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_WidgetConfigurations,
      Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> GetWidgetConfigurationsData([DataSourceRequest] DataSourceRequest request)
    {
      string userId = ViewBag.UserId;
      return PrepareJsonResultFromDataSourceResult(() =>
        _widgetService.GetWidgetConfigurationsData(ModelState, request, userId));
    }

    #endregion
  }
}
