using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupValuesToL1
    {
        public long? OrderSeq { get; set; }
        public short STOrderSeq { get; set; }
        public long SetupId { get; set; }
        public short STIOrderSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string InstructionName { get; set; }
        public long SetupInstructionId { get; set; }
        [StringLength(255)]
        public string Value { get; set; }
        [StringLength(50)]
        public string DataTypeNameDotNet { get; set; }
    }
}
