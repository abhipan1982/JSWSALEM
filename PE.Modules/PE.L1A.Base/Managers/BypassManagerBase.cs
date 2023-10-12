using System;
using System.Collections.Generic;
using System.Linq;
using L1A.Base.Models;
using PE.BaseDbEntity.Models;
using PE.BaseInterfaces.Managers.L1A;
using PE.Common;
using PE.L1A.Base.Handlers;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.L1A.L1Adapter.Managers.Concrete
{
  public class BypassManagerBase : BaseManager, IBypassManagerBase
  {
    protected readonly IBypassStorageProviderBase BypassStorageProvider;
    protected readonly BypassHandler BypassHandler;
    protected readonly Dictionary<string, bool> Word16Bypasses = new Dictionary<string, bool>();

    public BypassManagerBase(IModuleInfo moduleInfo, IBypassStorageProviderBase bypassStorageProvider, BypassHandler bypassHandler)
      : base(moduleInfo)
    {
      BypassStorageProvider = bypassStorageProvider;
      BypassHandler = bypassHandler;
    }

    public virtual void Init()
    {
      try
      {
        FillBypassConfigurations();

        if (!BypassStorageProvider.BypassConfigurations.Any())
        {
          NotificationController.Warn("No configuration for Bypasses");

          return;
        }

        FillBypassInstances();

        if (!BypassStorageProvider.BypassInstances.Any())
        {
          NotificationController.Warn("No Bypass instances in database, please import firstly");

          return;
        }

        if (BypassStorageProvider.L1OpcBypassProviders.Any())
        {
          NotificationController.Warn("BypassManager has been already initialized");

          return;
        }

        InitializeBypassProviders(false);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while Init Bypass Manager");
      }
    }

    public virtual void ImportBypasses()
    {
      try
      {
        FillBypassConfigurations();

        if (!BypassStorageProvider.BypassConfigurations.Any())
        {
          NotificationController.Warn("No configuration for Bypasses");

          return;
        }

        BypassStorageProvider.ClearBypassInstances();
        BypassHandler.TruncateBypassInstances();

        if (BypassStorageProvider.L1OpcBypassProviders.Any())
        {
          NotificationController.Warn("removing BypassProviders");

          BypassStorageProvider.RemoveBypassProviders();
        }

        InitializeBypassProviders(true);

        var bypassInstances = ImportBypassInstances();

        BypassStorageProvider.SetBypassInstances(bypassInstances);

        BypassHandler.InsertBypassInstances(bypassInstances);

        StartBypassProvidersSubscriptions();
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while import bypasses");
        throw;
      }

    }

    protected virtual void StartBypassProvidersSubscriptions()
    {
      foreach (var item in BypassStorageProvider.L1OpcBypassProviders)
      {
        try
        {
          item.Value.StartSubscription();
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex, $"Something went wrong while Start subscription for Bypass OPC Server: {item.Key}");
        }
      }
    }

    protected virtual List<BypassInstanceDto> ImportBypassInstances()
    {
      var bypassInstances = new List<BypassInstanceDto>();
      foreach (var provider in BypassStorageProvider.L1OpcBypassProviders)
      {
        try
        {
          var providerConfigurations = BypassStorageProvider.BypassConfigurations
                .Where(x => x.OpcServerAddress.Equals(provider.Key))
                .ToList();

          if (!providerConfigurations.Any())
          {
            NotificationController.Warn($"No bypass configurations for server: {provider.Key}");
            continue;
          }
          var providerBypassInstances = provider.Value.Import(providerConfigurations);

          bypassInstances.AddRange(providerBypassInstances);
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex, $"Something went wrong while import bypasses for server: {provider.Key}");
        }
      }

      return bypassInstances;
    }

    protected virtual void InitializeBypassProviders(bool doNotStartSubscriptions)
    {
      var bypassConfigurationsGroupedByOpcServer = BypassStorageProvider.BypassConfigurations
          .Where(cp => !string.IsNullOrEmpty(cp.OpcServerAddress))
          .GroupBy(cp => cp.OpcServerAddress);

      foreach (var opcServerGroup in bypassConfigurationsGroupedByOpcServer)
      {
        try
        {
          if (doNotStartSubscriptions)
          {
            var opcProvider = BypassStorageProvider.CreateL1OpcBypassProvider(opcServerGroup.Key, new List<BypassInstanceDto>(), ProcessBoolBypass, ProcessWord16Bypass);

            opcProvider.Init();
            opcProvider.StartWithoutSubscription();
            BypassStorageProvider.AddL1OpcBypassProvider(opcServerGroup.Key, opcProvider);
          }
          else
          {
            var providerInstances = BypassStorageProvider.BypassInstances
            .Where(x => x.OpcServerAddress.Equals(opcServerGroup.Key))
            .ToList();

            if (!providerInstances.Any())
            {
              NotificationController.Warn($"For Bypass OPCServer: {opcServerGroup.Key} there are no bypasses");

              continue;
            }

            var opcProvider = BypassStorageProvider.CreateL1OpcBypassProvider(opcServerGroup.Key, providerInstances, ProcessBoolBypass, ProcessWord16Bypass);

            opcProvider.Init();
            opcProvider.Start();
            BypassStorageProvider.AddL1OpcBypassProvider(opcServerGroup.Key, opcProvider);
          }
        }
        catch (System.Exception ex)
        {
          NotificationController.LogException(ex, $"Something went wrong while Run initialization for Bypass Opc Server: {opcServerGroup.Key}");
        }
      }
    }

    protected virtual void FillBypassConfigurations()
    {
      BypassStorageProvider.ClearBypassConfigurations();

      List<BypassConfigurationDto> configurations = BypassHandler.GetBypassConfigurations();

      BypassStorageProvider.SetBypassConfigurations(configurations);
    }

    protected virtual void FillBypassInstances()
    {
      BypassStorageProvider.ClearBypassInstances();

      List<BypassInstanceDto> instances = BypassHandler.GetBypassInstances();

      BypassStorageProvider.SetBypassInstances(instances);
    }

    protected virtual void ProcessBoolBypass(bool value, DateTime dateTime, BypassInstanceDto bypassInstance)
    {
      try
      {
        BypassHandler.InsertRaisedBypass(value, dateTime, bypassInstance);

        HmiRefresh(HMIRefreshKeys.ActiveBypasses);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
        $"Something went wrong while {MethodHelper.GetMethodName()}");
      }
    }

    protected virtual void ProcessWord16Bypass(ushort value, DateTime dateTime, BypassInstanceDto bypassInstance)
    {
      try
      {
        List<L1ARaisedBypass> bypasses = new List<L1ARaisedBypass>();
        var wordBypass = (Word16Flag)value;

        var flagsToCheck = Enum.GetValues(typeof(Word16Flag)).Cast<Word16Flag>();

        foreach (var flag in flagsToCheck)
        {
          string bypassName = bypassInstance.OpcBypassNode + "_" + flag.ToString();
          bool bypassValue = wordBypass.HasFlag(flag);

          if(Word16Bypasses.ContainsKey(bypassName))
          {
            var cacheValue = Word16Bypasses[bypassName];

            if (cacheValue == bypassValue)
              continue;
          }

          Word16Bypasses[bypassName] = bypassValue;

          bypasses.Add(new L1ARaisedBypass()
          {
            BypassName = bypassName,
            Timestamp = dateTime,
            Value = bypassValue,
            OpcServerAddress = bypassInstance.OpcServerAddress,
            OpcServerName = bypassInstance.OpcServerName,
            BypassTypeName = bypassInstance.BypassTypeName
          });
        }

        BypassHandler.InsertRaisedBypasses(bypasses);
        HmiRefresh(HMIRefreshKeys.ActiveBypasses);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
        $"Something went wrong while {MethodHelper.GetMethodName()}");
      }
    }

    [Flags]
    protected enum Word16Flag : ushort
    {
      Bit1 = 1,
      Bit2 = 2,
      Bit3 = 4,
      Bit4 = 8,
      Bit5 = 16,
      Bit6 = 32,
      Bit7 = 64,
      Bit8 = 128,
      Bit9 = 256,
      Bit10 = 512,
      Bit11 = 1024,
      Bit12 = 2048,
      Bit13 = 4096,
      Bit14 = 8192,
      Bit15 = 16384,
      Bit16 = 32768,
    }
  }
}
