using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MVHAssetLayout
    {
        [Key]
        public long AssetLayoutId { get; set; }
        public long FKLayoutId { get; set; }
        public long FKPrevAssetId { get; set; }
        public long FKNextAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKLayoutId))]
        [InverseProperty(nameof(MVHLayout.MVHAssetLayouts))]
        public virtual MVHLayout FKLayout { get; set; }
        [ForeignKey(nameof(FKNextAssetId))]
        [InverseProperty(nameof(MVHAsset.MVHAssetLayoutFKNextAssets))]
        public virtual MVHAsset FKNextAsset { get; set; }
        [ForeignKey(nameof(FKPrevAssetId))]
        [InverseProperty(nameof(MVHAsset.MVHAssetLayoutFKPrevAssets))]
        public virtual MVHAsset FKPrevAsset { get; set; }
    }
}
