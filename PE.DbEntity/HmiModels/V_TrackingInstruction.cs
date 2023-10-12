using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_TrackingInstruction
    {
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        [Required]
        [StringLength(50)]
        public string AreaName { get; set; }
        [StringLength(50)]
        public string PointName { get; set; }
        public short SeqNo { get; set; }
        public short? TrackingInstructionValue { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InstructionType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FeatureType { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TrackingAreaType { get; set; }
        [StringLength(50)]
        public string PointTypeName { get; set; }
        public int FeatureCode { get; set; }
        public int AreaCode { get; set; }
        public int? PointCode { get; set; }
        public short EnumTrackingInstructionType { get; set; }
        public long TrackingInstructionId { get; set; }
    }
}
