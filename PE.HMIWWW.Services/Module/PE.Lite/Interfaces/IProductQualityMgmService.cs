using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductQualityMgmService
  {
    DataSourceResult GetProductHistoryList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);
    //Task<VM_Base> UpdateProductQuality(ModelStateDictionary modelState, VM_Equipment cat);

    Task<VM_Base> AssignQualityAsync(ModelStateDictionary modelState, long productId, short quality,
      List<long> defectIds);

    Task<VM_Base> AssignRawMaterialQualityAsync(ModelStateDictionary modelState, long rawMaterialId, short quality,
      List<long> defectIds);

    DataSourceResult GetDefectsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);

    DataSourceResult GetProductDefectsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long productId);

    Dictionary<int, string> GetProductQualityList();
  }
}
