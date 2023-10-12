using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrdersOnYard
    {
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkOrderStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ToBeCompletedBeforeTs { get; set; }
        public bool? IsOverrun { get; set; }
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long SteelgradeId { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public int? MaterialsNumber { get; set; }
        public double? MaterialsWeight { get; set; }
        public int? ProductsNumber { get; set; }
        public double? ProductsWeight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastProductCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastProductMovementTs { get; set; }
    }
}
