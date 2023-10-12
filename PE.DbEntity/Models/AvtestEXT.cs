using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.Models
{
    [Keyless]
    [Table("AvtestEXT", Schema = "prj")]
    public partial class AvtestEXT
    {
        public long MaId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDC { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDedT { get; set; }
        [StringLength(255)]
        public string AUDBy { get; set; }
        public bool IsBefommit { get; set; }
        public bool IsDeld { get; set; }
    }
}
