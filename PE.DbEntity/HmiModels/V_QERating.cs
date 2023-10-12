using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_QERating
    {
        public long RatingId { get; set; }
        public long AssetId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public long? RatingRanking { get; set; }
        [Required]
        [StringLength(255)]
        public string SignalIdentifier { get; set; }
        [Required]
        [StringLength(400)]
        public string RulesIdentifier { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart1 { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart2 { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart3 { get; set; }
        public double? RatingValueForced { get; set; }
        public double? RatingAlternative { get; set; }
        public double? RatingValue { get; set; }
        public double? RatingCurrentValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChosenTs { get; set; }
        public int? RatingType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingModified { get; set; }
    }
}
