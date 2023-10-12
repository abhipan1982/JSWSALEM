using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Extensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class TrackingManagementService : BaseService, ITrackingManagementService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;
    public TrackingManagementService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetTrackingOverview(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      List<VM_FurnanceZone> furnanceZones = new List<VM_FurnanceZone>();
      IQueryable<V_RawMaterialLocation> rawMaterialLocations = _hmiContext.V_RawMaterialLocations
        .Where(x => x.IsVirtual == false)
        .OrderBy(x => x.AssetCode)
        .AsQueryable();

      var queryRawMaterialLocations =
        from material in rawMaterialLocations
        group material by material.AssetCode
        into furnanceZone
        orderby furnanceZone.Key
        select furnanceZone;

      foreach (IGrouping<short, V_RawMaterialLocation> groupedRawMaterialLocations in queryRawMaterialLocations)
      {
        furnanceZones.Add(new VM_FurnanceZone(groupedRawMaterialLocations));
      }

      return furnanceZones.ToDataSourceLocalResult(request, modelState, (x) => x);
    }

    public async Task<VM_Base> UpdateMaterialPosition(ModelStateDictionary modelState,
      VM_ReplaceMaterialPosition newMaterialPosition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref newMaterialPosition);
      var dcNewMaterialPositon = new DCMoveMaterial
      {
        RawMaterialId = newMaterialPosition.RawMaterialId,
        DragAssetCode = newMaterialPosition.DragAssetCode,
        DragOrderSeq = newMaterialPosition.DragOrderSeq,
        DropAssetCode = newMaterialPosition.DropAssetCode,
        DropOrderSeq = newMaterialPosition.DropOrderSeq
      };

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendUpdateMaterialPositionInTracking(dcNewMaterialPositon);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> DeleteMaterial(ModelStateDictionary modelState, VM_RemoveMaterial materialPosition)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref materialPosition);

      if (!materialPosition.HardRemove)
      {
        DCRemoveMaterial dcMaterialCoordinates = new DCRemoveMaterial(materialPosition.RawMaterialId)
        {
          AreaCode = TrackingArea.GetValue(materialPosition.AreaCode)
        };

        SendOfficeResult<DataContractBase> sendOfficeResult =
          await HmiSendOffice.RemoveMaterialFromTracking(dcMaterialCoordinates);

        HandleWarnings(sendOfficeResult, ref modelState);
      }
      else
      {
        DCHardRemoveMaterial dcMaterialCoordinates = new DCHardRemoveMaterial(materialPosition.RawMaterialId);

        SendOfficeResult<DataContractBase> sendOfficeResult =
          await HmiSendOffice.HardRemoveMaterialFromTracking(dcMaterialCoordinates);

        HandleWarnings(sendOfficeResult, ref modelState);
      }
      return result;
    }

    public async Task<VM_Base> MoveMaterialsUp(ModelStateDictionary modelState, VM_MoveMaterials material)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref material);
      DCUpdateArea asset = new DCUpdateArea(material.AssetCode);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendMoveMaterialsInAreaUp(asset);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> MoveMaterialsDown(ModelStateDictionary modelState, VM_MoveMaterials material)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref material);
      DCUpdateArea asset = new DCUpdateArea(material.AssetCode);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendMoveMaterialsInAreaDown(asset);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> ChargeIntoChargingGrid(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendChargingGridCharge(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> UnchargeFromChargingGrid(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendChargingGridUnCharge(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> ChargeIntoFurnace(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFurnaceCharge(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> UnchargeFromFurnace(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFurnaceUnCharge(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> DischargeForRolling(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_Base result = new VM_Base();

      DataContractBase dc = new DataContractBase();
      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFurnaceDischargeForRolling(dc).ConfigureAwait(false);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> UnDischargeFromRolling(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFurnaceUnDischargeFromRolling(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> DischargeForReject(ModelStateDictionary modelState, long? rawMaterialId)
    {
      VM_Base result = new VM_Base();

      DataContractBase dc = new DataContractBase();
      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFurnaceDischargeForReject(dc).ConfigureAwait(false);
      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> TransferLayer(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.TransferLayer(new DataContractBase());

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public IList<VM_WorkOrder> GetWorkOrderList()
    {
      List<VM_WorkOrder> result = new List<VM_WorkOrder>();

      foreach (PRMWorkOrder item in _peContext.PRMWorkOrders
        .Include(x => x.FKSteelgrade)
        .Include(x => x.FKHeat)
        .Include(x => x.FKMaterialCatalogue).AsQueryable())
      {
        result.Add(new VM_WorkOrder(item));
      }

      return result;
    }

    public async Task<VM_Base> EndOfWorkShop(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      DataContractBase dc = new DataContractBase();
      InitDataContract(dc);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEndOfWorkShop(dc);
      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public IList<VM_RawMaterialOverview> GetLayers(ModelStateDictionary modelState)
    {
      List<VM_RawMaterialOverview> result = new List<VM_RawMaterialOverview>();

      if (!modelState.IsValid)
      {
        return result;
      }

      var layers = _peContext.TRKRawMaterials.Where(x => x.EnumRawMaterialType == RawMaterialType.Layer);

      foreach (TRKRawMaterial item in layers)
      {
        result.Add(new VM_RawMaterialOverview(item));
      }

      return result;
    }

    public IList<VM_WorkOrder> GetWorkOrderToChargeList()
    {
      List<VM_WorkOrder> result = new List<VM_WorkOrder>();

      result = _peContext.PRMWorkOrders
        .Include(x => x.FKMaterialCatalogue)
        .Include(x => x.PRMMaterials)
        .Where(x => x.PRMMaterials.Any(m => !m.TRKRawMaterials.Any()) && !x.IsBlocked).ToList()
        .Select(x => new VM_WorkOrder(x)).ToList();

      return result;
    }

    public async Task<VM_Base> ChargeMaterialOnFurnaceExitAsync(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendChargeMaterialOnFurnaceExitAsync(new DCChargeMaterialOnFurnaceExit(workOrderId));

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> FinishLayerAsync(ModelStateDictionary modelState, long layerId, int areaCode)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendFinishLayerAsync(new DCLayer(layerId) { AreaCode = areaCode });

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> TransferLayerAsync(ModelStateDictionary modelState, long layerId, int areaCode)
    {
      VM_Base result = new VM_Base();

      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.SendTransferLayerAsync(new DCLayer(layerId) { AreaCode = areaCode });

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public IList<VM_WorkOrder> GetWorkOrderToChargeList(string text)
    {
      List<VM_WorkOrder> result = new List<VM_WorkOrder>();

      IQueryable<PRMWorkOrder> queryable = _peContext.PRMWorkOrders
        .Include(x => x.FKMaterialCatalogue)
      .Where(x => (x.EnumWorkOrderStatus == WorkOrderStatus.Charging ||
        x.EnumWorkOrderStatus == WorkOrderStatus.InRealization ||
        x.EnumWorkOrderStatus == WorkOrderStatus.New ||
        x.EnumWorkOrderStatus == WorkOrderStatus.Scheduled) &&
        !x.IsBlocked &&
        x.PRMMaterials.Any(m => !m.TRKRawMaterials.Any()));


      if (!string.IsNullOrEmpty(text))
      {
        queryable = queryable
          .Where(x => x.WorkOrderName
            .Contains(text));
      }

      result = queryable
        .Take(10)
        .ToList()
        .Select(x => new VM_WorkOrder(x))
        .ToList();

      return result;
    }
  }
}
