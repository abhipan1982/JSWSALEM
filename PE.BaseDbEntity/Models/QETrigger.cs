using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QETrigger")]
    public partial class QETrigger
    {
        public QETrigger()
        {
            QERuleMappingValues = new HashSet<QERuleMappingValue>();
        }

        [Key]
        public long TriggerId { get; set; }
        [Required]
        [StringLength(50)]
        public string TriggerName { get; set; }
        [StringLength(50)]
        public string TriggerDescription { get; set; }
        public long? FKAssetId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [StringLength(255)]
        public string LastTriggerVersion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.QETriggers))]
        public virtual MVHAsset FKAsset { get; set; }
        [InverseProperty(nameof(QERuleMappingValue.FKTrigger))]
        public virtual ICollection<QERuleMappingValue> QERuleMappingValues { get; set; }
    }
}
