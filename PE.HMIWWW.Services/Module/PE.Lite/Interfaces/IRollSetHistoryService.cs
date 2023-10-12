using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollSetHistoryService
  {
    DataSourceResult GetRollSetSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RollSetOverviewFull GetRollSetById(long? rollSetId);
    VM_RollSetOverviewFull GetRollSetDetails(ModelStateDictionary modelState, long rollSetId);
    VM_RollSetTurningHistory GetRollSetHistoryActual(ModelStateDictionary modelState, long id);
    VM_RollSetTurningHistory GetRollSetHistoryById(ModelStateDictionary modelState, long id);
  }
}
