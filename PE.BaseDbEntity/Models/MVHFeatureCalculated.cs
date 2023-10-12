using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("MVHFeatureCalculated")]
    public partial class MVHFeatureCalculated
    {
        [Key]
        public long FeatureCalculatedId { get; set; }
        public long FKFeatureId { get; set; }
        public int CalculatedValue { get; set; }
        public bool IsVirtual { get; set; }
        public short Seq { get; set; }
        public long FKFeatureId_1 { get; set; }
        public int Value_1 { get; set; }
        public CompareOperator EnumCompareOperator_1 { get; set; }
        public LogicalOperator EnumLogicalOperator { get; set; }
        public long? FKFeatureId_2 { get; set; }
        public int? Value_2 { get; set; }
        public CompareOperator EnumCompareOperator_2 { get; set; }
        public float? TimeFilter { get; set; }
        public LogicalOperator EnumLogicalOperator_ForNextSequence { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKFeatureId))]
        [InverseProperty(nameof(MVHFeature.MVHFeatureCalculatedFKFeatures))]
        public virtual MVHFeature FKFeature { get; set; }
        [ForeignKey(nameof(FKFeatureId_1))]
        [InverseProperty(nameof(MVHFeature.MVHFeatureCalculatedFKFeatureId_1Navigations))]
        public virtual MVHFeature FKFeatureId_1Navigation { get; set; }
        [ForeignKey(nameof(FKFeatureId_2))]
        [InverseProperty(nameof(MVHFeature.MVHFeatureCalculatedFKFeatureId_2Navigations))]
        public virtual MVHFeature FKFeatureId_2Navigation { get; set; }
    }
}
