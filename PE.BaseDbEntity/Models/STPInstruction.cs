using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class STPInstruction
    {
        public STPInstruction()
        {
            STPSetupTypeInstructions = new HashSet<STPSetupTypeInstruction>();
        }

        [Key]
        public long InstructionId { get; set; }
        public long FKDataTypeId { get; set; }
        public long FKUnitId { get; set; }
        [Required]
        [StringLength(10)]
        public string InstructionCode { get; set; }
        [Required]
        [StringLength(50)]
        public string InstructionName { get; set; }
        [StringLength(100)]
        public string InstructionDescription { get; set; }
        [StringLength(255)]
        public string DefaultValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKDataTypeId))]
        [InverseProperty(nameof(DBDataType.STPInstructions))]
        public virtual DBDataType FKDataType { get; set; }
        [InverseProperty(nameof(STPSetupTypeInstruction.FKInstruction))]
        public virtual ICollection<STPSetupTypeInstruction> STPSetupTypeInstructions { get; set; }
    }
}
