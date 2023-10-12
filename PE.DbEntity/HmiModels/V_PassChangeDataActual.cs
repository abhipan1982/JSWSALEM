using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_PassChangeDataActual
    {
        public long RollSetHistoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        public short EnumRollSetHistoryStatus { get; set; }
        public short? PositionInCassette { get; set; }
        public long RollSetId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollSetName { get; set; }
        public short RollSetType { get; set; }
        public long CassetteId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteName { get; set; }
        public short Arrangement { get; set; }
        public long StandId { get; set; }
        public short StandNo { get; set; }
        [Required]
        [StringLength(50)]
        public string StandName { get; set; }
        public short? Position { get; set; }
        public short EnumStandStatus { get; set; }
        public long? AssetId { get; set; }
        public double ActualDiameter { get; set; }
        [Required]
        [StringLength(50)]
        public string RollTypeName { get; set; }
        public double AccWeight { get; set; }
        public long AccBilletCnt { get; set; }
        public double? AccWeightWithCoeff { get; set; }
        public short GrooveNumber { get; set; }
        public short EnumRollGrooveStatus { get; set; }
        public long? AccBilletCntLimit { get; set; }
        public double? AccWeightLimit { get; set; }
        public long GrooveTemplateId { get; set; }
        [Required]
        [StringLength(50)]
        public string GrooveTemplateName { get; set; }
        public short EnumGrooveSetting { get; set; }
        public double? AccBilletCntRatio { get; set; }
        public double? AccWeightRatio { get; set; }
        public double? AccWeightCoeffRatio { get; set; }
    }
}
