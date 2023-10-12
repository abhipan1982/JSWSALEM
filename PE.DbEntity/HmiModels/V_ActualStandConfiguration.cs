using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ActualStandConfiguration
    {
        public long OrderSeq { get; set; }
        public short StandNo { get; set; }
        [Required]
        [StringLength(50)]
        public string StandName { get; set; }
        public short EnumStandStatus { get; set; }
        public long StandId { get; set; }
        public short NumberOfRolls { get; set; }
        [StringLength(30)]
        public string StandZoneName { get; set; }
        public bool IsOnLine { get; set; }
        public bool IsCalibrated { get; set; }
        public short? Position { get; set; }
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short? NumberOfPositions { get; set; }
        public short? Arrangement { get; set; }
        public long? CassetteTypeId { get; set; }
        public long? CassetteId { get; set; }
        public int? RollsetsNumber { get; set; }
        public long? RollSetId { get; set; }
        public short? RollSetGroovesSettings { get; set; }
    }
}
