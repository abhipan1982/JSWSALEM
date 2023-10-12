using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QERootCause")]
    public partial class QERootCause
    {
        public QERootCause()
        {
            QERootCauseAggregates = new HashSet<QERootCauseAggregate>();
        }

        [Key]
        public long RootCauseId { get; set; }
        public long FKRatingId { get; set; }
        [Required]
        [StringLength(400)]
        public string RootCauseName { get; set; }
        public int? RootCauseType { get; set; }
        public double? RootCausePriority { get; set; }
        [StringLength(400)]
        public string RootCauseInfo { get; set; }
        [StringLength(400)]
        public string RootCauseVerification { get; set; }
        [StringLength(400)]
        public string RootCauseCorrection { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKRatingId))]
        [InverseProperty(nameof(QERating.QERootCauses))]
        public virtual QERating FKRating { get; set; }
        [InverseProperty(nameof(QERootCauseAggregate.FKRootCause))]
        public virtual ICollection<QERootCauseAggregate> QERootCauseAggregates { get; set; }
    }
}
