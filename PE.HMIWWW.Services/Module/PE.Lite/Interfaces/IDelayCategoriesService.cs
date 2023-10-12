using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Event;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEventCatalogueCategoriesService
  {
    VM_EventCatalogueCategory GetEventCatalogueCategory(ModelStateDictionary modelState, long id);

    DataSourceResult GetEventCatalogueCategoriesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_Base> AddEventCatalogueCategoryAsync(ModelStateDictionary modelState, VM_EventCatalogueCategory vm);
    Task<VM_Base> UpdateEventCatalogueCategoriesAsync(ModelStateDictionary modelState, VM_EventCatalogueCategory vm);
    Task<VM_Base> DeleteEventCatalogueCategoryAsync(ModelStateDictionary modelState, VM_EventCatalogueCategory vm);
    IList<VM_EventCatalogueCategory> GetEventCatalogueCategories();
    IList<VM_EventGroupsCatalogue> GetEventGroups();
    Task<bool> ValidateEventCategoriesCode(string code);
    Task<bool> ValidateEventCategoriesName(string name);
    SelectList GetEnumAssignmentTypeList();
    List<DropDownTreeItemModel> GetEventTypesTree();
  }
}
