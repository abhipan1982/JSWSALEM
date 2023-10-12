using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.CassetteType;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ICassetteTypeService
  {
    DataSourceResult GetCassetteTypeList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> InsertCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel);
    VM_CassetteType GetCassetteType(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateCassetteType(ModelStateDictionary modelState, VM_CassetteType viewModel);
    Task<VM_Base> DeleteCassetteType(ModelStateDictionary modelState, VM_LongId viewModel);
  }
}
