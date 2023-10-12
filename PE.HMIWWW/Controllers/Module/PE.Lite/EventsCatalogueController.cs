using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Event;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class EventsCatalogueController : BaseController
  {
    private readonly IDelaysService _service;

    public EventsCatalogueController(IDelaysService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.EventTypes = ListValuesHelper.GetEventTypesList();
      return View("~/Views/Module/PE.Lite/EventsCatalogue/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public async Task<ActionResult> GetEventCatalogueList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = await _service.GetEventCatalogueList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events, RightLevel.View)]
    public async Task<ActionResult> GetEventCatalogueListByEventData([DataSourceRequest] DataSourceRequest request, short eventTypeCode, string eventCategoryGroupCode, string eventCatalogueCategoryCode)
    {
      DataSourceResult result = await _service.GetEventCatalogueListByEventData(ModelState, request, eventTypeCode, eventCategoryGroupCode, eventCatalogueCategoryCode);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    public Task<JsonResult> GetEventCatalogueSearchList([DataSourceRequest] DataSourceRequest request)
    {
      return PrepareJsonResultFromDataSourceResult(() => _service.GetEventCatalogueSearchList(ModelState, request));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
  RightLevel.View)]
    public JsonResult GetParentEventCodes()
    {
      return Json(_service.GetParentEventCodes());
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> AddEventCatalogueAsync(VM_EventCatalogue eventCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddEventCatalogueAsync(ModelState, eventCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventCatalogueAddPopup()
    {
      VM_EventCatalogue result = new VM_EventCatalogue();
      result.IsActive = true;
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/EventsCatalogue/_EventCatalogueAddPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateEventCatalogueAsync(VM_EventCatalogue eventCatalogue)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateEventCatalogueAsync(ModelState, eventCatalogue));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventCatalogueEditPopup(long eventCatalogueId)
    {
      ViewBag.ParentEvent = _service.GetEventCataloguesForParentSelector();
      ViewBag.CatalogueCategory = _service.GetEventCategories();
      return PreparePopupActionResultFromVm(() => _service.GetEventCatalogue(ModelState, eventCatalogueId),
        "~/Views/Module/PE.Lite/EventsCatalogue/_EventCatalogueEditPopup.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetEventCatalogue(ModelState, id),
        "~/Views/Module/PE.Lite/EventsCatalogue/_EventCatalogueBody.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager, RightLevel.View)]
    public Task<ActionResult> EventCatalogueDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetEventCatalogue(ModelState, id),
        "~/Views/Module/PE.Lite/EventsCatalogue/_EventCatalogueDetails.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteEvent(long itemId)
    {
      VM_EventCatalogue eventCat = new VM_EventCatalogue {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteEventCatalogueAsync(ModelState, eventCat));
    }

    public JsonResult ServerFiltering_GetEventsCatalogue()
    {
      IList<VM_EventCatalogueCategory> defectParents = _service.GetEventCategories();

      return Json(defectParents);
    }

    public JsonResult ServerFiltering_GetEventCataloguesForParentSelector()
    {
      IList<VM_EventCatalogue> defectParents = _service.GetEventCataloguesForParentSelector();

      return Json(defectParents);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateEventCode(string code)
    {
      bool result = await _service.ValidateEventCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateEventName(string name)
    {
      bool result = await _service.ValidateEventName(name);
      return Json(result);
    }
  }
}
