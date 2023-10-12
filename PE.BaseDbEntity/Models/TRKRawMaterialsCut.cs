using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class TRKRawMaterialsCut
    {
        [Key]
        public long RawMaterialCutId { get; set; }
        public long FKRawMaterialId { get; set; }
        public long FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CuttingTs { get; set; }
        public double CuttingLength { get; set; }
        public TypeOfCut EnumTypeOfCut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialsCuts))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialsCuts))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
