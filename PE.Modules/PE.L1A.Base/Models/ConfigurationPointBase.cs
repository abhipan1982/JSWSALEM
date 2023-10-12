namespace PE.L1A.Base.Models
{
  public class ConfigurationPointBase
  {
    public int FeatureCode { get; private set; }
    public ConfigurationPointType ConfigurationPointType { get; private set; }

    public ConfigurationPointBase(ConfigurationPointType configurationPointType, 
      int featureCode)
    {
      FeatureCode = featureCode;
      ConfigurationPointType = configurationPointType;
    }
  }
}
