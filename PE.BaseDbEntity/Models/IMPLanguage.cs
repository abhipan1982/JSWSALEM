using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Keyless]
    public partial class IMPLanguage
    {
        [StringLength(255)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Language { get; set; }
        [StringLength(255)]
        public string flag { get; set; }
    }
}
