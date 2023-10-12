using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.FeatureMap;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IFeatureMap
  {
    DataSourceResult GetFeatureMapOverList(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_FeatureMap> GetFeatureAsync(ModelStateDictionary modelState, long featureId);
    Task<VM_Base> EditFeatureLimitsAsync(ModelStateDictionary modelState, VM_FeatureMap data);
  }
}
