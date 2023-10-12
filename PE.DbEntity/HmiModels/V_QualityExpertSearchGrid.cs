using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_QualityExpertSearchGrid
    {
        public long RawMaterialId { get; set; }
        public short EnumRawMaterialStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RawMaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollingStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollingEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProductCreatedTs { get; set; }
        public double? LastGrading { get; set; }
        public long MaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEndTs { get; set; }
    }
}
