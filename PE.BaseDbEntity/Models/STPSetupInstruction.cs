using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKSetupTypeInstructionId), Name = "NCI_SetupTypeInstructionId")]
    [Index(nameof(FKSetupId), nameof(FKSetupTypeInstructionId), Name = "UQ_SetupId_SetupTypeInstructionId", IsUnique = true)]
    public partial class STPSetupInstruction
    {
        [Key]
        public long SetupInstructionId { get; set; }
        public long FKSetupId { get; set; }
        public long? FKSetupTypeInstructionId { get; set; }
        [StringLength(255)]
        public string Value { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKSetupId))]
        [InverseProperty(nameof(STPSetup.STPSetupInstructions))]
        public virtual STPSetup FKSetup { get; set; }
        [ForeignKey(nameof(FKSetupTypeInstructionId))]
        [InverseProperty(nameof(STPSetupTypeInstruction.STPSetupInstructions))]
        public virtual STPSetupTypeInstruction FKSetupTypeInstruction { get; set; }
    }
}
