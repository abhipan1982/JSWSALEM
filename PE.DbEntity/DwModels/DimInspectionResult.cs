using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimInspectionResult
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimInspectionResultRow { get; set; }
        public bool DimInspectionResultIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimInspectionResultHash { get; set; }
        public long DimInspectionResultKey { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string InspectionResult { get; set; }
    }
}
