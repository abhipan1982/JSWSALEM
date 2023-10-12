using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.BilletYard;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IBilletYardService
  {
    Task<IList<VM_BilletYard>> GetYards();
    Task<VM_BilletYardDetails> GetLocations(long yardId);
    VM_BilletLocationDetails GetLocationDetails(long id);
    Task<VM_BilletLocationDetails> GetMaterialsInLocation(long locationId);
    DataSourceResult GetScheduleGrid(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetHeatsOnYards(ModelStateDictionary modelState, DataSourceRequest request);
    Task<IList<PRMMaterialCatalogue>> GetMaterialCataloguesList();
    DataSourceResult GetHeatsInReception(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetHeatsInScrap(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetHeatGroupsInLocations(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetHeatsInChargingGrid(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetLocations(ModelStateDictionary modelState, DataSourceRequest request);
    DataSourceResult GetCharginGridHeatsGrid(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_Base> TransferHeatIntoLocation(ModelStateDictionary modelState, VM_HeatIntoLocation data);
    Task<VM_Base> TransferHeatIntoChargingGrid(ModelStateDictionary modelState, VM_HeatIntoChargingGrid data);
    VM_HeatsOnYards GetHeat(long heatId, short groupId, long locationId);
    VM_MaterialOverview GetMaterial(long id);
    Task<VM_Base> AddMaterials(ModelStateDictionary modelState, VM_Materials data);
    Task<VM_SearchResult> SearchLocationIds(ModelStateDictionary modelState, long heatId, long yardId);
    VM_BilletLocationDetails GetLocationWithMaterials(long id);
    Task<VM_Base> TransferHeatFromChargingGrid(ModelStateDictionary modelState, VM_HeatFromChargingGrid data);
    Task<VM_Base> ScrapMaterials(ModelStateDictionary modelState, VM_MaterialsScrap data);
    Task<VM_Base> UnscrapMaterials(ModelStateDictionary modelState, VM_MaterialsScrap data);
    Task<VM_Base> CreateHeatWithMaterials(ModelStateDictionary modelState, VM_Heat heat);
  }
}
