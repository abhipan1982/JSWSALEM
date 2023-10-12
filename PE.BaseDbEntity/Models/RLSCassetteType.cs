using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class RLSCassetteType
    {
        public RLSCassetteType()
        {
            RLSCassettes = new HashSet<RLSCassette>();
        }

        [Key]
        public long CassetteTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string CassetteTypeName { get; set; }
        [StringLength(100)]
        public string CassetteTypeDescription { get; set; }
        public short? NumberOfRolls { get; set; }
        public CassetteType EnumCassetteType { get; set; }
        public bool IsInterCassette { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(RLSCassette.FKCassetteType))]
        public virtual ICollection<RLSCassette> RLSCassettes { get; set; }
    }
}
