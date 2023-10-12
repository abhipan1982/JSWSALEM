using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCRuleEvaluation : DataContractBase
  {
    [DataMember] public string MaterialName { get; set; }
    [DataMember] public string TriggerName { get; set; }
    [DataMember] public DCRuleValues Values { get; set; }
  }
  public class DCRuleValues
  {
    [DataMember] public string Info { get; set; }
    [DataMember] public QEEvalExecutionStatus Status { get; set; }
    [DataMember] public List<DCMappingValue> RuleMappingValues { get; set; }
  }
  public class DCMappingValue
  {
    [DataMember] public DCMappingEntry Mapping { get; set; }
    [DataMember] public string RulesObjectValue { get; set; }
    [DataMember] public double NumValue { get; set; }
    [DataMember] public string TextValue { get; set; }
    [DataMember] public bool BooleanValue { get; set; }
    [DataMember] public long TimestampValue { get; set; }
    [DataMember] public DCRating RatingValue { get; set; }
    [DataMember] public List<DCLengthSeriesValue> LengthSeriesValue { get; set; }
    [DataMember] public List<DCTimeSeriesValue> TimeSeriesValue { get; set; }
  }
  public class DCLengthSeriesValue
  {
    [DataMember] public double LengthPosition { get; set; }
    [DataMember] public double Value { get; set; }
  }
  public class DCTimeSeriesValue
  {
    [DataMember] public long Time { get; set; }
    [DataMember] public double Value { get; set; }
  }
  public class DCRating
  {
    [DataMember] public double Code { get; set; }
    [DataMember] public double Value { get; set; }
    [DataMember] public string Alarm { get; set; }
    [DataMember] public double AffectedArea { get; set; }
    [DataMember] public int RatingGroup { get; set; }
    [DataMember] public int RatingType { get; set; }
    [DataMember] public List<DCRootCause> RootCauses { get; set; }
    [DataMember] public List<DCCompensation> Compensations { get; set; }
  }
  public class DCRootCause
  {
    [DataMember] public string Name { get; set; }
    [DataMember] public int Type { get; set; }
    [DataMember] public double Priority { get; set; }
    [DataMember] public string Info { get; set; }
    [DataMember] public string Verification { get; set; }
    [DataMember] public string Correction { get; set; }
    [DataMember] public List<int> Aggregates { get; set; }
  }
  public class DCCompensation
  {
    [DataMember] public string Name { get; set; }
    [DataMember] public DCCompensationType CompensationType { get; set; }
    [DataMember] public double Alternative { get; set; }
    [DataMember] public string Info { get; set; }
    [DataMember] public string Detail { get; set; }
    [DataMember] public List<int> Aggregates { get; set; }
  }
  public class DCCompensationType
  {
    [DataMember] public int Id { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public double RatingCode { get; set; }
  }
}
