using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QECompensationType")]
    public partial class QECompensationType
    {
        public QECompensationType()
        {
            QECompensations = new HashSet<QECompensation>();
        }

        [Key]
        public long CompensationTypeId { get; set; }
        [Required]
        [StringLength(400)]
        public string CompensationName { get; set; }
        public double CompensationRatingCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(QECompensation.FKCompensationType))]
        public virtual ICollection<QECompensation> QECompensations { get; set; }
    }
}
