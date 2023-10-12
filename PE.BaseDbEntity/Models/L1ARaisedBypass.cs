using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class L1ARaisedBypass
    {
        [Key]
        public long RaisedBypassId { get; set; }
        [Required]
        [StringLength(250)]
        public string BypassTypeName { get; set; }
        [Required]
        [StringLength(1000)]
        public string BypassName { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcServerAddress { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcServerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        public bool Value { get; set; }
    }
}
