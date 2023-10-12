using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class FactRating
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public bool FactRatingIsDeleted { get; set; }
        public long FactRatingRow { get; set; }
        [MaxLength(16)]
        public byte[] FactRatingHash { get; set; }
        public long FactRatingKey { get; set; }
        public long? DimAssetKey { get; set; }
        public long? DimMaterialKey { get; set; }
        public long? DimRawMaterialKey { get; set; }
        public int? DimYearKey { get; set; }
        public int DimDateKey { get; set; }
        public long? RatingRanking { get; set; }
        public double? RatingValueCurrent { get; set; }
        public double? RatingValueForced { get; set; }
        public double? RatingValueAlternative { get; set; }
        public double? RatingValueOriginal { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(255)]
        public string RuleSignal { get; set; }
        [StringLength(50)]
        public string RuleName { get; set; }
        public int? RatingType { get; set; }
        public int? RatingGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingModified { get; set; }
        [StringLength(400)]
        public string RatingAlarm { get; set; }
        public double? RatingCode { get; set; }
    }
}
