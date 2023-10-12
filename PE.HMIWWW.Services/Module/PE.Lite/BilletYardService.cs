using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.YRD;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.BilletYard;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;
using Kendo.Mvc.Extensions;
using System;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class BilletYardService : BaseService, IBilletYardService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public BilletYardService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetHeatsOnYards(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      request.RenameRequestMember(nameof(VM_HeatsOnYards.NumberOfMaterials), nameof(V_HeatsOnYard.MaterialsOnArea));

      IQueryable<V_HeatsOnYard> list = _hmiContext.V_HeatsOnYards.Where(x => x.AreaId != null).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_HeatsOnYards(data));

      return result;
    }

    public async Task<IList<VM_BilletYard>> GetYards()
    {
      List<short> assets = new List<short>
      {
        YardType.MaterialYard, YardType.MaterialReception, YardType.MaterialCharging, YardType.MaterialScrap
      };

      List<VM_BilletYard> result = new List<VM_BilletYard>();
      result = await _peContext.MVHAssets
        .Include(x => x.FKAssetType)
        .Where(x => x.FKAssetType != null && assets.Contains(x.FKAssetType.EnumYardType))
        .Select(x => new VM_BilletYard
        {
          BilletYardId = x.AssetId,
          YardName = x.AssetDescription,
          IsChargingGrid = x.FKAssetType.EnumYardType == YardType.MaterialCharging,
          IsReception = x.FKAssetType.EnumYardType == YardType.MaterialReception,
          IsYard = x.FKAssetType.EnumYardType == YardType.MaterialYard,
          IsScrapped = x.FKAssetType.EnumYardType == YardType.MaterialScrap
        })
        .OrderByDescending(x => x.IsReception)
        .ThenByDescending(x => x.IsYard)
        .ThenByDescending(x => x.IsChargingGrid)
        .ThenByDescending(x => x.IsScrapped)
        .ToListAsync();

      foreach (VM_BilletYard item in result)
      {
        item.NumberOfMaterials = await _peContext.PRMMaterialSteps
          .CountAsync(step =>
            (step.FKAssetId == item.BilletYardId || step.FKAsset.FKParentAssetId == item.BilletYardId) &&
            step.StepNo == 0);
        int capacity = await _peContext.MVHAssetsLocations.Where(x => x.FKAsset.FKParentAssetId == item.BilletYardId)
          .SumAsync(x => (int?)x.PieceMaxCapacity) ?? 0;
        item.FreeSpace = capacity - item.NumberOfMaterials;
      }

      return result;
    }

    public async Task<VM_BilletYardDetails> GetLocations(long yardId)
    {
      VM_BilletYardDetails result = new VM_BilletYardDetails();

      MVHAsset yard = _peContext.MVHAssets.FirstOrDefault(x => x.AssetId == yardId);
      result.BilletYardId = yardId;
      result.YardName = yard.AssetDescription;

      result.Locations = await _peContext.MVHAssets
        .Where(x => x.FKAssetType != null && x.FKAssetType.EnumYardType == YardType.Stack && x.FKParentAssetId == yardId)
        .Select(x => new VM_BilletLocation
        {
          LocationId = x.AssetId,
          Name = x.AssetDescription,
          NumberOfMaterials = x.PRMMaterialSteps.Count(step => step.StepNo == 0),
          Capacity = x.MVHAssetsLocation.PieceMaxCapacity,
          LocationX = x.MVHAssetsLocation.LocationX,
          LocationY = x.MVHAssetsLocation.LocationY,
          SizeX = x.MVHAssetsLocation.SizeX,
          SizeY = x.MVHAssetsLocation.SizeY
        }).ToListAsync();

      return result;
    }

    public VM_BilletLocationDetails GetLocationDetails(long id)
    {
      VM_BilletLocationDetails result = new VM_BilletLocationDetails();

      MVHAsset location = _peContext.MVHAssets.Include(x => x.MVHAssetsLocation).FirstOrDefault(x => x.AssetId == id);
      result.LocationId = location.AssetId;
      result.Name = location.AssetDescription;
      result.FillDirection = ((FillDirection)location.MVHAssetsLocation.EnumFillDirection).Name;
      result.SizeX = location.MVHAssetsLocation.PieceMaxCapacity / location.MVHAssetsLocation.LayersMaxNumber;
      result.SizeY = location.MVHAssetsLocation.LayersMaxNumber;

      return result;
    }

    public async Task<VM_BilletLocationDetails> GetMaterialsInLocation(long locationId)
    {
      VM_BilletLocationDetails result = new VM_BilletLocationDetails();

      result.LocationId = locationId;
      result.Materials = await _peContext.PRMMaterialSteps.Where(x => x.StepNo == 0 && x.FKAssetId == locationId)
        .Select(x => new VM_BilletLocationMaterial
        {
          HeatId = x.FKMaterial.FKHeat.HeatId,
          HeatName = x.FKMaterial.FKHeat.HeatName,
          MaterialId = x.FKMaterial.MaterialId,
          MaterialName = x.FKMaterial.MaterialName,
          PositionX = x.PositionX,
          PositionY = x.PositionY,
          GroupNo = x.GroupNo,
          LocationId = x.FKAssetId
        }).ToListAsync();

      return result;
    }

    public VM_BilletLocationDetails GetLocationWithMaterials(long id)
    {
      VM_BilletLocationDetails result = new VM_BilletLocationDetails();
      MVHAsset location = _peContext.MVHAssets.Include(x => x.MVHAssetsLocation).FirstOrDefault(x => x.AssetId == id);
      result.LocationId = location.AssetId;
      result.Name = location.AssetDescription;
      result.FillDirection = ((FillDirection)location.MVHAssetsLocation.EnumFillDirection).Name;
      result.SizeX = location.MVHAssetsLocation.PieceMaxCapacity / location.MVHAssetsLocation.LayersMaxNumber;
      result.SizeY = location.MVHAssetsLocation.LayersMaxNumber;
      result.Capacity = location.MVHAssetsLocation.PieceMaxCapacity;

      result.Materials = _peContext.PRMMaterialSteps.Where(x => x.StepNo == 0 && x.FKAssetId == result.LocationId)
        .Select(x => new VM_BilletLocationMaterial
        {
          HeatId = x.FKMaterial.FKHeat.HeatId,
          HeatName = x.FKMaterial.FKHeat.HeatName,
          MaterialId = x.FKMaterial.MaterialId,
          MaterialName = x.FKMaterial.MaterialName,
          PositionX = x.PositionX,
          PositionY = x.PositionY,
          GroupNo = x.GroupNo,
          LocationId = x.FKAssetId
        }).ToList();

      return result;
    }

    public DataSourceResult GetHeatsInReception(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      request.RenameRequestMember(nameof(VM_HeatsOnYards.NumberOfMaterials), nameof(V_HeatsOnYard.MaterialsOnArea));
      IQueryable<V_HeatsOnYard> list = _hmiContext.V_HeatsOnYards
        .Where(x => x.EnumYardType == YardType.MaterialReception.Value).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_HeatsOnYards(data));

      return result;
    }

    public DataSourceResult GetHeatsInScrap(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_HeatsOnYard> list = _hmiContext.V_HeatsOnYards.Where(x => x.EnumYardType == YardType.MaterialScrap.Value)
        .AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_HeatsOnYards(data));

      return result;
    }

    public DataSourceResult GetHeatGroupsInLocations(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      request.RenameRequestMember(nameof(VM_HeatsOnYards.AreaDescription), nameof(V_HeatsByGroupOnYard.AreaName));
      var list = _hmiContext.V_HeatsByGroupOnYards
        .Where(x => x.EnumYardType == YardType.Stack.Value).ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_HeatsOnYards(data));

      return result;
    }

    public DataSourceResult GetHeatsInChargingGrid(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_HeatsOnYard> list = _hmiContext.V_HeatsOnYards.Where(x => x.EnumYardType == YardType.MaterialCharging.Value)
        .AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_HeatsOnYards(data));

      return result;
    }

    public DataSourceResult GetLocations(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<VM_Location> data = _hmiContext.V_AssetsLocationOverviews
        .Where(x => x.EnumYardType == YardType.Stack.Value)
        .Select(x => new VM_Location
        {
          ParentAssetId = x.ParentAssetId,
          ParentAssetDescription = x.ParentAssetDescription,
          AssetId = x.AssetId,
          AssetDescription = x.AssetDescription,
          PieceMaxCapacity = x.PieceMaxCapacity,
          CountMaterials = x.CountMaterials ?? default,
          HeatIdInLastGroup = x.HeatIdInLastGroup,
          HeatNameInLastGroup = x.HeatNameInLastGroup
        }).ToList();


      result = data.ToDataSourceLocalResult(request, modelState, (x) => x);

      return result;
    }

    public DataSourceResult GetCharginGridHeatsGrid(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_WorkOrdersOnMaterialYard> list = _hmiContext.V_WorkOrdersOnMaterialYards
        .Where(x => x.EnumYardType == YardType.MaterialCharging.Value)
        //.Where(x => x.ScheduleId != null && x.MaterialsPlanned > 0 && x.MaterialsCharged < x.MaterialsPlanned)
        .OrderBy(x => x.ScheduleOrderSeq).AsQueryable();

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_ScheduleTask(data));

      return result;
    }

    public DataSourceResult GetScheduleGrid(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_WorkOrderSummary> list = _hmiContext.V_WorkOrderSummaries
        .Where(x => x.ScheduleId != null && x.MaterialsPlanned != x.MaterialsNumber)
        .OrderBy(x => x.ScheduleOrderSeq).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_ScheduleTask(data));

      return result;
    }

    public async Task<IList<PRMMaterialCatalogue>> GetMaterialCataloguesList()
    {
      List<PRMMaterialCatalogue> result = new List<PRMMaterialCatalogue>();
      result = await _peContext.PRMMaterialCatalogues.ToListAsync();

      return result;
    }

    public async Task<VM_Base> TransferHeatIntoLocation(ModelStateDictionary modelState, VM_HeatIntoLocation data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCTransferHeatLocation dataContract = new DCTransferHeatLocation();
      dataContract.HeatId = data.HeatId;
      dataContract.SourceLocationId = data.SourceLocationId ?? data.SourceYardId;
      dataContract.TargetLocationId = data.LocationId;
      dataContract.MaterialsNumber = data.MaterialsNumber;

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.TransferHeatIntoLocationAsync(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> TransferHeatIntoChargingGrid(ModelStateDictionary modelState,
      VM_HeatIntoChargingGrid data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCTransferHeatToChargingGrid dataContract = new DCTransferHeatToChargingGrid();
      dataContract.HeatId = data.HeatId;
      dataContract.SourceLocationId = data.SourceLocationId;
      dataContract.WorkOrderId = data.WorkOrderId;
      dataContract.MaterialsNumber = data.MaterialsNumber;

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.TransferHeatIntoChargingGridAsync(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> TransferHeatFromChargingGrid(ModelStateDictionary modelState,
      VM_HeatFromChargingGrid data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCTransferHeatLocation dataContract = new DCTransferHeatLocation();
      dataContract.HeatId = data.HeatId;
      dataContract.SourceLocationId = await _peContext.MVHAssets.Where(x => x.FKAssetType != null && x.FKAssetType.EnumYardType == YardType.MaterialCharging.Value)
        .Select(x => x.AssetId).FirstOrDefaultAsync();
      dataContract.TargetLocationId = data.TargetLocationId;
      dataContract.MaterialsNumber = data.MaterialsNumber;
      dataContract.WorkOrderId = data.WorkOrderId;

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.TransferHeatIntoLocationAsync(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_HeatsOnYards GetHeat(long heatId, short groupNo, long locationId)
    {
      VM_HeatsOnYards result = null;

      V_HeatsByGroupOnYard heat = _hmiContext.V_HeatsByGroupOnYards
        .Where(x => x.AssetId == locationId && x.GroupNo == groupNo).SingleOrDefault();
      if (heat != null)
      {
        result = new VM_HeatsOnYards(heat);
      }

      return result;
    }

    public VM_MaterialOverview GetMaterial(long materialId)
    {
      VM_MaterialOverview result = null;

      PRMMaterial material = _peContext.PRMMaterials
        .Include(i => i.FKWorkOrder)
        .Include(i => i.FKHeat)
        .Include(i => i.FKMaterialCatalogue)
        .Where(x => x.MaterialId == materialId)
        .SingleOrDefault();

      if (material != null)
      {
        return new VM_MaterialOverview(material);
      }

      return result;
    }

    public async Task<VM_Base> AddMaterials(ModelStateDictionary modelState, VM_Materials data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCCreateMaterialInReception dataContract = new DCCreateMaterialInReception();
      dataContract.HeatId = data.FKHeatId;
      dataContract.MaterialsNumber = data.MaterialsNumber;
      dataContract.MaterialCatalogueId = data.MaterialCatalogueId;

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.CreateMaterialInReceptionAsync(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_SearchResult> SearchLocationIds(ModelStateDictionary modelState, long heatId, long yardId)
    {
      VM_SearchResult result = new VM_SearchResult();

      result.LocationIds = await _peContext.PRMMaterialSteps
        .Where(x => x.StepNo == 0 && x.FKMaterial.FKHeatId == heatId && x.FKAsset.FKParentAssetId == yardId)
        .Select(x => x.FKAssetId)
        .Distinct()
        .ToListAsync();

      return result;
    }

    public async Task<VM_Base> ScrapMaterials(ModelStateDictionary modelState, VM_MaterialsScrap data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCScrapMaterial dataContract = new DCScrapMaterial();
      dataContract.HeatId = data.HeatId;
      dataContract.SourceLocationId = data.SourceLocationId ?? data.SourceYardId;
      dataContract.MaterialsNumber = data.MaterialsNumber;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ScrapMaterials(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UnscrapMaterials(ModelStateDictionary modelState, VM_MaterialsScrap data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCUnscrapMaterial dataContract = new DCUnscrapMaterial();
      dataContract.HeatId = data.HeatId;
      dataContract.MaterialsNumber = data.MaterialsNumber;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UnscrapMaterials(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> CreateHeatWithMaterials(ModelStateDictionary modelState, VM_Heat data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCCreateMaterialWithHeatInReception dataContract = new DCCreateMaterialWithHeatInReception();
      dataContract.HeatName = data.HeatName;
      dataContract.SteelgradeId = data.FKSteelgradeId;
      dataContract.HeatSupplierId = data.FKHeatSupplierId;
      dataContract.HeatWeight = data.HeatWeight;
      dataContract.IsDummy = data.IsDummy;
      dataContract.MaterialsNumber = data.MaterialsNumber;
      dataContract.MaterialCatalogueId = data.MaterialCatalogueId;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateHeatWithMaterials(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
  }
}
