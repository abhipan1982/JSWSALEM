using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_CassettesOverview
    {
        public long CassetteId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short EnumCassetteStatus { get; set; }
        public short Arrangement { get; set; }
        public short NumberOfPositions { get; set; }
        public long CassetteTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteTypeName { get; set; }
        public short? NumberOfRolls { get; set; }
        public bool IsInterCassette { get; set; }
        public short EnumCassetteType { get; set; }
        public int? RollsetsNumber { get; set; }
        public long? RollSetId { get; set; }
        public short? RollSetGroovesSettings { get; set; }
        [StringLength(50)]
        public string StandName { get; set; }
        public long? StandId { get; set; }
        public short? StandNo { get; set; }
    }
}
