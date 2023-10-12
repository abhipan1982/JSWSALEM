using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(AssetCode), Name = "NCI_AssetCode")]
    [Index(nameof(FKAssetId), Name = "NCI_AssetId")]
    [Index(nameof(FKRawMaterialId), Name = "NCI_RawMaterialId")]
    [Index(nameof(AssetCode), nameof(PositionSeq), nameof(OrderSeq), Name = "UQ_AssetPositionSeqOrderSeq", IsUnique = true)]
    public partial class TRKRawMaterialLocation
    {
        [Key]
        public long RawMaterialLocationId { get; set; }
        public long FKAssetId { get; set; }
        public int AssetCode { get; set; }
        public short PositionSeq { get; set; }
        public short OrderSeq { get; set; }
        public long? FKRawMaterialId { get; set; }
        [StringLength(50)]
        public string CorrelationId { get; set; }
        public AreaType EnumAreaType { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsOccupied { get; set; }
        public long? FKCtrAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialLocationFKAssets))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKCtrAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialLocationFKCtrAssets))]
        public virtual MVHAsset FKCtrAsset { get; set; }
        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialLocations))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
