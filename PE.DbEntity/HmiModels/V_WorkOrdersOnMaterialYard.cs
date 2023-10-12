using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrdersOnMaterialYard
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
        public long SteelgradeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public short MaterialsPlanned { get; set; }
        public int MaterialsNumber { get; set; }
        public int MaterialsCharged { get; set; }
        public int IsScheduled { get; set; }
        public long? ScheduleId { get; set; }
        public short? ScheduleOrderSeq { get; set; }
        public long MaterialCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        public int? MaterialsPlannedVSCharged { get; set; }
        public double? WeightOnArea { get; set; }
        public int? MaterialsOnArea { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastMaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastMaterialMovementTs { get; set; }
        public short EnumYardType { get; set; }
    }
}
