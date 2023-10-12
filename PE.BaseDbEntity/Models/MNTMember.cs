using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTMember
    {
        public MNTMember()
        {
            MNTCrewMembers = new HashSet<MNTCrewMember>();
        }

        [Key]
        public long MemberId { get; set; }
        [Required]
        [StringLength(50)]
        public string MemberName { get; set; }
        public long FKMemberRoleId { get; set; }
        [StringLength(450)]
        public string FKUserId { get; set; }
        public double CostPerHour { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMemberRoleId))]
        [InverseProperty(nameof(MNTMemberRole.MNTMembers))]
        public virtual MNTMemberRole FKMemberRole { get; set; }
        [InverseProperty(nameof(MNTCrewMember.FKMember))]
        public virtual ICollection<MNTCrewMember> MNTCrewMembers { get; set; }
    }
}
