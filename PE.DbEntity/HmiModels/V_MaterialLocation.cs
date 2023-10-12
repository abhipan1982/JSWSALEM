using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialLocation
    {
        public long RawMaterialId { get; set; }
        public long MaterialId { get; set; }
        public long SteelgradeId { get; set; }
        public long HeatId { get; set; }
        public long WorkOrderId { get; set; }
        public long AssetId { get; set; }
        public int AssetCode { get; set; }
        public int? AreaCode { get; set; }
        public short PositionSeq { get; set; }
        public short OrderSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
    }
}
