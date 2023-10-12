using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IActualStandsConfigurationService
  {
    DataSourceResult GetStandConfigurationList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    DataSourceResult GetPassChangeActualList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    DataSourceResult GetCassetteRollSetsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long? cassetteId, short? standStatus);
    DataSourceResult GetRollSetGroovesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long rollsetHistoryId, short? standStatus);
    VM_StandConfiguration GetStandConfiguration(ModelStateDictionary modelState, long id);
    VM_SwappingCassettes GetSwapCassette(ModelStateDictionary modelState, long id);
    VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id);
    VM_GroovesRoll GetGrooveDetails(ModelStateDictionary modelState, long id);
    VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id);
    VM_CassetteOverviewWithPositions GetCassetteWithPositions(ModelStateDictionary modelState, long id, long standId);
    DataSourceResult GetRollSetInCassetteList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long cassetteId);
    VM_RollChangeOperation GetRollChangeOperation(ModelStateDictionary modelState, long id);
    VM_RollChangeOperation GetRollChangeOperationForRollSet(ModelStateDictionary modelState, long id, short? positionId);
    Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm);
    VM_CassetteOverviewWithPositions GetCassetteOverviewWithPositions(ModelStateDictionary modelState, long id);
    Task<VM_Base> UpdateStandConfiguration(ModelStateDictionary modelState, VM_StandConfiguration actualVm);
    Task<VM_Base> MountCassette(ModelStateDictionary modelState, VM_StandConfiguration actualVm);
    Task<VM_Base> MountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation actualVm);
    Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_RollChangeOperation actualVm);
    Task<VM_Base> DismountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation actualVm);
    Task<VM_Base> DismountCassette(ModelStateDictionary modelState, VM_StandConfiguration actualVm);
  }
}
