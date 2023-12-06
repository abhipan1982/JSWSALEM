using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_FactRawMaterial
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? DimYearKey { get; set; }
        public long? DimDateKey { get; set; }
        public long DimShiftKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long DimRawMaterialKey { get; set; }
        public long? DimMaterialKey { get; set; }
        public long? DimProductKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        public long? DimSteelGroupKey { get; set; }
        public long? DimHeatKey { get; set; }
        public short DimRawMaterialStatusKey { get; set; }
        public long? DimParentRawMaterialKey { get; set; }
        public short? DimParentRawMaterialStatusKey { get; set; }
        public long DimLastAssetKey { get; set; }
        public long DimCrewKey { get; set; }
        public long? DimMaterialCatalogueKey { get; set; }
        public long? DimProductCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public DateTime? RawMaterialCreated { get; set; }
        public DateTime? RawMaterialUpdated { get; set; }
        public short LastProcessingStepNo { get; set; }
        public short CuttingSeqNo { get; set; }
        public short ChildsNo { get; set; }
        public bool IsDummy { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsMaterialAssigned { get; set; }
        public bool IsProductAssigned { get; set; }
        public bool IsTransferred2DW { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
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
        [Required]
        [StringLength(21)]
        public string ShiftKey { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(50)]
        public string ParentRawMaterialName { get; set; }
        public short? ParentCuttingSeqNo { get; set; }
        public short? ParentChildsNo { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? RawMaterialDeclaredLength { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? RawMaterialLastWeight { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? RawMaterialLastLength { get; set; }
    }
}
