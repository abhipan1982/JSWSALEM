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
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class CassetteController : BaseController
  {
    #region services

    private readonly ICassetteService _cassetteService;

    #endregion

    #region ctor

    public CassetteController(ICassetteService service)
    {
      _cassetteService = service;
    }

    #endregion

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.CassStatusShortList = ListValuesHelper.GetCassetteStatusShortList();
      ViewBag.CassStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.CassArrangement = ListValuesHelper.GetCassetteArrangement();
      ViewBag.CassType = ListValuesHelper.GetCassetteType();
      ViewBag.RsCounter = ListValuesHelper.GetRollSetCounter();
      ViewBag.RollSetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteTypeEnum();
      ViewBag.RollSetType = ListValuesHelper.GetRollsetTypeList();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Cassette/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public ActionResult AddCassetteDialog()
    {
      return PartialView("~/Views/Module/PE.Lite/Cassette/_AddPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditCassetteDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _cassetteService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/Cassette/_EditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<ActionResult> GetCasseteInfo(long casseteId)
    {
      return PreparePopupActionResultFromVm(() => _cassetteService.GetCassette(ModelState, casseteId), "~/Views/Module/PE.Lite/Cassette/_InfoPopup.cshtml");
    }


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetCassetteData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _cassetteService.GetCassetteList(ModelState, request));
    }

    #region Actions
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> InsertCassette(VM_CassetteOverview viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.InsertCassette(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DeleteCassette(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.DeleteCassette(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<JsonResult> UpdateCassette(VM_CassetteOverview viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.UpdateCassette(ModelState, viewModel));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Cassette, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public Task<JsonResult> DismountCassette(VM_LongId viewModel)
    {
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _cassetteService.DismountCassette(ModelState, viewModel));
    }
    #endregion
  }
}
