using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(Issue), Name = "UQ_Issue", IsUnique = true)]
    public partial class STPIssue
    {
        public STPIssue()
        {
            STPProductLayouts = new HashSet<STPProductLayout>();
        }

        [Key]
        public short IssueId { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Issue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(STPProductLayout.FKIssue))]
        public virtual ICollection<STPProductLayout> STPProductLayouts { get; set; }
    }
}
