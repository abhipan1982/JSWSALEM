using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_TriggerOverview
    {
        public long OrderSeq { get; set; }
        [Required]
        [StringLength(10)]
        public string TriggerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string TriggerName { get; set; }
        public bool IsActive { get; set; }
        public short EnumTriggerType { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        public int? AssetCode { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        public short PassNo { get; set; }
        [StringLength(50)]
        public string Relations { get; set; }
    }
}
