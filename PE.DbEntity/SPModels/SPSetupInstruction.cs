using System.ComponentModel.DataAnnotations.Schema;

namespace PE.DbEntity.SPModels
{
  public class SPSetupInstruction
  {
    public long? Seq { get; set; }
    public long SetupTypeId { get; set; }
    public string SetupTypeCode { get; set; }
    public string SetupTypeName { get; set; }
    public short OrderSeqSetup { get; set; }
    public bool IsRequiredSetup { get; set; }
    public long SetupId { get; set; }
    public string SetupCode { get; set; }
    public string SetupName { get; set; }
    public long SetupTypeInstructionId { get; set; }
    public long SetupInstructionId { get; set; }
    public short OrderSeq { get; set; }
    public long InstructionId { get; set; }
    public string InstructionCode { get; set; }
    public string InstructionName { get; set; }
    public string InstructionDescription { get; set; }
    public string InstructionDefaultValue { get; set; }
    public bool IsRequired { get; set; }
    public string Value { get; set; }
    public double? RangeFrom { get; set; }
    public double? RangeTo { get; set; }
    public long UnitId { get; set; }
    public string UnitSymbol { get; set; }
    public long DataTypeId { get; set; }
    public string DataType { get; set; }
    public string DataTypeNameDotNet { get; set; }
    public bool IsSentToL1 { get; set; }
    public long? AssetId { get; set; }
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
