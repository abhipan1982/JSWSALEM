using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(CrewName), Name = "UQ_Crews_Name", IsUnique = true)]
    public partial class EVTCrew
    {
        public EVTCrew()
        {
            EVTShiftCalendars = new HashSet<EVTShiftCalendar>();
            EVTShiftCrewPatterns = new HashSet<EVTShiftCrewPattern>();
            InverseNextCrew = new HashSet<EVTCrew>();
            MNTCrewMembers = new HashSet<MNTCrewMember>();
        }

        [Key]
        public long CrewId { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [StringLength(100)]
        public string CrewDescription { get; set; }
        [StringLength(100)]
        public string LeaderName { get; set; }
        public short? DfltCrewSize { get; set; }
        public short? OrderSeq { get; set; }
        public long? NextCrewId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(NextCrewId))]
        [InverseProperty(nameof(EVTCrew.InverseNextCrew))]
        public virtual EVTCrew NextCrew { get; set; }
        [InverseProperty(nameof(EVTShiftCalendar.FKCrew))]
        public virtual ICollection<EVTShiftCalendar> EVTShiftCalendars { get; set; }
        [InverseProperty(nameof(EVTShiftCrewPattern.FKCrew))]
        public virtual ICollection<EVTShiftCrewPattern> EVTShiftCrewPatterns { get; set; }
        [InverseProperty(nameof(EVTCrew.NextCrew))]
        public virtual ICollection<EVTCrew> InverseNextCrew { get; set; }
        [InverseProperty(nameof(MNTCrewMember.FKCrew))]
        public virtual ICollection<MNTCrewMember> MNTCrewMembers { get; set; }
    }
}
