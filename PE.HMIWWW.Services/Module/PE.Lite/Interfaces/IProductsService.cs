using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Products;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductsService
  {
    DataSourceResult GetProductsSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_ProductsOverview GetProductsDetails(ModelStateDictionary modelState, long productId);
    Task<Stream> RequestPdfFromZebraWebServiceForHmiAsync(ModelStateDictionary modelState, long productId);
    Task<VM_Base> AssignProductQualityAsync(ModelStateDictionary modelState, long productId, short quality, List<long> defectIds);
    DataSourceResult GetMaterialsListByProductId(ModelStateDictionary modelState, DataSourceRequest request, long productId);
    Task<VM_Base> CreateBundleAsync(ModelStateDictionary modelState, VM_Bundle bundle);
    Task<VM_Bundle> GetNewBundleDataAsync(ModelStateDictionary modelState, long workOrderId);
  }
}
