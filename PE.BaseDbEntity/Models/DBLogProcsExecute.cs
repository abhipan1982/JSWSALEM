using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    [Table("DBLogProcsExecute")]
    public partial class DBLogProcsExecute
    {
        [Required]
        [StringLength(50)]
        public string ProcName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FirstExecutionTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastExecutionTime { get; set; }
    }
}
