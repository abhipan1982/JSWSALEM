using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimFeature
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimFeatureKey { get; set; }
        public long DimAssetKey { get; set; }
        public long DimUOMKey { get; set; }
        public long DimDataTypeKey { get; set; }
        public short FeatureCode { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool FeatureIsMaterialRelated { get; set; }
        public bool FeatureIsLengthRelated { get; set; }
    }
}
