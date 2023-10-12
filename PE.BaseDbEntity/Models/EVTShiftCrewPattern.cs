using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("EVTShiftCrewPattern")]
    public partial class EVTShiftCrewPattern
    {
        [Key]
        public long ShiftCrewPatternId { get; set; }
        public short OrderSeq { get; set; }
        public long FKShiftDefinitionId { get; set; }
        public long FKCrewId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKCrewId))]
        [InverseProperty(nameof(EVTCrew.EVTShiftCrewPatterns))]
        public virtual EVTCrew FKCrew { get; set; }
        [ForeignKey(nameof(FKShiftDefinitionId))]
        [InverseProperty(nameof(EVTShiftDefinition.EVTShiftCrewPatterns))]
        public virtual EVTShiftDefinition FKShiftDefinition { get; set; }
    }
}
