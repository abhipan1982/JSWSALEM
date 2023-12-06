using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialList
    {
        public long OrderSeq { get; set; }
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RawMaterialCreatedTs { get; set; }
        public long? MaterialId { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaterialCreatedTs { get; set; }
        public short EnumRawMaterialStatus { get; set; }
        [StringLength(50)]
        public string RawMaterialStatus { get; set; }
    }
}
