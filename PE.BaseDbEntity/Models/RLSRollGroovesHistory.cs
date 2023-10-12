using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("RLSRollGroovesHistory")]
    public partial class RLSRollGroovesHistory
    {
        [Key]
        public long RollGrooveHistoryId { get; set; }
        public long FKRollId { get; set; }
        public long FKGrooveTemplateId { get; set; }
        public long? FKRollSetHistoryId { get; set; }
        public short GrooveNumber { get; set; }
        public RollGrooveStatus EnumRollGrooveStatus { get; set; }
        public GrooveCondition EnumGrooveCondition { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeactivatedTs { get; set; }
        public double AccWeight { get; set; }
        public double? AccWeightWithCoeff { get; set; }
        public long AccBilletCnt { get; set; }
        public double? ActDiameter { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKGrooveTemplateId))]
        [InverseProperty(nameof(RLSGrooveTemplate.RLSRollGroovesHistories))]
        public virtual RLSGrooveTemplate FKGrooveTemplate { get; set; }
        [ForeignKey(nameof(FKRollId))]
        [InverseProperty(nameof(RLSRoll.RLSRollGroovesHistories))]
        public virtual RLSRoll FKRoll { get; set; }
        [ForeignKey(nameof(FKRollSetHistoryId))]
        [InverseProperty(nameof(RLSRollSetHistory.RLSRollGroovesHistories))]
        public virtual RLSRollSetHistory FKRollSetHistory { get; set; }
    }
}
