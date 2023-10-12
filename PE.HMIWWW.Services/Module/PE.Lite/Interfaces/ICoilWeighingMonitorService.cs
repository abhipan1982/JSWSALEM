using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ICoilWeighingMonitorService
  {
    Task<VM_RawMaterialOverview> GetRawMaterialOnWeightAsync(ModelStateDictionary modelState, long? rawMaterialId);
    Task<DataSourceResult> GetRawMaterialsBeforeWeightListAsync(ModelStateDictionary modelState, DataSourceRequest request, DCMaterialPosition materialPosition);
    Task<DataSourceResult> GetRawMaterialsAfterWeightListAsync(ModelStateDictionary modelState, DataSourceRequest request, DCMaterialPosition materialPosition);
  }
}
