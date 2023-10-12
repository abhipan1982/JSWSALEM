using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RollsetInCassette
    {
        public long RollSetId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollSetName { get; set; }
        public long? FKUpperRollId { get; set; }
        public long? FKBottomRollId { get; set; }
        public short EnumRollSetStatus { get; set; }
        public short RollSetType { get; set; }
        public long? FKCassetteId { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        public short? PositionInCassette { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        public short EnumCassetteStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteName { get; set; }
        public long? FKStandId { get; set; }
        public short? StandNo { get; set; }
    }
}
