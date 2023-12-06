using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_TelegramValue
    {
        public long FakeIndex { get; set; }
        [StringLength(4000)]
        public string TelegramStructureIndex { get; set; }
        public long? ElementId { get; set; }
        public long? ParentElementId { get; set; }
        [Required]
        [StringLength(20)]
        public string ElementCode { get; set; }
        public long DataTypeId { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
        [StringLength(50)]
        public string DataTypeNameDotNet { get; set; }
        public long? TelegramStructureId { get; set; }
        public long? RootId { get; set; }
        public bool? IsRoot { get; set; }
        public bool IsStructure { get; set; }
        public bool? IsHeader { get; set; }
        [StringLength(100)]
        public string StructureGraph { get; set; }
        [StringLength(100)]
        public string StructurePath { get; set; }
        [StringLength(20)]
        public string StructureCode { get; set; }
        public int? StructureLevel { get; set; }
        [StringLength(100)]
        public string OrderSeq { get; set; }
        public long Sort { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        public long? TelegramId { get; set; }
        [StringLength(20)]
        public string TelegramCode { get; set; }
        public short? Id { get; set; }
    }
}
