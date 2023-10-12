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
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class GrooveTemplateController : BaseController
  {
    #region services

    private readonly IGrooveTemplateService _grooveTemplateService;

    #endregion

    #region ctor

    public GrooveTemplateController(IGrooveTemplateService service)
    {
      _grooveTemplateService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.ShapeList = ListValuesHelper.GetShapeList();
      ViewBag.GrooveSetting = ListValuesHelper.GetGrooveSettingEnum();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/GrooveTemplate/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddGrooveTemplateDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/GrooveTemplate/_AddPopup.cshtml", new VM_GrooveTemplate());
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditGrooveTemplateDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _grooveTemplateService.GetGrooveTemplate(ModelState, id), "~/Views/Module/PE.Lite/GrooveTemplate/_EditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetGrooveTemplatesData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _grooveTemplateService.GetGrooveTemplateList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertGrooveTemplate(VM_GrooveTemplate viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.InsertGrooveTemplate(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteGrooveTemplate(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.DeleteGrooveTemplate(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_GrooveTemplates, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateGrooveTemplate(VM_GrooveTemplate viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _grooveTemplateService.UpdateGrooveTemplate(ModelState, viewModel));
    }
    #endregion
  }
}
