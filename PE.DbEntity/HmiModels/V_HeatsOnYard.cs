using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_HeatsOnYard
    {
        public long OrderSeq { get; set; }
        public long? AreaId { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(100)]
        public string AreaDescription { get; set; }
        [StringLength(50)]
        public string AssetTypeName { get; set; }
        public short EnumYardType { get; set; }
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public double? HeatWeight { get; set; }
        public double? WeightOnArea { get; set; }
        public int? MaterialsOnArea { get; set; }
    }
}
