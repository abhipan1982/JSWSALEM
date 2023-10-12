using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_TimeLine
    {
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Evt { get; set; }
        [Required]
        [StringLength(50)]
        public string EvtName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public int? WorkOrderDuration { get; set; }
        public int? DelayDuration { get; set; }
        public int? WorkOrderLag { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(30)]
        public string EvtGroup { get; set; }
        public double? LastWeight { get; set; }
        public int? TotalEvtDuration { get; set; }
        public int? TotalEvtGroupDuration { get; set; }
        public long? OrderSeq { get; set; }
    }
}
