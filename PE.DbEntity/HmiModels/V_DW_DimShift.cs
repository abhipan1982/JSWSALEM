using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimShift
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimShiftKey { get; set; }
        public long? DimDateKey { get; set; }
        public long DimCrewKey { get; set; }
        [Required]
        [StringLength(21)]
        public string ShiftKey { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        public DateTime? ShiftStartTime { get; set; }
        public DateTime? ShiftEndTime { get; set; }
        public double? ShiftDurationH { get; set; }
        public double? ShiftDurationM { get; set; }
        public double? ShiftDurationS { get; set; }
        public bool ShiftEndsNextDay { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [StringLength(100)]
        public string CrewDescription { get; set; }
    }
}
