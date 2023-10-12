using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RollSetOverview
    {
        public long RollSetHistoryId { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        public short? PositionInCassette { get; set; }
        public long RollSetId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollSetName { get; set; }
        [StringLength(100)]
        public string RollSetDescription { get; set; }
        public short EnumRollSetStatus { get; set; }
        public short RollSetType { get; set; }
        public bool IsThirdRoll { get; set; }
        public long? CassetteId { get; set; }
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short EnumCassetteStatus { get; set; }
        public long? CassetteTypeId { get; set; }
        [StringLength(50)]
        public string CassetteTypeName { get; set; }
        public long? StandId { get; set; }
        public short? StandNo { get; set; }
        [StringLength(50)]
        public string StandName { get; set; }
        public bool? IsOnLine { get; set; }
        public short? Arrangement { get; set; }
        public short EnumStandStatus { get; set; }
        public long? BottomRollId { get; set; }
        [StringLength(50)]
        public string BottomRollName { get; set; }
        public double? BottomActualDiameter { get; set; }
        public long? BottomRollTypeId { get; set; }
        [StringLength(50)]
        public string BottomRollTypeName { get; set; }
        public long? UpperRollId { get; set; }
        [StringLength(50)]
        public string UpperRollName { get; set; }
        public double? UpperActualDiameter { get; set; }
        public long? UpperRollTypeId { get; set; }
        [StringLength(50)]
        public string UpperRollTypeName { get; set; }
        public long? ThirdRollId { get; set; }
        [StringLength(50)]
        public string ThirdRollName { get; set; }
        public double? ThirdActualDiameter { get; set; }
        public long? ThirdRollTypeId { get; set; }
        [StringLength(50)]
        public string ThirdRollTypeName { get; set; }
        public short? GrooveNumber { get; set; }
        [StringLength(50)]
        public string GrooveTemplateName { get; set; }
        public short EnumGrooveSetting { get; set; }
        public short? RollSetGroovesSettings { get; set; }
        public bool IsLastOne { get; set; }
    }
}
