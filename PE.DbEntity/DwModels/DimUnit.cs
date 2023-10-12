using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimUnit
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimUnitRow { get; set; }
        public bool DimUnitIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimUnitHash { get; set; }
        public long DimUnitKey { get; set; }
        public long DimUnitCategoryKey { get; set; }
        public bool UnitIsSI { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCategory { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitName { get; set; }
        public double UnitFactor { get; set; }
        public double UnitShift { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSISymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSIName { get; set; }
    }
}
