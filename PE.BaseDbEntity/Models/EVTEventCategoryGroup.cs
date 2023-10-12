using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class EVTEventCategoryGroup
    {
        public EVTEventCategoryGroup()
        {
            EVTEventCatalogueCategories = new HashSet<EVTEventCatalogueCategory>();
        }

        [Key]
        public long EventCategoryGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string EventCategoryGroupName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(EVTEventCatalogueCategory.FKEventCategoryGroup))]
        public virtual ICollection<EVTEventCatalogueCategory> EVTEventCatalogueCategories { get; set; }
    }
}
