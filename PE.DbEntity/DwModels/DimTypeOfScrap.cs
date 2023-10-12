using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimTypeOfScrap
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimTypeOfScrapRow { get; set; }
        public bool DimTypeOfScrapIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimTypeOfScrapHash { get; set; }
        public long DimTypeOfScrapKey { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TypeOfScrap { get; set; }
    }
}
