using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IFurnaceService
  {
    IEnumerable<VM_Furnace> GetMaterialInFurnace();
    VM_RawMaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long rawMaterialId);
    VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId);
    VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId);
    Task<VM_Base> StepForward(ModelStateDictionary modelState);
    Task<VM_Base> StepBackward(ModelStateDictionary modelState);
    List<VM_RawMaterialOverview> GetMaterialsInChargingArea(ModelStateDictionary modelState, List<long> materialsInAreas);
  }
}
