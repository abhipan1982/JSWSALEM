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
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Helpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Visualization;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;
using Microsoft.Data.SqlClient;
using System.Data;
using PE.DbEntity.EFCoreExtensions;
using Kendo.Mvc.Extensions;
using PE.Core;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class VisualizationService : MaterialInAreaBaseService, IVisualizationService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public VisualizationService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public DataSourceResult GetWorkOrdersInRealizationList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<PRMWorkOrder> workOrderList = _peContext.PRMWorkOrders
        .Where(x => x.EnumWorkOrderStatus == WorkOrderStatus.InRealization.Value)
        .Include(i => i.FKMaterialCatalogue)
        .Include(i => i.FKSteelgrade)
        .AsQueryable();
      workOrderList.Select(x => x.FKMaterialCatalogue).AsQueryable();


      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data));
    }

    public DataSourceResult GetWorkOrdersPlannedList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<PRMWorkOrder> workOrderList = _peContext.PRMWorkOrders
        .Where(x => x.EnumWorkOrderStatus == WorkOrderStatus.Scheduled.Value)
        .Include(i => i.FKMaterialCatalogue)
        .Include(i => i.FKSteelgrade)
        .AsQueryable();

      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data));
    }

    public DataSourceResult GetWorkOrdersProducedList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<PRMWorkOrder> workOrderList = _peContext.PRMWorkOrders
        .Where(x => x.EnumWorkOrderStatus == WorkOrderStatus.Finished.Value)
        .Include(i => i.FKMaterialCatalogue)
        .Include(i => i.FKSteelgrade)
        .AsQueryable();
      workOrderList.Select(x => x.FKMaterialCatalogue).AsQueryable();


      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data));
    }

    public async Task<VM_Base> RequestLastMaterialPosition(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.RequestLastMaterialPosition(new DataContractBase());

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> RequestLastTestMaterialPosition(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.RequestLastMaterialPosition(new DataContractBase());

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_TrackingMaterialOverview> GetTrackingMaterialDetails(ModelStateDictionary modelState,
      long? rawMaterialId)
    {
      VM_TrackingMaterialOverview result = null;
      if (!rawMaterialId.HasValue || rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      result = await _peContext.TRKRawMaterials
        .Where(x => x.RawMaterialId == rawMaterialId)
        .Select(x => new VM_TrackingMaterialOverview
        {
          MaterialId = x.FKMaterialId,
          MaterialName = x.FKMaterial.MaterialName,
          WorkOrderId = x.FKMaterial.FKWorkOrderId,
          WorkOrderName = x.FKMaterial.FKWorkOrder.WorkOrderName,
          HeatId = x.FKMaterial.FKHeatId,
          HeatName = x.FKMaterial.FKHeat.HeatName,
          SteelgradeId = x.FKMaterial.FKWorkOrder.FKSteelgradeId,
          SteelgradeName = x.FKMaterial.FKWorkOrder.FKSteelgrade.SteelgradeName,
          SteelgradeCode = x.FKMaterial.FKWorkOrder.FKSteelgrade.SteelgradeCode,
          SlittingFactor = x.SlittingFactor
        }).SingleOrDefaultAsync() ?? new VM_TrackingMaterialOverview();

      return result;
    }

    public VM_RawMaterialEXT GetRawMaterialExtDetails(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_RawMaterialEXT result = null;
      if (!rawMaterialId.HasValue || rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      TRKRawMaterial rawMatExt = _peContext.TRKRawMaterials
        .Where(x => x.RawMaterialId == rawMaterialId).Single();

      result = new VM_RawMaterialEXT(rawMatExt);

      return result;
    }

    public DataSourceResult GetMaterialsInArea(ModelStateDictionary modelState, DataSourceRequest request,
      List<VM_RawMaterialOverview> materials)
    {
      List<VM_RawMaterialOverview> materialsInArea = materials;

      if (materialsInArea == null || materialsInArea.Count == 0)
        materialsInArea = new List<VM_RawMaterialOverview>();

      return materialsInArea.ToDataSourceLocalResult(request, (x) => x);
    }

    public DataSourceResult GetMaterialsInLayer(ModelStateDictionary modelState, DataSourceRequest request,
      long layerId)
    {
      var materialsInLayerIds = _peContext.TRKLayerRawMaterialRelations
        .Where(x => x.ParentLayerRawMaterialId == layerId)
        .Select(x => x.ChildLayerRawMaterialId)
        .ToList();

      var materialsList = _peContext.TRKRawMaterials
        .Include(x => x.FKMaterial)
        .Include(x => x.FKMaterial.FKWorkOrder)
        .Include(x => x.FKMaterial.FKWorkOrder.FKHeat)
        .Include(x => x.FKMaterial.FKWorkOrder.FKHeat.FKSteelgrade)
        .Where(x => materialsInLayerIds.Contains(x.RawMaterialId));

      return materialsList.ToDataSourceLocalResult(request, modelState, data => new VM_RawMaterialOverview(data));
    }


    public List<VM_Asset> GetQueueAreas(ModelStateDictionary modelState, List<long> materialsInAreas, List<long> materialsInFurnace, int selected)
    {
      List<VM_Asset> result = new List<VM_Asset>();

      if (!modelState.IsValid)
      {
        return result;
      }
      DataTable materialsInFurnaceDataTable = GenerateDataTable(materialsInFurnace);
      SqlParameter[] parametersFurnance = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@RawMaterialId_list",
                            //SqlDbType =  SqlDbType.VarChar,
                            TypeName = "dbo.TRK_RM_Id_List",
                            IsNullable = false,
                            Direction = ParameterDirection.Input,
                            Value = materialsInFurnaceDataTable
                        }};

      var areaList = GetQueueAreas(materialsInAreas, _hmiContext);
      var furnaceList = _hmiContext.ExecuteSPL3L1MaterialsInFurnance(parametersFurnance).ToList();

      List<VM_RawMaterialOverview> materialAreaVM_List = new List<VM_RawMaterialOverview>();
      List<VM_Furnace> materialFurnaceVM_List = new List<VM_Furnace>();

      areaList.ForEach(x => materialAreaVM_List.Add(new VM_RawMaterialOverview(x)));
      furnaceList.ForEach(x => materialFurnaceVM_List.Add(new VM_Furnace(x)));

      List<V_Asset> areas = _hmiContext.V_Assets
        .Where(x => TrackingAreaHelpers.TrackingAreas
          .Select(y => y.TrackingAreaCode)
          .Contains(x.AssetCode) && x.AssetCode != TrackingArea.GREY_AREA.Value)
        .ToList();

      var selectedAreaAvailable = false;

      foreach (V_Asset item in areas)
      {
        if (item.AssetCode == TrackingArea.FCE_AREA && materialFurnaceVM_List.Any())
        {
          if (selected == item.AssetCode)
            selectedAreaAvailable = true;

          result.Add(new VM_Asset(item, materialFurnaceVM_List)
          {
            AreaSelected = selected == item.AssetCode
          });
        }
        else if (materialAreaVM_List.Where(x => x.AreaCode == item.AreaCode).ToList().Any())
        {
          if (selected == item.AssetCode)
            selectedAreaAvailable = true;

          result.Add(new VM_Asset(item, materialAreaVM_List.Where(x => x.AreaCode == item.AreaCode).ToList())
          {
            AreaSelected = selected == item.AssetCode
          });
        }
      }

      if (!selectedAreaAvailable)
      {
        if (result.Any(x => x.AssetCode == TrackingArea.FCE_AREA))
          result.First(x => x.AssetCode == TrackingArea.FCE_AREA).AreaSelected = true;
        else if (result.Any())
          result.First().AreaSelected = true;
      }

      return result;
    }

    public IList<VM_RawMaterialOverview> GetLayers(ModelStateDictionary modelState)
    {
      List<VM_RawMaterialOverview> result = new List<VM_RawMaterialOverview>();

      if (!modelState.IsValid)
      {
        return result;
      }

      IQueryable<TRKRawMaterial> layers = _peContext
        .TRKRawMaterials.Where(x => x.EnumRawMaterialType == RawMaterialType.Layer)
        .AsQueryable();

      foreach (TRKRawMaterial item in layers)
      {
        result.Add(new VM_RawMaterialOverview(item));
      }

      return result;
    }

    public IList<VM_RawMaterialOverview> GetLayerById(ModelStateDictionary modelState, long layerId)
    {
      List<VM_RawMaterialOverview> result = new List<VM_RawMaterialOverview>();

      if (!modelState.IsValid)
      {
        return result;
      }

      IQueryable<TRKRawMaterial> layers = _peContext.TRKRawMaterials
        .Where(x => x.RawMaterialId == layerId && x.EnumRawMaterialType == RawMaterialType.Layer)
        .AsQueryable();

      foreach (TRKRawMaterial item in layers)
      {
        result.Add(new VM_RawMaterialOverview(item)
        {
          LayerSelected = item.RawMaterialId == layerId
        });
      }

      return result;
    }

    public DataSourceResult GetMaterialInFurnace(ModelStateDictionary modelState, DataSourceRequest request,
      List<VM_Furnace> materials)
    {
      List<VM_Furnace> materialsInFurnace = materials;

      if (materialsInFurnace == null || materialsInFurnace.Count == 0)
        materialsInFurnace = new List<VM_Furnace>();

      return materialsInFurnace.ToDataSourceLocalResult(request, (x) => x);
    }

    public async Task<VM_Base> TrackingScrapAction(ModelStateDictionary modelState, VM_Scrap scrapModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref scrapModel);

      TypeOfScrap typeOfScrap;
      if (scrapModel.ScrapPercent == 1)
        typeOfScrap = TypeOfScrap.Scrap;
      else if (scrapModel.ScrapPercent == 0)
        typeOfScrap = TypeOfScrap.None;
      else
        typeOfScrap = TypeOfScrap.PartialScrap;

      DCL1ScrapData dc = new DCL1ScrapData(scrapModel.RawMaterialId)
      {
        TypeOfScrap = typeOfScrap,
        ScrapPercent = scrapModel?.ScrapPercent,
        ScrapRemark = scrapModel?.ScrapRemark,
        AssetId = scrapModel.AssetId
      };

      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.L1ScrapAction(dc);

      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> TrackingRemoveAction(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCRemoveMaterial dc = new DCRemoveMaterial(rawMaterialId);

      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RemoveMaterialFromTracking(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> TrackingReadyAction(ModelStateDictionary modelState, VM_RawMaterialGenealogy data)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      var productType = await _peContext.PRMProductCatalogueTypes.FirstAsync(x => x.ProductCatalogueTypeId == data.ProductCatalogueTypeId);

      int targetAreaAssetCode = 0;
      var productCatalogueTypeCode = productType.ProductCatalogueTypeCode.ToUpper();
      if (productCatalogueTypeCode.Equals(Constants.Bar.ToUpper()))
        targetAreaAssetCode = TrackingArea.ENTER_TABLE_AREA;
      else if (productCatalogueTypeCode.Equals(Constants.WireRod.ToUpper()))
        targetAreaAssetCode = TrackingArea.TRANSPORT_AREA;
      else if (productCatalogueTypeCode.Equals(Constants.Garret.ToUpper()))
        targetAreaAssetCode = TrackingArea.GARRET_AREA;

      DCMaterialReady dc = new DCMaterialReady(data.Id.Value)
      {
        ChildsNo = data.ChildsNo,
        ProductType = data.ProductCatalogueTypeId.Value,
        KeepInTracking = data.KeepInTracking,
        TargetAreaAssetCode = targetAreaAssetCode
      };
      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.MaterialReady(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> ProductUndoAction(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCProductUndo dc = new DCProductUndo(rawMaterialId);

      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ProductUndo(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_Scrap GetRawMaterialPartialScrap(long rawMaterialId)
    {
      VM_Scrap result = null;

      TRKRawMaterial data =
        _peContext.TRKRawMaterials.SingleOrDefault(x => x.RawMaterialId == rawMaterialId);
      result = data == null ? new VM_Scrap(rawMaterialId) : new VM_Scrap(data);

      return result;
    }

    public VM_RawMaterialRejection GetRawMaterialRejection(long rawMaterialId)
    {
      VM_RawMaterialRejection result = null;

      TRKRawMaterial data = _peContext.TRKRawMaterials.SingleOrDefault(x => x.RawMaterialId == rawMaterialId);
      result = data == null ? new VM_RawMaterialRejection(rawMaterialId) : new VM_RawMaterialRejection(data);

      return result;
    }

    public async Task<VM_Base> RejectRawMaterial(ModelStateDictionary modelState, VM_RawMaterialRejection rawMaterial)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      SendOfficeResult<DataContractBase> sendOfficeResult;

      DCRejectMaterialData dc = new DCRejectMaterialData
      {
        RawMaterialId = rawMaterial.RawMaterialId,
        RejectLocation = RejectLocation.GetValue(rawMaterial.EnumRejectLocation),
        OutputPieces = rawMaterial.OutputPieces
      };
      InitDataContract(dc);

      sendOfficeResult = await HmiSendOffice.RejectRawMaterial(dc).ConfigureAwait(false);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public IList<VM_Asset> GetAssets()
    {
      List<VM_Asset> result = new List<VM_Asset>();
      foreach (V_Asset item in _hmiContext.V_Assets.Where(x => !x.IsArea).AsQueryable())
      {
        result.Add(new VM_Asset(item));
      }

      return result;
    }
  }
}
