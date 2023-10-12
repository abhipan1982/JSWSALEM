using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(EventTypeCode), Name = "UQ_EventTypeCode", IsUnique = true)]
    public partial class EVTEventType
    {
        public EVTEventType()
        {
            EVTEventCatalogueCategories = new HashSet<EVTEventCatalogueCategory>();
            EVTEvents = new HashSet<EVTEvent>();
            InverseFKParentEvenType = new HashSet<EVTEventType>();
        }

        [Key]
        public long EventTypeId { get; set; }
        public long? FKParentEvenTypeId { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        [StringLength(100)]
        public string EventTypeDescription { get; set; }
        public bool IsVisibleOnHMI { get; set; }
        [StringLength(50)]
        public string HMIIcon { get; set; }
        [StringLength(100)]
        public string HMIColor { get; set; }
        [StringLength(255)]
        public string HMILink { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKParentEvenTypeId))]
        [InverseProperty(nameof(EVTEventType.InverseFKParentEvenType))]
        public virtual EVTEventType FKParentEvenType { get; set; }
        [InverseProperty(nameof(EVTEventCatalogueCategory.FKEventType))]
        public virtual ICollection<EVTEventCatalogueCategory> EVTEventCatalogueCategories { get; set; }
        [InverseProperty(nameof(EVTEvent.FKEventType))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(EVTEventType.FKParentEvenType))]
        public virtual ICollection<EVTEventType> InverseFKParentEvenType { get; set; }
    }
}
