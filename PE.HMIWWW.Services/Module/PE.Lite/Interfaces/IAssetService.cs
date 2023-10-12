using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IAssetService
  {
    DataSourceResult GetAssetOverList(ModelStateDictionary modelState, DataSourceRequest request);

    DataSourceResult GetFeatureByAssetId(ModelStateDictionary modelState, DataSourceRequest request, long assetId);
  }
}
