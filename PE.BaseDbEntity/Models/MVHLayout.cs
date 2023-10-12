using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MVHLayout
    {
        public MVHLayout()
        {
            MVHAssetLayouts = new HashSet<MVHAssetLayout>();
        }

        [Key]
        public long LayoutId { get; set; }
        [Required]
        [StringLength(50)]
        public string LayoutName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(MVHAssetLayout.FKLayout))]
        public virtual ICollection<MVHAssetLayout> MVHAssetLayouts { get; set; }
    }
}
