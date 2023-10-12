using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTMaintenanceSchedule
    {
        [Key]
        public long MaintenanceScheduleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedEndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        public short MaintenenaceScheduleStatus { get; set; }
        [StringLength(50)]
        public string MaintenanceScheduleName { get; set; }
        [StringLength(100)]
        public string MaintenanceScheduleDescription { get; set; }
        public long FKEquipmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKEquipmentId))]
        [InverseProperty(nameof(MNTEquipment.MNTMaintenanceSchedules))]
        public virtual MNTEquipment FKEquipment { get; set; }
    }
}
