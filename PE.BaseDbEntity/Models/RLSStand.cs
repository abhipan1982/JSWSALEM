using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class RLSStand
    {
        public RLSStand()
        {
            RLSCassettes = new HashSet<RLSCassette>();
        }

        [Key]
        public long StandId { get; set; }
        public short StandNo { get; set; }
        [Required]
        [StringLength(50)]
        public string StandName { get; set; }
        public StandStatus EnumStandStatus { get; set; }
        [Required]
        public bool? IsOnLine { get; set; }
        public bool IsCalibrated { get; set; }
        public short NumberOfRolls { get; set; }
        public short Arrangement { get; set; }
        [StringLength(30)]
        public string StandZoneName { get; set; }
        public short? Position { get; set; }
        public long? FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(RLSCassette.FKStand))]
        public virtual ICollection<RLSCassette> RLSCassettes { get; set; }
    }
}
