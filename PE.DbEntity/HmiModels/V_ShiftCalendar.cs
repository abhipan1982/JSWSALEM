using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ShiftCalendar
    {
        public long OrderSeq { get; set; }
        public long ShiftCalendarId { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        public bool IsActualShift { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PlannedEndTime { get; set; }
        public long ShiftDefinitionId { get; set; }
        public long CrewId { get; set; }
        public bool IsActive { get; set; }
        public int? ShiftDuration { get; set; }
        public int? ActiveShiftDuration { get; set; }
    }
}
