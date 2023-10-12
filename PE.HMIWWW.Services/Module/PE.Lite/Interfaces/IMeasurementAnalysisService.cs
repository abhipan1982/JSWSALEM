using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementAnalysis;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IMeasurementAnalysisService
  {
    Task<VM_RawMaterialMeasurement> GeMaterialMeasurement(ModelStateDictionary modelState, long rawMaterialId, long featureId);
    DataSourceResult GetFeaturesByType(ModelStateDictionary modelState, DataSourceRequest request, int areaCode, bool lengthRelated);
    Task<VM_RawMaterialMeasurementBundle> GetMeasurementComparison(long[] rawMaterialIds, long[] featureIds, bool timeNormalization);
  }
}
