using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimTimeM
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long? DimTimeMinKey { get; set; }
        public int? TimeHours { get; set; }
        public int? TimeMinutes { get; set; }
        public int? TimeHalfHour { get; set; }
        public int? TimeQuarterHour { get; set; }
        public int? TimeMinuteOfDay { get; set; }
        public int? TimeHalfHourOfDay { get; set; }
        public int? TimeQuarterHourOfDay { get; set; }
        [StringLength(8)]
        public string TimeString { get; set; }
        [StringLength(8)]
        public string TimeString12 { get; set; }
        public int? TimeHours12 { get; set; }
        [Required]
        [StringLength(2)]
        public string TimeAmPm { get; set; }
        [StringLength(2)]
        public string TimeHourCode { get; set; }
        [StringLength(5)]
        public string TimeMinuteCode { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? TimeTime { get; set; }
    }
}
