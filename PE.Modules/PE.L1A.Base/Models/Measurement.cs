using System;
using System.Collections.Generic;
using System.Text;

namespace PE.L1A.Base.Models
{
  [Serializable]
  public class Measurement
  {
    public int FeatureCode { get; set; }
    public List<MeasurementSample> Samples { get; set; } = new List<MeasurementSample>();
  }

  [Serializable]
  public class MeasurementSample
  {
    public bool IsValid { get; set; } = true;
    public DateTime MeasurementDate { get; set; }
    public double Value { get; set; }
  }
}
