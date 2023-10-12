using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKWorkOrderId), nameof(FKSetupId), Name = "NCI_WorkOrderId_SetupId")]
    public partial class STPSetupWorkOrder
    {
        [Key]
        public long SetupWorkOrderId { get; set; }
        public long FKWorkOrderId { get; set; }
        public long FKSetupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CalculatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SentTs { get; set; }
        public bool IsAmbiguous { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKSetupId))]
        [InverseProperty(nameof(STPSetup.STPSetupWorkOrders))]
        public virtual STPSetup FKSetup { get; set; }
        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.STPSetupWorkOrders))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
    }
}
