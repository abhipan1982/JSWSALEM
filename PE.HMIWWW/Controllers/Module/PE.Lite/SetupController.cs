using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System,
    RightLevel.View)]
  public class SetupController : BaseController
  {
    private readonly ISetupService _setupService;

    public SetupController(ISetupService service)
    {
      _setupService = service;
    }

    #region View

    //TELEGRAM
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Setup/Telegram.cshtml");
    }
    //SETUP
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult Properties(long setupType)
    {
      ViewBag.SetupType = setupType;
      ViewBag.Page_Title = $"{VM_Resources.HMI_MENU_Setup} - {ResourceController.GetResourceTextByResourceKey($"HMI_MENU_Setup{_setupService.GetSetupNameByType(setupType)}")}";
      List<string> filters = _setupService.GetListOfFiltersNameForSetup(setupType);
      return View("~/Views/Module/PE.Lite/Setup/Setup.cshtml", filters);
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> FindSetupProperties(long setupId)
    {
      ViewBag.SetupId = setupId;
      List<VM_Filters> listOfFilters = _setupService.GetValueOfFiltersForSetup(setupId);
      ViewBag.SetupPropertiesGridHeight = listOfFilters.Any(x => x.ParameterNameTranslated != null) ? "85%" : "99%";

      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupGrid.cshtml", listOfFilters);
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult FindSetupRelationInstructionForWorkOrder(long setupId, long setupTypeId, long workOrderId)
    {
      ViewBag.SetupId = setupId;
      ViewBag.SetupTypeId = setupTypeId;
      ViewBag.WorkOrderId = workOrderId;
      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupRelationGridForWorkOrder.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> SetupEditPopup(long setupId)
    {
      return PreparePopupActionResultFromVm(() => _setupService.GetFiltersListForSetupWithValues(ModelState, setupId), "~/Views/Module/PE.Lite/Setup/_SetupPopupEdit.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> SetupCopyPopup(long setupId)
    {
      return PreparePopupActionResultFromVm(() => _setupService.GetFiltersListForSetupWithValues(ModelState, setupId), "~/Views/Module/PE.Lite/Setup/_SetupPopupCopy.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public Task<ActionResult> SetupCreatePopup(long setupType)
    {
      return PreparePopupActionResultFromVm(() => _setupService.GetFiltersListForSetupType(ModelState, setupType), "~/Views/Module/PE.Lite/Setup/_SetupPopupCreate.cshtml");
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> SlidePopupDetails(long workOrderId)
    {
      long model = workOrderId;

      await Task.CompletedTask;

      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupDetailsSlidePopup.cshtml", model);
    }

    #endregion

    #region JSON


    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //SETUP
    public Task<JsonResult> GetSetupParametersSearchGridData([DataSourceRequest] DataSourceRequest request, long setupType)
    {
      return PrepareJsonResultFromDataSourceResult(() => _setupService.GetSetupSearchGridData(ModelState, request, setupType));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public JsonResult GetFilteringData(string tableName, string columnId, string columnName)
    {
      return Json(_setupService.GetFilteringData(tableName, columnId, columnName).ToList());
    }
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //public JsonResult FindIdOfInstruction(List<VM_Filters> listOfFilters)
    //{
    //  return Json(1, JsonRequestBehavior.AllowGet);
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //public async Task<JsonResult> GetSetupRelationInstructionData([DataSourceRequest] DataSourceRequest request, long setupId, long setupTypeId, long workOrderId)
    //{
    //  return Json(await _setupService.GetSetupRelationInstructions(ModelState, request, setupId, setupTypeId, workOrderId), JsonRequestBehavior.AllowGet);
    //  //return await PrepareJsonResultFromDataSourceResult(async () => await _setupService.GetSetupRelationInstructions(ModelState, request, setupId, setupTypeId, workOrderId));
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //public async Task<JsonResult> GetSetupRelationSummary([DataSourceRequest] DataSourceRequest request, long workOrderId)
    //{
    //  return Json(await _setupService.GetSetupRelationSummary(ModelState, request, workOrderId), JsonRequestBehavior.AllowGet);
    //}
    //[SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    //public async Task<JsonResult> CompareToCurrentSetup(long workOrderId)
    //{
    //  return Json(await _setupService.CompareToCurrentSetup(workOrderId), JsonRequestBehavior.AllowGet);
    //}

    #endregion

    #region Actions

    //SETUP
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> CreateSetup(VM_ListOfFilters model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.CreateSetup(ModelState, model));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> UpdateSetup(VM_SetupValues model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.UpdateSetupValue(ModelState, model));
    }
    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> UpdateSetupParameters(VM_ListOfFilters model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.UpdateSetupParameters(ModelState, model));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Update)]
    public async Task<ActionResult> CopySetup(VM_ListOfFilters model)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.CopySetup(ModelState, model));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.Delete)]
    public async Task<ActionResult> DeleteSetup(long setupId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.DeleteSetup(ModelState, setupId));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> SendSetupsToL1(long workOrderId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.SendSetupsToL1(ModelState, workOrderId));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public async Task<ActionResult> CalculateSetup(long setupId)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _setupService.CalculateSetup(ModelState, setupId));
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult SetupDetailsView(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupDetails.cshtml", workOrderId);
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult SetupRelationsView(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupRelations.cshtml", workOrderId);
    }
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public ActionResult SetupHistoryView(long workOrderId)
    {
      return PartialView("~/Views/Module/PE.Lite/Setup/_SetupHistory.cshtml", workOrderId);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Setup, Constants.SmfAuthorization_Module_System, RightLevel.View)]
    public Task<JsonResult> GetSetupInstructionData([DataSourceRequest] DataSourceRequest request, long setupId)
    {
      return PrepareJsonResultFromDataSourceResult(() => _setupService.GetSetupInstructions(ModelState, request, setupId));
    }

    #endregion
  }
}
