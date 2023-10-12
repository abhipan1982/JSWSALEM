using System.Collections.Concurrent;
using System.Collections.Generic;
using PE.BaseInterfaces.Managers.L1A;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;

namespace PE.L1A.Base.Providers.Concrete
{
  public class ConfigurationStorageProviderBase : IConfigurationStorageProviderBase
  {
    public bool IsInitialized { get; set; }

    public Dictionary<int, ConfigurationPointBase> L1ConfigurationPoints { get; set; } 
      = new Dictionary<int, ConfigurationPointBase>();

    public Dictionary<int, L1MillControlDataBase> L1MillControlDatas { get; set; } 
      = new Dictionary<int, L1MillControlDataBase>();

    public Dictionary<int, List<FeatureConfigurationPointBase>> FeatureConfigurationPointsByParentFeatureCode { get; set; } 
      = new Dictionary<int, List<FeatureConfigurationPointBase>>();

    public Dictionary<string, IL1SignalProviderBase> L1SignalProviders { get; set; } 
      = new Dictionary<string, IL1SignalProviderBase>();

    public Dictionary<string, IL1MillControlDataProviderBase> L1MillControlDataProviders { get; set; } 
      = new Dictionary<string, IL1MillControlDataProviderBase>();

    public Dictionary<int, FeatureConfigurationPointBase> FeatureConfigurationPointDictionary { get; set; }
      = new Dictionary<int, FeatureConfigurationPointBase>();

    public List<FeatureCalculatedPointBase> CalculatedFeatures { get; set; } 
      = new List<FeatureCalculatedPointBase>();
  }
}
