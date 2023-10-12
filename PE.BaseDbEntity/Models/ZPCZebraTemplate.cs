using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(ZebraTemplateCode), Name = "UQ_ZebraTemplateCode", IsUnique = true)]
    public partial class ZPCZebraTemplate
    {
        [Key]
        public long ZebraTemplateId { get; set; }
        [Required]
        [StringLength(10)]
        public string ZebraTemplateCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ZebraTemplateName { get; set; }
        public string ZebraTemplate { get; set; }
        public bool IsDefault { get; set; }
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
