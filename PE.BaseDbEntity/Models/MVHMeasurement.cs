using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(CreatedTs), Name = "NCI_CreatedTs")]
    [Index(nameof(FKFeatureId), Name = "NCI_FKFeatureId")]
    [Index(nameof(FKFeatureId), nameof(CreatedTs), Name = "NCI_FKFeatureId_CreatedTs")]
    [Index(nameof(FKFeatureId), nameof(FKRawMaterialId), Name = "NCI_FKFeatureId_FKRawMaterialId")]
    [Index(nameof(FKFeatureId), nameof(IsValid), Name = "NCI_FKFeatureId_IsValid")]
    [Index(nameof(FKFeatureId), nameof(IsValid), nameof(CreatedTs), Name = "NCI_FKFeatureId_IsValid_CreatedTs")]
    [Index(nameof(FKRawMaterialId), Name = "NCI_FKRawMaterialId")]
    [Index(nameof(NoOfSamples), Name = "NCI_NoOfSamples")]
    public partial class MVHMeasurement
    {
        public MVHMeasurement()
        {
            MVHSamples = new HashSet<MVHSample>();
        }

        [Key]
        public long MeasurementId { get; set; }
        public long FKFeatureId { get; set; }
        public long? FKRawMaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Required]
        public bool? IsValid { get; set; }
        public short NoOfSamples { get; set; }
        public double ValueAvg { get; set; }
        public double? ValueMin { get; set; }
        public double? ValueMax { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FirstMeasurementTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastMeasurementTs { get; set; }
        public double? ActualLength { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKFeatureId))]
        [InverseProperty(nameof(MVHFeature.MVHMeasurements))]
        public virtual MVHFeature FKFeature { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.MVHMeasurements))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
        [InverseProperty(nameof(MVHSample.FKMeasurement))]
        public virtual ICollection<MVHSample> MVHSamples { get; set; }
    }
}
