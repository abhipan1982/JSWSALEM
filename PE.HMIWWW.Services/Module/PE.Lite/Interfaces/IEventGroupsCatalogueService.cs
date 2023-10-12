using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Event;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IEventGroupsCatalogueService
  {
    VM_EventGroupsCatalogue GetEventGroup(ModelStateDictionary modelState, long id);
    DataSourceResult GetEventGroupList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> AddEventGroupAsync(ModelStateDictionary modelState, VM_EventGroupsCatalogue vm);
    Task<VM_Base> UpdateEventGroupAsync(ModelStateDictionary modelState, VM_EventGroupsCatalogue vm);

    Task<VM_Base> DeleteEventGroupAsync(ModelStateDictionary modelState, VM_EventGroupsCatalogue vm);

    Task<bool> ValidateEventGroupsCode(string code);
    Task<bool> ValidateEventGroupsName(string name);
  }
}
