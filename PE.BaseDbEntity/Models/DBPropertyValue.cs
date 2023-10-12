using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKPropertyId), Name = "NCI_PropertyId")]
    public partial class DBPropertyValue
    {
        [Key]
        public long PropertyValueId { get; set; }
        public long FKPropertyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Value { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKPropertyId))]
        [InverseProperty(nameof(DBProperty.DBPropertyValues))]
        public virtual DBProperty FKProperty { get; set; }
    }
}
