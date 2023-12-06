using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimDate
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long? DimDateKey { get; set; }
        public int? DimYearKey { get; set; }
        public int? DimHalfOfYearKey { get; set; }
        public int? DimQuarterKey { get; set; }
        public int? DimMonthKey { get; set; }
        public int? DimWeekKey { get; set; }
        public int? DimWeekISOKey { get; set; }
        public int? DateDayOfYear { get; set; }
        public int? DimDayOfMonthKey { get; set; }
        public int? DimDayOfWeekKey { get; set; }
        public long? DimCalendarKey { get; set; }
        public bool? IsWeekend { get; set; }
        [StringLength(30)]
        public string MonthName { get; set; }
        [StringLength(10)]
        public string WeekDayName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FullDateTime { get; set; }
        public DateTime? FullDateTime2 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FullDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateDay { get; set; }
        [StringLength(10)]
        public string DateANSI { get; set; }
        [StringLength(10)]
        public string DateUS { get; set; }
        [StringLength(10)]
        public string DateUK { get; set; }
        [StringLength(10)]
        public string DateDE { get; set; }
        [StringLength(10)]
        public string DateIT { get; set; }
        [StringLength(8)]
        public string DateISO { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateFirstOfYear { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateLastOfYear { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateFirstOfMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateLastOfMonth { get; set; }
        [StringLength(7)]
        public string YearMonth { get; set; }
        [StringLength(8)]
        public string YearWeek { get; set; }
        [StringLength(7)]
        public string YearQuarter { get; set; }
    }
}
