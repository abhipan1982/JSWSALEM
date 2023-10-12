using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Tracking;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ITrackingService
  {
    DataSourceResult GetTrackingList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    VM_TrackingOverview GetTrackingDetails(ModelStateDictionary modelState, long dimRawMaterialKey, long? workOrderId,
      long? heatId);
  }
}
