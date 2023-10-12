using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactEvent
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactEventIsDeleted { get; set; }
        public long FactEventRow { get; set; }
        [MaxLength(16)]
        public byte[] FactEventHash { get; set; }
        public long FactEventKey { get; set; }
        public long? FactEventKeyParent { get; set; }
        public long DimEventTypeKey { get; set; }
        public long DimRootEventTypeKey { get; set; }
        public long? DimEventCatalogueKey { get; set; }
        public int DimYearKey { get; set; }
        public int DimDateKey { get; set; }
        public long? DimShiftKey { get; set; }
        public long? DimWorkOrderKey { get; set; }
        public long? DimAssetKey { get; set; }
        public long? DimRawMaterialKey { get; set; }
        public long? DimMaterialKey { get; set; }
        [StringLength(450)]
        public string DimUserKey { get; set; }
        [Required]
        [StringLength(76)]
        public string EventDayShiftCrew { get; set; }
        [StringLength(10)]
        public string EventShiftCode { get; set; }
        public bool EventIsDelay { get; set; }
        public bool EventIsPlanned { get; set; }
        public bool EventIsOpen { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(10)]
        public string EventCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        public double? ScrapPercent { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        public double? MaterialWeight { get; set; }
        public double? ProductWeight { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public int? AssetCode { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        public short RootEventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string RootEventTypeName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EventDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEnd { get; set; }
        public double? EventStdTime { get; set; }
        [StringLength(200)]
        public string EventUserComment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventUserUpdated { get; set; }
        public int EventDuration { get; set; }
        [Required]
        [StringLength(30)]
        public string EventDurationFD { get; set; }
    }
}
