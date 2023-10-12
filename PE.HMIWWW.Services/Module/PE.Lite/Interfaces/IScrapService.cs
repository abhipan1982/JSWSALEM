using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.ScrapGroup;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IScrapService
  {
    DataSourceResult GetScrapGroupList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_ScrapGroup GetScrapGroup(ModelStateDictionary modelState, long id);
    Task<VM_Base> CreateScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrap);
    Task<VM_Base> UpdateScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrap);
    Task<VM_Base> DeleteScrapGroup(ModelStateDictionary modelState, VM_ScrapGroup scrap);
    IList<VM_ScrapGroup> GetScrapGroups();
    VM_ScrapGroup GetScrapGroupDetails(ModelStateDictionary modelState, long id);
    Task<bool> ValidateScrapGroupCode(string code);
    Task<bool> ValidateScrapGroupName(string name);
  }
}
