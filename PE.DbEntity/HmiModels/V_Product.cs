using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Product
    {
        public long ProductId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductCreatedTs { get; set; }
        public bool IsDummy { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public bool IsAssigned { get; set; }
        public double ProductWeight { get; set; }
        public long? RawMaterialId { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        public double? ProductThickness { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ProductRollingDate { get; set; }
        public bool HasDefect { get; set; }
        public int DefectsNumber { get; set; }
        public short EnumInspectionResult { get; set; }
    }
}
