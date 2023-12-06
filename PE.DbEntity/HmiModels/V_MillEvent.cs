using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MillEvent
    {
        public long EventId { get; set; }
        public short EventTypeCode { get; set; }
        [StringLength(100)]
        public string EventTypeDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEnd { get; set; }
        public long? ShiftCalendarId { get; set; }
        public long? WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public short? AssetCode { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(200)]
        public string UserComment { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        public int? Duration { get; set; }
    }
}
