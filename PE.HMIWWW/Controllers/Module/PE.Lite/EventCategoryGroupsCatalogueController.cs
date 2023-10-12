using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Event;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class EventCategoryGroupsCatalogueController : BaseController
  {
    private readonly IEventGroupsCatalogueService _service;

    public EventCategoryGroupsCatalogueController(IEventGroupsCatalogueService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public ActionResult Index()
    {
      return View("~/Views/Module/PE.Lite/EventCategoryGroupsCatalogue/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public async Task<ActionResult> GetEventGroupsCatalogueList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetEventGroupList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> AddEventGroupsCatalogue(VM_EventGroupsCatalogue eventGroupsCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddEventGroupAsync(ModelState, eventGroupsCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventGroupsCatalogueAddPopup()
    {
      VM_EventGroupsCatalogue result = new VM_EventGroupsCatalogue();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/EventCategoryGroupsCatalogue/_EventGroupsCatalogueAddPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetEventGroup(ModelState, id),
        "~/Views/Module/PE.Lite/EventCategoryGroupsCatalogue/_EventGroupsCatalogueBody.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateEventGroupsCatalogue(VM_EventGroupsCatalogue eventGroupsCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateEventGroupAsync(ModelState, eventGroupsCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventGroupsCatalogueEditPopup(long eventCatalogueId)
    {
      return PreparePopupActionResultFromVm(() => _service.GetEventGroup(ModelState, eventCatalogueId),
        "~/Views/Module/PE.Lite/EventCategoryGroupsCatalogue/_EventGroupsCatalogueEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteEventGroupsCatalogue(long itemId)
    {
      VM_EventGroupsCatalogue eventCat = new VM_EventGroupsCatalogue {EventCategoryGroupId = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteEventGroupAsync(ModelState, eventCat));
    }
    //public JsonResult ServerFiltering_GetDelayGroups()
    //{
    //  IList<VM_DelayGroupsCatalogue> SteelGroups = _service.GetDelayGroups();

    //  return Json(SteelGroups);
    //}


    [HttpPost]
    public async Task<JsonResult> ValidateEventGroupsCode(string code)
    {
      bool result = await _service.ValidateEventGroupsCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateEventGroupsName(string name)
    {
      bool result = await _service.ValidateEventGroupsName(name);
      return Json(result);
    }
  }
}
