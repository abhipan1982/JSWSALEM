using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimRawMaterial
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimRawMaterialKey { get; set; }
        public short DimRawMaterialStatusKey { get; set; }
        public long? DimParentRawMaterialKey { get; set; }
        public long? DimMaterialKey { get; set; }
        public long? DimProductKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long? DimHeatKey { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public DateTime? RawMaterialCreated { get; set; }
        public DateTime? RawMaterialUpdated { get; set; }
        public short LastProcessingStepNo { get; set; }
        public short CuttingSeqNo { get; set; }
        public short ChildsNo { get; set; }
        public bool IsDummy { get; set; }
        public bool IsTransferred2DW { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialWeight { get; set; }
    }
}
