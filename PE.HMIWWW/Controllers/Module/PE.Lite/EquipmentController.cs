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
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class EquipmentController : BaseController
  {
    private readonly IEquipmentService _service;

    public EquipmentController(IEquipmentService service)
    {
      _service = service;
    }

    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
      base.OnActionExecuting(ctx);

      ViewBag.EquipmentStatus = ListValuesHelper.GetEquipmentStatuses();
      ViewBag.EquipmentServiceType = ListValuesHelper.GetEquipmentServiceTypes();
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/Equipment/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> GetEquipmentList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetEquipmentList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public Task<ActionResult> EquipmentEditPopupAsync(VM_LongId viewModel)
    {
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      return PreparePopupActionResultFromVm(() => _service.GetEquipment(viewModel.Id), "~/Views/Module/PE.Lite/Equipment/_EquipmentEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public Task<ActionResult> EquipmentStatusEditPopupAsync(long id)
    {
      return PreparePopupActionResultFromVm(() => _service.GetEquipmentStatus(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentStatusEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public Task<ActionResult> EquipmentClonePopupAsync(long id)
    {
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      return PreparePopupActionResultFromVm(() => _service.GetEquipment(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentClonePopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> UpdateEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> UpdateEquipmentStatusAsync(VM_EquipmentStatus vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.UpdateEquipmentStatus(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> CreateEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CreateEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> CloneEquipmentAsync(VM_Equipment vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.CloneEquipment(ModelState, vm));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public async Task<ActionResult> DeleteEquipmentAsync(long id)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() => _service.DeleteEquipment(id));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.Update)]
    public Task<ActionResult> EquipmentCreatePopupAsync()
    {
      ViewBag.EquipmentGroupList = ListValuesHelper.GetEquipmentGroupsList();
      return PreparePopupActionResultFromVm(() => _service.GetEmptyEquipment(), "~/Views/Module/PE.Lite/Equipment/_EquipmentCreatePopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public Task<ActionResult> ShowEquipmentHistory(long id)
    {
      return PreparePopupActionResultFromVm(() => _service.GetEquipmentHistory(id), "~/Views/Module/PE.Lite/Equipment/_EquipmentHistoryPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Equipment, Constants.SmfAuthorization_Module_Maintenance, RightLevel.View)]
    public async Task<ActionResult> GetEquipmentHistory([DataSourceRequest] DataSourceRequest request, long id)
    {
      DataSourceResult result = _service.GetEquipmentHistoryList(id, ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }
  }
}
