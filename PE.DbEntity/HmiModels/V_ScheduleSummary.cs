using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ScheduleSummary
    {
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        public bool IsTestOrder { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEndTs { get; set; }
        public long MaterialCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        public long ProductCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
        public long SteelgradeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public long ScheduleId { get; set; }
        public short ScheduleOrderSeq { get; set; }
        public long PlannedDuration { get; set; }
        public short MaterialsPlanned { get; set; }
        public int MaterialsNumber { get; set; }
        public int RawMaterialsNumber { get; set; }
        public int RawMaterialsParents { get; set; }
        public int RawMaterialsKids { get; set; }
        public int RawMaterialsInvalid { get; set; }
        public int RawMaterialsUnassigned { get; set; }
        public int RawMaterialsAssigned { get; set; }
        public int RawMaterialsCharged { get; set; }
        public int RawMaterialsDischarged { get; set; }
        public int RawMaterialsInStorage { get; set; }
        public int RawMaterialsInMill { get; set; }
        public int RawMaterialsInFinalProduction { get; set; }
        public int RawMaterialsOnCoolingBed { get; set; }
        public int RawMaterialsRolled { get; set; }
        public int RawMaterialsInTransport { get; set; }
        public int RawMaterialsFinished { get; set; }
        public int RawMaterialsScrapped { get; set; }
        public int RawMaterialsRejected { get; set; }
        public int RawMaterialsDivided { get; set; }
        public short? LowestRawMaterialStatus { get; set; }
        public short? HighestRawMaterialStatus { get; set; }
    }
}
