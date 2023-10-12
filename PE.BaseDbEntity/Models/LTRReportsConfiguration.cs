using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("LTRReportsConfiguration")]
    public partial class LTRReportsConfiguration
    {
        [Key]
        public long ReportId { get; set; }
        [StringLength(20)]
        public string ReportName { get; set; }
        [StringLength(100)]
        public string ServerURL { get; set; }
        [StringLength(100)]
        public string Login { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(100)]
        public string ReportPath { get; set; }
        [StringLength(20)]
        public string ReportFormat { get; set; }
        [StringLength(200)]
        public string OutputPath { get; set; }
        [StringLength(10)]
        public string Extension { get; set; }
        [StringLength(100)]
        public string DefaultParamValue { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
    }
}
