using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Heat
    {
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? HeatSupplierId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeatCreatedTs { get; set; }
        public double? HeatWeight { get; set; }
        public bool IsDummy { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public double? Density { get; set; }
        [StringLength(50)]
        public string HeatSupplierName { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        public int? MaterialsNumber { get; set; }
    }
}
