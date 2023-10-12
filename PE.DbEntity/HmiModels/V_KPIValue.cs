using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_KPIValue
    {
        public long KPIValueId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime KPITime { get; set; }
        public long KPIDefinitionId { get; set; }
        public double KPIValue { get; set; }
        public long? WorkOrderId { get; set; }
        [Required]
        [StringLength(10)]
        public string KPICode { get; set; }
        [Required]
        [StringLength(50)]
        public string KPIName { get; set; }
        public double MinValue { get; set; }
        public double AlarmTo { get; set; }
        public double WarningTo { get; set; }
        public double MaxValue { get; set; }
        public long UnitId { get; set; }
        public short EnumGaugeDirection { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
    }
}
