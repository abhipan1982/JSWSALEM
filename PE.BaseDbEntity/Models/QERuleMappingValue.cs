using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QERuleMappingValue")]
    public partial class QERuleMappingValue
    {
        public QERuleMappingValue()
        {
            QEMappingValues = new HashSet<QEMappingValue>();
        }

        [Key]
        public long RuleMappingValueId { get; set; }
        public long? FKTriggerId { get; set; }
        public long? FKRawMaterialId { get; set; }
        [StringLength(400)]
        public string RuleMappingValueInfo { get; set; }
        public QEEvalExecutionStatus EnumQEEvalExecutionStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.QERuleMappingValues))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
        [ForeignKey(nameof(FKTriggerId))]
        [InverseProperty(nameof(QETrigger.QERuleMappingValues))]
        public virtual QETrigger FKTrigger { get; set; }
        [InverseProperty(nameof(QEMappingValue.FKRuleMappingValue))]
        public virtual ICollection<QEMappingValue> QEMappingValues { get; set; }
    }
}
