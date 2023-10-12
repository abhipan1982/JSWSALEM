using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Measurements;
using PE.HMIWWW.ViewModel.Module.Lite.Visualization;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IMeasurementsService
  {
    DataSourceResult GetFeatures(ModelStateDictionary modelState, DataSourceRequest request, int areaCode);

    DataSourceResult GetMeasurements(ModelStateDictionary modelState, DataSourceRequest request, long featureId,
      long workOrderId);

    IList<VM_RawMaterialInArea> GetRawMaterialWithArea(ModelStateDictionary modelState, long rawMaterialId);

    Task<List<VM_AreaRawMaterialMeasurements>> GeMaterialMeasurements(ModelStateDictionary modelState,
      long? rawMaterialId);

    DataSourceResult GetMeasurementsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long areaCode, long rawMaterialId);

    IList<VM_Temperature> GetFurnaceTemperatures(ModelStateDictionary modelState);
  }
}
