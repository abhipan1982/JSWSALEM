using System;

namespace PE.BaseDbEntity.Models
{
  public class SPQEAliasTable
  {
    public string ResultValue { get; set; }
    public string ResultValueText { get; set; }
    public bool? ResultValueBoolean { get; set; }
    public DateTime? ResultValueTimestamp { get; set; }
    public double? ResultValueNumber { get; set; }
    public double? ResultValueSample { get; set; }
    public double? LengthOffsetFromHead { get; set; }
    public DateTime? TimeOffsetFromHead { get; set; }
    public long? UnitId { get; set; }
  }
}
