using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimTime
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimTimeRow { get; set; }
        public bool DimTimeIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimTimeHash { get; set; }
        public long DimTimeKey { get; set; }
        public int TimeHour { get; set; }
        public int TimeMinute { get; set; }
        public int TimeSecond { get; set; }
        public int TimeHalfHour { get; set; }
        public int TimeQuarterHour { get; set; }
        public int TimeSecondOfDay { get; set; }
        public int TimeMinuteOfDay { get; set; }
        public int TimeHalfHourOfDay { get; set; }
        public int TimeQuarterHourOfDay { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string TimeString { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string TimeString12 { get; set; }
        public int TimeHour12 { get; set; }
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string TimeAmPm { get; set; }
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string TimeHourCode { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string TimeMinuteCode { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan TimeTime { get; set; }
    }
}
