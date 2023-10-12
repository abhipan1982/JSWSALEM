using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Concrete
{
  public class BypassStorageProviderBase : IBypassStorageProviderBase
  {
    private List<BypassConfigurationDto> _bypassConfigurations = new List<BypassConfigurationDto>();
    private List<BypassInstanceDto> _bypassInstances = new List<BypassInstanceDto>();
    private Dictionary<string, L1OpcBypassProviderBase> _l1OpcBypassProviders = new Dictionary<string, L1OpcBypassProviderBase>();

    public IReadOnlyList<BypassConfigurationDto> BypassConfigurations => _bypassConfigurations;
    public IReadOnlyList<BypassInstanceDto> BypassInstances => _bypassInstances;
    public IReadOnlyDictionary<string, L1OpcBypassProviderBase> L1OpcBypassProviders => _l1OpcBypassProviders;

    public void SetBypassConfigurations(List<BypassConfigurationDto> configurations)
    {
      _bypassConfigurations = configurations;
    }

    public void ClearBypassConfigurations()
    {
      _bypassConfigurations = new List<BypassConfigurationDto>();
    }
    
    public void SetBypassInstances(List<BypassInstanceDto> instances)
    {
      _bypassInstances = instances;
    }

    public void ClearBypassInstances()
    {
      _bypassInstances = new List<BypassInstanceDto>();
    }

    public void AddL1OpcBypassProvider(string key, L1OpcBypassProviderBase provider)
    {
      if(L1OpcBypassProviders.ContainsKey(key))
      {
        NotificationController.Error($"BypassStorageProvider already contains BypassProvider with key: {key}");
      }
      else
      {
        _l1OpcBypassProviders.Add(key, provider);
        NotificationController.Warn($"Added BypassProvider with key: {key}");
      }
    }

    public virtual void RemoveBypassProviders()
    {
      foreach (var item in _l1OpcBypassProviders)
      {
        try
        {
          item.Value.Stop();

          _l1OpcBypassProviders.Remove(item.Key);
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex, $"Something went wrong while Stop and Remove provider: {item.Key}");
        }
      }
    }

    public virtual L1OpcBypassProviderBase CreateL1OpcBypassProvider(string opcServerAddress, List<BypassInstanceDto> bypassInstances, Action<bool, DateTime, BypassInstanceDto> processBoolBypass, Action<ushort, DateTime, BypassInstanceDto> processWord16Bypass)
    {
      return new L1OpcBypassProviderBase(opcServerAddress, bypassInstances, processBoolBypass, processWord16Bypass);    
    }
  }
}
