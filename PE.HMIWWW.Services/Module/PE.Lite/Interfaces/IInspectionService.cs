using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.QualityInspection;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IInspectionService
  {
    DataSourceResult GetInspectionRawMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetCoilInspectionStationRawMaterialSearchList(ModelStateDictionary modelState,
      DataSourceRequest request);
    DataSourceResult GetBundleInspectionStationRawMaterialSearchList(ModelStateDictionary modelState,
      DataSourceRequest request);
    DataSourceResult GetDefectsByRawMaterialId(ModelStateDictionary modelState, DataSourceRequest request,
      long rawMaterialId);
    VM_QualityInspection GetInspectionDetailsByRawMaterialId(ModelStateDictionary modelState, long rawMaterialId);
    Task<VM_Base> DeleteDefect(ModelStateDictionary modelState, long defectId);
    Task<VM_Base> AssignRawMaterialQuality(ModelStateDictionary modelState, VM_QualityInspection rawMaterialQuality);
    Task<VM_Base> AssignRawMaterialFinalQuality(ModelStateDictionary modelState,
      VM_QualityInspection rawMaterialQuality);
    VM_QualityInspection GetQualityByRawMaterial(long id);
    VM_Defect GetDefect(long id);
    IList<VM_Asset> GetAssets();
    IList<VM_DefectCatalogue> GetDefectCatalogues();
    Task<VM_Base> UpdateDefect(ModelStateDictionary modelState, VM_Defect defect);
    VM_RawMaterialOverview GetRawMaterialDetails(ModelStateDictionary modelState, long rawMaterialId);
    VM_Scrap GetScrapByRawMaterial(long id);
    DataSourceResult GetInspectionByWorkOrder(ModelStateDictionary modelState, DataSourceRequest request, long workOrderId);
  }
}
