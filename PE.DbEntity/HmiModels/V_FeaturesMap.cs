using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_FeaturesMap
    {
        public long OrderSeq { get; set; }
        public long? AssetSeq { get; set; }
        public long AssetId { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public bool IsDelayCheckpoint { get; set; }
        public long FeatureId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool IsMaterialRelated { get; set; }
        public bool IsLengthRelated { get; set; }
        public bool IsActive { get; set; }
        public bool IsSampled { get; set; }
        public bool OnHMI { get; set; }
        public long UnitId { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCategoryName { get; set; }
        public long DataTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string DataTypeName { get; set; }
        public int? AreaCode { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
    }
}
