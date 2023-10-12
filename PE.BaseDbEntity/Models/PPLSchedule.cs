using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKWorkOrderId), Name = "UQ_WorkOrderId", IsUnique = true)]
    public partial class PPLSchedule
    {
        [Key]
        public long ScheduleId { get; set; }
        public long FKWorkOrderId { get; set; }
        public short OrderSeq { get; set; }
        public long PlannedDuration { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedEndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.PPLSchedule))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
    }
}
