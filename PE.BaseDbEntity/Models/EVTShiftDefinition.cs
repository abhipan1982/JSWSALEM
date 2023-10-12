using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ShiftCode), Name = "UQ_ShiftCode", IsUnique = true)]
    public partial class EVTShiftDefinition
    {
        public EVTShiftDefinition()
        {
            EVTShiftCalendars = new HashSet<EVTShiftCalendar>();
            EVTShiftCrewPatterns = new HashSet<EVTShiftCrewPattern>();
        }

        [Key]
        public long ShiftDefinitionId { get; set; }
        public long FKShiftLayoutId { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        public TimeSpan DefaultStartTime { get; set; }
        public TimeSpan DefaultEndTime { get; set; }
        public bool ShiftStartsPreviousDay { get; set; }
        public bool ShiftEndsNextDay { get; set; }
        public long? NextShiftDefinitionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKShiftLayoutId))]
        [InverseProperty(nameof(EVTShiftLayout.EVTShiftDefinitions))]
        public virtual EVTShiftLayout FKShiftLayout { get; set; }
        [InverseProperty(nameof(EVTShiftCalendar.FKShiftDefinition))]
        public virtual ICollection<EVTShiftCalendar> EVTShiftCalendars { get; set; }
        [InverseProperty(nameof(EVTShiftCrewPattern.FKShiftDefinition))]
        public virtual ICollection<EVTShiftCrewPattern> EVTShiftCrewPatterns { get; set; }
    }
}
