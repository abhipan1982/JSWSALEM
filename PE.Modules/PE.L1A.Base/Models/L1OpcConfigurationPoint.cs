
namespace PE.L1A.Base.Models
{
  public class OpcConfigurationPoint : ExtendedConfigurationPointBase
  {
    public string OpcServer { get; private set; }
    public string TagUrl { get; private set; }    

    public OpcConfigurationPoint(ConfigurationPointType configurationPointType,
      int featureCode,
      int retentionFactor, 
      long timeOffsetOfSamples,
      string opcServer, 
      string tagUrl)
      : base(configurationPointType, 
        featureCode,
        retentionFactor,
        timeOffsetOfSamples,
        false)
    {
      OpcServer = opcServer;
      TagUrl = tagUrl;      
    }
  }
}
