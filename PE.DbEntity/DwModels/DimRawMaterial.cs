using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimRawMaterial
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimRawMaterialRow { get; set; }
        public bool DimRawMaterialIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimRawMaterialHash { get; set; }
        public long DimRawMaterialKey { get; set; }
        public long DimMaterialKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long DimHeatKey { get; set; }
        public long? DimMaterialCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public short MaterialSeqNo { get; set; }
        public short RawMaterialCuttingSeqNo { get; set; }
        public double? RawMaterialWeight { get; set; }
        public double? RawMaterialLength { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RawMaterialCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialProductionStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialProductionEnd { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialRollingStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialRollingEnd { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RawMaterialStatus { get; set; }
    }
}
