using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.PRM;

using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using PE.HMIWWW.ViewModel.Module.Lite.EventCalendar;
using PE.HMIWWW.ViewModel.Module.Lite.Inspection;
using PE.HMIWWW.ViewModel.Module.Lite.KPI;
using PE.HMIWWW.ViewModel.Module.Lite.Material;
using PE.HMIWWW.ViewModel.Module.Lite.Product;
using PE.HMIWWW.ViewModel.Module.Lite.Products;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;
using PE.DbEntity.EFCoreExtensions;
using PE.BaseDbEntity.Models;
using PE.DbEntity.Models;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class WorkOrderService : BaseService, IWorkOrderService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;
    private readonly PECustomContext _peCustomContext;

    public WorkOrderService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetWorkOrderOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_WorkOrderSearchGrid> workOrderList = _hmiContext.V_WorkOrderSearchGrids.AsQueryable();
      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderSummary(data));
    }

    public DataSourceResult GetWorkOrderInRealizationList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_WorkOrderSearchGrid> workOrderList = _hmiContext.V_WorkOrderSearchGrids.Where(x => x.EnumWorkOrderStatus == WorkOrderStatus.InRealization.Value).AsQueryable();
      return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderSummary(data));
    }

    public VM_WorkOrderOverview GetWorkOrderDetails(ModelStateDictionary modelState, long workOrderId)
    {
      VM_WorkOrderOverview result = null;
      PRMWorkOrder workOrder = null;

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      workOrder = _peContext.PRMWorkOrders
        .Include(i => i.FKMaterialCatalogue.FKMaterialCatalogueType)
        .Include(i => i.FKProductCatalogue)
        .Include(i => i.FKProductCatalogue.FKShape)
        .Include(i => i.FKMaterialCatalogue.FKShape)
        .Include(i => i.FKProductCatalogue.FKProductCatalogueType)
        .Include(i => i.FKHeat)
        .Include(i => i.FKSteelgrade)
        .Include(i => i.FKSteelgrade.PRMSteelgradeChemicalComposition)
        .Include(i => i.PRMMaterials)
        .Include(i => i.PRFKPIValues)
        .Where(x => x.WorkOrderId == workOrderId)
        .Single();

      double? metallicYield = workOrder.PRFKPIValues
        .OrderByDescending(x => x.KPIValueId)
        .FirstOrDefault(x => x.FKKPIDefinitionId == KPIType.WOMY)
        ?.KPIValue;

      result = new VM_WorkOrderOverview(workOrder, metallicYield);

      return result;
    }

    public DataSourceResult GetMaterialsListByWorkOrderId(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      List<DbEntity.SPModels.SPL3L1MaterialAssignment> result = FindL3L1MaterialAssignment(workOrderId).ToList();
      if (result.Count > 0)
        result = SortMaterials(result);
      request.RenameRequestSortDescriptor(nameof(VM_L3L1MaterialAssignment.HasDefects), nameof(VM_L3L1MaterialAssignment.DefectsNumber));
      request.FilterShortByBooleanValue(nameof(VM_L3L1MaterialAssignment.HasDefects), nameof(VM_L3L1MaterialAssignment.DefectsNumber), 1);
      return result.ToDataSourceLocalResult(request, modelState, data => new VM_L3L1MaterialAssignment(data));
    }

    public DataSourceResult GetNoScheduledWorkOrderList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      try
      {
        IQueryable<PRMWorkOrder> workOrderList = _peContext.PRMWorkOrders
          .Where(x => x.EnumWorkOrderStatus < WorkOrderStatus.Scheduled.Value && !x.IsBlocked)
          .Include(i => i.FKMaterialCatalogue)
          .Include(i => i.FKSteelgrade)
          .AsQueryable();
        workOrderList.Select(x => x.FKMaterialCatalogue).AsQueryable();


        return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data));
      }
      catch (Exception e)
      {
        string ex = e.Message;
        return null;
      }
    }
    //av@
    //public DataSourceResult GetNoScheduledWorkOrderList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  try
    //  {
    //    IQueryable<PRMWorkOrdersEXT> workOrderList = _peCustomContext.PRMWorkOrdersEXTs
    //      .Where(x => x.EnumWorkOrderStatus < WorkOrderStatus.Scheduled.Value && !x.IsBlocked)
    //      .Include(i => i.FKMaterialCatalogue)
    //      .Include(i => i.FKSteelgrade)
    //      .AsQueryable();
    //    workOrderList.Select(x => x.FKMaterialCatalogue).AsQueryable();


    //    return workOrderList.ToDataSourceLocalResult(request, modelState, data => new VM_WorkOrderOverview(data));
    //  }
    //  catch (Exception e)
    //  {
    //    string ex = e.Message;
    //    return null;
    //  }
    //}
    //Av@

    public async Task<VM_Base> CreateWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref workOrder);

      DCWorkOrderEXT dcWorkOrder = new DCWorkOrderEXT
      {
        WorkOrderName = workOrder.WorkOrderName,
        IsTestOrder = workOrder.IsTestOrder,
        TargetOrderWeight = workOrder.TargetOrderWeight,
        TargetOrderWeightMin = workOrder.TargetOrderWeightMin,
        TargetOrderWeightMax = workOrder.TargetOrderWeightMax,
        BundleWeightMin = workOrder.BundleWeightMin,
        BundleWeightMax = workOrder.BundleWeightMax,
        CreatedInL3Ts = DateTime.Now,
        ToBeCompletedBeforeTs = workOrder.ToBeCompletedBeforeTs,
        FKHeatId = workOrder.FKHeatId,
        FKSteelgradeId = workOrder.FKSteelgradeId.Value,
        FKProductCatalogueId = workOrder.FKProductCatalogueId,
        FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId,
        FKCustomerId = workOrder.FKCustomerId,
        MaterialsNumber = workOrder.MaterialsNumber,
        WorkOrderStatus = WorkOrderStatus.New
      };

      InitDataContract(dcWorkOrder);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendWorkOrderAsync(dcWorkOrder);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      if (sendOfficeResult.OperationSuccess)
      {
        DCMaterialEXT dcMaterial = new DCMaterialEXT
        {
          IsTestOrder = workOrder.IsTestOrder,
          FKWorkOrderIdRef = workOrder.WorkOrderName,
          Weight = workOrder.TargetOrderWeight / (workOrder.MaterialsNumber > 0 ? workOrder.MaterialsNumber : 1),
          MaterialsNumber = workOrder.MaterialsNumber,
          FKHeatId = workOrder.FKHeatId.Value,
          FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId
        };
        // NOTE: do not create materials for WO, use yards
        SendOfficeResult<DataContractBase> sendOfficeResultForMaterial = await HmiSendOffice.CreateMaterialAsync(dcMaterial);
        HandleWarnings(sendOfficeResultForMaterial, ref modelState);
      }

      //return view model
      return result;
    }

    public async Task<VM_Base> EditWorkOrder(ModelStateDictionary modelState, VM_WorkOrder workOrder)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref workOrder);

      DCWorkOrderEXT dcWorkOrder = new DCWorkOrderEXT
      {
        WorkOrderId = workOrder.WorkOrderId,
        WorkOrderName = workOrder.WorkOrderName,
        IsTestOrder = workOrder.IsTestOrder,
        TargetOrderWeight = workOrder.TargetOrderWeight,
        TargetOrderWeightMin = workOrder.TargetOrderWeightMin,
        TargetOrderWeightMax = workOrder.TargetOrderWeightMax,
        CreatedInL3Ts = workOrder.WorkOrderCreatedInL3Ts,
        ToBeCompletedBeforeTs = workOrder.ToBeCompletedBeforeTs,
        FKHeatId = workOrder.FKHeatId,
        FKSteelgradeId = workOrder.FKSteelgradeId.Value,
        FKProductCatalogueId = workOrder.FKProductCatalogueId,
        FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId,
        FKCustomerId = workOrder.FKCustomerId,
        MaterialsNumber = workOrder.MaterialsNumber,
        WorkOrderStatus = WorkOrderStatus.GetValue(workOrder.WorkOrderStatus)
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendWorkOrderAsync(dcWorkOrder);

      HandleWarnings(sendOfficeResult, ref modelState);

      if (sendOfficeResult.OperationSuccess)
      {
        DCMaterialEXT dcMaterial = new DCMaterialEXT
        {
          //MaterialId is used only to pass number of materials assigned to work order
          FKWorkOrderIdRef = workOrder.WorkOrderName,
          FKMaterialCatalogueId = workOrder.FKMaterialCatalogueId,
          MaterialsNumber = workOrder.MaterialsNumber,
          FKHeatId = workOrder.FKHeatId.Value,
          Weight = workOrder.TargetOrderWeight / (workOrder.MaterialsNumber > 0 ? workOrder.MaterialsNumber : 1)
        };
        SendOfficeResult<DataContractBase> sendOfficeResultForMaterial =
          await HmiSendOffice.UpdateMaterialAsync(dcMaterial);
        HandleWarnings(sendOfficeResultForMaterial, ref modelState);
      }

      return result;
    }

    public async Task<VM_WorkOrder> GetWorkOrder(long? id)
    {
      VM_WorkOrder result = null;

      PRMWorkOrder workOrder = await _peContext.PRMWorkOrders
        .Include(x => x.FKHeat)
        .Include(x => x.FKMaterialCatalogue)
        .SingleOrDefaultAsync(x => x.WorkOrderId == id);

      result = workOrder == null ? null : new VM_WorkOrder(workOrder);
      result.MaterialsNumber = await _peContext.PRMMaterials.Where(x => x.FKWorkOrderId == id).CountAsync();

      return result;
    }

    public IList<PRMHeat> GetHeatList()
    {
      List<PRMHeat> result = new List<PRMHeat>();
      PRMHeat emptyHeat = new PRMHeat { HeatName = "" };
      result = _peContext.PRMHeats.ToList();
      result.Add(emptyHeat);

      return result;
    }

    public IList<PRMProductCatalogue> GetProductList()
    {
      List<PRMProductCatalogue> result = new List<PRMProductCatalogue>();
      result = _peContext.PRMProductCatalogues.ToList();

      return result;
    }

    public IList<PRMCustomer> GetCustomerList()
    {
      List<PRMCustomer> result = new List<PRMCustomer>();
      result = _peContext.PRMCustomers.ToList();

      return result;
    }

    public IList<PRMMaterialCatalogue> GetMaterialList()
    {
      List<PRMMaterialCatalogue> result = new List<PRMMaterialCatalogue>();
      result = _peContext.PRMMaterialCatalogues.ToList();

      return result;
    }

    public async Task<VM_Base> DeleteWorkOrder(ModelStateDictionary modelState, VM_WorkOrderOverview workOrder)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref workOrder);

      DCWorkOrderEXT entryDataContract = new DCWorkOrderEXT { WorkOrderName = workOrder.WorkOrderName };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteWorkOrderAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<long?> GetWorkOrderIdByMaterialId(long id)
    {
      long? workOrderId;
      workOrderId = await _peContext.PRMMaterials.Where(x => x.MaterialId == id).Select(x => x.FKWorkOrderId).FirstAsync();

      return workOrderId;
    }

    public async Task<VM_BilletCatalogueDetails> GetBilletCatalogueDetails(long billetCatalogId)
    {
      VM_BilletCatalogueDetails result = null;
      PRMMaterialCatalogue data = await _peContext.PRMMaterialCatalogues
        .Where(w => w.MaterialCatalogueId == billetCatalogId)
        .SingleOrDefaultAsync();

      result = new VM_BilletCatalogueDetails(data);

      return result;
    }

    public async Task<VM_ProductCatalogue> GetProductCatalogueDetails(long productCatalogId)
    {
      VM_ProductCatalogue result = null;
      PRMProductCatalogue data = await _peContext.PRMProductCatalogues

        .Include(i => i.FKShape)
        .Include(i => i.FKProductCatalogueType)
        .Where(w => w.ProductCatalogueId == productCatalogId)
        .SingleOrDefaultAsync();

      result = new VM_ProductCatalogue(data);

      return result;
    }
    //av@
    //public async Task<VM_ProductCatalogue> GetProductCatalogueDetails(long productCatalogId)
    //{
    //  VM_ProductCatalogue result = null;
    //  PRMProductCatalogueEXT data = await _peCustomContext.PRMProductCatalogueEXTs

    //    //.Include(i => i.FKShape)
    //    .Include(i => i.FKProductCatalogueId)
    //    .Where(w => w.ProductCatalogueId == productCatalogId)
    //    .SingleOrDefaultAsync();

    //  result = new VM_ProductCatalogue(data);

    //  return result;
    //}
    //Av@


    public async Task<VM_WorkOrderMaterials> GetWorkOrderMaterialsDetails(ModelStateDictionary modelState, long workOrderId)
    {
      VM_WorkOrderMaterials result = null;

      PRMWorkOrder workOrder = await _peContext.PRMWorkOrders
        .SingleOrDefaultAsync(x => x.WorkOrderId == workOrderId);

      result = workOrder == null ? null : new VM_WorkOrderMaterials(workOrder);

      return result;
    }

    public async Task<VM_Base> EditMaterialNumber(ModelStateDictionary modelState,
      VM_WorkOrderMaterials workOrderMaterials)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderMaterials dataContract = new DCWorkOrderMaterials
      {
        WorkOrderId = workOrderMaterials.WorkOrderId,
        MaterialsNumber = workOrderMaterials.MaterialsNumber
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.EditMaterialNumberAsync(dataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public DataSourceResult GetWorkOrderProducts(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_ProductOverview> result = _hmiContext.V_ProductOverviews
        .Where(x => x.WorkOrderId == workOrderId);
      return result.ToDataSourceLocalResult(request, modelState, data => new VM_ProductsOverview(data));
    }

    public DataSourceResult GetWorkOrderEvents(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_Event> result = _hmiContext.V_Events
        .AsNoTracking()
        .Where(x => x.WorkOrderId == workOrderId &&
          (x.EventTypeId != EventType.Checkpoint1DelayMicroStop && x.EventTypeId != EventType.Checkpoint1Delay));
      return result.ToDataSourceLocalResult(request, modelState, data => new VM_EventCalendar(data));
    }

    public DataSourceResult GetWorkOrderDelays(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      request.RenameRequestSortDescriptor(nameof(VM_DelayOverview.DelayDurationText), nameof(VM_DelayOverview.DelayDuration));
      request.FilterDurationByStringValue(nameof(VM_DelayOverview.DelayDurationText), nameof(VM_DelayOverview.DelayDuration));
      IQueryable<V_DelayOverview> result = _hmiContext.V_DelayOverviews
        .AsNoTracking()
        .Where(x => x.WorkOrderId == workOrderId);
      return result.ToDataSourceLocalResult(request, modelState, data => new VM_DelayOverview(data));
    }

    public DataSourceResult GetWorkOrderDefects(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_DefectsSummary> defectList = _hmiContext.V_DefectsSummaries.Where(x => x.WorkOrderId == workOrderId).AsQueryable();
      return defectList.ToDataSourceLocalResult<V_DefectsSummary, VM_Defect>(request, modelState, data => new VM_Defect(data));
    }

    public DataSourceResult GetWorkOrderRejects(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_RawMaterialOverview> rejectList = _hmiContext.V_RawMaterialOverviews
        .Where(x => x.WorkOrderId == workOrderId && x.EnumRejectLocation > 0)
        .AsQueryable();
      return rejectList.ToDataSourceLocalResult<V_RawMaterialOverview, VM_RawMaterialOverview>(request, modelState, data => new VM_RawMaterialOverview(data));
    }

    public async Task<long?> GetHeatByName(string heatName)
    {
      long? id;
      if (heatName == null)
      {
        id = null;
      }
      else
      {
        id = await _peContext.PRMHeats.Where(x => x.HeatName.Equals(heatName)).Select(x => x.HeatId).FirstAsync();
      }

      return id;
    }

    public VM_WorkOrderSummary GetWorkOrderSummary(long workorderId)
    {
      VM_WorkOrderSummary result = new VM_WorkOrderSummary();
      V_WorkOrderSummary data = _hmiContext.V_WorkOrderSummaries.Where(x => x.WorkOrderId == workorderId).SingleOrDefault();

      if (data != null)
      {
        result = new VM_WorkOrderSummary(data);
      }

      return result;
    }

    public DataSourceResult GetWorkOrderScraps(ModelStateDictionary modelState, DataSourceRequest request,
      long workOrderId)
    {
      IQueryable<V_RawMaterialOverview> rejectList = _hmiContext.V_RawMaterialOverviews
        .Where(x => x.WorkOrderId == workOrderId && x.IsScrap)
        .AsQueryable();
      return rejectList.ToDataSourceLocalResult<V_RawMaterialOverview, VM_RawMaterialOverview>(request, modelState, data => new VM_RawMaterialOverview(data));
    }

    public async Task<VM_Base> SendWorkOrderReportAsync(ModelStateDictionary modelState, VM_WorkOrderConfirmation workOrderConfirmation)
    {
      VM_Base result = new VM_Base();

      long workOrderId = 0;

      if (workOrderConfirmation.WorkOrderId != null)
      {
        workOrderId = workOrderConfirmation.WorkOrderId.Value;
        await ValidateWorkOrderCanBeSent(modelState, workOrderId);
      }
      else
      {
        V_WorkOrderSummary workOrder = _hmiContext.V_WorkOrderSummaries
          .Where(x => x.EnumWorkOrderStatus == WorkOrderStatus.InRealization && !x.IsTestOrder)
          .OrderByDescending(x => x.WorkOrderStartTs).FirstOrDefault();

        if (workOrder != null)
          workOrderId = workOrder.WorkOrderId;
      }

      if (!modelState.IsValid)
        return result;

      DCWorkOrderConfirmation dc = new DCWorkOrderConfirmation(workOrderId)
      {
        IsEndOfWorkShop = workOrderConfirmation.IsEndOfWorkShop
      };
      InitDataContract(dc);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendWorkOrderReportAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    private List<DbEntity.SPModels.SPL3L1MaterialAssignment> FindL3L1MaterialAssignment(long workOrderId)
    {
      SqlParameter[] parameters = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@workOrderId",
                            SqlDbType =  SqlDbType.BigInt,
                            IsNullable = false,
                            Direction = ParameterDirection.Input,
                            Value = workOrderId
                        }};

      return _hmiContext.ExecuteL3L1MaterialAssignment(parameters);
    }

    private List<DbEntity.SPModels.SPL3L1MaterialAssignment> SortMaterials(List<DbEntity.SPModels.SPL3L1MaterialAssignment> materials)
    {
      List<DbEntity.SPModels.SPL3L1MaterialAssignment> result = new List<DbEntity.SPModels.SPL3L1MaterialAssignment>();

      if (materials == null || materials.Count == 0)
        return result;

      List<long> materialIds = materials
        .OrderBy(x => x.MaterialId)
        .GroupBy(x => x.MaterialId)
        .Select(x => x.First().MaterialId)
        .ToList();

      foreach (long item in materialIds)
      {
        DbEntity.SPModels.SPL3L1MaterialAssignment parent = materials.First(x => x.MaterialId == item && x.ParentRawMaterialId == null);
        result.Add(parent);
        materials.Remove(parent);

        if (materials.Any(x => x.MaterialId == item))
        {
          result.AddRange(SortChildrenMaterials(
             parent.RawMaterialId.Value, materials.Where(x => x.MaterialId == item).OrderBy(x => x.RawMaterialId).ToList()));
          materials.RemoveAll(x => x.MaterialId == item);
        }
      }

      return result;
    }

    private List<DbEntity.SPModels.SPL3L1MaterialAssignment> SortChildrenMaterials(long parentId, List<DbEntity.SPModels.SPL3L1MaterialAssignment> children)
    {
      List<DbEntity.SPModels.SPL3L1MaterialAssignment> result = new List<DbEntity.SPModels.SPL3L1MaterialAssignment>();
      List<long> materialIds = children.Select(x => x.RawMaterialId.Value).ToList();
      foreach (long item in materialIds)
      {
        if (!children.Any())
          return result;
        DbEntity.SPModels.SPL3L1MaterialAssignment child = children.FirstOrDefault(x => x.RawMaterialId == item);
        if (child != null && child.ParentRawMaterialId == parentId)
        {
          result.Add(child);
          children.Remove(child);
          if (children.Any(x => x.ParentRawMaterialId == item))
          {
            result.AddRange(SortChildrenMaterials(item, children));
          }
        }
      }

      return result;
    }

    private async Task ValidateWorkOrderCanBeSent(ModelStateDictionary modelState, long workOrderId)
    {
      PRMWorkOrder workOrder;
      int scrapsWithoutAsset;
      int defectsWithDefaultCategory;
      int delaysWithDefaultCategory;

      workOrder = await _peContext.PRMWorkOrders.FirstOrDefaultAsync(x => x.WorkOrderId == workOrderId);

      scrapsWithoutAsset = await _hmiContext.V_RawMaterialOverviews
        .Where(x => x.WorkOrderId == workOrderId && x.ScrapPercent.HasValue && x.ScrapPercent > 0 && x.AssetId == null)
        .CountAsync();

      long defaultDefectCatalogueId = await _peContext.QTYDefectCatalogues.Where(x => x.IsDefault).Select(x => x.DefectCatalogueId).FirstOrDefaultAsync();
      defectsWithDefaultCategory = await _hmiContext.V_DefectsSummaries
        .Where(x => x.WorkOrderId == workOrderId && x.DefectCatalogueId == defaultDefectCatalogueId)
        .CountAsync();

      delaysWithDefaultCategory = await _peContext.EVTEvents
        .Where(x => x.FKWorkOrder.WorkOrderId == workOrderId && x.FKEventCatalogue.IsDefault)
        .CountAsync();

      if (workOrder.IsSentToL3)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("WorkOrderHasBeenAlreadySentToL3"));
      }
      else if (workOrder.EnumWorkOrderStatus != WorkOrderStatus.Finished)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("WorkOrderNotInFinishedStatus"));
      }

      if (workOrder.IsTestOrder)
        AddModelStateError(modelState, ResourceController.GetErrorText("CannotSendTestWorkOrder"));

      if (scrapsWithoutAsset > 0)
        AddModelStateError(modelState, ResourceController.GetErrorText("ScrapWithoutAsset"));

      if (defectsWithDefaultCategory > 0)
        AddModelStateError(modelState, ResourceController.GetErrorText("DefectHasAssignedDefaultCatalogue"));

      if (delaysWithDefaultCategory > 0)
        AddModelStateError(modelState, ResourceController.GetErrorText("DelayHasAssignedDefaultCatalogue"));
    }

    public async Task<VM_Base> CancelWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      var isAssigned = _peContext.PRMMaterials.Where(x=>x.FKWorkOrderId == workOrderId).Any(x => x.IsAssigned);

      if (isAssigned)
      {
        AddModelStateError(modelState,ResourceController.GetResourceTextByResourceKey("MESSAGE_MaterialsAssignedOrderNotCancelableAndBlockable"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderCancel dcWorkOrder = new DCWorkOrderCancel(workOrderId) { IsCancel = true };
      InitDataContract(dcWorkOrder);

      ////request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateCanceledWorkOrderAsync(dcWorkOrder);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UnCancelWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderCancel dcWorkOrder = new DCWorkOrderCancel(workOrderId) { IsCancel = false };
      InitDataContract(dcWorkOrder);

      ////request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateUnCanceledWorkOrderAsync(dcWorkOrder);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> BlockWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      if (workOrderId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      var isAssigned = await _peContext.PRMMaterials.Where(x => x.FKWorkOrderId == workOrderId).AnyAsync(x => x.IsAssigned);

      if (isAssigned)
      {
        AddModelStateError(modelState, ResourceController.GetResourceTextByResourceKey("MESSAGE_MaterialsAssignedOrderNotCancelableAndBlockable"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderBlock dcWorkOrder = new DCWorkOrderBlock(workOrderId) { IsBlock = true };
      InitDataContract(dcWorkOrder);

      ////request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateBlockedWorkOrderAsync(dcWorkOrder);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UnBlockWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      DCWorkOrderBlock dcWorkOrder = new DCWorkOrderBlock(workOrderId) { IsBlock = false };
      InitDataContract(dcWorkOrder);

      ////request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateUnBlockedWorkOrderAsync(dcWorkOrder);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
  }
}
