using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ParameterValueId), Name = "NCI_ParameterValueId")]
    [Index(nameof(FKSetupId), nameof(FKParameterId), Name = "UQ_SetupId_ParameterId", IsUnique = true)]
    public partial class STPSetupParameter
    {
        [Key]
        public long SetupParameterId { get; set; }
        public long FKSetupId { get; set; }
        public long FKParameterId { get; set; }
        public long ParameterValueId { get; set; }
        [Required]
        public bool? IsRequired { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKParameterId))]
        [InverseProperty(nameof(STPParameter.STPSetupParameters))]
        public virtual STPParameter FKParameter { get; set; }
        [ForeignKey(nameof(FKSetupId))]
        [InverseProperty(nameof(STPSetup.STPSetupParameters))]
        public virtual STPSetup FKSetup { get; set; }
    }
}
