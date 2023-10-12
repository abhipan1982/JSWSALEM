using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimFeature
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimFeatureRow { get; set; }
        public bool DimFeatureIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimFeatureHash { get; set; }
        public long DimFeatureKey { get; set; }
        public long DimAssetKey { get; set; }
        public long DimUnitKey { get; set; }
        public long DimDataTypeKey { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool FeatureIsMaterialRelated { get; set; }
        public bool FeatureIsLengthRelated { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(50)]
        public string UOMSymbol { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
    }
}
