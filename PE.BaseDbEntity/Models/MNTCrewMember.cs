using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTCrewMember
    {
        [Key]
        public long CrewMemberId { get; set; }
        public long FKCrewId { get; set; }
        public long FKMemberId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCrewId))]
        [InverseProperty(nameof(EVTCrew.MNTCrewMembers))]
        public virtual EVTCrew FKCrew { get; set; }
        [ForeignKey(nameof(FKMemberId))]
        [InverseProperty(nameof(MNTMember.MNTCrewMembers))]
        public virtual MNTMember FKMember { get; set; }
    }
}
