using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    [Table("MVHFeaturesBAK")]
    public partial class MVHFeaturesBAK
    {
        public long FeatureId { get; set; }
        public long FKAssetId { get; set; }
        public long FKUnitOfMeasureId { get; set; }
        public long FKExtUnitOfMeasureId { get; set; }
        public long FKDataTypeId { get; set; }
        public long? FKParentFeatureId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool IsSampledFeature { get; set; }
        public bool IsConsumptionFeature { get; set; }
        public bool IsMaterialRelated { get; set; }
        public bool IsLengthRelated { get; set; }
        public bool IsQETrigger { get; set; }
        public bool IsDigital { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnHMI { get; set; }
        public bool IsTrackingPoint { get; set; }
        public bool IsMeasurementPoint { get; set; }
        public double? SampleOffsetTime { get; set; }
        public double? ConsumptionAggregationTime { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public int? RetentionFactor { get; set; }
        public FeatureType EnumFeatureType { get; set; }
        public CommChannelType EnumCommChannelType { get; set; }
        public AggregationStrategy EnumAggregationStrategy { get; set; }
        [StringLength(350)]
        public string CommAttr1 { get; set; }
        [StringLength(350)]
        public string CommAttr2 { get; set; }
        [StringLength(350)]
        public string CommAttr3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
    }
}
