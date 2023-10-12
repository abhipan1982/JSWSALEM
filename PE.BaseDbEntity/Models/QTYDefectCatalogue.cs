using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QTYDefectCatalogue")]
    [Index(nameof(FKDefectCatalogueCategoryId), Name = "NCI_DefectCatalogueCategoryId")]
    [Index(nameof(FKParentDefectCatalogueId), Name = "NCI_ParentDefectCatalogueId")]
    [Index(nameof(DefectCatalogueCode), Name = "UQ_DefectCatalogueCode", IsUnique = true)]
    public partial class QTYDefectCatalogue
    {
        public QTYDefectCatalogue()
        {
            InverseFKParentDefectCatalogue = new HashSet<QTYDefectCatalogue>();
            QTYDefects = new HashSet<QTYDefect>();
        }

        [Key]
        public long DefectCatalogueId { get; set; }
        public long FKDefectCatalogueCategoryId { get; set; }
        public long? FKParentDefectCatalogueId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DefectCatalogueName { get; set; }
        [StringLength(200)]
        public string DefectCatalogueDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKDefectCatalogueCategoryId))]
        [InverseProperty(nameof(QTYDefectCatalogueCategory.QTYDefectCatalogues))]
        public virtual QTYDefectCatalogueCategory FKDefectCatalogueCategory { get; set; }
        [ForeignKey(nameof(FKParentDefectCatalogueId))]
        [InverseProperty(nameof(QTYDefectCatalogue.InverseFKParentDefectCatalogue))]
        public virtual QTYDefectCatalogue FKParentDefectCatalogue { get; set; }
        [InverseProperty(nameof(QTYDefectCatalogue.FKParentDefectCatalogue))]
        public virtual ICollection<QTYDefectCatalogue> InverseFKParentDefectCatalogue { get; set; }
        [InverseProperty(nameof(QTYDefect.FKDefectCatalogue))]
        public virtual ICollection<QTYDefect> QTYDefects { get; set; }
    }
}
