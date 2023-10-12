using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Feature
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
        [StringLength(75)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool IsMaterialRelated { get; set; }
        public bool IsLengthRelated { get; set; }
        public bool IsActive { get; set; }
        public bool IsSampledFeature { get; set; }
        public bool IsDigital { get; set; }
        public bool IsOnHMI { get; set; }
        public bool IsQETrigger { get; set; }
        public bool IsTrackingPoint { get; set; }
        public bool IsMeasurementPoint { get; set; }
        public bool IsConsumptionPoint { get; set; }
        public short EnumCommChannelType { get; set; }
        public short EnumFeatureType { get; set; }
        public short EnumFeatureProvider { get; set; }
        public short EnumAggregationStrategy { get; set; }
        public short EnumTagValidationResult { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CommChannelType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FeatureType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FeatureProvider { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AggregationStrategy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TagValidationResult { get; set; }
        public int? RetentionFactor { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public double? SampleOffsetTime { get; set; }
        public double? ConsumptionAggregationTime { get; set; }
        public int? ParentFeatureCode { get; set; }
        [StringLength(75)]
        public string ParentFeatureName { get; set; }
        [StringLength(350)]
        public string CommAddress { get; set; }
        [StringLength(350)]
        public string CommTagNameSpace { get; set; }
        [StringLength(350)]
        public string CommAttr3 { get; set; }
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
        public short? MaxLength { get; set; }
        public int? AreaCode { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
    }
}
