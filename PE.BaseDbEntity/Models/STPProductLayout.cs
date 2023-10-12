using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class STPProductLayout
    {
        [Key]
        public long ProductLayoutId { get; set; }
        public long FKProductCatalogueId { get; set; }
        public short FKLayoutId { get; set; }
        public short FKIssueId { get; set; }
        public double ExitSpeed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKIssueId))]
        [InverseProperty(nameof(STPIssue.STPProductLayouts))]
        public virtual STPIssue FKIssue { get; set; }
        [ForeignKey(nameof(FKLayoutId))]
        [InverseProperty(nameof(STPLayout.STPProductLayouts))]
        public virtual STPLayout FKLayout { get; set; }
        [ForeignKey(nameof(FKProductCatalogueId))]
        [InverseProperty(nameof(PRMProductCatalogue.STPProductLayouts))]
        public virtual PRMProductCatalogue FKProductCatalogue { get; set; }
    }
}
