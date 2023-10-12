using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AssetsLocationOverview
    {
        public long? OrderSeq { get; set; }
        public long ParentAssetId { get; set; }
        [Required]
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string ParentAssetDescription { get; set; }
        [StringLength(50)]
        public string ParentAssetType { get; set; }
        public short ParentEnumYardType { get; set; }
        public long AssetId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        [StringLength(50)]
        public string AssetType { get; set; }
        public short EnumYardType { get; set; }
        public short LayersMaxNumber { get; set; }
        public int WeightMaxCapacity { get; set; }
        public int PieceMaxCapacity { get; set; }
        public short FillOrderSeq { get; set; }
        public double? WeightMaterials { get; set; }
        public int? CountMaterials { get; set; }
        public int? CountMaterialsInLastGroup { get; set; }
        public long? HeatIdInLastGroup { get; set; }
        [StringLength(50)]
        public string HeatNameInLastGroup { get; set; }
        public double? WeightProducts { get; set; }
        public int? CountProducts { get; set; }
        public int? CountProductsInLastGroup { get; set; }
        public long? WorkOrderIdInLastGroup { get; set; }
        [StringLength(50)]
        public string WorkOrderNameInLastGroup { get; set; }
    }
}
