using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class RLSRollSet
    {
        public RLSRollSet()
        {
            RLSRollSetHistories = new HashSet<RLSRollSetHistory>();
        }

        [Key]
        public long RollSetId { get; set; }
        public long? FKUpperRollId { get; set; }
        public long? FKBottomRollId { get; set; }
        public long? FKThirdRollId { get; set; }
        public RollSetStatus EnumRollSetStatus { get; set; }
        public short RollSetType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Required]
        [StringLength(50)]
        public string RollSetName { get; set; }
        [StringLength(100)]
        public string RollSetDescription { get; set; }
        public bool IsThirdRoll { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKBottomRollId))]
        [InverseProperty(nameof(RLSRoll.RLSRollSetFKBottomRolls))]
        public virtual RLSRoll FKBottomRoll { get; set; }
        [ForeignKey(nameof(FKThirdRollId))]
        [InverseProperty(nameof(RLSRoll.RLSRollSetFKThirdRolls))]
        public virtual RLSRoll FKThirdRoll { get; set; }
        [ForeignKey(nameof(FKUpperRollId))]
        [InverseProperty(nameof(RLSRoll.RLSRollSetFKUpperRolls))]
        public virtual RLSRoll FKUpperRoll { get; set; }
        [InverseProperty(nameof(RLSRollSetHistory.FKRollSet))]
        public virtual ICollection<RLSRollSetHistory> RLSRollSetHistories { get; set; }
    }
}
