using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimShift
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimShiftRow { get; set; }
        public bool DimShiftIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimShiftHash { get; set; }
        public long DimShiftKey { get; set; }
        public long DimCalendarKey { get; set; }
        public int? DimDateKey { get; set; }
        public long DimCrewKey { get; set; }
        public long DimShiftDefinitionKey { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        [Required]
        [StringLength(21)]
        public string ShiftDateWithCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShiftStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShiftEndTime { get; set; }
        public int? ShiftDurationH { get; set; }
        public int? ShiftDurationM { get; set; }
        public int? ShiftDurationS { get; set; }
        public bool ShiftEndsNextDay { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [StringLength(100)]
        public string CrewDescription { get; set; }
    }
}
