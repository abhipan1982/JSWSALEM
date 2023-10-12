using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKAssetId), Name = "NCI_AssetId")]
    [Index(nameof(FKDataTypeId), Name = "NCI_DataTypeId")]
    [Index(nameof(FKUnitOfMeasureId), Name = "NCI_UnitOfMeasureId")]
    [Index(nameof(FeatureCode), Name = "UQ_FeatureCode", IsUnique = true)]
    public partial class MVHFeature
    {
        public MVHFeature()
        {
            EVTTriggersFeatures = new HashSet<EVTTriggersFeature>();
            InverseFKParentFeature = new HashSet<MVHFeature>();
            MVHFeatureCalculatedFKFeatureId_1Navigations = new HashSet<MVHFeatureCalculated>();
            MVHFeatureCalculatedFKFeatureId_2Navigations = new HashSet<MVHFeatureCalculated>();
            MVHFeatureCalculatedFKFeatures = new HashSet<MVHFeatureCalculated>();
            MVHFeatureCustoms = new HashSet<MVHFeatureCustom>();
            MVHMeasurements = new HashSet<MVHMeasurement>();
            TRKTrackingInstructions = new HashSet<TRKTrackingInstruction>();
        }

        [Key]
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
        [Required]
        public bool? IsMaterialRelated { get; set; }
        public bool IsLengthRelated { get; set; }
        public bool IsQETrigger { get; set; }
        [Required]
        public bool? IsDigital { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsOnHMI { get; set; }
        public bool IsTrackingPoint { get; set; }
        public bool IsMeasurementPoint { get; set; }
        public bool IsConsumptionPoint { get; set; }
        public double? SampleOffsetTime { get; set; }
        public double? ConsumptionAggregationTime { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public int? RetentionFactor { get; set; }
        public FeatureType EnumFeatureType { get; set; }
        public FeatureProvider EnumFeatureProvider { get; set; }
        public CommChannelType EnumCommChannelType { get; set; }
        public AggregationStrategy EnumAggregationStrategy { get; set; }
        public TagValidationResult EnumTagValidationResult { get; set; }
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

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.MVHFeatures))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKDataTypeId))]
        [InverseProperty(nameof(DBDataType.MVHFeatures))]
        public virtual DBDataType FKDataType { get; set; }
        [ForeignKey(nameof(FKParentFeatureId))]
        [InverseProperty(nameof(MVHFeature.InverseFKParentFeature))]
        public virtual MVHFeature FKParentFeature { get; set; }
        [InverseProperty(nameof(EVTTriggersFeature.FKFeature))]
        public virtual ICollection<EVTTriggersFeature> EVTTriggersFeatures { get; set; }
        [InverseProperty(nameof(MVHFeature.FKParentFeature))]
        public virtual ICollection<MVHFeature> InverseFKParentFeature { get; set; }
        [InverseProperty(nameof(MVHFeatureCalculated.FKFeatureId_1Navigation))]
        public virtual ICollection<MVHFeatureCalculated> MVHFeatureCalculatedFKFeatureId_1Navigations { get; set; }
        [InverseProperty(nameof(MVHFeatureCalculated.FKFeatureId_2Navigation))]
        public virtual ICollection<MVHFeatureCalculated> MVHFeatureCalculatedFKFeatureId_2Navigations { get; set; }
        [InverseProperty(nameof(MVHFeatureCalculated.FKFeature))]
        public virtual ICollection<MVHFeatureCalculated> MVHFeatureCalculatedFKFeatures { get; set; }
        [InverseProperty(nameof(MVHFeatureCustom.FKFeature))]
        public virtual ICollection<MVHFeatureCustom> MVHFeatureCustoms { get; set; }
        [InverseProperty(nameof(MVHMeasurement.FKFeature))]
        public virtual ICollection<MVHMeasurement> MVHMeasurements { get; set; }
        [InverseProperty(nameof(TRKTrackingInstruction.FKFeature))]
        public virtual ICollection<TRKTrackingInstruction> TRKTrackingInstructions { get; set; }
    }
}
