using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(RollTypeName), Name = "UQ_RollType_Name", IsUnique = true)]
    public partial class RLSRollType
    {
        public RLSRollType()
        {
            RLSRolls = new HashSet<RLSRoll>();
        }

        [Key]
        public long RollTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string RollTypeName { get; set; }
        [StringLength(100)]
        public string RollTypeDescription { get; set; }
        public double? DiameterMin { get; set; }
        public double? DiameterMax { get; set; }
        public double? RoughnessMin { get; set; }
        public double? RoughnessMax { get; set; }
        public double? YieldStrengthRef { get; set; }
        public double? RollLength { get; set; }
        public double? AccWeightLimit { get; set; }
        public long? AccBilletCntLimit { get; set; }
        /// <summary>
        /// refers to PE.Core.Constants.RollSetType
        /// </summary>
        public short? MatchingRollsetType { get; set; }
        [StringLength(30)]
        public string RollSteelgrade { get; set; }
        [StringLength(50)]
        public string DrawingName { get; set; }
        [StringLength(20)]
        public string ChokeType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(RLSRoll.FKRollType))]
        public virtual ICollection<RLSRoll> RLSRolls { get; set; }
    }
}
