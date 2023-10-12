using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimProduct
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimProductRow { get; set; }
        public bool DimProductIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimProductHash { get; set; }
        public long DimProductKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long? DimHeatKey { get; set; }
        public long DimProductCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductCreated { get; set; }
        public bool ProductIsAssignedWithRawMaterial { get; set; }
        public int? RawMaterialsAssigned { get; set; }
    }
}
