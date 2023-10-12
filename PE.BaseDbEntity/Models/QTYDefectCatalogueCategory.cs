using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QTYDefectCatalogueCategory")]
    [Index(nameof(DefectCatalogueCategoryCode), Name = "UQ_DefectCatalogueCategoryCode", IsUnique = true)]
    public partial class QTYDefectCatalogueCategory
    {
        public QTYDefectCatalogueCategory()
        {
            QTYDefectCatalogues = new HashSet<QTYDefectCatalogue>();
        }

        [Key]
        public long DefectCatalogueCategoryId { get; set; }
        public long? FKDefectCategoryGroupId { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string DefectCatalogueCategoryName { get; set; }
        [StringLength(200)]
        public string DefectCatalogueCategoryDescription { get; set; }
        public AssignmentType EnumAssignmentType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKDefectCategoryGroupId))]
        [InverseProperty(nameof(QTYDefectCategoryGroup.QTYDefectCatalogueCategories))]
        public virtual QTYDefectCategoryGroup FKDefectCategoryGroup { get; set; }
        [InverseProperty(nameof(QTYDefectCatalogue.FKDefectCatalogueCategory))]
        public virtual ICollection<QTYDefectCatalogue> QTYDefectCatalogues { get; set; }
    }
}
