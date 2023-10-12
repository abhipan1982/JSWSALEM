using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite;
using PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class ActualStandConfigurationController : BaseController
  {
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.StandStat = ListValuesHelper.GetStandStat();
      ViewBag.Arrang = ListValuesHelper.GetCassetteArrangement();
      ViewBag.ArrangNoUndefined = ListValuesHelper.GetCassetteArrangementNoUndefined();
      ViewBag.CassetteType = ListValuesHelper.GetCassetteType();
      ViewBag.CassetteStatus = ListValuesHelper.GetCassetteStatus();
      ViewBag.RollsetStatus = ListValuesHelper.GetRollSetStatus();
      ViewBag.GrooveStatus = ListValuesHelper.GetRollGrooveStatus();
      ViewBag.StandStatNoUndefined = ListValuesHelper.GetStandStatNoUndefined();
      ViewBag.NumberOfRolls = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.StandActivity = ListValuesHelper.GetStandActivity();
      ViewBag.IsThirdActive = ListValuesHelper.GetNumberOfActiveRoll();
      ViewBag.RSHistStatus = ListValuesHelper.GetRollSetHistoryStatus();
      ViewBag.RollsetTypes = ListValuesHelper.GetRollsetTypeList();
      ViewBag.GrooveCondition = ListValuesHelper.GetGrooveConditionEnum();
    }

    #region services

    IActualStandsConfigurationService _actualStandsConfigurationService;

    #endregion

    #region ctor

    public ActualStandConfigurationController(IActualStandsConfigurationService service)
    {
      _actualStandsConfigurationService = service;
    }

    #endregion

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/ActualStandConfiguration/Index.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> RollSetInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_RollSetInfoPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> RenderNewCassetteDetails(long cassetteId, long standId)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassetteWithPositions(ModelState, cassetteId, standId), "~/Views/Module/PE.Lite/ActualStandConfiguration/_NewCassetteDetailsPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> CassetteInfoPopupDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassette(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_CassetteInfoPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditStandConfigurationDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_EditPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> MountCassetteDialog(long id)
    {
      ViewBag.AvailableCassettes = ListValuesHelper.GetCassettesReadyForMount();

      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_MountCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> MountEmptyCassetteDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_MountEmptyCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> DismountCassetteDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetStandConfiguration(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_DismountCassettePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> MountRollSetDialog(long id)
    {
      ViewBag.AvailableRollSets = ListValuesHelper.GetRollsetsReadyOnlyWithTypes();

      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperation(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_MountRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> DismountRollSetDialog(long id, short? param)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperationForRollSet(ModelState, id, param), "~/Views/Module/PE.Lite/ActualStandConfiguration/_DismountRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> SwapRollSetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollChangeOperation(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_SwapRollSetPopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> PassChangeGrooveDialog(long id)
    {
      ViewBag.GrooveConditionSelectList = new SelectList(ListValuesHelper.GetGrooveConditionEnum().ToList(), "Value", "Text");
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_PassChangeGroovePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> EditGrooveDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetRollSetDisplay(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_EditGroovePopup.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetStandConfigurationCatalogueData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetStandConfigurationList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetPassChangeActualData([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetPassChangeActualList(ModelState, request));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetCassetteRollSetsData([DataSourceRequest] DataSourceRequest request, long? cassetteId, short? standStatus)
    {
      return PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetCassetteRollSetsList(ModelState, request, cassetteId, standStatus));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetRollSetInCassetteList([DataSourceRequest] DataSourceRequest request, long? cassetteId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetRollSetInCassetteList(ModelState, request, (long)cassetteId));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetRollSetGroovesData([DataSourceRequest] DataSourceRequest request, long rollsetHistoryId, short? standStatus) // rollsetHistoryId,
    {
      return PrepareJsonResultFromDataSourceResult(() => _actualStandsConfigurationService.GetRollSetGroovesList(ModelState, request, rollsetHistoryId, standStatus));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> AssembleCassetteAndRollsetDialog(long id)
    {
      return PreparePopupActionResultFromVm(() => _actualStandsConfigurationService.GetCassetteOverviewWithPositions(ModelState, id), "~/Views/Module/PE.Lite/ActualStandConfiguration/_AssembleCassettePopup.cshtml");
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetCassetteToStandList([DataSourceRequest] DataSourceRequest request, long standId)
    {
      return Json(ActualStandsConfigurationService.GetCassetteToStandList(standId));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public JsonResult GetEmptyCassetteToStandList([DataSourceRequest] DataSourceRequest request, long standId)
    {
      return Json(ActualStandsConfigurationService.GetEmptyCassetteToStandList(standId));
    }

    [HttpGet]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public JsonResult GetAvailableRollsets(short type)
    {
      return Json(ListValuesHelper.GetRollsetsReadyOnlyWithTypes(type));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> UpdateStandConfiguration(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.UpdateStandConfiguration(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountCassette(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.MountCassette(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> MountRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.MountRollSet(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> SwapRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.SwapRollSet(ModelState, viewModel));
    }
    #region Actions

    #region Grooves

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> ConfigRollSetSubmit(VM_RollsetDisplay viewModel, string submit)
    {
      //submit = "";
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.UpdateGroovesToRollSetDisplay(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountRollSet(VM_RollChangeOperation viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.DismountRollSet(ModelState, viewModel));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_ActualStandConfiguration, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> DismountCassette(VM_StandConfiguration viewModel, string submit)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _actualStandsConfigurationService.DismountCassette(ModelState, viewModel));
    }


    #endregion

    #endregion
  }
}
