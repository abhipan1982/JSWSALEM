namespace PE.L1A.Base.Models
{
  public class ExtendedConfigurationPointBase : ConfigurationPointBase
  {
    public int RetentionFactor { get; private set; }
    public long TimeOffsetOfSamples { get; private set; }
    public bool IsResendEnabled { get; private set; }

    public ExtendedConfigurationPointBase(ConfigurationPointType configurationPointType,
      int featureCode,
      int retentionFactor,
      long timeOffsetOfSamples,
      bool isResendEnabled)
      : base(configurationPointType, featureCode)
    {
      RetentionFactor = retentionFactor;
      TimeOffsetOfSamples = timeOffsetOfSamples;
      IsResendEnabled = isResendEnabled;
    }
  }

}
