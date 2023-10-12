using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IBundleService
  {
    DataSourceResult GetBundleSearchList(ModelStateDictionary modelState, DataSourceRequest request);
  }
}
