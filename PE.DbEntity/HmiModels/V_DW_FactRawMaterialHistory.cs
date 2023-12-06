using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_FactRawMaterialHistory
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? DimYearKey { get; set; }
        public long? DimDateKey { get; set; }
        public long DimShiftKey { get; set; }
        public long DimRawMaterialStepKey { get; set; }
        public long DimAssetKey { get; set; }
        public long DimRawMaterialKey { get; set; }
        public short DimRawMaterialStatusKey { get; set; }
        public short? DimParentRawMaterialStatusKey { get; set; }
        public short ProcessingStepNo { get; set; }
        public DateTime? RawMaterialStepCreated { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short PassNo { get; set; }
        public bool IsLastPass { get; set; }
        public bool IsReversed { get; set; }
        public double? RawMaterialLength { get; set; }
        public double? RawMaterialDeclaredLength { get; set; }
        public double? RawMaterialLastLength { get; set; }
        public short CuttingSeqNo { get; set; }
        public short ChildsNo { get; set; }
        public short TypeOfCut { get; set; }
        public double? HeadPartLength { get; set; }
        public double? TailPartLength { get; set; }
        public double? HeadPartCumm { get; set; }
        public double? TailPartCumm { get; set; }
        public double? Elongation { get; set; }
        public double? MotherOffset { get; set; }
        public double? RelLength { get; set; }
        public long? DimParentRawMaterialKey { get; set; }
        [StringLength(50)]
        public string ParentRawMaterialName { get; set; }
        public short? ParentCuttingSeqNo { get; set; }
        public short? ParentChildsNo { get; set; }
        public bool IsTransferred2DW { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
    }
}
