using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimYear
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimYearRow { get; set; }
        public bool DimYearIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimYearHash { get; set; }
        public int DimYearKey { get; set; }
        public int? Year { get; set; }
    }
}
