using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialOverview
    {
        public long MaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        public bool IsDummy { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public long FKWorkOrderId { get; set; }
        public double MaterialWeight { get; set; }
        public bool IsAssigned { get; set; }
        public long FKHeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short? RawMaterialStatus { get; set; }
    }
}
