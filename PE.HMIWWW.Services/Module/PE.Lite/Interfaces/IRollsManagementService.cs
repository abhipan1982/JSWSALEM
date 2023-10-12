using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollsManagementService
  {
    DataSourceResult GetRollsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> InsertRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    VM_RollsWithTypes GetRoll(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    Task<VM_Base> ScrapRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel);
    Task<VM_Base> DeleteRoll(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<List<VM_RollTypeDiameterLimit>> GetRollLimits();
  }
}
