using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;

namespace PE.HMIWWW.Controllers.Module
{
  public class RollSetHistoryController : BaseController
  {
    private readonly IRollSetHistoryService _rollSetHistoryService;

    public RollSetHistoryController(IRollSetHistoryService service)
    {
      _rollSetHistoryService = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.RollSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
    }

    // GET: Material
    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_RollShop, RightLevel.View)]
    public ActionResult Index(long? rollSetId)
    {
      return View("~/Views/Module/PE.Lite/RollSetHistory/Index.cshtml", rollSetId != null ? _rollSetHistoryService.GetRollSetById(rollSetId) : null);
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_RollShop, RightLevel.View)]
    public Task<JsonResult> GetRollSetSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _rollSetHistoryService.GetRollSetSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_RollShop, RightLevel.View)]
    public Task<ActionResult> ElementDetails(long rollSetId)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(rollSetId);
      return PrepareActionResultFromVm(() => _rollSetHistoryService.GetRollSetHistoryActual(ModelState, rollSetId), "~/Views/Module/PE.Lite/RollSetHistory/_HistoryRollSetBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_RollSetManagement, Constants.SmfAuthorization_Module_RollShop, RightLevel.View)]
    public async Task<ActionResult> GetRollSetHistoryById(long id)
    {
      ViewBag.RSetHistory = ListValuesHelper.GetRollSetHistory(id);
      VM_RollSetTurningHistory model = _rollSetHistoryService.GetRollSetHistoryById(ModelState, id);
      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/RollSetHistory/_RollSetHistoryDetails.cshtml", model);
    }
  }
}
