using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrderSearchGrid
    {
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public bool IsTestOrder { get; set; }
        public bool IsBlocked { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        public double TargetOrderWeight { get; set; }
        public short L3NumberOfBillets { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3Ts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
    }
}
