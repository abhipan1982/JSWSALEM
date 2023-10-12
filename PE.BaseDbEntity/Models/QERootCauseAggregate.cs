using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QERootCauseAggregate")]
    public partial class QERootCauseAggregate
    {
        [Key]
        public long RootCauseAggregateId { get; set; }
        public long FKRootCauseId { get; set; }
        public long FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKRootCauseId))]
        [InverseProperty(nameof(QERootCause.QERootCauseAggregates))]
        public virtual QERootCause FKRootCause { get; set; }
    }
}
