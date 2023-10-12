using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ActualRollsOnStandsDiameter
    {
        public long OrderSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string StandName { get; set; }
        public long? AssetId { get; set; }
        public bool IsOnLine { get; set; }
        public short EnumCassetteStatus { get; set; }
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        public short EnumRollSetStatus { get; set; }
        [StringLength(50)]
        public string RollSetName { get; set; }
        public short EnumRollStatus { get; set; }
        [StringLength(50)]
        public string RollName { get; set; }
        public double? ActualDiameter { get; set; }
    }
}
