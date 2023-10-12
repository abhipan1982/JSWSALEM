using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("EVTEventCatalogue")]
    [Index(nameof(FKEventCatalogueCategoryId), Name = "NCI_DelayCatalogueCategoryId")]
    [Index(nameof(FKParentEventCatalogueId), Name = "NCI_ParentDelayCatalogueId")]
    [Index(nameof(EventCatalogueCode), Name = "UQ_DelayCatalogueCode", IsUnique = true)]
    public partial class EVTEventCatalogue
    {
        public EVTEventCatalogue()
        {
            EVTEvents = new HashSet<EVTEvent>();
            InverseFKParentEventCatalogue = new HashSet<EVTEventCatalogue>();
        }

        [Key]
        public long EventCatalogueId { get; set; }
        public long? FKParentEventCatalogueId { get; set; }
        public long FKEventCatalogueCategoryId { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(100)]
        public string EventCatalogueDescription { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        public double StdEventTime { get; set; }
        public bool IsPlanned { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKEventCatalogueCategoryId))]
        [InverseProperty(nameof(EVTEventCatalogueCategory.EVTEventCatalogues))]
        public virtual EVTEventCatalogueCategory FKEventCatalogueCategory { get; set; }
        [ForeignKey(nameof(FKParentEventCatalogueId))]
        [InverseProperty(nameof(EVTEventCatalogue.InverseFKParentEventCatalogue))]
        public virtual EVTEventCatalogue FKParentEventCatalogue { get; set; }
        [InverseProperty(nameof(EVTEvent.FKEventCatalogue))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(EVTEventCatalogue.FKParentEventCatalogue))]
        public virtual ICollection<EVTEventCatalogue> InverseFKParentEventCatalogue { get; set; }
    }
}
