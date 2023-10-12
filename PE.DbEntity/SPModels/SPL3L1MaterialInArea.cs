using System;
using System.Collections.Generic;
using System.Text;

namespace PE.DbEntity.SPModels
{
  public class SPL3L1MaterialInArea
  {
    public short OrderSeq { get; set; }
    public int AreaCode { get; set; }
    public long? RawMaterialId { get; set; }
    public string RawMaterialName { get; set; }
    public double? LastWeight { get; set; }
    public double? LastLength { get; set; }
    public long? WorkOrderId { get; set; }
    public string WorkOrderName { get; set; }
    public long? SteelgradeId { get; set; }
    public string SteelgradeCode { get; set; }
    public long? HeatId { get; set; }
    public string HeatName { get; set; }
    
  }
}
