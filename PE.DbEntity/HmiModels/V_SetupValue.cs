using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupValue
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
        public long SetupId { get; set; }
        [StringLength(10)]
        public string SetupCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupName { get; set; }
        public long SetupTypeInstructionId { get; set; }
        public long SetupInstructionId { get; set; }
        public short STIOrderSeq { get; set; }
        public long InstructionId { get; set; }
        [Required]
        [StringLength(10)]
        public string InstructionCode { get; set; }
        [Required]
        [StringLength(50)]
        public string InstructionName { get; set; }
        [StringLength(100)]
        public string InstructionDescription { get; set; }
        [StringLength(255)]
        public string InstructionDefaultValue { get; set; }
        public bool IsRequired { get; set; }
        [StringLength(255)]
        public string Value { get; set; }
        public double? RangeFrom { get; set; }
        public double? RangeTo { get; set; }
        public long UnitId { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        public long DataTypeId { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
        [StringLength(50)]
        public string DataTypeNameDotNet { get; set; }
        public bool IsSentToL1 { get; set; }
        public long? AssetId { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
        public long? Steelgrade { get; set; }
        [Column("Product Size")]
        public long? Product_Size { get; set; }
        [Column("Work Order")]
        public long? Work_Order { get; set; }
        [Column("Heat Number")]
        public long? Heat_Number { get; set; }
        public long? Layout { get; set; }
        public long? Issue { get; set; }
        [Column("Previous Steelgrade")]
        public long? Previous_Steelgrade { get; set; }
        [Column("Previous Product Size")]
        public long? Previous_Product_Size { get; set; }
        [Column("Previous Layout")]
        public long? Previous_Layout { get; set; }
    }
}
