using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class DelaysController : BaseController
  {
    private readonly IDelaysService _service;

    public DelaysController(IDelaysService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Delays/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetDelayList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetDelayList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateDelayAsync(VM_Delay delay)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateDelayAsync(ModelState, delay));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public async Task<ActionResult> CreateDelayAsync(VM_Delay delay)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateDelayAsync(ModelState, delay));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public Task<ActionResult> DelayEditPopup(VM_LongId viewModel)
    {
      ViewBag.CatalogueList = _service.GetEventCatalogue();
      ViewBag.CategoryList = _service.GetEventCatalogueCategory();
      ViewBag.AssetList = _service.GetAssets();
      ViewBag.WorkOrdersList = _service.GetWorkOrders();
      return PreparePopupActionResultFromVm(() => _service.GetDelay(ModelState, viewModel.Id),
        "~/Views/Module/PE.Lite/Delays/_DelayEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public Task<ActionResult> DelayCreatePopup()
    {
      ViewBag.CatalogueList = _service.GetEventCatalogue();
      ViewBag.AssetList = _service.GetAssets();
      ViewBag.WorkOrdersList = _service.GetWorkOrders();
      return PreparePopupActionResultFromVm(() => new VM_Delay(),
        "~/Views/Module/PE.Lite/Delays/_DelayCreatePopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public async Task<ActionResult> DivideDelayAsync(VM_DelayDivision delay)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DivideDelayAsync(ModelState, delay));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.Update)]
    public Task<ActionResult> DelayDividePopup(long delayId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetDelayDivision(ModelState, delayId),
        "~/Views/Module/PE.Lite/Delays/_DelayDividePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetDelayBetweenDatesList([DataSourceRequest] DataSourceRequest request,
      DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result = _service.GetDelayBetweenDatesList(ModelState, request, startDateTime, endDateTime);

      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetDelaysPlannedBetweenDatesList([DataSourceRequest] DataSourceRequest request,
      DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result =
        _service.GetDelaysPlannedBetweenDatesList(ModelState, request, startDateTime, endDateTime);

      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetDelaysUnpannedBetweenDatesList([DataSourceRequest] DataSourceRequest request,
      DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result =
        _service.GetDelaysUnplannedBetweenDatesList(ModelState, request, startDateTime, endDateTime);

      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetPlannedDelays([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetPlannedDelays(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public JsonResult GetDelaysSummary([DataSourceRequest] DataSourceRequest request, DateTime startDateTime,
      DateTime endDateTime)
    {
      return Json(_service.GetDelaysSummary(startDateTime, endDateTime));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public ActionResult ElementDetails(long shiftId, long? workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/Delays/_DelaysBody.cshtml",
        new VM_ShiftWorkOrderModel(shiftId, workOrderId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public Task<JsonResult> GetDelaysOverview([DataSourceRequest] DataSourceRequest request, long shiftId,
      long? workOrderId)
    {
      return PrepareJsonResultFromDataSourceResult(() =>
        _service.GetDelaysOverviewByShiftIdAndWorkOrderId(ModelState, request,
          new VM_ShiftWorkOrderModel(shiftId, workOrderId)));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Visualization, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetPlannedDelaysWidget()
    {
      IList<VM_Delay> delaysList = await _service.GetUpcomingPlannedDelays(ModelState);
      return PartialView("~/Views/Shared/Widget/_PlannedDelaysWidget.cshtml", delaysList);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Quality,
      RightLevel.View)]
    public async Task<ActionResult> GetActiveDelay([DataSourceRequest] DataSourceRequest request)
    {
      return await PrepareJsonResultFromDataSourceResult(() => _service.GetActiveDelay(ModelState, request));
    }
    
    public async Task<JsonResult> FilterEventCatalogue(long? eventCatalogueCategoryId)
    {
      var result = await _service.FilterEventCatalogue(eventCatalogueCategoryId);
      return Json(result);
    }
  }
}
