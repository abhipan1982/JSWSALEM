using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_CassettesInStand
    {
        public long? OrderSeq { get; set; }
        public long StandId { get; set; }
        public short StandNo { get; set; }
        public short EnumStandStatus { get; set; }
        [StringLength(30)]
        public string StandZoneName { get; set; }
        public bool IsOnLine { get; set; }
        public short? Position { get; set; }
        public long? CassetteId { get; set; }
        public short EnumCassetteStatus { get; set; }
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short? Arrangement { get; set; }
        [StringLength(50)]
        public string CassetteTypeName { get; set; }
        public short EnumCassetteType { get; set; }
        public long? CassetteTypeId { get; set; }
        public long? RollSetHistoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedInMillTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedFromMillTs { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
    }
}
