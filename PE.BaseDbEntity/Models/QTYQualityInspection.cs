using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKRawMaterialId), Name = "UQ_RawMaterialId", IsUnique = true)]
    public partial class QTYQualityInspection
    {
        [Key]
        public long QualityInspectionId { get; set; }
        public long? FKMaterialId { get; set; }
        public long? FKRawMaterialId { get; set; }
        public long? FKProductId { get; set; }
        [StringLength(400)]
        public string VisualInspection { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        public CrashTest EnumCrashTest { get; set; }
        public InspectionResult EnumInspectionResult { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKMaterialId))]
        [InverseProperty(nameof(PRMMaterial.QTYQualityInspections))]
        public virtual PRMMaterial FKMaterial { get; set; }
        [ForeignKey(nameof(FKProductId))]
        [InverseProperty(nameof(PRMProduct.QTYQualityInspections))]
        public virtual PRMProduct FKProduct { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.QTYQualityInspection))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
