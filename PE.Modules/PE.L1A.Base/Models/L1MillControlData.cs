
using PE.BaseDbEntity.EnumClasses;

namespace PE.L1A.Base.Models
{
  public class L1MillControlDataBase
  {
    public int Code { get; set; }
    public CommChannelType EnumCommChannelType { get; set; }
    public string CommAttr1 { get; set; }
    public string CommAttr2 { get; set; }
    public string CommAttr3 { get; set; }
  }
}
