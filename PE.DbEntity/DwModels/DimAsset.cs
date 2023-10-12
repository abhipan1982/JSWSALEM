using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimAsset
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimAssetRow { get; set; }
        public bool DimAssetIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimAssetHash { get; set; }
        public long DimAssetKey { get; set; }
        public long? DimAssetKeyParent { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        public int AssetOrderSeq { get; set; }
        public bool AssetIsDelayCheckpoint { get; set; }
        public bool AssetIsArea { get; set; }
        public bool AssetIsZone { get; set; }
        public bool AssetIsReversible { get; set; }
        public int? AssetCodeParent { get; set; }
        [StringLength(50)]
        public string AssetNameParent { get; set; }
        [StringLength(100)]
        public string AssetDescriptionParent { get; set; }
    }
}
