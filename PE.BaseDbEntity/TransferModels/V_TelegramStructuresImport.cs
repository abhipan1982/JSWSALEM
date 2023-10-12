using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    public partial class V_TelegramStructuresImport
    {
        public long? Sort { get; set; }
        public long? FKTelegramElementId { get; set; }
        public long? FKParentTelegramElementId { get; set; }
        public short? SeqNo { get; set; }
        [StringLength(255)]
        public string StructureCode { get; set; }
        [StringLength(255)]
        public string StructureName { get; set; }
        [StringLength(255)]
        public string StructureDescription { get; set; }
        [StringLength(255)]
        public string StructureSource { get; set; }
        [StringLength(255)]
        public string Prefix { get; set; }
        [StringLength(255)]
        public string Sufix { get; set; }
        public long? TelegramStructureId { get; set; }
        public bool? IsNew { get; set; }
        [StringLength(255)]
        public string Example { get; set; }
    }
}
