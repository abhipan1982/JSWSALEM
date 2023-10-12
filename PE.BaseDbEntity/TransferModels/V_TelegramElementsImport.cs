using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    public partial class V_TelegramElementsImport
    {
        public long? FKDatatypeId { get; set; }
        public short? ByteLength { get; set; }
        public double? ByteOffset { get; set; }
        public double? Bytes { get; set; }
        [StringLength(255)]
        public string ElementCode { get; set; }
        [StringLength(255)]
        public string ElementName { get; set; }
        [StringLength(255)]
        public string ElementDescription { get; set; }
        public bool? IsStructure { get; set; }
        public short? IsStructureOrig { get; set; }
        public long? TelegramElementId { get; set; }
        public bool? IsNew { get; set; }
        [StringLength(255)]
        public string Example { get; set; }
    }
}
