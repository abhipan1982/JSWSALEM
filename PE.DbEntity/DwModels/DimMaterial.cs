using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimMaterial
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimMaterialRow { get; set; }
        public bool DimMaterialIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimMaterialHash { get; set; }
        public long DimMaterialKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        public long DimHeatKey { get; set; }
        public long? DimMaterialCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public short MaterialSeqNo { get; set; }
        public double MaterialWeight { get; set; }
        public double? MaterialLength { get; set; }
        public double MaterialThickness { get; set; }
        public double? MaterialWidth { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialProductionStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialProductionEnd { get; set; }
        public bool MaterialIsAssignedWithRawMaterial { get; set; }
        public int RawMaterialsAssigned { get; set; }
    }
}
