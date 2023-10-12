using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QECompensation")]
    public partial class QECompensation
    {
        public QECompensation()
        {
            QECompensationAggregates = new HashSet<QECompensationAggregate>();
        }

        [Key]
        public long CompensationId { get; set; }
        public long FKRatingId { get; set; }
        public long FKCompensationTypeId { get; set; }
        [StringLength(400)]
        public string CompensationName { get; set; }
        public double? CompensationAlternative { get; set; }
        [StringLength(400)]
        public string CompensationInfo { get; set; }
        [StringLength(400)]
        public string CompensationDetail { get; set; }
        public bool IsChosen { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChosenTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCompensationTypeId))]
        [InverseProperty(nameof(QECompensationType.QECompensations))]
        public virtual QECompensationType FKCompensationType { get; set; }
        [ForeignKey(nameof(FKRatingId))]
        [InverseProperty(nameof(QERating.QECompensations))]
        public virtual QERating FKRating { get; set; }
        [InverseProperty(nameof(QECompensationAggregate.FKCompensation))]
        public virtual ICollection<QECompensationAggregate> QECompensationAggregates { get; set; }
    }
}
