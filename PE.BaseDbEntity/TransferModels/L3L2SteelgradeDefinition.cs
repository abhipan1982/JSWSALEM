using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Table("L3L2SteelgradeDefinition", Schema = "xfr")]
    public partial class L3L2SteelgradeDefinition
    {
        [Key]
        public long CounterId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        public bool IsUpdated { get; set; }
        public short CommStatus { get; set; }
        [StringLength(400)]
        public string CommMessage { get; set; }
        [StringLength(400)]
        public string ValidationCheck { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeDescription { get; set; }
        [StringLength(10)]
        public string ScrapGroupCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedTs { get; set; }
    }
}
