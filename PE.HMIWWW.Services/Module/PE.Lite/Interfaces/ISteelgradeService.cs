using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.SteelFamily;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ISteelgradeService
  {
    DataSourceResult GetSteelgradeList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_Steelgrade GetSteelgrade(ModelStateDictionary modelState, long id);
    Task<VM_Base> CreateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade);
    Task<VM_Base> UpdateSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgrade);
    Task<VM_Base> DeleteSteelgrade(ModelStateDictionary modelState, VM_Steelgrade steelgradeId);
    IList<VM_Steelgrade> GetSteelgradeParents();
    VM_Steelgrade GetSteelgradeDetails(ModelStateDictionary modelState, long id);
    IList<VM_Steelgrade> GetSteelgradesByHeat(long? heatId);
    Task<bool> ValidateSteelgradeCode(string code);
    Task<bool> ValidateSteelgradeName(string name);
    IList<VM_SteelFamily> GetSteelFamilies();
  }
}
