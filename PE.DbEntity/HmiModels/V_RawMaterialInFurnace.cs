using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialInFurnace
    {
        public long? OrderSeq { get; set; }
        public long RawMaterialInFurnaceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChargingTs { get; set; }
        public double? Temperature { get; set; }
        public int TimeInFurnace { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short EnumRawMaterialStatus { get; set; }
        public long? MaterialId { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
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
        public double? Thickness { get; set; }
        public double? LastWeight { get; set; }
        public double? LastLength { get; set; }
        public long? RawMaterialLocationId { get; set; }
    }
}
