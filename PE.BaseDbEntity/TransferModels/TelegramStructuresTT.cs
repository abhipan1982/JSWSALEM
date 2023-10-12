using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("TelegramStructuresTT", Schema = "xfr")]
    public partial class TelegramStructuresTT
    {
        [StringLength(255)]
        public string ParentElementCode { get; set; }
        [StringLength(255)]
        public string ElementCode { get; set; }
        [StringLength(255)]
        public string StructureDescription { get; set; }
        public short? SeqNo { get; set; }
        [StringLength(255)]
        public string StructureSource { get; set; }
        [StringLength(255)]
        public string Prefix { get; set; }
        [StringLength(255)]
        public string Sufix { get; set; }
        [StringLength(255)]
        public string DataType { get; set; }
        [StringLength(255)]
        public string ElementDescription { get; set; }
        [StringLength(255)]
        public string Example { get; set; }
    }
}
