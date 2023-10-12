using System;

namespace PE.BaseDbEntity.Models
{
  public class SPCalculatedKPI
  {
    public long KPIDefinitionId { get; set; }
    public DateTime KPITime { get; set; }
    public double KPIValue { get; set; }
  }
}
