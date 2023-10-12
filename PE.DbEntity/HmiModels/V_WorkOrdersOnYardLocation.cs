using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrdersOnYardLocation
    {
        public long OrderSeq { get; set; }
        public long? AreaId { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(100)]
        public string AreaDescription { get; set; }
        public long AssetId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        [StringLength(50)]
        public string AssetTypeName { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public double? HeatWeight { get; set; }
        public double? WeightOnAsset { get; set; }
        public int? ProductsOnAsset { get; set; }
    }
}
