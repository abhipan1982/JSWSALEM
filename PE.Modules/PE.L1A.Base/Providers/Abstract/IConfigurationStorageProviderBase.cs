using System.Collections.Concurrent;
using System.Collections.Generic;
using PE.L1A.Base.Models;

namespace PE.L1A.Base.Providers.Abstract
{
  public interface IConfigurationStorageProviderBase
  {
    bool IsInitialized { get; set; }
    Dictionary<int, ConfigurationPointBase> L1ConfigurationPoints { get; set; }
    Dictionary<int, L1MillControlDataBase> L1MillControlDatas { get; set; }
    Dictionary<int, List<FeatureConfigurationPointBase>> FeatureConfigurationPointsByParentFeatureCode { get; set; }
    Dictionary<int, FeatureConfigurationPointBase> FeatureConfigurationPointDictionary { get; set; }
    Dictionary<string, IL1SignalProviderBase> L1SignalProviders { get; set; }
    Dictionary<string, IL1MillControlDataProviderBase> L1MillControlDataProviders { get; set; }
    List<FeatureCalculatedPointBase> CalculatedFeatures { get; set; }
  }
}
