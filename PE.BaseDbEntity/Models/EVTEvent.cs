using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKWorkOrderId), Name = "NCI_FKWorkOrderId")]
    public partial class EVTEvent
    {
        public EVTEvent()
        {
            InverseFKParentEvent = new HashSet<EVTEvent>();
        }

        [Key]
        public long EventId { get; set; }
        public long FKEventTypeId { get; set; }
        public long? FKEventCatalogueId { get; set; }
        public long? FKShiftCalendarId { get; set; }
        public long? FKWorkOrderId { get; set; }
        public long? FKRawMaterialId { get; set; }
        public long? FKAssetId { get; set; }
        public long? FKParentEventId { get; set; }
        [StringLength(450)]
        public string FKUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UserCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UserUpdatedTs { get; set; }
        [StringLength(200)]
        public string UserComment { get; set; }
        public bool IsPlanned { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.EVTEvents))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKEventCatalogueId))]
        [InverseProperty(nameof(EVTEventCatalogue.EVTEvents))]
        public virtual EVTEventCatalogue FKEventCatalogue { get; set; }
        [ForeignKey(nameof(FKEventTypeId))]
        [InverseProperty(nameof(EVTEventType.EVTEvents))]
        public virtual EVTEventType FKEventType { get; set; }
        [ForeignKey(nameof(FKParentEventId))]
        [InverseProperty(nameof(EVTEvent.InverseFKParentEvent))]
        public virtual EVTEvent FKParentEvent { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.EVTEvents))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
        [ForeignKey(nameof(FKShiftCalendarId))]
        [InverseProperty(nameof(EVTShiftCalendar.EVTEvents))]
        public virtual EVTShiftCalendar FKShiftCalendar { get; set; }
        [ForeignKey(nameof(FKWorkOrderId))]
        [InverseProperty(nameof(PRMWorkOrder.EVTEvents))]
        public virtual PRMWorkOrder FKWorkOrder { get; set; }
        [InverseProperty(nameof(EVTEvent.FKParentEvent))]
        public virtual ICollection<EVTEvent> InverseFKParentEvent { get; set; }
    }
}
