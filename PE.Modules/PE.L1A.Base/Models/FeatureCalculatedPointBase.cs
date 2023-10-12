using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;

namespace PE.L1A.Base.Models
{
  public class FeatureCalculatedPointBase
  {
    /// <summary>
    /// FeatureCode of calculated feature
    /// </summary>
    public int CalculatedFeatureCode { get; set; }

    /// <summary>
    /// Value with which feature will be triggered
    /// </summary>
    public int CalculatedValue { get; set; }

    /// <summary>
    /// If virtual - signal will not be populated to tracking - just will be used for another calculated feature
    /// </summary>
    public bool IsVirtual { get; set; }

    /// <summary>
    /// Sequence inside the same calculated feature
    /// </summary>
    public short Seq { get; set; }

    /// <summary>
    /// IsResendEnabled flag of Feature
    /// </summary>
    public bool IsResendEnabled { get; set; }

    /// <summary>
    /// FeatureCode 1 which value will be a part of logic
    /// </summary>
    public int FeatureCode_1 { get; set; }

    /// <summary>
    /// Value which will be used for compare operator with Feature_1 value
    /// </summary>
    public int Value_1 { get; set; }

    /// <summary>
    /// Compare operator which will be used to compare Value_1 with Feature_1 value
    /// </summary>
    public CompareOperator EnumCompareOperator_1 { get; set; }

    /// <summary>
    /// Logical operator if additional condition will be needed
    /// </summary>
    public LogicalOperator EnumLogicalOperator { get; set; }

    /// <summary>
    /// If EnumLogicalOperator is not NONE -> Feature_2 which will be used for second condition
    /// </summary>
    public int? FeatureCode_2 { get; set; }

    /// <summary>
    /// If EnumLogicalOperator is not NONE -> Value which will be used for compare operator with Feature_2 value
    /// </summary>
    public int? Value_2 { get; set; }

    /// <summary>
    /// If EnumLogicalOperator is not NONE -> Compare operator which will be used to compare Value_2 with Feature_2 value
    /// </summary>
    public CompareOperator EnumCompareOperator_2 { get; set; }

    /// <summary>
    /// Time filter in which conditions must be satisfied(int milliseconds)
    /// </summary>
    public double? TimeFilter { get; set; }

    /// <summary>
    /// Logical operator if additional condition will be needed(next sequence)
    /// </summary>
    public LogicalOperator EnumLogicalOperator_ForNextSequence { get; set; }
  }
}
