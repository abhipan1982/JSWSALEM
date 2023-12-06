using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
//using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.External.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Helpers;
using PE.Models.DataContracts.External.DBA;
using SMF.Core.Notification;
using PE.DbEntity.TransferModels;
using PE.Models.DataContracts.Internal.DBA;
using PE.DbEntity.PEContext;

namespace PE.DBA.DataBaseAdapter.Handlers
{
  public class DataTransferHandler
  {
    public async Task<IEnumerable<L3L2BatchDataDefinition>> GetDataToBeTransfered(TransferCustomContext ctx)
    {
      return await ctx.L3L2BatchDataDefinitions
        .Where(x => x.CommStatus == CommStatus.New.Value && !x.IsUpdated)
        .OrderBy(x => x.CreatedTs)
        .Take(2)
        .ToListAsync();
    }


    public async Task UpdateTransferTableCommStatusesAsync(TransferCustomContext ctx,
      DCBatchDataStatus batchDataProcessingResult)
    {
      L3L2BatchDataDefinition target =
        await ctx.L3L2BatchDataDefinitions.SingleAsync(x => x.CounterId == batchDataProcessingResult.Counter);
      target.CommStatus = batchDataProcessingResult.Status.Value;
      target.CommMessage = batchDataProcessingResult.BackMessage;

      NotificationController.Debug(string.Format("Status changed for WorkOrder {0}.", target.PO_NO));
    }

    public async Task<bool> UpdateBatchDataWithTimeoutAsync(TransferCustomContext ctx)
    {
      IEnumerable<L3L2BatchDataDefinition> toBeUpdated = await ctx.L3L2BatchDataDefinitions
        .Where(x => x.CommStatus == CommStatus.BeingProcessed.Value
          && x.UpdatedTs < DateTime.Now.AddSeconds(-120)
        )
        .OrderBy(x => x.CreatedTs)
        .ToListAsync();


      foreach (L3L2BatchDataDefinition data in toBeUpdated)
      {
        data.CommStatus = CommStatus.New.Value;
        data.UpdatedTs = DateTime.Now;

        NotificationController.Warn(string.Format(
          "System detected timeout during processing BatchData {0}. Preparing data for resend.", data.PO_NO));
      }

      return toBeUpdated.Any();
    }

    public Task<L3L2BatchDataDefinition> GetWorkOrderDefinitionByIdAsync( TransferCustomContext ctx, long counter)
    {
      return ctx.L3L2BatchDataDefinitions.Where(x => x.CounterId == counter).SingleOrDefaultAsync();
    }

    public void UpdateWorkOrderDefinition(L3L2BatchDataDefinition wod, DCL3L2BatchDataDefinitionExt dc)
    {
      wod.UpdatedTs = DateTime.Now;
      wod.CommStatus = CommStatus.New.Value;
      wod.CommMessage = null;
      wod.ValidationCheck = null;

      //wod.WorkOrderName = dc.WorkOrderName?.Trim();
      //wod.ExternalWorkOrderName = dc.ExternalWorkOrderName?.Trim();
      //wod.PreviousWorkOrderName = dc.PreviousWorkOrderName?.Trim();
      //wod.OrderDeadline = dc.OrderDeadline?.Trim();
      //wod.HeatName = dc.HeatName?.Trim();
      //wod.BilletWeight = dc.BilletWeight?.Trim();
      //wod.NumberOfBillets = dc.NumberOfBillets?.Trim();
      //wod.CustomerName = dc.CustomerName?.Trim();
      //wod.BundleWeightMin = dc.BundleWeightMin?.Trim();
      //wod.BundleWeightMax = dc.BundleWeightMax?.Trim();
      //wod.TargetWorkOrderWeight = dc.TargetWorkOrderWeight?.Trim();
      //wod.TargetWorkOrderWeightMin = dc.TargetWorkOrderWeightMin?.Trim();
      //wod.TargetWorkOrderWeightMax = dc.TargetWorkOrderWeightMax?.Trim();
      //wod.MaterialCatalogueName = dc.MaterialCatalogue?.Trim();
      //wod.ProductCatalogueName = dc.ProductCatalogue?.Trim();
      //wod.SteelgradeCode = dc.SteelgradeCode?.Trim();
      //wod.InputThickness = dc.InputThickness?.Trim();
      //wod.InputWidth = dc.InputWidth?.Trim();
      //wod.BilletLength = dc.BilletLength?.Trim();
      //wod.InputShapeSymbol = dc.InputShapeSymbol?.Trim();
      //wod.OutputThickness = dc.OutputThickness?.Trim();
      //wod.OutputWidth = dc.OutputWidth?.Trim();
      //wod.OutputShapeSymbol = dc.OutputShapeSymbol?.Trim();
    }

    //AV@
    public Task<L3L2WorkOrderDefinition> GetWorkOrderDefinitionByIdAsyncEXT(TransferContext ctx, long counter)
    {
      return ctx.L3L2WorkOrderDefinitions.Where(x => x.CounterId == counter).SingleOrDefaultAsync();
    }

    //AV@


    public void UpdateWorkOrderDefinitionEXT(L3L2WorkOrderDefinition wod, DCL3L2WorkOrderDefinitionExtMOD dc)
    {
      wod.UpdatedTs = DateTime.Now;
      wod.CommStatus = CommStatus.New.Value;
      wod.CommMessage = null;
      wod.ValidationCheck = null;

      wod.WorkOrderName = dc.WorkOrderName?.Trim();
      wod.ExternalWorkOrderName = dc.ExternalWorkOrderName?.Trim();
      wod.PreviousWorkOrderName = dc.PreviousWorkOrderName?.Trim();
      wod.OrderDeadline = dc.OrderDeadline?.Trim();
      wod.HeatName = dc.HeatName?.Trim();
      wod.BilletWeight = dc.BilletWeight?.Trim();
      wod.NumberOfBillets = dc.NumberOfBillets?.Trim();
      wod.CustomerName = dc.CustomerName?.Trim();
      wod.BundleWeightMin = dc.BundleWeightMin?.Trim();
      wod.BundleWeightMax = dc.BundleWeightMax?.Trim();
      wod.TargetWorkOrderWeight = dc.TargetWorkOrderWeight?.Trim();
      wod.TargetWorkOrderWeightMin = dc.TargetWorkOrderWeightMin?.Trim();
      wod.TargetWorkOrderWeightMax = dc.TargetWorkOrderWeightMax?.Trim();
      wod.MaterialCatalogueName = dc.MaterialCatalogue?.Trim();
      wod.ProductCatalogueName = dc.ProductCatalogue?.Trim();
      wod.SteelgradeCode = dc.SteelgradeCode?.Trim();
      wod.InputThickness = dc.InputThickness?.Trim();
      wod.InputWidth = dc.InputWidth?.Trim();
      wod.BilletLength = dc.BilletLength?.Trim();
      wod.InputShapeSymbol = dc.InputShapeSymbol?.Trim();
      wod.OutputThickness = dc.OutputThickness?.Trim();
      wod.OutputWidth = dc.OutputWidth?.Trim();
      wod.OutputShapeSymbol = dc.OutputShapeSymbol?.Trim();
    }

    //public Task<L2L3WorkOrderReport> GetWorkOrderReportByIdAsync(TransferContext ctx, long counter)
    //{
    //  return ctx.L2L3WorkOrderReports.Where(x => x.Counter == counter).SingleOrDefaultAsync();
    //}

    //public Task<L2L3ProductReport> GetProductReportByIdAsync(TransferContext ctx, long counter)
    //{
    //  return ctx.L2L3ProductReports.SingleOrDefaultAsync(x => x.Counter == counter);
    //}

    //public L2L3WorkOrderReport CreateWorkOrderReport(TransferContext ctx, DCL2L3WorkOrderReport message)
    //{
    //  if (ctx == null) { ctx = new TransferContext(); }
    //  DateTime now = DateTime.Now;

    //  L2L3WorkOrderReport workOrderReport = new L2L3WorkOrderReport
    //  {
    //    CreatedTs = now,
    //    UpdatedTs = now,
    //    CommStatus = CommStatus.New,
    //    WorkOrderName = (message.WorkOrderName).Truncate(50),
    //    IsWorkOrderFinished = message.IsWorkOrderFinished ? "1" : "0",
    //    MaterialCatalogueName = message.MaterialName.ToString().Truncate(50),
    //    InputWidth = message.InputWidth.ToString().Truncate(10),
    //    InputThickness = message.InputThickness.ToString().Truncate(10),
    //    ProductCatalogueName = message.ProductName.Truncate(50),
    //    OutputWidth = message.OutputWidth.ToString().Truncate(10),
    //    OutputThickness = message.OutputThickness.ToString().Truncate(10),
    //    HeatName = message.HeatName.Truncate(50),
    //    SteelgradeCode = message.SteelgradeCode.Truncate(50),
    //    TargetWorkOrderWeight = message.TargetWorkOrderWeight.ToString().Truncate(10),
    //    NumberOfProducts = message.ProductsNumber.ToString().Truncate(10),
    //    TotalWeightOfProducts = message.TotalProductsWeight.ToString().Truncate(10),
    //    TotalWeightOfMaterials = message.TotalRawMaterialWeight.ToString(),
    //    NumberOfPlannedMaterials = message.RawMaterialsPlanned.ToString().Truncate(10),
    //    NumberOfChargedMaterials = message.RawMaterialsCharged.ToString().Truncate(10),
    //    NumberOfRolledMaterials = message.RawMaterialsRolled.ToString().Truncate(10),
    //    NumberOfScrappedMaterials = message.RawMaterialsScrapped.ToString().Truncate(10),
    //    NumberOfRejectedMaterials = message.RawMaterialsRejected.ToString().Truncate(10),
    //    NumberOfPiecesRejectedInLocation1 = message.NumberOfPiecesRejectedInLocation1.ToString().Truncate(10),
    //    WeightOfPiecesRejectedInLocation1 = message.WeightOfPiecesRejectedInLocation1.ToString().Truncate(10),
    //    WorkOrderStart = message.WorkOrderStartTs.HasValue ? message.WorkOrderStartTs.Value.ToString("yyyyMMddHHmmss") : now.ToString("yyyyMMddHHmmss"),
    //    WorkOrderEnd = message.WorkOrderEndTs.HasValue ? message.WorkOrderEndTs.Value.ToString("yyyyMMddHHmmss") : now.ToString("yyyyMMddHHmmss"),
    //    DelayDuration = message.DelayDuration.ToString().Truncate(10),
    //    ShiftName = message.ShiftName.Truncate(10),
    //    CrewName = message.CrewName.Truncate(50),
    //  };

    //  return workOrderReport;
    //}

    //public L2L3ProductReport CreateCoilReport(TransferContext ctx, DCL2L3ProductReport message)
    //{
    //  if (ctx == null)
    //    ctx = new TransferContext();
    //  var now = DateTime.Now;

    //  var coilReport = new L2L3ProductReport
    //  {
    //    CreatedTs = now,
    //    UpdatedTs = now,
    //    CommStatus = CommStatus.New,
    //    ShiftName = message.ShiftName.Truncate(10),
    //    WorkOrderName = (message.WorkOrderName).Truncate(50),
    //    SteelgradeCode = message.SteelgradeCode.Truncate(50),
    //    HeatName = message.HeatName.Truncate(50),
    //    SequenceInWorkOrder = message.SequenceInWorkOrder.ToString().Truncate(5),
    //    ProductName = message.ProductName.Truncate(50),
    //    ProductType = message.ProductType.Truncate(1),
    //    OutputWeight = message.OutputWeight.ToString().Truncate(10),
    //    OutputWidth = message.OutputWidth.ToString().Truncate(10),
    //    OutputThickness = message.OutputThickness.ToString().Truncate(10),
    //    OutputPieces = message.OutputPieces.ToString().Truncate(3),
    //    InspectionResult = message.InspectionResult.ToString().Truncate(1),
    //  };

    //  return coilReport;
    //}

    //public void AddWorkOrderReportToTransferTable(DCL2L3WorkOrderReport dc, TransferContext ctx,
    //  L2L3WorkOrderReport workOrderReport)
    //{
    //  if (ctx.L2L3WorkOrderReports.Any(x => x.WorkOrderName.Equals(dc.WorkOrderName)))
    //  {
    //    throw new InvalidOperationException($"WorkOrder {dc.WorkOrderName} already exists in work order report tranfer table.");
    //  }

    //  ctx.L2L3WorkOrderReports.Add(workOrderReport);
    //}

    //public void AddCoilReportToTransferTable(DCL2L3ProductReport dc, TransferContext ctx,
    //  L2L3ProductReport coilReport)
    //{
    //  if (ctx.L2L3ProductReports.Any(x => x.ProductName.Equals(dc.ProductName)))
    //  {
    //    NotificationController.Info($"Product {dc.ProductName} already exists in product report transfer table. Sending another one.");
    //    //Do not prevent multiple reports for single product [MB]
    //    //throw new InvalidOperationException($"Product {dc.ProductName} already exists in coil report transfer table.");
    //  }

    //  ctx.L2L3ProductReports.Add(coilReport);
    //}
  }
}
