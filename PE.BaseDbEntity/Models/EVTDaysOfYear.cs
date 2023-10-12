using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("EVTDaysOfYear")]
    [Index(nameof(DateDay), Name = "UQ_DateDay", IsUnique = true)]
    public partial class EVTDaysOfYear
    {
        public EVTDaysOfYear()
        {
            EVTShiftCalendars = new HashSet<EVTShiftCalendar>();
        }

        [Key]
        public long DaysOfYearId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateDay { get; set; }
        public long FKShiftLayoutId { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? WeekNo { get; set; }
        public bool IsWeekend { get; set; }
        [StringLength(30)]
        public string MonthName { get; set; }
        [StringLength(30)]
        public string WeekDayName { get; set; }
        public int HalfOfYear { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateANSI { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateUS { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateUK { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateDE { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateIT { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string DateISO { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FirstOfMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastOfMonth { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FirstOfYear { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastOfYear { get; set; }
        public int? ISOWeekNumber { get; set; }
        public int? WeekNumber { get; set; }
        public int? YearNumber { get; set; }
        public int? MonthNumber { get; set; }
        public int? DayYearNumber { get; set; }
        public int? DayNumber { get; set; }
        public int? WeekDayNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKShiftLayoutId))]
        [InverseProperty(nameof(EVTShiftLayout.EVTDaysOfYears))]
        public virtual EVTShiftLayout FKShiftLayout { get; set; }
        [InverseProperty(nameof(EVTShiftCalendar.FKDaysOfYear))]
        public virtual ICollection<EVTShiftCalendar> EVTShiftCalendars { get; set; }
    }
}
