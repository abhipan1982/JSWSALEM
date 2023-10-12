using System;

namespace PE.L1A.Base.Models
{
  public class FeatureMeasurement : MeasurementDto
  { 
    public DateTime ExpiryDateTime { get; set; }

    public FeatureMeasurement(int featureCode, DateTime expiryDateTime, DateTime measurementDateTime, double value)
      : base(featureCode, measurementDateTime, value)
    {
      ExpiryDateTime = expiryDateTime;
    }
  }
  [Serializable]
  public class MeasurementDto
  {
    public MeasurementDto(int featureCode, DateTime measurementDateTime, double value)
    {
      FeatureCode = featureCode;
      MeasurementDateTime = measurementDateTime;
      Value = value;
    }

    public int FeatureCode { get; set; }
    public DateTime MeasurementDateTime { get; set; }
    public double Value { get; set; }
  }
}
