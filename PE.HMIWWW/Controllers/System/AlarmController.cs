using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.ViewModel.System;
using PE.Services.System;

namespace PE.HMIWWW.Controllers
{
  public class AlarmController : BaseController
  {
    #region services

    private readonly IAlarmsService _alarmsService;

    #endregion

    #region ctor

    public AlarmController(IAlarmsService alarmsService)
    {
      _alarmsService = alarmsService;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.YesNo = ListValuesHelper.GetTextFromYesNoStatus();
      ViewBag.AlarmTypes = ListValuesHelper.GetAlarmTypes();
    }

    #region interface

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public ActionResult Index(string owner = null, bool? toConfirm = null)
    {
      VM_AlarmSelection alarmSelection = null;
      if (owner != null || toConfirm != null)
      {
        alarmSelection = new VM_AlarmSelection(owner, toConfirm);
      }

      return View("Index", alarmSelection);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public JsonResult GetAlarmData([DataSourceRequest] DataSourceRequest request)
    {
      return Json(_alarmsService.GetAlarmList(ModelState, request, GetCultureName()));
      //return PrepareJsonResultFromDataSourceResult(() => );
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetLastAlarmData([DataSourceRequest] DataSourceRequest request)
    {
      request.Page = 1;
      return PrepareJsonResultFromDataSourceResult(() =>
        _alarmsService.GetAlarmList(ModelState, request, GetCultureName()));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<JsonResult> GetLastShortAlarmData([DataSourceRequest] DataSourceRequest request)
    {
      request.Page = 1;
      return PrepareJsonResultFromDataSourceResult(() =>
        _alarmsService.GetShortAlarmList(ModelState, request, GetCultureName()));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.Update)]
    public Task<JsonResult> Confirm(long alarmId)
    {
      string userId = ViewBag.User_Id.ToString();
      return PrepareJsonResultFromVm(() => _alarmsService.Confirm(ModelState, alarmId, userId));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Alarm, Constants.SmfAuthorization_Module_System,
      RightLevel.View)]
    public Task<ActionResult> DetailsDialog(long id)
    {
      return PrepareActionResultFromVm(() => _alarmsService.GetAlarmDetails(ModelState, id), "_AlarmDetailsPopup");
    }

    #endregion
  }
}
