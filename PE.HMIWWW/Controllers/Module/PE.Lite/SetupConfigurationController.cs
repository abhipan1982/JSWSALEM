using System;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class SetupConfigurationController : BaseController
  {
    private readonly ISetupConfigurationService _setupConfigurationService;

    public SetupConfigurationController(ISetupConfigurationService service)
    {
      _setupConfigurationService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/SetupConfiguration/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<ActionResult> ElementDetails(long configurationId)
    {
      return PartialView("~/Views/Module/PE.Lite/SetupConfiguration/_ConfigurationBody.cshtml",
        await _setupConfigurationService.FindConfigurationAsync(configurationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetConfigurationSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _setupConfigurationService.GetConfigurationSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult SetupConfigurationsView(long configurationId)
    {
      return PartialView("~/Views/Module/PE.Lite/SetupConfiguration/_SetupConfigurations.cshtml", configurationId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetSetupConfigurationDetails([DataSourceRequest] DataSourceRequest request, long configurationId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _setupConfigurationService.GetSetupConfigurationDetails(ModelState, request, configurationId));
      // return Json(_setupConfigurationService.GetSetupConfigurationDetails(ModelState, request, configurationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetSetupConfigurationsSearchGridData([DataSourceRequest] DataSourceRequest request, long configurationId)
    {
      ViewBag.SetupList = ListValuesHelper.GetSetups();
      return PrepareJsonResultFromDataSourceResult(() => _setupConfigurationService.GetSetupConfigurationsSearchGridData(ModelState, request, configurationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<ActionResult> FindSetupInstruction(long setupId, long setupConfigurationId)
    {
      long setupType = await _setupConfigurationService.GetSetupType(setupId);
      ViewBag.Parameters = _setupConfigurationService.GetListOfFiltersNameForSetup(setupType);
      ViewBag.SetupConfigurationId = setupConfigurationId;

      return PartialView("~/Views/Module/PE.Lite/SetupConfiguration/_SetupGrid.cshtml", setupId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public async Task<ActionResult> GetSetupParameters([DataSourceRequest] DataSourceRequest request, long setupId, long setupConfigurationId)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _setupConfigurationService.GetSetupParametersGridData(ModelState, request, setupId, setupConfigurationId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetSetupInstructionData([DataSourceRequest] DataSourceRequest request, long setupId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _setupConfigurationService.GetSetupInstructions(ModelState, request, setupId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> SetupConfigurationCreatePopup()
    {
      return PreparePopupActionResultFromVm(() => _setupConfigurationService.GetEmptyConfiguration(),
        "~/Views/Module/PE.Lite/SetupConfiguration/_SetupConfigurationCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> CreateSetupConfiguration(VM_SetupConfiguration model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.CreateSetupConfigurationAsync(ModelState, model));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> SetupConfigurationEditPopup(long configurationId)
    {
      return PreparePopupActionResultFromVm(() => _setupConfigurationService.GetConfiguration(configurationId, true),
        "~/Views/Module/PE.Lite/SetupConfiguration/_SetupConfigurationEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> EditSetupConfiguration(VM_SetupConfiguration model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.EditSetupConfigurationAsync(ModelState, model));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteSetupConfiguration(long configurationId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.DeleteSetupConfigurationAsync(ModelState, configurationId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> SendSetupConfiguration(long configurationId, bool steelgradeRelated)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.SendSetupConfigurationAsync(ModelState, configurationId, steelgradeRelated));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> SetupConfigurationClonePopup(long configurationId)
    {
      return PreparePopupActionResultFromVm(() => _setupConfigurationService.GetConfiguration(configurationId, false),
        "~/Views/Module/PE.Lite/SetupConfiguration/_SetupConfigurationClonePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> CloneSetupConfiguration(VM_SetupConfiguration model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.CloneSetupConfigurationAsync(ModelState, model));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_SetupConfiguration, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> CreateSetupConfigurationVersion(long configurationId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _setupConfigurationService.CreateSetupConfigurationVersionAsync(ModelState, configurationId));
    }
  }
}
