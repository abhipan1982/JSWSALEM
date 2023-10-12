using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("RLSRollSetHistory")]
    public partial class RLSRollSetHistory
    {
        public RLSRollSetHistory()
        {
            RLSRollGroovesHistories = new HashSet<RLSRollGroovesHistory>();
        }

        [Key]
        public long RollSetHistoryId { get; set; }
        public long FKRollSetId { get; set; }
        public long? FKCassetteId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MountedInMillTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DismountedFromMillTs { get; set; }
        public RollSetHistoryStatus EnumRollSetHistoryStatus { get; set; }
        public short? PositionInCassette { get; set; }
        public double? AccWeightLimit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCassetteId))]
        [InverseProperty(nameof(RLSCassette.RLSRollSetHistories))]
        public virtual RLSCassette FKCassette { get; set; }
        [ForeignKey(nameof(FKRollSetId))]
        [InverseProperty(nameof(RLSRollSet.RLSRollSetHistories))]
        public virtual RLSRollSet FKRollSet { get; set; }
        [InverseProperty(nameof(RLSRollGroovesHistory.FKRollSetHistory))]
        public virtual ICollection<RLSRollGroovesHistory> RLSRollGroovesHistories { get; set; }
    }
}
