using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.SteelFamily;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ISteelFamilyService
  {
    DataSourceResult GetSteelFamilyList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_SteelFamily GetSteelFamily(ModelStateDictionary modelState, long id);
    Task<VM_Base> CreateSteelFamily(ModelStateDictionary modelState, VM_SteelFamily group);
    Task<VM_Base> UpdateSteelFamily(ModelStateDictionary modelState, VM_SteelFamily group);
    Task<VM_Base> DeleteSteelFamily(ModelStateDictionary modelState, VM_SteelFamily group);
    IList<VM_SteelFamily> GetSteelFamilies();
    VM_SteelFamily GetSteelFamilyDetails(ModelStateDictionary modelState, long id);
    Task<bool> ValidateSteelFamilyCode(string code);
    Task<bool> ValidateSteelFamilyName(string name);
  }
}
