using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupStructure
    {
        public long? OrderSeq { get; set; }
        public long SetupTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SetupTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        public short STOrderSeq { get; set; }
        public bool IsRequiredSetup { get; set; }
        public short? STIOrderSeq { get; set; }
        public long? InstructionId { get; set; }
        [StringLength(10)]
        public string InstructionCode { get; set; }
        [StringLength(50)]
        public string InstructionName { get; set; }
        [StringLength(100)]
        public string InstructionDescription { get; set; }
        public bool? IsRequiredInstruction { get; set; }
        [StringLength(255)]
        public string DefaultValue { get; set; }
        public double? RangeFrom { get; set; }
        public double? RangeTo { get; set; }
        public long? SetupTypeInstructionId { get; set; }
        public long? UnitId { get; set; }
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        public long? DataTypeId { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
        public bool? IsSentToL1 { get; set; }
        public long? AssetId { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        public short? Steelgrade { get; set; }
        [Column("Product Size")]
        public short? Product_Size { get; set; }
        [Column("Work Order")]
        public short? Work_Order { get; set; }
        [Column("Heat Number")]
        public short? Heat_Number { get; set; }
        public short? Layout { get; set; }
        public short? Issue { get; set; }
        [Column("Previous Steelgrade")]
        public short? Previous_Steelgrade { get; set; }
        [Column("Previous Product Size")]
        public short? Previous_Product_Size { get; set; }
        [Column("Previous Layout")]
        public short? Previous_Layout { get; set; }
        [Required]
        [StringLength(70)]
        public string StpInstrKey { get; set; }
    }
}
