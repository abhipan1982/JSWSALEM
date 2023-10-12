using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactMaterial
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactMaterialIsDeleted { get; set; }
        public long FactMaterialRow { get; set; }
        [MaxLength(16)]
        public byte[] FactMaterialHash { get; set; }
        public long FactMaterialKey { get; set; }
        public long? FactRawMaterialKey { get; set; }
        public int DimYearKey { get; set; }
        public int DimDateKey { get; set; }
        public long? DimShiftKey { get; set; }
        public long? DimShiftDefinitionKey { get; set; }
        public long? DimCrewKey { get; set; }
        public long DimMaterialKey { get; set; }
        public long? DimRawMaterialKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long DimHeatKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        public long? DimSteelGroupKey { get; set; }
        public short? DimMaterialStatusKey { get; set; }
        public long? DimCustomerKey { get; set; }
        public long? DimMaterialCatalogueKey { get; set; }
        public long? DimProductCatalogueKey { get; set; }
        public long? DimAssetKeyLastOn { get; set; }
        public long? DimScrapGroupKey { get; set; }
        public short? DimTypeOfScrapKey { get; set; }
        public short? DimInspectionResultKey { get; set; }
        public long? DimAssetKeyMaterialScrap { get; set; }
        public long? DimDefectCatalogueKeyLastOne { get; set; }
        public long? DimProductKey { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short MaterialSeqNo { get; set; }
        public short? MaterialCuttingSeq { get; set; }
        public short? MaterialChildSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreated { get; set; }
        public double MaterialWeight { get; set; }
        public double MaterialThickness { get; set; }
        public double? MaterialWidth { get; set; }
        public double? MaterialLength { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        public bool MaterialIsTest { get; set; }
        public bool? MaterialIsVirtual { get; set; }
        public bool MaterialIsReject { get; set; }
        public bool MaterialIsScrap { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialHeatName { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(10)]
        public string SteelGroupCode { get; set; }
        [StringLength(50)]
        public string SteelGroupName { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public double? MaterialLastWeight { get; set; }
        public double? MaterialLastLength { get; set; }
        public double? MaterialLastTemperature { get; set; }
        public double? MaterialLastGrading { get; set; }
        public short? MaterialSlittingFactor { get; set; }
        public double MaterialScrapPercent { get; set; }
        public double MaterialScrapWeight { get; set; }
        public double MaterialRejectWeight { get; set; }
        [StringLength(200)]
        public string MaterialScrapRemarks { get; set; }
        public double? FurnaceExitTemperature { get; set; }
        public int? FurnaceHeatingDuration { get; set; }
        public int MaterialDefectsNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MaterialInspectionResult { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MaterialStatus { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MaterialChargeType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkOrderStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProductCreated { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public double ProductWeightBad { get; set; }
        public long MaterialDelayDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialProductionStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialProductionEnd { get; set; }
        public long? MaterialProductionDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialProductionStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialProductionEnd { get; set; }
        public long? RawMaterialProductionDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialRollingStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialRollingEnd { get; set; }
        public long? MaterialRollingDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialCharged { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialDischarged { get; set; }
        [Required]
        [StringLength(21)]
        public string ShiftDateWithCode { get; set; }
        [StringLength(50)]
        public string CrewName { get; set; }
    }
}
