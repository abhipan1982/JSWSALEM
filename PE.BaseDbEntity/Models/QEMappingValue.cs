using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QEMappingValue")]
    public partial class QEMappingValue
    {
        public QEMappingValue()
        {
            QELengthSeriesValues = new HashSet<QELengthSeriesValue>();
            QETimeSeriesValues = new HashSet<QETimeSeriesValue>();
        }

        [Key]
        public long MappingValueId { get; set; }
        public long FKRuleMappingValueId { get; set; }
        public long FKMappingEntryId { get; set; }
        public long? FKRatingId { get; set; }
        public double? NumValue { get; set; }
        [StringLength(400)]
        public string TextValue { get; set; }
        public bool? BooleanValue { get; set; }
        public long? TimeStampValue { get; set; }
        public string RulesObjectValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMappingEntryId))]
        [InverseProperty(nameof(QEMappingEntry.QEMappingValues))]
        public virtual QEMappingEntry FKMappingEntry { get; set; }
        [ForeignKey(nameof(FKRatingId))]
        [InverseProperty(nameof(QERating.QEMappingValues))]
        public virtual QERating FKRating { get; set; }
        [ForeignKey(nameof(FKRuleMappingValueId))]
        [InverseProperty(nameof(QERuleMappingValue.QEMappingValues))]
        public virtual QERuleMappingValue FKRuleMappingValue { get; set; }
        [InverseProperty(nameof(QELengthSeriesValue.FKMappingValue))]
        public virtual ICollection<QELengthSeriesValue> QELengthSeriesValues { get; set; }
        [InverseProperty(nameof(QETimeSeriesValue.FKMappingValue))]
        public virtual ICollection<QETimeSeriesValue> QETimeSeriesValues { get; set; }
    }
}
