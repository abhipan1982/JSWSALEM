using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDataAnalysisService
  {
    DataSourceResult GetDelaysDataList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetWorkOrdersDataList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetDefectsDataList(ModelStateDictionary modelState, DataSourceRequest request);
  }
}
