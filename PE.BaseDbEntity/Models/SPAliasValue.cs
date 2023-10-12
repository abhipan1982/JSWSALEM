using System;

namespace PE.BaseDbEntity.Models
{
  public class SPAliasValue
  {
    public string ResultSet { get; set; }
    public string ResultValue { get; set; }
    public double ResultValueNumber { get; set; }
    public bool ResultValueBoolean { get; set; }
    public DateTime ResultValueTimestamp { get; set; }
    public string ResultValueText { get; set; }
    public string ErrorText { get; set; }
    public int ErrorCode { get; set; }
    public int RecordsCount { get; set; }
  }
}
