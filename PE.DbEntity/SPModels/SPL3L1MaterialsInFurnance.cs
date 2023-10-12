using System;
using System.Collections.Generic;
using System.Text;

namespace PE.DbEntity.SPModels
{
  public class SPL3L1MaterialsInFurnance
  {
    public short? OrderSeq { get; set; }
    public string RawMaterialName { get; set; }
    public double? Temperature { get; set; }
    public long? WorkOrderId { get; set; }
    public string WorkOrderName { get; set; }
    public string HeatName { get; set; }
    public long? RawMaterialId { get; set; }
  }
}
