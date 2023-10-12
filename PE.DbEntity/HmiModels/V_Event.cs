using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Event
    {
        public long OrderSeq { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateDay { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public int? Month { get; set; }
        public int? DayNumber { get; set; }
        public int? WeekDayNumber { get; set; }
        public int? WeekNumber { get; set; }
        public long EventId { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEndTs { get; set; }
        public long? ParentEventId { get; set; }
        public long? ShiftCalendarId { get; set; }
        public long? WorkOrderId { get; set; }
        public long? RawMaterialId { get; set; }
        public long? AssetId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(200)]
        public string UserComment { get; set; }
        public bool EventIsDelay { get; set; }
        public bool EventIsPlanned { get; set; }
        public bool EventIsOpen { get; set; }
        public long EventTypeId { get; set; }
        [StringLength(100)]
        public string EventTypeDescription { get; set; }
        public long? ParentEventTypeId { get; set; }
        [StringLength(255)]
        public string HMILink { get; set; }
        [StringLength(100)]
        public string HMIColor { get; set; }
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(100)]
        public string EventCatalogueDescription { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
    }
}
