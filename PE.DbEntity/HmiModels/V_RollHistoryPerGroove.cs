using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RollHistoryPerGroove
    {
        public long? OrderSeq { get; set; }
        public long RollId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollName { get; set; }
        public long RollGrooveHistoryId { get; set; }
        public double? ActDiameter { get; set; }
        public short GrooveNumber { get; set; }
        public double AccWeight { get; set; }
        public long AccBilletCnt { get; set; }
        public double? AccWeightWithCoeff { get; set; }
        public short EnumRollGrooveStatus { get; set; }
        public short EnumGrooveCondition { get; set; }
        [StringLength(255)]
        public string GrooveRemarks { get; set; }
        public long RollSetId { get; set; }
        public long RollSetHistoryId { get; set; }
        public double? AccWeightLimit { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollSetMountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollSetDismountedTs { get; set; }
        [Required]
        [StringLength(50)]
        public string GrooveTemplateName { get; set; }
    }
}
