using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class PRFKPIDefinition
    {
        public PRFKPIDefinition()
        {
            PRFKPIValues = new HashSet<PRFKPIValue>();
        }

        [Key]
        public long KPIDefinitionId { get; set; }
        [Required]
        [StringLength(10)]
        public string KPICode { get; set; }
        [Required]
        [StringLength(50)]
        public string KPIName { get; set; }
        [StringLength(100)]
        public string KPIDescription { get; set; }
        [StringLength(400)]
        public string KPIFormula { get; set; }
        [StringLength(50)]
        public string KPIProcedure { get; set; }
        public bool IsWorkOrderBased { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public double MinValue { get; set; }
        public double AlarmTo { get; set; }
        public double WarningTo { get; set; }
        public double MaxValue { get; set; }
        public long FKUnitId { get; set; }
        public GaugeDirection EnumGaugeDirection { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRFKPIValue.FKKPIDefinition))]
        public virtual ICollection<PRFKPIValue> PRFKPIValues { get; set; }
    }
}
