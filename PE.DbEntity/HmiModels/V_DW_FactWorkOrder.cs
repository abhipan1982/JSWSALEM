using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_FactWorkOrder
    {
        public DateTime? FactLoadTs { get; set; }
        [StringLength(50)]
        public string DataSource { get; set; }
        public int? DimYearKey { get; set; }
        public long? DimDateKey { get; set; }
        public int? DimShiftKey { get; set; }
        public long DimWorkOrderKey { get; set; }
        public int? DimShiftKeyStart { get; set; }
        public int? DimShiftKeyEnd { get; set; }
        public long? DimSteelGroupKey { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long DimHeatKey { get; set; }
        public long DimMaterialCatalogueTypeKey { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        public long DimMaterialShapeKey { get; set; }
        public long DimProductCatalogueTypeKey { get; set; }
        public long DimProductCatalogueKey { get; set; }
        public long DimProductShapeKey { get; set; }
        public long? DimCustomerKey { get; set; }
        public short DimWorkOrderStatusKey { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3Ts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProductionStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProductionEnd { get; set; }
        public double? WorkOrderDurationD { get; set; }
        public double? WorkOrderDurationH { get; set; }
        public double? WorkOrderDurationM { get; set; }
        public double? WorkOrderDurationS { get; set; }
        public bool IsTestOrder { get; set; }
        public int IsScheduledNow { get; set; }
        public int IsOnOneShift { get; set; }
        public double TargetWeight { get; set; }
        public double? TargetMinWeight { get; set; }
        public double? TargetMaxWeight { get; set; }
        public int? MaterialsCount { get; set; }
        public double? MaterialsWeight { get; set; }
        public int? ProductsCount { get; set; }
        public double? ProductsWeight { get; set; }
        public int Completion { get; set; }
        public int MetallicYield { get; set; }
        public int QualityYield { get; set; }
        [StringLength(50)]
        public string NextAggregate { get; set; }
        [StringLength(50)]
        public string OperationCode { get; set; }
        [StringLength(50)]
        public string QualityPolicy { get; set; }
        [StringLength(50)]
        public string ExtraLabelInformation { get; set; }
        [StringLength(50)]
        public string ExternalSteelgradeCode { get; set; }
        public long? PlannedDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PlannedEndTime { get; set; }
    }
}
