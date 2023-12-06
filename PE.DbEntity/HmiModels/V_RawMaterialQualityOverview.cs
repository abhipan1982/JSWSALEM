using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialQualityOverview
    {
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short RawMaterialStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        public bool? HasDefects { get; set; }
        public int? CountDefects { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        public short? EnumCrashTest { get; set; }
        [StringLength(400)]
        public string VisualInspection { get; set; }
        public short? EnumInspectionResult { get; set; }
    }
}
