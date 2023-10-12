using System;

namespace PE.L1A.Base.Models
{
  public class TrackingPointSignalBase : ConfigurationPointBase
  {
    public int Value { get; private set; }
    public DateTime SourceTimestamp { get; private set; }
    public DateTime ProcessExpertTimestamp { get; private set; }
    public bool IsResendEnabled { get; private set; }

    public TrackingPointSignalBase(int featureCode, int value, 
      DateTime sourceTimestamp, DateTime processExpertTimestamp, 
      bool isResendEnabled)
      : base(ConfigurationPointType.TrackingPoint, featureCode)
    {
      Value = value; 
      SourceTimestamp = sourceTimestamp; 
      ProcessExpertTimestamp = processExpertTimestamp;
      IsResendEnabled = isResendEnabled;
    }
  }

}
