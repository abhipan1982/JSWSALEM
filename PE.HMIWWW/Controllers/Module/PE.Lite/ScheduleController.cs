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
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;
using SMF.Core.Interfaces;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ScheduleController : BaseController
  {
    private readonly IScheduleService _service;

    public ScheduleController(IScheduleService service)
    {
      _service = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.ScheduleStatuses = ListValuesHelper.GetScheduleStatuses();
      ViewBag.RejectLocation = ListValuesHelper.GetRejectLocationList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Schedule/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.View)]
    public Task<JsonResult> GetSchedule([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetSchedule(ModelState, request));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public Task<JsonResult> AddWorkOrderToSchedule(long workOrderId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddWorkOrderToSchedule(ModelState, workOrderId));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public Task<JsonResult> RemoveItemFromSchedule(long scheduleId, long workOrderId, short orderSeq)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.RemoveItemFromSchedule(ModelState, scheduleId, workOrderId, orderSeq));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public Task<JsonResult> MoveItemInSchedule(long scheduleId, long workOrderId, int seqId, int newIndex)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.MoveItemInSchedule(ModelState, scheduleId, workOrderId, seqId, newIndex));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public Task<ActionResult> CloneSchedulePopup(long scheduleId)
    {
      ViewBag.MaxMaterialsCount = GetParameter("ScheduleMaxMaterialsCount").ValueInt.GetValueOrDefault();
      return PreparePopupActionResultFromVm(() => _service.GetScheduleData(ModelState, scheduleId),
        "~/Views/Module/PE.Lite/Schedule/_CreateTestSchedulePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public async Task<ActionResult> CreateTestSchedule(VM_Schedule viewModel)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.CreateTestSchedule(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.View)]
    public Task<ActionResult> PreparePratialForSchedule()
    {
      return PrepareActionResultFromVm(() => _service.PreparePratialForSchedule(ModelState),
        "~/Views/Module/PE.Lite/Schedule/_ScheduleSlideScreen.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.View)]
    public ActionResult GetSchedulePartial()
    {
      return PartialView("~/Views/Module/PE.Lite/Schedule/_SchedulePartialGrid.cshtml");
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Schedule, Constants.SmfAuthorization_Module_ProdPlanning,
      RightLevel.Update)]
    public Task<JsonResult> EndOfWorkOrder(long workOrderId)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.EndOfWorkOrder(ModelState, workOrderId));
    }
  }
}
