using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    public partial class DBDatabase
    {
        public int ServerId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string ServerName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string ServerAddress { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DatabaseName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string DatabaseSchema { get; set; }
        [Required]
        [StringLength(50)]
        public string DatabaseDescription { get; set; }
        public bool IsWarehouse { get; set; }
        [StringLength(255)]
        public string ConnectionString { get; set; }
    }
}
