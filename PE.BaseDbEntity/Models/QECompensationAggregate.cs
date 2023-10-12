using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QECompensationAggregate")]
    public partial class QECompensationAggregate
    {
        [Key]
        public long CompensationAggregateId { get; set; }
        public long FKCompensationId { get; set; }
        public long FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCompensationId))]
        [InverseProperty(nameof(QECompensation.QECompensationAggregates))]
        public virtual QECompensation FKCompensation { get; set; }
    }
}
