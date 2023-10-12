using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class QTYDefectCategoryGroup
    {
        public QTYDefectCategoryGroup()
        {
            QTYDefectCatalogueCategories = new HashSet<QTYDefectCatalogueCategory>();
        }

        [Key]
        public long DefectCategoryGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string DefectCategoryGroupName { get; set; }
        [StringLength(2000)]
        public string DefectCategoryGroupDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(QTYDefectCatalogueCategory.FKDefectCategoryGroup))]
        public virtual ICollection<QTYDefectCatalogueCategory> QTYDefectCatalogueCategories { get; set; }
    }
}
