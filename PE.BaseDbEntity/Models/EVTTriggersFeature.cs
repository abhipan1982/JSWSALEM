using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKFeatureId), Name = "NCI_FeatureId")]
    [Index(nameof(FKTriggerId), Name = "NCI_TriggerId")]
    public partial class EVTTriggersFeature
    {
        [Key]
        public long TriggersFeatureId { get; set; }
        public long FKTriggerId { get; set; }
        public long FKFeatureId { get; set; }
        public short PassNo { get; set; }
        public short OrderSeq { get; set; }
        [StringLength(50)]
        public string Relations { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKFeatureId))]
        [InverseProperty(nameof(MVHFeature.EVTTriggersFeatures))]
        public virtual MVHFeature FKFeature { get; set; }
        [ForeignKey(nameof(FKTriggerId))]
        [InverseProperty(nameof(EVTTrigger.EVTTriggersFeatures))]
        public virtual EVTTrigger FKTrigger { get; set; }
    }
}
