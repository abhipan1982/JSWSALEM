using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(AliasName), Name = "UQ_AliasName", IsUnique = true)]
    public partial class QEAlias
    {
        public QEAlias()
        {
            QEAliasValues = new HashSet<QEAliasValue>();
        }

        [Key]
        public long AliasId { get; set; }
        [Required]
        [StringLength(50)]
        public string AliasName { get; set; }
        [StringLength(200)]
        public string AliasDescription { get; set; }
        public QESignalType EnumQESignalType { get; set; }
        [StringLength(2000)]
        public string StaticValue { get; set; }
        [StringLength(4000)]
        public string SQLQuery { get; set; }
        [StringLength(50)]
        public string TableName { get; set; }
        [StringLength(50)]
        public string ColumnName { get; set; }
        [StringLength(10)]
        public string Aggregation { get; set; }
        [StringLength(400)]
        public string WhereClause { get; set; }
        [StringLength(50)]
        public string ColumnId { get; set; }
        public double? LimitMin { get; set; }
        public double? LimitMax { get; set; }
        public long? FKUnitId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(QEAliasValue.FKAlias))]
        public virtual ICollection<QEAliasValue> QEAliasValues { get; set; }
    }
}
