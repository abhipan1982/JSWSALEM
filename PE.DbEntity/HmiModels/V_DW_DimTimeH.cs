using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimTimeH
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public short? DimTimeHKey { get; set; }
        public int? TimeHours { get; set; }
        public int? TimeHours12 { get; set; }
        [Required]
        [StringLength(2)]
        public string TimeAmPm { get; set; }
        [StringLength(2)]
        public string TimeHourCode { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? TimeTime { get; set; }
    }
}
