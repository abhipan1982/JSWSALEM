using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(TriggerCode), Name = "NCI_TriggerCode", IsUnique = true)]
    [Index(nameof(EnumTriggerType), Name = "NCI_TriggerType")]
    public partial class EVTTrigger
    {
        public EVTTrigger()
        {
            EVTTriggersFeatures = new HashSet<EVTTriggersFeature>();
        }

        [Key]
        public long TriggerId { get; set; }
        [Required]
        [StringLength(10)]
        public string TriggerCode { get; set; }
        [Required]
        [StringLength(50)]
        public string TriggerName { get; set; }
        [StringLength(100)]
        public string TriggerDescription { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public TriggerType EnumTriggerType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(EVTTriggersFeature.FKTrigger))]
        public virtual ICollection<EVTTriggersFeature> EVTTriggersFeatures { get; set; }
    }
}
