using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKDataTypeId), Name = "NCI_DataTypeId")]
    public partial class DBProperty
    {
        public DBProperty()
        {
            DBPropertyValues = new HashSet<DBPropertyValue>();
        }

        [Key]
        public long PropertyId { get; set; }
        public long FKDataTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string PropertyCode { get; set; }
        [Required]
        [StringLength(50)]
        public string PropertyName { get; set; }
        [StringLength(100)]
        public string PropertyDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKDataTypeId))]
        [InverseProperty(nameof(DBDataType.DBProperties))]
        public virtual DBDataType FKDataType { get; set; }
        [InverseProperty(nameof(DBPropertyValue.FKProperty))]
        public virtual ICollection<DBPropertyValue> DBPropertyValues { get; set; }
    }
}
