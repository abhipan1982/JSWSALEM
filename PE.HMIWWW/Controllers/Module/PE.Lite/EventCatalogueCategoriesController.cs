using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.Core;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Controllers;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Event;

namespace PE.HMIWWW.Controllers.Module.PE.Lite
{
  public class EventCatalogueCategoriesController : BaseController
  {
    private readonly IEventCatalogueCategoriesService _service;

    public EventCatalogueCategoriesController(IEventCatalogueCategoriesService service)
    {
      _service = service;
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public ActionResult Index()
    {
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      return View("~/Views/Module/PE.Lite/EventCatalogueCategories/Index.cshtml");
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.View)]
    public async Task<ActionResult> GetEventCatalogueCategoriesList([DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = _service.GetEventCatalogueCategoriesList(ModelState, request);
      return await PrepareJsonResultFromDataSourceResult(() => result);
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> AddEventCatalogueCategoriesAsync(VM_EventCatalogueCategory vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.AddEventCatalogueCategoryAsync(ModelState, vm));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventCatalogueCategoriesAddPopup()
    {
      ViewBag.EventTypesTree = _service.GetEventTypesTree();
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      VM_EventCatalogueCategory result = new VM_EventCatalogueCategory();
      return PreparePopupActionResultFromVm(() => result,
        "~/Views/Module/PE.Lite/EventCatalogueCategories/_EventCatalogueCategoriesAddPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public async Task<ActionResult> UpdateEventCatalogueCategoriesAsync(VM_EventCatalogueCategory vm)
    {
      return await TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.UpdateEventCatalogueCategoriesAsync(ModelState, vm));
    }

    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Update)]
    public Task<ActionResult> EventCatalogueCategoriesEditPopup(long eventCatalogueCategoryId)
    {
      ViewBag.EventTypesTree = _service.GetEventTypesTree();
      ViewBag.EnumAssignmentType = _service.GetEnumAssignmentTypeList();
      return PreparePopupActionResultFromVm(
        () => _service.GetEventCatalogueCategory(ModelState, eventCatalogueCategoryId),
        "~/Views/Module/PE.Lite/EventCatalogueCategories/_EventCatalogueCategoriesEditPopup.cshtml");
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Delays, Constants.SmfAuthorization_Module_Events,
      RightLevel.Delete)]
    public Task<JsonResult> DeleteEventCatalogueCategory(long itemId)
    {
      VM_EventCatalogueCategory eventCat = new VM_EventCatalogueCategory {Id = itemId};
      return TaskPrepareJsonResultFromVm<VM_Base, Task<VM_Base>>(() =>
        _service.DeleteEventCatalogueCategoryAsync(ModelState, eventCat));
    }

    [HttpPost]
    [SmfAuthorization(Constants.SmfAuthorization_Controller_Steelgrade, Constants.SmfAuthorization_Module_ProdManager,
      RightLevel.View)]
    public Task<ActionResult> ElementDetails(long id)
    {
      return PrepareActionResultFromVm(() => _service.GetEventCatalogueCategory(ModelState, id),
        "~/Views/Module/PE.Lite/EventCatalogueCategories/_EventCatalogueCategoriesBody.cshtml");
    }

    public JsonResult ServerFiltering_GetEventGroups()
    {
      IList<VM_EventGroupsCatalogue> steelGroups = _service.GetEventGroups();

      return Json(steelGroups);
    }

    public JsonResult ServerFiltering_GetEnumAssignmentType()
    {
      SelectList list = _service.GetEnumAssignmentTypeList();

      return Json(list);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateEventCategoriesCode(string code)
    {
      bool result = await _service.ValidateEventCategoriesCode(code);
      return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> ValidateEventCategoriesName(string name)
    {
      bool result = await _service.ValidateEventCategoriesName(name);
      return Json(result);
    }
  }
}
