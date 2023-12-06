using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimCrew
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimCrewKey { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
        [StringLength(100)]
        public string CrewDescription { get; set; }
        [StringLength(100)]
        public string CrewLeaderName { get; set; }
        public short? DefaultCrewSize { get; set; }
        public short? CrewOrderSeq { get; set; }
    }
}
