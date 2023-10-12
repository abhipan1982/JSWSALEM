using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrdersOnProductYard
    {
        public long OrderSeq { get; set; }
        public long? AreaId { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(100)]
        public string AreaDescription { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? HeatWeight { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public bool? IsOverrun { get; set; }
        public double? WeightOnArea { get; set; }
        public int? ProductsOnArea { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastProductCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastProductMovementTs { get; set; }
    }
}
