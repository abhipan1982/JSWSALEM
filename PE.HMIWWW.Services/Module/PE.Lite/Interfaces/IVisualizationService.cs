using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.EnumClasses;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Visualization;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IVisualizationService
  {
    Task<VM_Base> RequestLastMaterialPosition(ModelStateDictionary modelState);
    DataSourceResult GetMaterialsInArea(ModelStateDictionary modelState, DataSourceRequest request, List<VM_RawMaterialOverview> materials);
    DataSourceResult GetWorkOrdersInRealizationList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetWorkOrdersPlannedList(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetWorkOrdersProducedList(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_TrackingMaterialOverview> GetTrackingMaterialDetails(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> TrackingRemoveAction(ModelStateDictionary modelState, long rawMaterialId);
    Task<VM_Base> TrackingReadyAction(ModelStateDictionary modelState, VM_RawMaterialGenealogy rawMaterialId);
    Task<VM_Base> ProductUndoAction(ModelStateDictionary modelState, long rawMaterialId);
    Task<VM_Base> TrackingScrapAction(ModelStateDictionary modelState, VM_Scrap scrapModel);
    DataSourceResult GetMaterialInFurnace(ModelStateDictionary modelState, DataSourceRequest request, List<VM_Furnace> materials);
    VM_RawMaterialEXT GetRawMaterialExtDetails(ModelStateDictionary modelState, long? rawMaterialId);
    List<VM_Asset> GetQueueAreas(ModelStateDictionary modelState, List<long> materialsInAreas, List<long> materialsInFurnace, int selected);
    IList<VM_Asset> GetAssets();
    VM_Scrap GetRawMaterialPartialScrap(long rawMaterialId);
    VM_RawMaterialRejection GetRawMaterialRejection(long rawMaterialId);
    Task<VM_Base> RejectRawMaterial(ModelStateDictionary modelState, VM_RawMaterialRejection rawMaterial);
    DataSourceResult GetMaterialsInLayer(ModelStateDictionary modelState, DataSourceRequest request, long layerId);
    IList<VM_RawMaterialOverview> GetLayers(ModelStateDictionary modelState);
    IList<VM_RawMaterialOverview> GetLayerById(ModelStateDictionary modelState, long layerId);
  }
}
