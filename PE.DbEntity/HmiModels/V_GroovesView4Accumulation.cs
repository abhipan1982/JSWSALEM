using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_GroovesView4Accumulation
    {
        public long FKRollSetId { get; set; }
        public long RollSetHistoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        public short? PositionInCassette { get; set; }
        public long RollGrooveHistoryId { get; set; }
        public short GrooveNumber { get; set; }
        public long FKGrooveTemplateId { get; set; }
        public short EnumRollGrooveStatus { get; set; }
        public long AccBilletCnt { get; set; }
        public double AccWeight { get; set; }
        public double? ActDiameter { get; set; }
        public long FKCassetteTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short Arrangement { get; set; }
        public short StandNo { get; set; }
        public short EnumStandStatus { get; set; }
        public long StandId { get; set; }
        public short NumberOfRolls { get; set; }
        public short EnumRollSetStatus { get; set; }
        public bool IsThirdRoll { get; set; }
    }
}
