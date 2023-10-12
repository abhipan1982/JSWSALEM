using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("EVTEventCatalogueCategory")]
    [Index(nameof(EventCatalogueCategoryCode), Name = "UQ_DelayCatalogueCategoryCode", IsUnique = true)]
    public partial class EVTEventCatalogueCategory
    {
        public EVTEventCatalogueCategory()
        {
            EVTEventCatalogues = new HashSet<EVTEventCatalogue>();
        }

        [Key]
        public long EventCatalogueCategoryId { get; set; }
        public long? FKEventCategoryGroupId { get; set; }
        public long FKEventTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCategoryCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        [StringLength(100)]
        public string EventCatalogueCategoryDescription { get; set; }
        public bool IsDefault { get; set; }
        public AssignmentType EnumAssignmentType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKEventCategoryGroupId))]
        [InverseProperty(nameof(EVTEventCategoryGroup.EVTEventCatalogueCategories))]
        public virtual EVTEventCategoryGroup FKEventCategoryGroup { get; set; }
        [ForeignKey(nameof(FKEventTypeId))]
        [InverseProperty(nameof(EVTEventType.EVTEventCatalogueCategories))]
        public virtual EVTEventType FKEventType { get; set; }
        [InverseProperty(nameof(EVTEventCatalogue.FKEventCatalogueCategory))]
        public virtual ICollection<EVTEventCatalogue> EVTEventCatalogues { get; set; }
    }
}
