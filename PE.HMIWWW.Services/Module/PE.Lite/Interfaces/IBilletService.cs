using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Steelgrade;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IBilletService
  {
    VM_MaterialCatalogue GetMaterialCatalogue(long id);

    DataSourceResult GetProductCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_Base> UpdateMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue);
    Task<VM_Base> CreateMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue);
    Task<VM_Base> DeleteMaterialCatalogue(ModelStateDictionary modelState, VM_MaterialCatalogue materialCatalogue);
    IList<VM_Steelgrade> GetSteelgradeList();
    IList<VM_Shape> GetShapeList();
    VM_MaterialCatalogue GetBilletDetails(ModelStateDictionary modelState, long id);
    VM_MaterialCatalogue GetBilletCatalogueOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId);

    IList<VM_MaterialCatalogueType> GetMaterialCatalogueTypeList();

    IList<VM_MaterialCatalogue> GetMaterialCataloguesByAnyFeaure(string text);
  }
}
