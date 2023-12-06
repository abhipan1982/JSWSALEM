using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MaterialsInWorkOrder
    {
        public long MaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreatedTs { get; set; }
        public bool IsDummy { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public long? WorkOrderId { get; set; }
        public long HeatId { get; set; }
        public double MaterialWeight { get; set; }
        public bool IsAssigned { get; set; }
        public short MaterialSeqNo { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short EnumRawMaterialStatus { get; set; }
        public bool HasDefect { get; set; }
        public int DefectsNumber { get; set; }
    }
}
