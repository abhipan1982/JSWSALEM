using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ParameterCode), Name = "UQ_ParameterCode", IsUnique = true)]
    [Index(nameof(ParameterName), Name = "UQ_ParameterName", IsUnique = true)]
    public partial class STPParameter
    {
        public STPParameter()
        {
            STPSetupParameters = new HashSet<STPSetupParameter>();
            STPSetupTypeParameters = new HashSet<STPSetupTypeParameter>();
        }

        [Key]
        public long ParameterId { get; set; }
        [Required]
        [StringLength(10)]
        public string ParameterCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ParameterName { get; set; }
        [StringLength(100)]
        public string ParameterDescription { get; set; }
        [StringLength(20)]
        public string TableName { get; set; }
        [StringLength(20)]
        public string ColumnId { get; set; }
        [StringLength(20)]
        public string ColumnValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(STPSetupParameter.FKParameter))]
        public virtual ICollection<STPSetupParameter> STPSetupParameters { get; set; }
        [InverseProperty(nameof(STPSetupTypeParameter.FKParameter))]
        public virtual ICollection<STPSetupTypeParameter> STPSetupTypeParameters { get; set; }
    }
}
