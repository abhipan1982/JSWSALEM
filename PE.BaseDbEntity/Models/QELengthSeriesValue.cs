using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QELengthSeriesValue")]
    public partial class QELengthSeriesValue
    {
        [Key]
        public long LengthSeriesValueId { get; set; }
        public long FKMappingValueId { get; set; }
        public double LengthPosition { get; set; }
        public double LengthSeriesValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMappingValueId))]
        [InverseProperty(nameof(QEMappingValue.QELengthSeriesValues))]
        public virtual QEMappingValue FKMappingValue { get; set; }
    }
}
