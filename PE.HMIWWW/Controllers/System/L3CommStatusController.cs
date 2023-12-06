using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.System
{
  public class L3CommStatusController : BaseController
  {
    #region services

    private readonly IL3CommStatusService _l3CommStatusService;

    #endregion

    #region ctor

    public L3CommStatusController(IL3CommStatusService service)
    {
      _l3CommStatusService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.CommStatusList = ListValuesHelper.GetCommStatusList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/System/L3CommStatus/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL3TransferTableWorkOrderList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL3TransferTableWorkOrderList(ModelState, request));
    }

    //Av@071123--


    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL3L2BatchDataDefinitionList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL3L2BatchDataDefinitionList(ModelState, request));
    }



    //Av@231123

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL2L3BatchReportList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL2L3BatchReport(ModelState, request));
    }





    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL3TransferTableGeneralList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL3TransferTableGeneralList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<ActionResult> WorkOrderDetails(long counterId)
    {
      return PrepareActionResultFromVm(() => _l3CommStatusService.GetWorkOrderDefinition(ModelState, counterId),
        "~/Views/System/L3CommStatus/_WorkOrderDefinitionDetails.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> WorkOrderDefinitionCreatePopup()
    {
      return PreparePopupActionResultFromVm(() => new VM_L3L2WorkOrderDefinition(),
        "~/Views/System/L3CommStatus/_WorkOrderDefinitionCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> WorkOrderDefinitionImportPopup()
    {
      return PreparePopupActionResultFromVm(() => new VM_L3L2WorkOrderDefinition(),
        "~/Views/System/L3CommStatus/_WorkOrderDefinitionImportPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<ActionResult> WorkOrderDefinitionEditPopup(long counterId)
    {
      return PreparePopupActionResultFromVm(() => _l3CommStatusService.GetWorkOrderDefinition(ModelState, counterId),
        "~/Views/System/L3CommStatus/_WorkOrderDefinitionEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> CreateWorkOrderDefinition(VM_L3L2WorkOrderDefinition workOrderDefinition)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _l3CommStatusService.CreateWorkOrderDefinition(ModelState, workOrderDefinition));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateWorkOrderDefinition(VM_L3L2WorkOrderDefinition workOrderDefinition)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _l3CommStatusService.UpdateWorkOrderDefinition(ModelState, workOrderDefinition));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public async Task<ActionResult> DeleteWorkOrderDefinition(long id)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _l3CommStatusService.DeleteWorkOrderDefinition(ModelState, new VM_L3L2WorkOrderDefinition
        {
          CounterId = id
        }));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL3TransferTableWorkOrderReports([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL3TransferTableWorkOrderReports(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<ActionResult> WorkOrderReportDetails(long counterId)
    {
      return PrepareActionResultFromVm(() => _l3CommStatusService.GetWorkOrderReport(ModelState, counterId),
        "~/Views/System/L3CommStatus/_WorkOrderReportDetails.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public async Task<ActionResult> ResetWorkOrderReport(long id)
    {
      VM_L2L3WorkOrderReport workOrderDefinition = new VM_L2L3WorkOrderReport { CounterId = id };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _l3CommStatusService.ResetWorkOrderReport(ModelState, workOrderDefinition));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetL3TransferTableProductReports([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _l3CommStatusService.GetL3TransferTableProductReports(ModelState, request));
    }

    

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<ActionResult> ProductReportDetails(long counterId)
    {
      return PrepareActionResultFromVm(() => _l3CommStatusService.GetProductReport(ModelState, counterId),
        "~/Views/System/L3CommStatus/_ProductReportDetails.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
      RightLevel.Delete)]
    public async Task<ActionResult> ResetProductReport(long id)
    {
      VM_L2L3ProductReport productDefinition = new VM_L2L3ProductReport { Counter = id };
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _l3CommStatusService.ResetProductReport(ModelState, productDefinition));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_L3CommStatus, Constants.SmfAuthorization_Module_System,
     RightLevel.Update)]
    public async Task<ActionResult> ImportWorkOrderDefinition(IEnumerable<IFormFile> files)
    {
      if (files != null && files.Count() == 1)
      {
        var file = files.First();
        if (file.Length > 0)
        {
          using var ms = new MemoryStream();
          await file.CopyToAsync(ms);
          return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
            _l3CommStatusService.ImportWorkOrderDefinition(ModelState, ms));
        }
      }

      return Ok();
    }
  }
}
