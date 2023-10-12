using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactWorkOrder
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactWorkOrderIsDeleted { get; set; }
        public long FactWorkOrderRow { get; set; }
        [MaxLength(16)]
        public byte[] FactWorkOrderHash { get; set; }
        public long FactWorkOrderKey { get; set; }
        public long? FactWorkOrderKeyParent { get; set; }
        public int DimYearKey { get; set; }
        public int DimDateKey { get; set; }
        [StringLength(4000)]
        public string DimYear { get; set; }
        [StringLength(4000)]
        public string DimMonth { get; set; }
        [Required]
        [StringLength(4000)]
        public string DimWeek { get; set; }
        [StringLength(4000)]
        public string DimDate { get; set; }
        [StringLength(10)]
        public string DimShiftCode { get; set; }
        [StringLength(51)]
        public string DimShiftKey { get; set; }
        [StringLength(50)]
        public string DimCrewName { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string DimMaterialCatalogueName { get; set; }
        public long DimProductCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string DimProductCatalogueName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string DimProductThickness { get; set; }
        public long DimSteelgradeKey { get; set; }
        [StringLength(50)]
        public string DimSteelgradeName { get; set; }
        public long? DimSteelGroupKey { get; set; }
        public long? DimScrapGroupKey { get; set; }
        public long? DimHeatKey { get; set; }
        [StringLength(50)]
        public string DimHeatName { get; set; }
        public long? DimCustomerKey { get; set; }
        public short DimWorkOrderStatusKey { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEnd { get; set; }
        [StringLength(50)]
        public string WorkOrderExternalName { get; set; }
        public long WorkOrderDuration { get; set; }
        [Required]
        [StringLength(30)]
        public string WorkOrderDurationFT { get; set; }
        public bool WorkOrderIsTest { get; set; }
        public double WorkOrderTargetWeight { get; set; }
        public double? WorkOrderTargetMinWeight { get; set; }
        public double? WorkOrderTargetMaxWeight { get; set; }
        public int? WorkOrderMaterialNumber { get; set; }
        public double? WorkOrderMaterialWeight { get; set; }
        public int WorkOrderProductNumber { get; set; }
        public double WorkOrderProductWeight { get; set; }
        public double WorkOrderProductWeightBad { get; set; }
        public int WorkOrderRawMaterialNumber { get; set; }
        public double WorkOrderRawMaterialWeight { get; set; }
        public double WorkOrderRejectedWeight { get; set; }
        public int WorkOrderRejectedNumber { get; set; }
        public double WorkOrderScrappedWeight { get; set; }
        public int WorkOrderScrappedNumber { get; set; }
        public double? MinFurnaceExitTemperature { get; set; }
        public double? MaxFurnaceExitTemperature { get; set; }
        public double? AvgFurnaceExitTemperature { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? MinSpeed { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? MaxSpeed { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? AvgSpeed { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? MinRollGap { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? MaxRollGap { get; set; }
        [Column(TypeName = "numeric(1, 1)")]
        public decimal? AvgRollGap { get; set; }
        public long WorkOrderDelayDuration { get; set; }
        public long? WorkOrderRollingDuration { get; set; }
        public double? WorkOrderMetallicYield { get; set; }
        public double? WorkOrderQualityYield { get; set; }
        public double? WorkOrderProductionPlanVariance { get; set; }
        public double? WorkOrderTestTimePercentageOfTotal { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkOrderStatus { get; set; }
    }
}
