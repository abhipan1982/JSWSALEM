using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IGrindingTurningService
  {
    DataSourceResult GetPlannedRollsetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    DataSourceResult GetScheduledRollsetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);
    VM_RollSetTurningHistory GetRollSetHistoryActual(ModelStateDictionary modelState, long id);
    VM_RollSetTurningHistory GetRollSetHistoryById(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm);
    Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> ConfirmUpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm);
    SelectList GetGrooveList();
    VM_GrooveTemplate GetGrooveTemplate(long grooveTemplateId);
  }
}
