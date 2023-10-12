﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ProductsOnYard
    {
        public long? OrderSeq { get; set; }
        public bool? IsFirstInQueue { get; set; }
        public long ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public bool IsAssigned { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? HeatWeight { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public int AssetOrderSeq { get; set; }
        public long AssetId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public long? AreaId { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(100)]
        public string AreaDescription { get; set; }
        public short PositionX { get; set; }
        public short PositionY { get; set; }
    }
}
