using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    /// <summary>
    /// PE.Core.Constants.MaterialShapeType
    /// </summary>
    [Index(nameof(FKRawMaterialId), nameof(ProcessingStepNo), Name = "NCI_FKRawMaterialId_ProcessingStepNo", IsUnique = true)]
    public partial class TRKRawMaterialsStep
    {
        [Key]
        public long RawMaterialStepId { get; set; }
        public short ProcessingStepNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProcessingStepTs { get; set; }
        public long FKRawMaterialId { get; set; }
        public long FKAssetId { get; set; }
        public short PassNo { get; set; }
        public bool IsReversed { get; set; }
        public bool IsAssetExit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialsSteps))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialsSteps))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
