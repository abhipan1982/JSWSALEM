using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DelayOverview
    {
        public long OrderSeq { get; set; }
        [Required]
        [StringLength(76)]
        public string DelayHeader { get; set; }
        public long EventId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateDay { get; set; }
        public long? ShiftCalendarId { get; set; }
        public int? ShiftIdDelayStart { get; set; }
        public int? ShiftIdDelayEnd { get; set; }
        [StringLength(10)]
        public string ShiftCode { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(10)]
        public string EventCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string EventCatalogueCategoryName { get; set; }
        public double StdEventTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEndTs { get; set; }
        public int? DelayDuration { get; set; }
        [Required]
        [StringLength(66)]
        [Unicode(false)]
        public string DelayDurationFD { get; set; }
        public bool IsPlanned { get; set; }
        public int IsOpen { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public long? AssetId { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string UserComment { get; set; }
    }
}
