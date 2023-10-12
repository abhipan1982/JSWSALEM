using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimDate
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimDateRow { get; set; }
        public bool DimDateIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimDateHash { get; set; }
        public int DimDateKey { get; set; }
        public long DimCalendarKey { get; set; }
        public int DimYearKey { get; set; }
        public int DateHalfOfYear { get; set; }
        public int DateQuarter { get; set; }
        public int DateMonth { get; set; }
        public int DateWeek { get; set; }
        public int DateWeekISO { get; set; }
        public int DateDayOfYear { get; set; }
        public int DateDayOfMonth { get; set; }
        public int DateDayOfWeek { get; set; }
        public bool DateIsWeekend { get; set; }
        [Required]
        [StringLength(30)]
        public string DateMonthName { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateWeekDayName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateFullDateTime { get; set; }
        [Precision(3)]
        public DateTime DateFullDateTime2 { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateFullDate { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateANSI { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateUS { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateUK { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateDE { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string DateIT { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string DateISO { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateFirstOfYear { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateLastOfYear { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateFirstOfMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateLastOfMonth { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string DateYearMonth { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string DateYearWeek { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string DateYearQuarter { get; set; }
    }
}
