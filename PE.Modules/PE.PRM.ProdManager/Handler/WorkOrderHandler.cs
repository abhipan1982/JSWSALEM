using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.Models.DataContracts.Internal.PRM;

using PE.PRM.Base.Handlers.Models;
using PE.PRM.ProdManager.Handler.Models;

namespace PE.PRM.ProdManager.Handler
{
  public class WorkOrderHandler
  {
    public PRMWorkOrder CreateWorkOrder(PEContext ctx,
      bool isTest,
      double? targetOrderWeight,
      ICollection<PRMMaterial> materials,
      PRMSteelgrade steelgrade,
      PRMHeat heat,
      PRMMaterialCatalogue materialCatalogue,
      long productCatalogue,
      PRMCustomer customer,
      DateTime? l3Created = null,
      string workOrderName = null,
      string externalWorkOrderName = null,
      DateTime? toBeCompletedBefore = null,
      double? bundleWeightMin = null,
      double? bundleWeightMax = null,
      double? targetOrderWeightMin = null,
      double? targetOrderWeightMax = null
    )
    {
      if (isTest)
      {
        workOrderName = workOrderName ?? "TestOrder" + l3Created?.ToString("yyyyMMddHHmmss");
        targetOrderWeight = targetOrderWeight ?? 1000;
      }
      else
      {
        if (workOrderName == null)
        {
          throw new Exception("Work order name must be set");
        }

        if (targetOrderWeight == null)
        {
          throw new Exception("Order weight must be set");
        }
      }

      PRMWorkOrder workOrder = new PRMWorkOrder
      {
        EnumWorkOrderStatus = WorkOrderStatus.New,
        IsTestOrder = isTest,
        WorkOrderCreatedInL3Ts = l3Created ?? DateTime.Now,
        WorkOrderName = workOrderName,
        ExternalWorkOrderName = externalWorkOrderName,
        ToBeCompletedBeforeTs = toBeCompletedBefore ?? DateTime.Now,
        TargetOrderWeight = (double)targetOrderWeight,
        TargetOrderWeightMin = targetOrderWeightMin,
        TargetOrderWeightMax = targetOrderWeightMax,
        BundleWeightMin = bundleWeightMin,
        BundleWeightMax = bundleWeightMax,
        FKProductCatalogueId = productCatalogue,
        FKCustomer = customer,
        FKMaterialCatalogue = materialCatalogue,
        FKSteelgrade = steelgrade,
        PRMMaterials = materials,
        FKHeat = heat,
        L3NumberOfBillets = (short)materials.Count
      };

      return workOrder;
    }


    ////////////////  /// <summary>
    ////////////////  /// Create new work order and workorder ext
    ////////////////  /// </summary>
    ////////////////  /// <param name="ctx"></param>
    ////////////////  /// <param name="productCatalogue"></param>
    ////////////////  /// <param name="workOrderName"></param>
    ////////////////  /// <param name="toBeReadyBefore"></param>
    ////////////////  /// <param name="targetOrderWeight"></param>
    ////////////////  /// <param name="targetOrderWeightMin"></param>
    ////////////////  /// <param name="targetOrderWeightMax"></param>
    ////////////////  /// <param name="qualityPolicy"></param>
    ////////////////  /// <param name="extraLabelInformation"></param>
    ////////////////  /// <param name="customerId"></param>
    ////////////////  /// <param name="reheatingGroup"></param>
    ////////////////  /// <returns></returns>
    ////////////////  public PRMWorkOrder CreateWorkOrder(PEContext ctx, long productCatalogue, PRMCustomer customer, long reheatingGroup, PRMMaterialCatalogue materialCatalogue, List<PRMMaterial> materials, PRMHeat heat, DCL3L2WorkOrderDefinition message)
    ////////////////{
    ////////////////  PRMWorkOrder workOrder = new PRMWorkOrder()
    ////////////////  {
    ////////////////    WorkOrderStatus = DbEntity.Enums.OrderStatus.ENUM_New,
    ////////////////    CreatedTs = DateTime.Now,
    ////////////////    LastUpdateTs = DateTime.Now,
    ////////////////    CreatedInL3 = message.L3Created,
    ////////////////    WorkOrderName = message.WorkOrderName,
    ////////////////    ToBeCompletedBefore = message.ToBeReadyBefore,
    ////////////////    TargetOrderWeight = message.TargetOrderWeight,
    ////////////////    TargetOrderWeightMin = message.TargetOrderWeightMin,
    ////////////////    TargetOrderWeightMax = message.TargetOrderWeightMax,
    ////////////////    FKProductCatalogueId = productCatalogue,
    ////////////////    QualityPolicy = message.QualityPolicy,
    ////////////////    ExtraLabelInformation = message.ExtraLabelInformation,
    ////////////////    FKReheatingGroupId = reheatingGroup,
    ////////////////    PRMCustomer = customer,
    ////////////////    NextAggregate = message.NextAggregate,
    ////////////////    OperationCode = message.OperationCode,
    ////////////////    ExternalSteelgradeCode = message.ExternalSteelGradeCode,


    ////////////////    PRMWorkOrdersEXT = new PRMWorkOrdersEXT()
    ////////////////    {
    ////////////////      CreatedTs = DateTime.Now,
    ////////////////    },
    ////////////////    PRMMaterialCatalogue = materialCatalogue,
    ////////////////    PRMMaterials = materials,
    ////////////////    PRMHeat = heat

    ////////////////  };

    ////////////////  return workOrder;
    ////////////////}

    ////////////////public PRMWorkOrder CreateTestWorkOrder(PEContext ctx, long productCatalogue, PRMCustomer customer, long? reheatingGroup, PRMMaterialCatalogue materialCatalogue, List<PRMMaterial> materials, PRMHeat heat,  double? weight)
    ////////////////{
    ////////////////  return new PRMWorkOrder()
    ////////////////  {
    ////////////////    WorkOrderStatus = DbEntity.Enums.OrderStatus.ENUM_Scheduled,
    ////////////////    CreatedTs = DateTime.Now,
    ////////////////    LastUpdateTs = DateTime.Now,
    ////////////////    CreatedInL3 = DateTime.Now,
    ////////////////    WorkOrderName = "TestWorkOrder" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute,
    ////////////////    ToBeCompletedBefore = DateTime.Now,
    ////////////////    TargetOrderWeight = (double)weight,
    ////////////////    TargetOrderWeightMin = weight,
    ////////////////    TargetOrderWeightMax = weight,
    ////////////////    FKProductCatalogueId = productCatalogue,
    ////////////////    QualityPolicy = "",
    ////////////////    ExtraLabelInformation = "Test",
    ////////////////    FKReheatingGroupId = reheatingGroup,
    ////////////////    PRMCustomer = customer,
    ////////////////    NextAggregate = "",
    ////////////////    OperationCode = "Test order",
    ////////////////    ExternalSteelgradeCode = "Test",


    ////////////////    PRMWorkOrdersEXT = new PRMWorkOrdersEXT()
    ////////////////    {
    ////////////////      CreatedTs = DateTime.Now,
    ////////////////    },
    ////////////////    PRMMaterialCatalogue = materialCatalogue,
    ////////////////    PRMMaterials = materials,
    ////////////////    PRMHeat = heat,
    ////////////////IsTestOrder = true
    ////////////////  };
    ////////////////}

    /// <summary>
    ///   Return whole work order, find by id with ext and materials included
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderId"></param>
    /// <returns></returns>
    /// 

    public Task<PRMWorkOrder> GetWorkOrderByIdAsyncEXT(PEContext ctx, long workOrderId)
    {
      return ctx.PRMWorkOrders
        .Include(x => x.PRMMaterials)
        .Include(x => x.FKProductCatalogue)
        .SingleAsync(x => x.WorkOrderId == workOrderId);
    }



    /// <summary>
    ///   Return whole work order, with informations related
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderId"></param>
    /// <returns></returns>
    public Task<PRMWorkOrder> GetWorkOrderByIdWithDeepIncludeAsync(PEContext ctx, long workOrderId)
    {
      return ctx.PRMWorkOrders
        .Include(x => x.PRMMaterials)
        .Include(x => x.PRMProducts)
        .Include(x => x.FKHeat)
        .Include(x => x.FKSteelgrade)
        .Include(x => x.FKProductCatalogue)
        .Include(x => x.FKMaterialCatalogue)
        .Include(x => x.EVTEvents)
        .Where(x => x.WorkOrderId == workOrderId).SingleAsync();
    }

    public void CreateWorkOrderByUserEXT(PEContext ctx, DCWorkOrderEXT dc)
    {
      PRMWorkOrder workOrder = new PRMWorkOrder
      {
        EnumWorkOrderStatus = dc.WorkOrderStatus,
        WorkOrderName = dc.WorkOrderName,
        IsTestOrder = dc.IsTestOrder,
        TargetOrderWeight = dc.TargetOrderWeight,
        TargetOrderWeightMin = dc.TargetOrderWeightMin ?? dc.TargetOrderWeight,
        TargetOrderWeightMax = dc.TargetOrderWeightMax ?? dc.TargetOrderWeight,
        BundleWeightMin = dc.BundleWeightMin,
        BundleWeightMax = dc.BundleWeightMax,
        WorkOrderCreatedInL3Ts = dc.CreatedInL3Ts,
        ToBeCompletedBeforeTs = dc.ToBeCompletedBeforeTs,
        FKProductCatalogueId = dc.FKProductCatalogueId,
        FKHeatId = dc.FKHeatId,
        FKSteelgradeId = dc.FKSteelgradeId,
        FKMaterialCatalogueId = dc.FKMaterialCatalogueId,
        FKCustomerId = dc.FKCustomerId,
        L3NumberOfBillets = (short)dc.MaterialsNumber
      };

      ctx.PRMWorkOrders.Add(workOrder);
    }

    public void UpdateWorkOrderByUserEXT(PEContext ctx, PRMWorkOrder workOrder, DCWorkOrderEXT dc)
    {
      workOrder.WorkOrderName = dc.WorkOrderName;
      workOrder.IsTestOrder = dc.IsTestOrder;
      workOrder.TargetOrderWeight = dc.TargetOrderWeight;
      workOrder.TargetOrderWeightMin = dc.TargetOrderWeightMin;
      workOrder.TargetOrderWeightMax = dc.TargetOrderWeightMax;
      workOrder.ToBeCompletedBeforeTs = dc.ToBeCompletedBeforeTs;
      workOrder.FKProductCatalogueId = dc.FKProductCatalogueId;
      workOrder.FKCustomerId = dc.FKCustomerId;
      workOrder.FKMaterialCatalogueId = dc.FKMaterialCatalogueId;
      workOrder.FKHeatId = dc.FKHeatId;
      workOrder.FKSteelgradeId = dc.FKSteelgradeId;
      workOrder.L3NumberOfBillets = (short)dc.MaterialsNumber;

      if (dc.WorkOrderStatus != null)
      {
        workOrder.EnumWorkOrderStatus = dc.WorkOrderStatus;
      }
    }

    /// <summary>
    ///   True if work order with this name exist
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderName"></param>
    /// <returns></returns>
    public Task<PRMWorkOrder> GetWorkOrderByNameAsyncEXT(PEContext ctx, string workOrderName)
    {
      return ctx.PRMWorkOrders.Where(x => x.WorkOrderName.ToLower().Equals(workOrderName.ToLower()))
        .SingleOrDefaultAsync();
    }

    /// <summary>
    ///   Update work order properties
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderName"></param>
    /// <param name="toBeReadyBefore"></param>
    /// <param name="targetOrderWeight"></param>
    /// <param name="targetOrderWeightMin"></param>
    /// <param name="targetOrderWeightMax"></param>
    public async Task<PRMWorkOrder> UpdateWorkOrderAsync(PEContext ctx,
      long productCatalogue,
      PRMCustomer customer,
      PRMMaterialCatalogue matCatalogue,
      List<PRMMaterial> materials,
      PRMHeat heat,
      PRMSteelgrade steelgrade,
      PRMWorkOrder parentWorkOrder,
      ValidatedBatchData validatedWorkOrder)
    {
      PRMWorkOrder workOrder = await ctx.PRMWorkOrders
        .Where(x => x.WorkOrderName.ToLower().Equals(validatedWorkOrder.WorkOrderName.ToLower())).SingleAsync();
      workOrder.ToBeCompletedBeforeTs = validatedWorkOrder.OrderDeadline;
      workOrder.TargetOrderWeight = validatedWorkOrder.TargetWorkOrderWeight;

      workOrder.BundleWeightMin = validatedWorkOrder.BundleWeightMin;
      workOrder.BundleWeightMax = validatedWorkOrder.BundleWeightMax;

      workOrder.TargetOrderWeightMin = validatedWorkOrder.TargetWorkOrderWeightMin;
      workOrder.TargetOrderWeightMax = validatedWorkOrder.TargetWorkOrderWeightMax;

      workOrder.FKProductCatalogueId = productCatalogue;
      workOrder.ExternalWorkOrderName = validatedWorkOrder.ExternalWorkOrderName;
      workOrder.FKCustomer = customer;
      workOrder.FKMaterialCatalogue = matCatalogue;
      workOrder.PRMMaterials = materials;
      workOrder.FKHeat = heat;
      workOrder.FKSteelgrade = steelgrade;
      workOrder.FKParentWorkOrder = parentWorkOrder;
      workOrder.L3NumberOfBillets = validatedWorkOrder.NumberOfBillets;

      return workOrder;
    }

    /// <summary>
    ///   Update work order status by his name
    ///   Be sure if work order exist before run this function (IsWorkOrderExist())
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="workOrderName"></param>
    /// <param name="status"></param>
    public async Task UpdateWorkOrderStatusAsync(PEContext ctx, long workOrderId, WorkOrderStatus status,
      DateTime? scheduledTime = null)
    {
      PRMWorkOrder workOrder = await ctx.PRMWorkOrders
        .SingleAsync(x => x.WorkOrderId == workOrderId);
      workOrder.EnumWorkOrderStatus = status;
    }

    public async Task<long> WorkOrderIdByNameAsyncEXT(PEContext ctx, string workOrderName)
    {
      PRMWorkOrder workOrder = await ctx.PRMWorkOrders
        .SingleOrDefaultAsync(x => x.WorkOrderName.ToLower().Equals(workOrderName.ToLower()));

      return workOrder.WorkOrderId;
    }

    //public async Task<PRMMaterial> FindFirstUnassignedMaterialInWorkOrderAsync(PEContext ctx, long workOrderId)
    //{
    //  return await ctx.PRMMaterials
    //                        .Where(w =>
    //                                      w.FKWorkOrderId == workOrderId &&
    //                                      w.IsAssigned == false)
    //                        .Take(1)
    //                        .FirstOrDefaultAsync();
    //}

    public void UpdateCanceledWorkOrderByUser(PRMWorkOrder workOrder, DCWorkOrderCancel dc)
    {
      workOrder.EnumWorkOrderStatus = dc.IsCancel ? WorkOrderStatus.Cancelled : WorkOrderStatus.New;
    }

    public void UpdateBlockedWorkOrderByUser(PRMWorkOrder workOrder, DCWorkOrderBlock dc)
    {
      workOrder.IsBlocked = dc.IsBlock;
    }
  }
}
