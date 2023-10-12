using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("STPSetupSent")]
    public partial class STPSetupSent
    {
        [Key]
        public long SetupSentId { get; set; }
        public long FKSetupId { get; set; }
        public long FKWorkOrderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SentTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKSetupId))]
        [InverseProperty(nameof(STPSetup.STPSetupSents))]
        public virtual STPSetup FKSetup { get; set; }
        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.STPSetupSents))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
    }
}
