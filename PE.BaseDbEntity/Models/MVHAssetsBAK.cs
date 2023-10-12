using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    [Table("MVHAssetsBAK")]
    public partial class MVHAssetsBAK
    {
        public long AssetId { get; set; }
        public int OrderSeq { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        public long? FKParentAssetId { get; set; }
        public long? FKAssetTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsArea { get; set; }
        public bool IsZone { get; set; }
        public bool IsTrackingPoint { get; set; }
        public bool IsDelayCheckpoint { get; set; }
        public bool IsReversible { get; set; }
        public TrackingAreaType EnumTrackingAreaType { get; set; }
        public bool? IsPositionBased { get; set; }
        public short? PositionsNumber { get; set; }
        public short? VirtualPositionsNumber { get; set; }
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
