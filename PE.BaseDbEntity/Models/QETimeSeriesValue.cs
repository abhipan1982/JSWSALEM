using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QETimeSeriesValue")]
    public partial class QETimeSeriesValue
    {
        [Key]
        public long TimeSeriesValueId { get; set; }
        public long FKMappingValueId { get; set; }
        public long TimePosition { get; set; }
        public double TimeSeriesValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMappingValueId))]
        [InverseProperty(nameof(QEMappingValue.QETimeSeriesValues))]
        public virtual QEMappingValue FKMappingValue { get; set; }
    }
}
