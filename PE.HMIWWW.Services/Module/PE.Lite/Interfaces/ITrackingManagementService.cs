using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.TrackingManagement;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ITrackingManagementService
  {
    DataSourceResult GetTrackingOverview(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_Base> UpdateMaterialPosition(ModelStateDictionary modelState, VM_ReplaceMaterialPosition data);
    Task<VM_Base> DeleteMaterial(ModelStateDictionary modelState, VM_RemoveMaterial data);
    Task<VM_Base> MoveMaterialsUp(ModelStateDictionary modelState, VM_MoveMaterials data);
    Task<VM_Base> MoveMaterialsDown(ModelStateDictionary modelState, VM_MoveMaterials data);
    Task<VM_Base> ChargeIntoChargingGrid(ModelStateDictionary modelState);
    Task<VM_Base> UnchargeFromChargingGrid(ModelStateDictionary modelState);
    Task<VM_Base> ChargeIntoFurnace(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> UnchargeFromFurnace(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> DischargeForRolling(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> UnDischargeFromRolling(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> DischargeForReject(ModelStateDictionary modelState, long? rawMaterialId);
    Task<VM_Base> TransferLayer(ModelStateDictionary modelState);
    IList<VM_WorkOrder> GetWorkOrderList();
    Task<VM_Base> EndOfWorkShop(ModelStateDictionary modelState);
    IList<VM_RawMaterialOverview> GetLayers(ModelStateDictionary modelState);
    IList<VM_WorkOrder> GetWorkOrderToChargeList();
    Task<VM_Base> ChargeMaterialOnFurnaceExitAsync(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> FinishLayerAsync(ModelStateDictionary modelState, long layerId, int areaCode);
    Task<VM_Base> TransferLayerAsync(ModelStateDictionary modelState, long layerId, int areaCode);
    IList<VM_WorkOrder> GetWorkOrderToChargeList(string text);
  }
}
