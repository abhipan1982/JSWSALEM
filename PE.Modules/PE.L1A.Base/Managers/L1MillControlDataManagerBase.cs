using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using PE.L1A.Base.Providers.Concrete;
using SMF.Core.DC;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.L1A.Base.Managers
{
  
  public class L1MillControlDataManagerBase : IL1MillControlDataManagerBase
  {
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;

    public L1MillControlDataManagerBase(IConfigurationStorageProviderBase configurationStorageProvider)
    {
      ConfigurationStorageProvider = configurationStorageProvider;
    }

    public virtual void Init()
    {
      HandleMillControlDatas(ConfigurationStorageProvider.L1MillControlDatas.Values.ToList());
    }    

    public virtual Task<DataContractBase> SendMillControlDataMessage(DCMillControlMessage dc)
    {
      if (!ConfigurationStorageProvider.L1MillControlDatas.ContainsKey(dc.MillControlDataCode))
        throw new InvalidOperationException($"{MethodHelper.GetMethodName()} MillControlData not found with code: {dc.MillControlDataCode}");

      var millControlDataConfiguration = ConfigurationStorageProvider.L1MillControlDatas[dc.MillControlDataCode];

      if(millControlDataConfiguration.EnumCommChannelType == CommChannelType.Undefined)
        throw new InvalidOperationException($"{MethodHelper.GetMethodName()} MillControlData for code: {dc.MillControlDataCode} has undefined commChannelType");

      if (!ConfigurationStorageProvider.L1MillControlDataProviders.ContainsKey(millControlDataConfiguration.CommAttr1))
        throw new InvalidOperationException($"{MethodHelper.GetMethodName()} MillControlData for code: {dc.MillControlDataCode} has not initialized MillControlDataProvider");

      var millControlDataProvider = ConfigurationStorageProvider.L1MillControlDataProviders[millControlDataConfiguration.CommAttr1];

      millControlDataProvider.SendMillControlDataMessage(dc, millControlDataConfiguration);

      return Task.FromResult(new DataContractBase());
    }

    protected virtual void HandleMillControlDatas(List<L1MillControlDataBase> millControlDatas)
    {
      if (millControlDatas.Any())
      {
        var millControlDatasGroupedByOpcServer = millControlDatas
          .Where(cp => !string.IsNullOrEmpty(cp.CommAttr1))
          .GroupBy(cp => cp.CommAttr1);

        foreach (var opcServerGroup in millControlDatasGroupedByOpcServer)
        {
          try
          {
            var opcProvider = new L1OpcMillControlDataProviderBase(opcServerGroup.Key);

            opcProvider.Init();
            opcProvider.Start();
            ConfigurationStorageProvider.L1MillControlDataProviders.Add(opcServerGroup.Key, opcProvider);
          }
          catch (System.Exception ex)
          {
            NotificationController.LogException(ex, $"Something went wrong while Run initialization for Opc Server: {opcServerGroup.Key}");
            throw;
          }
        }
      }
    }
  }
}
