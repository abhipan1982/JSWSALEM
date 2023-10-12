using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using PE.L1A.Base.Providers.Concrete;

namespace PE.L1A.Base.Providers.Abstract
{
  public interface IBypassStorageProviderBase
  {
    IReadOnlyList<BypassConfigurationDto> BypassConfigurations { get; }
    IReadOnlyList<BypassInstanceDto> BypassInstances { get; }
    IReadOnlyDictionary<string, L1OpcBypassProviderBase> L1OpcBypassProviders { get;}

    void SetBypassConfigurations(List<BypassConfigurationDto> configurations);
    void ClearBypassConfigurations();
    
    void SetBypassInstances(List<BypassInstanceDto> instances);
    void ClearBypassInstances();
    void AddL1OpcBypassProvider(string key, L1OpcBypassProviderBase provider);
    void RemoveBypassProviders();
    L1OpcBypassProviderBase CreateL1OpcBypassProvider(string opcServerAddress, List<BypassInstanceDto> bypassInstances, Action<bool, DateTime, BypassInstanceDto> processBoolBypass, Action<ushort, DateTime, BypassInstanceDto> processWord16Bypass);
  }
}
