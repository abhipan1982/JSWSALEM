using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("QERating")]
    public partial class QERating
    {
        public QERating()
        {
            QECompensations = new HashSet<QECompensation>();
            QEMappingValues = new HashSet<QEMappingValue>();
            QERootCauses = new HashSet<QERootCause>();
        }

        [Key]
        public long RatingId { get; set; }
        public double? RatingCode { get; set; }
        public double? RatingValue { get; set; }
        public double? RatingValueForced { get; set; }
        [StringLength(400)]
        public string RatingAlarm { get; set; }
        public double? RatingAffectedArea { get; set; }
        public int? RatingGroup { get; set; }
        public int? RatingType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RatingModified { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(QECompensation.FKRating))]
        public virtual ICollection<QECompensation> QECompensations { get; set; }
        [InverseProperty(nameof(QEMappingValue.FKRating))]
        public virtual ICollection<QEMappingValue> QEMappingValues { get; set; }
        [InverseProperty(nameof(QERootCause.FKRating))]
        public virtual ICollection<QERootCause> QERootCauses { get; set; }
    }
}
