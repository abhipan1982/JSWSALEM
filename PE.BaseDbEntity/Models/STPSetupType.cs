using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(OrderSeq), Name = "UQ_OrderSeq", IsUnique = true)]
    [Index(nameof(SetupTypeCode), Name = "UQ_SetupTypeCode", IsUnique = true)]
    [Index(nameof(SetupTypeName), Name = "UQ_SetupTypeName", IsUnique = true)]
    public partial class STPSetupType
    {
        public STPSetupType()
        {
            STPSetupTypeInstructions = new HashSet<STPSetupTypeInstruction>();
            STPSetupTypeParameters = new HashSet<STPSetupTypeParameter>();
            STPSetups = new HashSet<STPSetup>();
        }

        [Key]
        public long SetupTypeId { get; set; }
        public short OrderSeq { get; set; }
        [Required]
        [StringLength(10)]
        public string SetupTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        [StringLength(100)]
        public string SetupTypeDescription { get; set; }
        [Required]
        public bool? IsRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsSteelgradeRelated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(STPSetupTypeInstruction.FKSetupType))]
        public virtual ICollection<STPSetupTypeInstruction> STPSetupTypeInstructions { get; set; }
        [InverseProperty(nameof(STPSetupTypeParameter.FKSetupType))]
        public virtual ICollection<STPSetupTypeParameter> STPSetupTypeParameters { get; set; }
        [InverseProperty(nameof(STPSetup.FKSetupType))]
        public virtual ICollection<STPSetup> STPSetups { get; set; }
    }
}
