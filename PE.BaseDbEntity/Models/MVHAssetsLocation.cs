using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("MVHAssetsLocation")]
    public partial class MVHAssetsLocation
    {
        [Key]
        public long FKAssetId { get; set; }
        public FillDirection EnumFillDirection { get; set; }
        public FillPattern EnumFillPattern { get; set; }
        public short LocationX { get; set; }
        public short LocationY { get; set; }
        public short SizeX { get; set; }
        public short SizeY { get; set; }
        public short LayersMaxNumber { get; set; }
        public int PieceMaxCapacity { get; set; }
        public int WeightMaxCapacity { get; set; }
        public int VolumeMaxCapacity { get; set; }
        public short FillOrderSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.MVHAssetsLocation))]
        public virtual MVHAsset FKAsset { get; set; }
    }
}
