using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.RollChange;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRollChangeService
  {
    VM_ConfirmationForRmAndIm BuildVmConfirmationForRmAndIm(short? operationType, long? rollsetId,
      long? mountedRollsetId, short? position, short? standNo);

    DataSourceResult GetStandGridData(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      long firstStand, long lastStand);

    DataSourceResult GetRollsetGridData(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request,
      long standNo);

    DataSourceResult GetRollGroovesGridData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long rollSetId, long standNo);

    List<V_CassettesInStand> GetCassettesInfoRmIm();
    List<V_RollSetOverview> GetAvailableRollsets(short standNo);
    V_RollSetOverview GetRollSetDetails(long rollSetId);
    Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    Task<VM_Base> DismountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    Task<VM_Base> MountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel);
    short GetGrooveNumber(long rollId);
    long? GetRollId(V_RollSetOverview rollSet);
  }
}
