using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(Layout), Name = "UQ_Layout", IsUnique = true)]
    public partial class STPLayout
    {
        public STPLayout()
        {
            STPProductLayouts = new HashSet<STPProductLayout>();
        }

        [Key]
        public short LayoutId { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Layout { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(STPProductLayout.FKLayout))]
        public virtual ICollection<STPProductLayout> STPProductLayouts { get; set; }
    }
}
