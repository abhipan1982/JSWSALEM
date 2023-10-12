using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    public partial class MNTMaintenanceAction
    {
        public long MaintenanceActionId { get; set; }
        public long FKMaintenanceScheduleId { get; set; }
        public long FKMemberId { get; set; }
        public short ActionStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string ActionName { get; set; }
        [StringLength(100)]
        public string ActionDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMaintenanceScheduleId))]
        public virtual MNTMaintenanceSchedule FKMaintenanceSchedule { get; set; }
        [ForeignKey(nameof(FKMemberId))]
        public virtual MNTMember FKMember { get; set; }
    }
}
