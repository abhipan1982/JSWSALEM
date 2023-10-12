using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrderSummary
    {
        public long OrderSeq { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public bool IsTestOrder { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsSentToL3 { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3Ts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        public double TargetOrderWeight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEndTs { get; set; }
        public long MaterialCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string MaterialShapeName { get; set; }
        public long ProductCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
        [StringLength(50)]
        public string ProductShapeName { get; set; }
        public double ProductThickness { get; set; }
        public double? ProductWidth { get; set; }
        public long SteelgradeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? HeatWeight { get; set; }
        public long? ScheduleId { get; set; }
        public short? ScheduleOrderSeq { get; set; }
        public long? PlannedDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
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
        public int MaterialsRejectedOnMill { get; set; }
        public double MaterialsRejectedWeight { get; set; }
        public double MaterialsRejectedWeightBeforeFurnace { get; set; }
        public double MaterialsRejectedWeightAfterFurnace { get; set; }
        public double MaterialsRejectedWeightOnMill { get; set; }
        public int MaterialsScrapped { get; set; }
        public double MaterialsScrappedWeight { get; set; }
        public double MaterialsLossWeight { get; set; }
        public int? MaterialsPlannedVSCharged { get; set; }
        public double WorkOrderCompletion { get; set; }
        public double MetallicYield { get; set; }
        public int MaterialsAfterOnCoolingBed { get; set; }
        public bool? AllRawMaterialsWithStatusHigher80 { get; set; }
    }
}
