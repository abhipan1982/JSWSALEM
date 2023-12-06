using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_UnitOfMeasure
    {
        public long? SeqNo { get; set; }
        public long UOMCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMCategoryName { get; set; }
        public long UOMId { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMName { get; set; }
        public long UOMSIId { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSISymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSIName { get; set; }
        public double UOMFactor { get; set; }
    }
}
