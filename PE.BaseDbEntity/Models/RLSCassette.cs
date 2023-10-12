using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class RLSCassette
    {
        public RLSCassette()
        {
            RLSRollSetHistories = new HashSet<RLSRollSetHistory>();
        }

        [Key]
        public long CassetteId { get; set; }
        public long FKCassetteTypeId { get; set; }
        public long? FKStandId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteName { get; set; }
        [StringLength(100)]
        public string CassetteDescription { get; set; }
        /// <summary>
        /// SRC.Core.Constants.CassetteStatus
        /// </summary>
        public CassetteStatus EnumCassetteStatus { get; set; }
        public short NumberOfPositions { get; set; }
        /// <summary>
        /// 0 - undefined, 1 - horizontal, 2- veritical, 3- other
        /// </summary>
        public short Arrangement { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCassetteTypeId))]
        [InverseProperty(nameof(RLSCassetteType.RLSCassettes))]
        public virtual RLSCassetteType FKCassetteType { get; set; }
        [ForeignKey(nameof(FKStandId))]
        [InverseProperty(nameof(RLSStand.RLSCassettes))]
        public virtual RLSStand FKStand { get; set; }
        [InverseProperty(nameof(RLSRollSetHistory.FKCassette))]
        public virtual ICollection<RLSRollSetHistory> RLSRollSetHistories { get; set; }
    }
}
