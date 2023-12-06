using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_FactDelay
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int? DimYearKey { get; set; }
        public long? DimDateKey { get; set; }
        public int? DimShiftKey { get; set; }
        public long DimDelayKey { get; set; }
        public int? DimShiftKeyStart { get; set; }
        public int? DimShiftKeyEnd { get; set; }
        public long? DimDelayCatalogueKey { get; set; }
        public long? DimDelayCatalogueCategoryKey { get; set; }
        public long? DimRawMaterialKey { get; set; }
        [StringLength(128)]
        public string DimUserKey { get; set; }
        public DateTime? DelayStart { get; set; }
        public DateTime? DelayEnd { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? DelayDurationD { get; set; }
        [Column(TypeName = "numeric(21, 8)")]
        public decimal? DelayDurationH { get; set; }
        [Column(TypeName = "numeric(24, 8)")]
        public decimal? DelayDurationM { get; set; }
        [Column(TypeName = "numeric(27, 8)")]
        public decimal? DelayDuration { get; set; }
        [Required]
        [StringLength(86)]
        public string DelayDurationFD { get; set; }
        [StringLength(200)]
        public string UserComment { get; set; }
        public bool IsPlanned { get; set; }
        public bool IsOpen { get; set; }
    }
}
