using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("TelegramElementsTT", Schema = "xfr")]
    public partial class TelegramElementsTT
    {
        [StringLength(255)]
        public string ElementCode { get; set; }
        [StringLength(255)]
        public string DataType { get; set; }
        public double? ByteLength { get; set; }
        [StringLength(255)]
        public string ElementDescription { get; set; }
        public short? IsStructure { get; set; }
        [StringLength(255)]
        public string IfStructure { get; set; }
        public short? IsDefined { get; set; }
        [StringLength(255)]
        public string Example { get; set; }
    }
}
