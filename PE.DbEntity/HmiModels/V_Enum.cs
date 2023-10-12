using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Enum
    {
        public long? OrderSeq { get; set; }
        public long EnumNameId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string EnumName { get; set; }
        public bool EnumNameIsCustom { get; set; }
        public long EnumValue { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string EnumKeyword { get; set; }
        public bool EnumKeywordIsCustom { get; set; }
    }
}
