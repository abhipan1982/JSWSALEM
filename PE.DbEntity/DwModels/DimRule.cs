using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimRule
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimRuleRow { get; set; }
        public bool DimRuleIsDeleted { get; set; }
        public bool DimRuleIsCurrent { get; set; }
        [MaxLength(16)]
        public byte[] DimRuleHash { get; set; }
        public int? DimRuleKey { get; set; }
        [Required]
        [StringLength(400)]
        public string RuleIdentifier { get; set; }
        [Required]
        [StringLength(255)]
        public string SignalIdentifier { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RuleDirection { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RuleParamType { get; set; }
    }
}
