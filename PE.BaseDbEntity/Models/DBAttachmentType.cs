using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class DBAttachmentType
    {
        [Key]
        public long AttachmentTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string AttachmentTypeName { get; set; }
        [Required]
        [StringLength(10)]
        public string Extension { get; set; }
    }
}
