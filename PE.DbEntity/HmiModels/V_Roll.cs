using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Roll
    {
        public long RollId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollName { get; set; }
        [StringLength(100)]
        public string RollDescription { get; set; }
        public short EnumRollStatus { get; set; }
        public double InitialDiameter { get; set; }
        public double ActualDiameter { get; set; }
        public double? MinimumDiameter { get; set; }
        public short GroovesNumber { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        public short EnumRollScrapReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScrapTime { get; set; }
        public long RollTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollTypeName { get; set; }
        [StringLength(100)]
        public string RollTypeDescription { get; set; }
        public short? MatchingRollsetType { get; set; }
        public double? RollLength { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        public double? RoughnessMin { get; set; }
        public double? RoughnessMax { get; set; }
        public double? YieldStrengthRef { get; set; }
        [StringLength(30)]
        public string RollSteelgrade { get; set; }
        public long? RollSetId { get; set; }
        [StringLength(50)]
        public string RollSetName { get; set; }
        public int IsUpperRoll { get; set; }
        public int IsBottomRoll { get; set; }
        public int IsThirdRoll { get; set; }
    }
}
