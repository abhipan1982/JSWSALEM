using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKSetupTypeId), nameof(FKParameterId), Name = "UQ_SetupTypeId_ParameterId", IsUnique = true)]
    public partial class STPSetupTypeParameter
    {
        [Key]
        public long SetupTypeParameterId { get; set; }
        public long FKSetupTypeId { get; set; }
        public long FKParameterId { get; set; }
        [StringLength(100)]
        public string DefaultParameterValue { get; set; }
        public bool DefaultIsRequired { get; set; }
        public short? OrderSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKParameterId))]
        [InverseProperty(nameof(STPParameter.STPSetupTypeParameters))]
        public virtual STPParameter FKParameter { get; set; }
        [ForeignKey(nameof(FKSetupTypeId))]
        [InverseProperty(nameof(STPSetupType.STPSetupTypeParameters))]
        public virtual STPSetupType FKSetupType { get; set; }
    }
}
