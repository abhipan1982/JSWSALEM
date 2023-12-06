using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialsInFurnace
    {
        public long Sorting { get; set; }
        public int IsFirstOne { get; set; }
        public int IsLastOne { get; set; }
        public short OrderSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChargingTs { get; set; }
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short EnumRawMaterialStatus { get; set; }
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
    }
}
