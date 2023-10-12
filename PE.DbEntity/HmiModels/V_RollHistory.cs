using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RollHistory
    {
        public long RollId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollName { get; set; }
        public double ActualDiameter { get; set; }
        public double InitialDiameter { get; set; }
        public double? MinimumDiameter { get; set; }
        public short EnumRollStatus { get; set; }
        [StringLength(100)]
        public string RollDescription { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScrapTime { get; set; }
        public short EnumRollScrapReason { get; set; }
        [Required]
        [StringLength(50)]
        public string RollTypeName { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        public long RollSetId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollSetName { get; set; }
        public short EnumRollSetStatus { get; set; }
        public long RollSetHistoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedInMillTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedFromMillTs { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GrooveCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GrooveActivatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GrooveDeactivatedTs { get; set; }
        public long RollGrooveHistoryId { get; set; }
        public long GrooveTemplateId { get; set; }
        public short EnumRollGrooveStatus { get; set; }
        public short GrooveNumber { get; set; }
        public double AccWeight { get; set; }
        public long AccBilletCnt { get; set; }
        public double? AccWeightWithCoeff { get; set; }
        [StringLength(255)]
        public string GrooveRemarks { get; set; }
        public short EnumGrooveCondition { get; set; }
        [Required]
        [StringLength(50)]
        public string GrooveTemplateName { get; set; }
        public short EnumGrooveSetting { get; set; }
        public long? AccBilletCntLimit { get; set; }
        public double? AccWeightLimit { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RollLocalization { get; set; }
        public short? StandNo { get; set; }
        [StringLength(50)]
        public string StandName { get; set; }
    }
}
