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
using PE.HMIWWW.ViewModel.Module.Lite.ProductYard;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;
using System;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ProductYardService : BaseService, IProductYardService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public ProductYardService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetWorkOrdersOnYards(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      request.RenameRequestMember(nameof(VM_WorOrdersOnYards.YardName),
        nameof(V_WorkOrdersOnProductYard.AreaDescription));
      request.RenameRequestMember(nameof(VM_WorOrdersOnYards.ProductsNumber),
        nameof(V_WorkOrdersOnProductYard.ProductsOnArea));
      IQueryable<V_WorkOrdersOnProductYard> list = _hmiContext.V_WorkOrdersOnProductYards.Where(x => x.AreaId != null)
        .AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_WorOrdersOnYards(data));

      return result;
    }

    public DataSourceResult GetFinishedWorkOrders(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      request.RenameRequestMember(nameof(VM_WorOrdersOnYards.YardName),
        nameof(V_WorkOrdersOnProductYard.AreaDescription));
      IQueryable<V_WorkOrdersOnYard> list = _hmiContext.V_WorkOrdersOnYards
        .Where(x => x.ProductsNumber.HasValue && x.EnumWorkOrderStatus == WorkOrderStatus.Finished.Value).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_WorOrdersOnYards(data));

      return result;
    }

    public IList<VM_ProductYard> GetYards()
    {
      List<short> assets = new List<short> { YardType.ProductYard };

      List<VM_ProductYard> result = new List<VM_ProductYard>();
      result = _peContext.MVHAssets
        //TODO Verify this
        .Where(x => x.FKAssetType != null && assets.Contains(x.FKAssetType.EnumYardType))
        .Select(x => new VM_ProductYard { ProductYardId = x.AssetId, YardName = x.AssetDescription })
        .ToList();

      foreach (VM_ProductYard item in result)
      {
        item.NumberOfProducts = _peContext.PRMProductSteps
          .Count(step => (step.FKAssetId == item.ProductYardId || step.FKAsset.FKParentAssetId == item.ProductYardId) &&
                         step.StepNo == 0);
        item.WeightOfProducts = (int)(_peContext.PRMProductSteps.Where(step =>
            (step.FKAssetId == item.ProductYardId || step.FKAsset.FKParentAssetId == item.ProductYardId) &&
            step.StepNo == 0)
          .Sum(step => (double?)step.FKProduct.ProductWeight) ?? 0);

        int capacity = _peContext.MVHAssetsLocations.Where(x => x.FKAsset.FKParentAssetId == item.ProductYardId)
          .Sum(x => (int?)x.WeightMaxCapacity) ?? 0;
        item.FreeSpace = capacity - item.WeightOfProducts;
      }

      return result;
    }

    public async Task<VM_ProductYardDetails> GetLocations(long yardId)
    {
      VM_ProductYardDetails result = new VM_ProductYardDetails();
      MVHAsset yard = await _peContext.MVHAssets.FirstOrDefaultAsync(x => x.AssetId == yardId);
      result.ProductYardId = yardId;
      result.YardName = yard.AssetDescription;

      result.Locations = await _peContext.MVHAssets
        .Where(x => /*x.EnumAssetType == AssetType.Stack &&*/ x.FKParentAssetId == yardId)
        .Select(x => new VM_ProductLocation
        {
          LocationId = x.AssetId,
          Name = x.AssetDescription,
          NumberOfProducts = x.PRMProductSteps.Count(step => step.StepNo == 0),
          WeightOfProducts =
            x.PRMProductSteps.Where(step => step.StepNo == 0).Sum(step => (int?)step.FKProduct.ProductWeight) ?? 0,
          Capacity = x.MVHAssetsLocation.WeightMaxCapacity,
          LocationX = x.MVHAssetsLocation.LocationX,
          LocationY = x.MVHAssetsLocation.LocationY,
          SizeX = x.MVHAssetsLocation.SizeX,
          SizeY = x.MVHAssetsLocation.SizeY
        }).ToListAsync();

      return result;
    }

    public DataSourceResult GetWorkOrdersInLocationList(ModelStateDictionary modelState, DataSourceRequest request,
      long locationId)
    {
      DataSourceResult result = null;

      IQueryable<V_WorkOrdersOnYardLocation> list = _hmiContext.V_WorkOrdersOnYardLocations
        .Where(x => x.AssetId == locationId).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_ProductLocationWorkOrder(data));

      return result;
    }

    public async Task<VM_ProductLocationDetails> GetWorkOrdersInLocation(long locationId)
    {
      VM_ProductLocationDetails result = new VM_ProductLocationDetails();
      result.LocationId = locationId;
      result.WorkOrders = await _hmiContext.V_WorkOrdersOnYardLocations.Where(x => x.AssetId == locationId)
        .Select(x => new VM_ProductLocationWorkOrder
        {
          HeatId = x.HeatId,
          HeatName = x.HeatName,
          LocationId = x.AssetId,
          LocationName = x.AssetDescription,
          WorkOrderId = x.WorkOrderId,
          WorkOrderName = x.WorkOrderName,
          SteelgradeId = x.SteelgradeId,
          SteelgradeName = x.SteelgradeName,
          ProductCount = x.ProductsOnAsset,
          ProductWeight = x.WeightOnAsset
        }).ToListAsync();

      return result;
    }

    public DataSourceResult GetLocationsByWorkOrder(ModelStateDictionary modelState, DataSourceRequest request,
      long? workOrderId)
    {
      DataSourceResult result = null;

      if (workOrderId != null)
      {
        List<V_WorkOrdersOnYardLocation> list = _hmiContext.V_WorkOrdersOnYardLocations
          .Where(x => x.WorkOrderId == workOrderId).ToList();
        result = list.ToDataSourceLocalResult(request, modelState, data => new VM_Location(data));
      }
      else
      {
        request.RenameRequestMember(nameof(VM_Location.AreaId), nameof(V_AssetsLocationOverview.ParentAssetId));
        request.RenameRequestMember(nameof(VM_Location.AreaDescription),
          nameof(V_AssetsLocationOverview.ParentAssetDescription));
        List<V_AssetsLocationOverview> list = _hmiContext.V_AssetsLocationOverviews
          .Where(x => x.ParentEnumYardType == YardType.ProductYard.Value).ToList();
        result = list.ToDataSourceLocalResult(request, modelState, data => new VM_Location(data));
      }

      return result;
    }

    public DataSourceResult GetLocations(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;
      request.RenameRequestMember(nameof(VM_Location.AreaId), nameof(V_AssetsLocationOverview.ParentAssetId));
      request.RenameRequestMember(nameof(VM_Location.AreaDescription),
        nameof(V_AssetsLocationOverview.ParentAssetDescription));
      List<V_AssetsLocationOverview> list = _hmiContext.V_AssetsLocationOverviews
          .Where(x => x.ParentEnumYardType == YardType.ProductYard.Value).ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_Location(data));

      return result;
    }

    public DataSourceResult GetProductsOnYards(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_ProductsOnYard> list = _hmiContext.V_ProductsOnYards.ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_ProductOnYard(data));

      return result;
    }

    public DataSourceResult GetProductsInLocationByWo(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId, long locationId)
    {
      DataSourceResult result = null;

      IQueryable<V_ProductsOnYard> list = _hmiContext.V_ProductsOnYards
        .Where(x => x.WorkOrderId == workOrderId && x.AssetId == locationId).AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_ProductOnYard(data));

      return result;
    }

    public async Task<VM_SearchResult> SearchLocationIds(ModelStateDictionary modelState, long workOrderId, long yardId)
    {
      VM_SearchResult result = new VM_SearchResult();

      result.LocationIds = await _peContext.PRMProductSteps
        .Where(x => x.StepNo == 0 && x.FKProduct.FKWorkOrderId == workOrderId && x.FKAsset.FKParentAssetId == yardId)
        .Select(x => x.FKAssetId)
        .Distinct()
        .ToListAsync();

      return result;
    }

    public async Task<VM_Base> RelocateProduct(ModelStateDictionary modelState, long targetLocationId, List<long> sourceLocations, List<long> products)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCProductRelocation dataContract = new DCProductRelocation
      {
        TargetLocationId = targetLocationId,
        Products = new List<ProductRelocationDetail>()
      };

      List<long> sourceLocationsDistinct = sourceLocations.Distinct().ToList();
      foreach (long item in sourceLocationsDistinct)
      {
        ProductRelocationDetail product = new ProductRelocationDetail
        {
          SourceLocationId = item,
          ProductsIds = new List<long>()
        };
        int[] v = sourceLocations.Select((b, i) => b == item ? i : -1).Where(i => i != -1).ToArray();
        for (int i = 0; i < v.Length; i++)
        {
          product.ProductsIds.Add(products[v[i]]);
        }

        dataContract.Products.Add(product);
      }

      //for (int i = 0; i < products.Count; i++)
      //{
      //  ProductRelocationDetail product = new ProductRelocationDetail();
      //  Products.add
      //}

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RelocateProducts(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DispatchWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderToDispatch dataContract = new DCWorkOrderToDispatch();
      dataContract.Id = workOrderId;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DispatchWorkOrder(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> ReorderLocationSeq(ModelStateDictionary modelState, long locationId, short oldIndex,
      short newIndex)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCProductYardLocationOrder dataContract = new DCProductYardLocationOrder();
      dataContract.LocationId = locationId;
      dataContract.OldIndex = oldIndex;
      dataContract.NewIndex = newIndex;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ReorderLocationSeq(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
  }
}
