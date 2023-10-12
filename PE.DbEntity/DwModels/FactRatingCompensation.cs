using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactRatingCompensation
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactRatingCompensationIsDeleted { get; set; }
        public long FactRatingCompensationRow { get; set; }
        [MaxLength(16)]
        public byte[] FactRatingCompensationHash { get; set; }
        public long FactRatingCompensationKey { get; set; }
        public long FactRatingKey { get; set; }
        public long? DimMaterialKey { get; set; }
        [StringLength(400)]
        public string CompensationName { get; set; }
        [Required]
        [StringLength(400)]
        public string CompensationTypeName { get; set; }
        public double? CompensationAlternative { get; set; }
        [StringLength(400)]
        public string CompensationInfo { get; set; }
        [StringLength(400)]
        public string CompensationDetail { get; set; }
        public bool CompensationIsChosen { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompensationChosen { get; set; }
        public string CompensationAggregates { get; set; }
    }
}
