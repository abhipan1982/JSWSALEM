using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimHour
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimHourRow { get; set; }
        public bool DimHourIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimHourHash { get; set; }
        public short DimHourKey { get; set; }
        public int Hour24 { get; set; }
        public int Hour12 { get; set; }
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string HourAmPm { get; set; }
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string HourCode { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan HourTime { get; set; }
    }
}
