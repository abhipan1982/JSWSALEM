using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ILayerService
  {
    DataSourceResult GetLayerSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RawMaterialOverview GetLayerDetails(ModelStateDictionary modelState, long materialId);
    VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long materialId);
    VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId);

    DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);

    DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);

    DataSourceResult GetChildrenByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);
  }
}
