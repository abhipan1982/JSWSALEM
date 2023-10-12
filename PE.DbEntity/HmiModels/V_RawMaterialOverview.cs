using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialOverview
    {
        public long OrderSeq { get; set; }
        public long RawMaterialId { get; set; }
        [StringLength(50)]
        public string DisplayedMaterialName { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RawMaterialCreatedTs { get; set; }
        public bool RawMaterialIsDummy { get; set; }
        public bool RawMaterialIsVirtual { get; set; }
        public short RawMaterialCutNo { get; set; }
        public short RawMaterialChildNo { get; set; }
        public long? MaterialId { get; set; }
        public long? ProductId { get; set; }
        public int IsLayer { get; set; }
        public long? ParentLayerId { get; set; }
        public short SlittingFactor { get; set; }
        public double? ScrapPercent { get; set; }
        [StringLength(200)]
        public string ScrapRemarks { get; set; }
        public short EnumRawMaterialType { get; set; }
        public short EnumTypeOfScrap { get; set; }
        public short EnumRejectLocation { get; set; }
        public short EnumRawMaterialStatus { get; set; }
        public short OutputPieces { get; set; }
        public double? WeighingStationWeight { get; set; }
        public double? RawMaterialLastWeight { get; set; }
        public double? RawMaterialLastLength { get; set; }
        public bool IsScrap { get; set; }
        public bool IsMaterialAssigned { get; set; }
        public bool IsProductAssigned { get; set; }
        [StringLength(50)]
        public string ParentRawMaterialName { get; set; }
        public short? ParentRawMaterialCutNo { get; set; }
        public short? ParentRawMaterialChildNo { get; set; }
        public bool IsAssetExit { get; set; }
        public long? AssetId { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        public short? MaterialSeqNo { get; set; }
        public double? MaterialWeight { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        [StringLength(10)]
        public string SteelGroupCode { get; set; }
        [StringLength(50)]
        public string SteelGroupName { get; set; }
        [Required]
        [StringLength(21)]
        public string ShiftKey { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        public double? ProductsWeight { get; set; }
        public int DefectsNumber { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        [StringLength(400)]
        public string VisualInspection { get; set; }
        public short EnumCrashTest { get; set; }
        public short EnumInspectionResult { get; set; }
        public bool? HasDefects { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChargeTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DischargeTime { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InspectionResult { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RawMaterialStatus { get; set; }
    }
}
