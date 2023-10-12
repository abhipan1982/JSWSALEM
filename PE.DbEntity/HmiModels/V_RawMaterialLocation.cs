using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialLocation
    {
        public short OrderSeq { get; set; }
        public bool IsVirtual { get; set; }
        public long AssetId { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        public bool IsArea { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public double? LastWeight { get; set; }
        public double? LastLength { get; set; }
        public long? MaterialId { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
    }
}
