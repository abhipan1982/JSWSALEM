using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("EVTShiftCalendar")]
    [Index(nameof(FKCrewId), Name = "NCI_CrewId")]
    [Index(nameof(FKShiftDefinitionId), Name = "NCI_ShiftDefinitionId")]
    [Index(nameof(PlannedEndTime), Name = "UQ_PlannedEndTime", IsUnique = true)]
    [Index(nameof(PlannedStartTime), Name = "UQ_PlannedStartTime", IsUnique = true)]
    public partial class EVTShiftCalendar
    {
        public EVTShiftCalendar()
        {
            EVTEvents = new HashSet<EVTEvent>();
            PRFKPIValues = new HashSet<PRFKPIValue>();
            TRKRawMaterials = new HashSet<TRKRawMaterial>();
        }

        [Key]
        public long ShiftCalendarId { get; set; }
        public long FKDaysOfYearId { get; set; }
        public long FKShiftDefinitionId { get; set; }
        public long FKCrewId { get; set; }
        public bool IsActualShift { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedEndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCrewId))]
        [InverseProperty(nameof(EVTCrew.EVTShiftCalendars))]
        public virtual EVTCrew FKCrew { get; set; }
        [ForeignKey(nameof(FKDaysOfYearId))]
        [InverseProperty(nameof(EVTDaysOfYear.EVTShiftCalendars))]
        public virtual EVTDaysOfYear FKDaysOfYear { get; set; }
        [ForeignKey(nameof(FKShiftDefinitionId))]
        [InverseProperty(nameof(EVTShiftDefinition.EVTShiftCalendars))]
        public virtual EVTShiftDefinition FKShiftDefinition { get; set; }
        [InverseProperty(nameof(EVTEvent.FKShiftCalendar))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(PRFKPIValue.FKShiftCalendar))]
        public virtual ICollection<PRFKPIValue> PRFKPIValues { get; set; }
        [InverseProperty(nameof(TRKRawMaterial.FKShiftCalendar))]
        public virtual ICollection<TRKRawMaterial> TRKRawMaterials { get; set; }
    }
}
