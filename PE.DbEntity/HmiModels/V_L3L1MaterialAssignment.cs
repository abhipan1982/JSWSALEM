using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_L3L1MaterialAssignment
    {
        public long Sorting { get; set; }
        public long MaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreatedTs { get; set; }
        public long HeatId { get; set; }
        public long? WorkOrderId { get; set; }
        public double MaterialWeight { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsDummy { get; set; }
        [Required]
        [StringLength(50)]
        public string DisplayMaterialName { get; set; }
        public long? RawMaterialId { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialCreatedTs { get; set; }
        public short? EnumRawMaterialStatus { get; set; }
        public double? LastWeight { get; set; }
        public long? ParentRawMaterialId { get; set; }
        public short? EnumRejectLocation { get; set; }
        public short? EnumTypeOfScrap { get; set; }
        public short? OutputPieces { get; set; }
        public double? ScrapPercent { get; set; }
        [StringLength(50)]
        public string RawMaterialStatus { get; set; }
    }
}
