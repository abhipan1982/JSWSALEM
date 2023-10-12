using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTMemberRole
    {
        public MNTMemberRole()
        {
            MNTMembers = new HashSet<MNTMember>();
        }

        [Key]
        public long MemberRoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string MemberRoleName { get; set; }
        [StringLength(100)]
        public string MemberRoleDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(MNTMember.FKMemberRole))]
        public virtual ICollection<MNTMember> MNTMembers { get; set; }
    }
}
