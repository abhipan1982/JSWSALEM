using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactRatingRootCause
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactRatingRootCauseIsDeleted { get; set; }
        public long FactRatingRootCauseRow { get; set; }
        [MaxLength(16)]
        public byte[] FactRatingRootCauseHash { get; set; }
        public long FactRatingRootCauseKey { get; set; }
        public long FactRatingKey { get; set; }
        public long? DimMaterialKey { get; set; }
        [Required]
        [StringLength(400)]
        public string RootCauseName { get; set; }
        public int? RootCauseType { get; set; }
        public double? RootCausePriority { get; set; }
        [StringLength(400)]
        public string RootCauseInfo { get; set; }
        [StringLength(400)]
        public string RootCauseCorrection { get; set; }
        [StringLength(400)]
        public string RootCauseVerification { get; set; }
        public string RootCauseAggregates { get; set; }
    }
}
