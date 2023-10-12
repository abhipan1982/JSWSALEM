using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimCrew
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimCrewRow { get; set; }
        public bool DimCrewIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimCrewHash { get; set; }
        public long DimCrewKey { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [StringLength(100)]
        public string CrewDescription { get; set; }
        [StringLength(100)]
        public string CrewLeaderName { get; set; }
        public short? CrewDefaultSize { get; set; }
        public short? CrewOrderSeq { get; set; }
    }
}
