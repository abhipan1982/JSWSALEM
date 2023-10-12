using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IBundleWeighingMonitorService
  {
    Task<VM_RawMaterialOverview> GetRawMaterialOnWeightAsync(ModelStateDictionary modelState, long? rawMaterialId);
    Task<DataSourceResult> GetWorkOrdersBeforeWeightListAsync(ModelStateDictionary modelState, DataSourceRequest request);
    Task<DataSourceResult> GetRawMaterialsAfterWeightListAsync(ModelStateDictionary modelState, DataSourceRequest request, long? rawMaterialId);
  }
}
