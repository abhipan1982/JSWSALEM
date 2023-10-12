using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Products;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IRawMaterialService
  {
    DataSourceResult GetRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetMeasurementSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RawMaterialOverview GetRawMaterialById(long? materialId);
    VM_RawMaterialOverview GetRawMaterialDetails(ModelStateDictionary modelState, long materialId);
    VM_RawMaterialMeasurements GetMeasurementDetails(ModelStateDictionary modelState, long materialId);
    VM_RawMaterialHistory GetHistoryDetails(ModelStateDictionary modelState, long rawMaterialStepId);
    VM_L3MaterialData GetL3MaterialData(ModelStateDictionary modelState, long measurementId);
    VM_RawMaterialGenealogy GetRawMaterialGenealogy(ModelStateDictionary modelState, long materialId);
    Task<VM_Base> AssignRawMaterial(ModelStateDictionary modelState, long rawMaterialId, long l3MaterialId);
    Task<VM_Base> UnassignRawMaterial(ModelStateDictionary modelState, long rawMaterialId);
    DataSourceResult GetMeasurmentsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);
    DataSourceResult GetHistoryByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);
    IList<VM_Asset> GetAssets();
    Task<VM_Base> AssignRawMaterialQualityAsync(ModelStateDictionary modelState, VM_QualityAssignment rawMaterialQuality);
    List<VM_DefectCatalogue> GetDefectsList();
    DataSourceResult GetRawMaterialEvents(ModelStateDictionary modelState, DataSourceRequest request, long rawMaterialId);
    DataSourceResult GetNotAssignedRawMaterials(ModelStateDictionary modelState, DataSourceRequest request);
    VM_RawMaterialGenealogy GetMaterialDivisionHistory(ModelStateDictionary modelState, long materialId);
    VM_RawMaterialGenealogy GetMaterialForReadyOperation(ModelStateDictionary modelState, long materialId);
    IList<PRMProductCatalogueType> GetProductCatalogueTypes();
    DataSourceResult GetDefectListByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request, long rawMaterialId);
  }
}
