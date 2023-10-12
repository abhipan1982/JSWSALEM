using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QEMappingEntry")]
    public partial class QEMappingEntry
    {
        public QEMappingEntry()
        {
            QEMappingValues = new HashSet<QEMappingValue>();
        }

        [Key]
        public long MappingEntryId { get; set; }
        [Required]
        [StringLength(255)]
        public string SignalIdentifier { get; set; }
        [Required]
        [StringLength(400)]
        public string RulesIdentifier { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart1 { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart2 { get; set; }
        [StringLength(50)]
        public string RulesIdentifierPart3 { get; set; }
        public QEDirection EnumQEDirection { get; set; }
        public QEParamType EnumQEParamType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(QEMappingValue.FKMappingEntry))]
        public virtual ICollection<QEMappingValue> QEMappingValues { get; set; }
    }
}
