using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IProductService
  {
    DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    VM_ProductCatalogue GetProductCatalogue(long id);
    Task<VM_Base> CreateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);
    Task<VM_Base> UpdateProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);
    Task<VM_Base> DeleteProductCatalogue(ModelStateDictionary modelState, VM_ProductCatalogue productCatalogue);

    //Task<VM_ProductCatalogue> GetProductDetails(ModelStateDictionary modelState, long id);//@Av
    IList<VM_Steelgrade> GetSteelgradeList();
    IList<VM_Shape> GetShapeList();
    IList<VM_ProductCatalogueType> GetProductCatalogueTypeList();
    VM_ProductCatalogue GetProductDetails(ModelStateDictionary modelState, long id);
    VM_ProductCatalogue GetProductCatalogueOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId);
  }
}
