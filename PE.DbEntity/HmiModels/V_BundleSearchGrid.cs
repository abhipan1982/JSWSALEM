using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_BundleSearchGrid
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
        public long RootRawMaterialId { get; set; }
    }
}
