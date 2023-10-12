using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(RollName), Name = "UQ_RollName", IsUnique = true)]
    public partial class RLSRoll
    {
        public RLSRoll()
        {
            RLSRollGroovesHistories = new HashSet<RLSRollGroovesHistory>();
            RLSRollSetFKBottomRolls = new HashSet<RLSRollSet>();
            RLSRollSetFKThirdRolls = new HashSet<RLSRollSet>();
            RLSRollSetFKUpperRolls = new HashSet<RLSRollSet>();
        }

        [Key]
        public long RollId { get; set; }
        public long FKRollTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollName { get; set; }
        [StringLength(100)]
        public string RollDescription { get; set; }
        public double InitialDiameter { get; set; }
        public double ActualDiameter { get; set; }
        public double? MinimumDiameter { get; set; }
        public double? DiameterOfMaterial { get; set; }
        public double? DiameterOfTool { get; set; }
        public short GroovesNumber { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ScrapTime { get; set; }
        public RollScrapReason EnumRollScrapReason { get; set; }
        public RollStatus EnumRollStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKRollTypeId))]
        [InverseProperty(nameof(RLSRollType.RLSRolls))]
        public virtual RLSRollType FKRollType { get; set; }
        [InverseProperty(nameof(RLSRollGroovesHistory.FKRoll))]
        public virtual ICollection<RLSRollGroovesHistory> RLSRollGroovesHistories { get; set; }
        [InverseProperty(nameof(RLSRollSet.FKBottomRoll))]
        public virtual ICollection<RLSRollSet> RLSRollSetFKBottomRolls { get; set; }
        [InverseProperty(nameof(RLSRollSet.FKThirdRoll))]
        public virtual ICollection<RLSRollSet> RLSRollSetFKThirdRolls { get; set; }
        [InverseProperty(nameof(RLSRollSet.FKUpperRoll))]
        public virtual ICollection<RLSRollSet> RLSRollSetFKUpperRolls { get; set; }
    }
}
