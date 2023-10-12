using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Furnace;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using System.Data;
using Microsoft.Data.SqlClient;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.Helpers;
using PE.DbEntity.EFCoreExtensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class FurnaceService : MaterialInAreaBaseService, IFurnaceService
  {
    public const int FRN = 1200;
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public FurnaceService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public IEnumerable<VM_Furnace> GetMaterialInFurnace()
    {
      return _hmiContext.V_RawMaterialInFurnaces.ToList().Select(x => new VM_Furnace(x)).OrderBy(x => x.Position);
    }

    public VM_RawMaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long rawMaterialId)
    {
      VM_RawMaterialOverview result = null;

      if (rawMaterialId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      TRKRawMaterial material = _peContext.TRKRawMaterials
        .Single(x => x.RawMaterialId == rawMaterialId);

      TRKRawMaterialRelation parentRelation = _peContext.TRKRawMaterialRelations
        .Include(x => x.ParentRawMaterial)
        .FirstOrDefault(x => x.ChildRawMaterialId == rawMaterialId);

      result = new VM_RawMaterialOverview(material, parentRelation?.ParentRawMaterial);

      return result;
    }

    public VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId)
    {
      VM_WorkOrderOverview result = null;

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      PRMWorkOrder workOrder = _peContext.PRMWorkOrders
        .Include(i => i.PRMMaterials)
        .Include(i => i.FKMaterialCatalogue)
        .Include(i => i.FKSteelgrade)
        .Where(x => x.WorkOrderId == workOrderId)
        .Single();

      result = new VM_WorkOrderOverview(workOrder);

      return result;
    }

    public VM_HeatOverview GetHeatDetails(ModelStateDictionary modelState, long heatId)
    {
      VM_HeatOverview result = null;

      if (heatId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      PRMHeat heat = _peContext.PRMHeats
        .Include(i => i.PRMHeatChemicalAnalyses)
        .Include(i => i.FKHeatSupplier)
        .Include(i => i.FKSteelgrade)
        .Where(x => x.HeatId == heatId)
        .Single();

      result = new VM_HeatOverview(heat);

      return result;
    }
    public async Task<VM_Base> StepForward(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      DCUpdateArea area = new DCUpdateArea(FRN);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendMoveMaterialsInAreaDown(area);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public async Task<VM_Base> StepBackward(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      DCUpdateArea area = new DCUpdateArea(FRN);
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendMoveMaterialsInAreaUp(area);

      HandleWarnings(sendOfficeResult, ref modelState);
      return result;
    }

    public List<VM_RawMaterialOverview> GetMaterialsInChargingArea(ModelStateDictionary modelState, List<long> materialsInAreas)
    {
      List<VM_RawMaterialOverview> result = new List<VM_RawMaterialOverview>();

      if (!modelState.IsValid)
      {
        return result;
      }

      materialsInAreas = materialsInAreas.TakeLast(5).ToList();
      var areaList = GetQueueAreas(materialsInAreas, _hmiContext).ToList();
      short seq = 1;
      areaList.ForEach(x => result.Add(new VM_RawMaterialOverview(x, seq++)));

      return result;
    }
  }
}
