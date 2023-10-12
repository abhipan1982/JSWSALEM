using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_MeasurementSearchGrid
    {
        public long RawMaterialId { get; set; }
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
        public short EnumRawMaterialStatus { get; set; }
        public short EnumLayerStatus { get; set; }
        public bool RawMaterialIsLayer { get; set; }
        public bool IsParent { get; set; }
        public bool IsChild { get; set; }
        public long? ParentRawMaterialId { get; set; }
        public long RootRawMaterialId { get; set; }
        public long RootLayerId { get; set; }
        public long? MaterialId { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string DisplayedMaterialName { get; set; }
    }
}
