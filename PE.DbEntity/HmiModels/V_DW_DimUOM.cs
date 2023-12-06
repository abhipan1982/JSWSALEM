using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimUOM
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimUOMKey { get; set; }
        public long DimUOMSIKey { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMCategory { get; set; }
        public long UOMCategoryKey { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMName { get; set; }
        public double UOMFactor { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSymbolSI { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMNameSI { get; set; }
    }
}
