using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class DBLog
    {
        [Key]
        public long DBLogId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LogDateTs { get; set; }
        [StringLength(10)]
        public string LogType { get; set; }
        [StringLength(50)]
        public string LogSource { get; set; }
        [StringLength(50)]
        public string LogValue { get; set; }
        [StringLength(1000)]
        public string ErrorMessage { get; set; }
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
