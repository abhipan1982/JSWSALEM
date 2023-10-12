using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.DbEntity.SPModels
{
  public class SPWorkOrderSummary
  {
    public long OrderSeq { get; set; }
    public long WorkOrderId { get; set; }
    public string WorkOrderName { get; set; }
    public bool IsTestOrder { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsSentToL3 { get; set; }
    public short EnumWorkOrderStatus { get; set; }
    public DateTime WorkOrderCreatedInL3Ts { get; set; }
    public DateTime WorkOrderCreatedTs { get; set; }
    public DateTime ToBeCompletedBeforeTs { get; set; }
    public double TargetOrderWeight { get; set; }
    public DateTime? WorkOrderStartTs { get; set; }
    public DateTime? WorkOrderEndTs { get; set; }
    public long MaterialCatalogueId { get; set; }
    public string MaterialCatalogueName { get; set; }
    public string MaterialShapeName { get; set; }
    public long ProductCatalogueId { get; set; }
    public string ProductCatalogueName { get; set; }
    public string ProductCatalogueTypeCode { get; set; }
    public string ProductShapeName { get; set; }
    public double ProductThickness { get; set; }
    public double? ProductWidth { get; set; }
    public long SteelgradeId { get; set; }
    public string SteelgradeCode { get; set; }
    public string SteelgradeName { get; set; }
    public long? HeatId { get; set; }
    public string HeatName { get; set; }
    public double? HeatWeight { get; set; }
    public long? ScheduleId { get; set; }
    public short? ScheduleOrderSeq { get; set; }
    public long? PlannedDuration { get; set; }
    public DateTime? PlannedStartTime { get; set; }
    public DateTime? PlannedEndTime { get; set; }
    public int IsProducedNow { get; set; }
    public int IsScheduled { get; set; }
    public short MaterialsPlanned { get; set; }
    public int MaterialsNumber { get; set; }
    public double MaterialsWeight { get; set; }
    public int RawMaterialsNumber { get; set; }
    public double RawMaterialsWeight { get; set; }
    public int ProductsNumber { get; set; }
    public double ProductsWeight { get; set; }
    public int MaterialsCharged { get; set; }
    public int MaterialsDischarged { get; set; }
    public int MaterialsRolled { get; set; }
    public int MaterialsRejected { get; set; }
    public int MaterialsRejectedBeforeFurnace { get; set; }
    public int MaterialsRejectedAfterFurnace { get; set; }
    public double MaterialsRejectedWeight { get; set; }
    public double MaterialsRejectedWeightBeforeFurnace { get; set; }
    public double MaterialsRejectedWeightAfterFurnace { get; set; }
    public int MaterialsScrapped { get; set; }
    public double MaterialsScrappedWeight { get; set; }
    public double MaterialsLossWeight { get; set; }
    public int? MaterialsPlannedVSCharged { get; set; }
    public double WorkOrderCompletion { get; set; }
    public double MetallicYield { get; set; }
  }
}
