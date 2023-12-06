using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimAsset
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimAssetKey { get; set; }
        public short AssetSeq { get; set; }
        public short AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(100)]
        public string AssetDescription { get; set; }
        public long? ParentAssetKey { get; set; }
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        public bool IsCheckpoint { get; set; }
        public bool IsReversible { get; set; }
    }
}
