using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.Models.DataContracts.Internal.PRM;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Heat;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class HeatService : BaseService, IHeatService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public HeatService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
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
        .Include(i => i.FKHeatSupplier)
        .Include(i => i.PRMMaterials)
        .Include(i => i.PRMHeatChemicalAnalyses)
        .Include(i => i.FKSteelgrade.PRMSteelgradeChemicalComposition)
        .Where(x => x.HeatId == heatId)
        .Single();

      result = new VM_HeatOverview(heat);

      return result;
    }

    public VM_HeatOverview GetHeatOverviewByWorkOrderId(ModelStateDictionary modelState, long workOrderId)
    {
      VM_HeatOverview result = null;

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
        .Include(x => x.FKHeat)
        .Include(x => x.FKHeat.FKHeatSupplier)
        .Include(x => x.FKHeat.PRMMaterials)
        .Include(x => x.FKHeat.PRMHeatChemicalAnalyses)
        .Include(x => x.FKHeat.FKSteelgrade.PRMSteelgradeChemicalComposition)
        .Where(x => x.WorkOrderId == workOrderId)
        .Single();

      result = new VM_HeatOverview(workOrder.FKHeat);

      return result;
    }
    public DataSourceResult GetHeatOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_Heats.ToDataSourceLocalResult(request, modelState, data => new VM_HeatSummary(data));

      return result;
    }

    public DataSourceResult GetMaterialsListByHeatId(ModelStateDictionary modelState, DataSourceRequest request,
      long heatId)
    {
      IQueryable<PRMMaterial> materialsList = _peContext.PRMMaterials.Include(i => i.FKWorkOrder)
        ?.Where(x => x.FKHeatId == heatId).AsQueryable();

      return materialsList.ToDataSourceLocalResult(request, modelState, data => new VM_MaterialOverview(data));
    }

    public DataSourceResult GetWorkOrderListByHeatId(ModelStateDictionary modelState, DataSourceRequest request,
      long heatId)
    {
      IQueryable<PRMMaterial> materialsInHeat = _peContext.PRMMaterials.Where(x => x.FKHeatId == heatId).AsQueryable();
      IEnumerable<long> workOrderIds = materialsInHeat.Where(x => x.FKWorkOrderId.HasValue).Select(x => x.FKWorkOrderId.Value).Distinct().ToList();
      List<PRMWorkOrder> workOrdersList = _peContext.PRMWorkOrders.Where(x => workOrderIds.Contains(x.WorkOrderId)).ToList();

      return workOrdersList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data.WorkOrderId,
        data.WorkOrderName,
        //TODOMN - exclude this
        //data.CreatedTs, 
        DateTime.Now,
        data.TargetOrderWeight,
        WorkOrderStatus.GetValue(data.EnumWorkOrderStatus)));
    }

    public async Task<VM_Base> CreateHeat(ModelStateDictionary modelState, VM_Heat heat)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref heat);

      DCHeatEXT dCHeat = new DCHeatEXT
      {
        HeatName = heat.HeatName,
        FKSteelgradeId = heat.FKSteelgradeId,
        FKHeatSupplierId = heat.FKHeatSupplierId,
        HeatWeight = heat.HeatWeight,
        IsDummy = heat.IsDummy
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendCreateHeatAsync(dCHeat);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> EditHeat(ModelStateDictionary modelState, VM_Heat heat)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref heat);

      DCHeatEXT dCHeat = new DCHeatEXT
      {
        HeatName = heat.HeatName,
        FKSteelgradeId = heat.FKSteelgradeId,
        FKHeatSupplierId = heat.FKHeatSupplierId,
        HeatWeight = heat.HeatWeight,
        IsDummy = heat.IsDummy
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEditHeatAsync(dCHeat);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public IList<PRMHeatSupplier> GetSupplierList()
    {
      List<PRMHeatSupplier> result = new List<PRMHeatSupplier>();
      result = _peContext.PRMHeatSuppliers.ToList();

      return result;
    }

    public IList<PRMSteelgrade> GetSteelgradeList()
    {
      List<PRMSteelgrade> result = new List<PRMSteelgrade>();
      result = _peContext.PRMSteelgrades.ToList();

      return result;
    }

    public IList<PRMMaterialCatalogue> GetMaterialList()
    {
      List<PRMMaterialCatalogue> result = new List<PRMMaterialCatalogue>();
      result = _peContext.PRMMaterialCatalogues.ToList();

      return result;
    }

    public async Task<IList<VM_HeatAutocomplete>> GetHeatsByAnyFeaure(string text, bool isTest)
    {
      IList<VM_HeatAutocomplete> result = new List<VM_HeatAutocomplete>();

      if (!string.IsNullOrEmpty(text))
      {
        result = await (from heat in _peContext.PRMHeats
                        where (heat.HeatName.Contains(text) ||
                              (heat.FKSteelgradeId != null && heat.FKSteelgrade.SteelgradeCode.Contains(text)) ||
                              (heat.FKSteelgradeId != null && heat.FKSteelgrade.SteelgradeName.Contains(text)))
                              &&
                              heat.IsDummy == isTest
                        select new VM_HeatAutocomplete
                        {
                          HeatId = heat.HeatId,
                          HeatName = heat.HeatName,
                          IsDummy = heat.IsDummy,
                          SteelgradeCode = heat.FKSteelgrade != null ? heat.FKSteelgrade.SteelgradeCode : null,
                          SteelgradeName = heat.FKSteelgrade != null ? heat.FKSteelgrade.SteelgradeName : null,
                          MaterialsCount = heat.PRMMaterials.Count(),
                          MaterialsAssigned = heat.PRMMaterials.Count(x => x.FKWorkOrderId != null)
                        }).ToListAsync();
      }

      return result;
    }

    public async Task<VM_Heat> GetHeat(long heatId)
    {
      VM_Heat result;

      PRMHeat heat = await _peContext.PRMHeats
        .Include(i => i.FKHeatSupplier)
        .Include(i => i.PRMMaterials)
        .Include(i => i.PRMHeatChemicalAnalyses)
        .Include(i => i.FKSteelgrade.PRMSteelgradeChemicalComposition)
        .Where(x => x.HeatId == heatId)
        .SingleAsync();

      result = new VM_Heat(heat);

      return result;
    }
  }
}
