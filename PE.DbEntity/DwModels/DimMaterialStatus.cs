using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimMaterialStatus
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimMaterialStatusRow { get; set; }
        public bool DimMaterialStatusIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimMaterialStatusHash { get; set; }
        public long DimMaterialStatusKey { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string MaterialStatus { get; set; }
    }
}
