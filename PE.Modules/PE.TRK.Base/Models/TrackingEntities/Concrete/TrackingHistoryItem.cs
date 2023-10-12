using System;
using System.Collections.Generic;
using System.Text;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  [Serializable]
  public class TrackingHistoryItem
  {
    public int AreaCode { get; set; }
    public TrackingHistoryTypeEnum TrackingHistoryType { get; set; }
    public DateTime Date { get; set; }
  }

  public enum TrackingHistoryTypeEnum
  {
    Charge,
    Discharge
  }
}
