using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using L1A.Base.Models;
using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Client;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Helpers;
using PE.L1A.Base.Managers;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using Polly;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Concrete
{
  public class L1OpcBypassProviderBase : OPCSignalProviderBase
  {
    protected List<BypassInstanceDto> BypassInstances;
    protected readonly Action<bool, DateTime, BypassInstanceDto> ProcessBoolBypass;
    protected readonly Action<ushort, DateTime, BypassInstanceDto> ProcessWord16Bypass;
    public const short BypassBool = 100;
    public const short BypassWord16 = 200;

    public L1OpcBypassProviderBase(string opcServerAddress, 
      List<BypassInstanceDto> bypassInstances,
      Action<bool, DateTime, BypassInstanceDto> processBoolBypass, 
      Action<ushort, DateTime, BypassInstanceDto> processWord16Bypass)
      : base(opcServerAddress)
    {
      BypassInstances = bypassInstances;
      ProcessBoolBypass = processBoolBypass;
      ProcessWord16Bypass = processWord16Bypass;
    }

    /// <summary>
    /// Import Bypasses
    /// </summary>
    /// <param name="configurations"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public virtual List<BypassInstanceDto> Import(List<BypassConfigurationDto> configurations)
    {
      if (OpcClient == null)
        throw new Exception("Connection to Bypass OPCServer has not been established!");

      BypassInstances = new List<BypassInstanceDto>();

      NotificationController.Warn($"Browsing nodes for OPC server: {OpcServerAddress}");

      var node = OpcClient.BrowseNode(OpcObjectTypes.ObjectsFolder);

      Browse(node, configurations);

      NotificationController.Warn($"Finished Browsing nodes for OPC server: {OpcServerAddress}. BypassInstances: {BypassInstances.Count}");

      return BypassInstances;
    }

    /// <summary>
    /// Should be used only while subscription need to be reinitialized after import
    /// </summary>
    /// <param name="sleepPeriodWhenFail"></param>
    public virtual void StartWithoutSubscription(int sleepPeriodWhenFail = 3000)
    {
      var policy = Policy
        .Handle<Exception>()
        .WaitAndRetryForever(sleepDuration => TimeSpan.FromMilliseconds(sleepPeriodWhenFail),
        (exception, timespan) =>
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
          $"Something went wrong while starting connection for Bypass OPC Server: {OpcServerAddress}.", OpcServerAddress);
          NotificationController.LogException(exception, $"Something went wrong while Start connection to Bypass OPC Server: {OpcServerAddress} Retry...");
        });

      policy.Execute(StartWithoutSubscriptionUsingPolly);

    }

    /// <summary>
    /// Can be used only if subscription is not yet started
    /// </summary>
    /// <param name="sleepPeriodWhenFail"></param>
    public virtual void StartSubscription(int sleepPeriodWhenFail = 3000)
    {
      var policy = Policy
        .Handle<Exception>()
        .WaitAndRetryForever(sleepDuration => TimeSpan.FromMilliseconds(sleepPeriodWhenFail),
        (exception, timespan) =>
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
           $"Something went wrong while starting subscription for Bypass OPC Server: {OpcServerAddress}.", OpcServerAddress);
          NotificationController.LogException(exception, $"Something went wrong while Start subscription to Bypass OPC Server: {OpcServerAddress} Retry...");
        });

      policy.Execute(StartSubscriptionUsingPolly);
    }

    /// <summary>
    /// GetSubscriptions
    /// </summary>
    /// <returns></returns>
    protected override Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>> GetSubscriptions()
    {
      var result = new Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>>();

      try
      {
        foreach (var bypassInstance in BypassInstances)
        {
          switch (bypassInstance.BypassTypeCode)
          {
            case BypassBool:
              result.Add(bypassInstance.OpcBypassNode, HandleBoolBypass);
              break;
            case BypassWord16:
              result.Add(bypassInstance.OpcBypassNode, HandleWord16Bypass);
              break;
            default:
              NotificationController.Error($"ByPass of type code {bypassInstance.BypassTypeCode} not defined!");
              break;
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);
      }

      return result;
    }

    /// <summary>
    /// Method to handle BoolBypassSubscription
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    protected virtual void HandleBoolBypass(object sender, OpcDataChangeReceivedEventArgs eventArgs)
    {
      try
      {
        OpcMonitoredItem item = (OpcMonitoredItem)sender;
        var tagName = item.NodeId.ToString();
        NotificationController.Debug($"Received Bool Bypass: {tagName}, Value: {eventArgs.Item.Value.Value}");
        var sourceTimestamp = eventArgs.Item.Value.SourceTimestamp?.ToLocalTime() ?? DateTime.Now.ExcludeMiliseconds();

        var value = Convert.ToBoolean(eventArgs.Item.Value.Value);
        var instance = BypassInstances.First(x => x.OpcBypassNode.Equals(tagName));

        ProcessBoolBypass(value, sourceTimestamp, instance);
      }
      catch (Exception e)
      {
        if (e is FormatException || e is InvalidCastException || e is FormatException)
        {
          NotificationController.LogException(e,
            $"Something went wrong while {MethodHelper.GetMethodName()} for Bypass OPC Server: {OpcServerAddress} -> " +
            $"cannot convert {eventArgs.Item?.Value?.Value} to bool");
        }
        else
        {
          NotificationController.LogException(e,
          $"Something went wrong while {MethodHelper.GetMethodName()} for Bypass OPC Server: {OpcServerAddress}");
        }
      }
    }

    /// <summary>
    /// Method to handle Word16BypassSubscription
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    protected virtual void HandleWord16Bypass(object sender, OpcDataChangeReceivedEventArgs eventArgs)
    {
      try
      {
        OpcMonitoredItem item = (OpcMonitoredItem)sender;
        var tagName = item.NodeId.ToString();
        NotificationController.Debug($"Received WORD Bypass: {tagName}, Value: {eventArgs.Item.Value.Value}");
        var sourceTimestamp = eventArgs.Item.Value.SourceTimestamp?.ToLocalTime() ?? DateTime.Now.ExcludeMiliseconds();

        var value = Convert.ToUInt16(eventArgs.Item.Value.Value);
        var instance = BypassInstances.First(x => x.OpcBypassNode.Equals(tagName));

        ProcessWord16Bypass(value, sourceTimestamp, instance);
      }
      catch (Exception e)
      {
        if (e is FormatException || e is InvalidCastException || e is FormatException)
        {
          NotificationController.LogException(e,
            $"Something went wrong while {MethodHelper.GetMethodName()} for Bypass OPC Server: {OpcServerAddress} -> " +
            $"cannot convert {eventArgs.Item?.Value?.Value} to bool");
        }
        else
        {
          NotificationController.LogException(e,
          $"Something went wrong while {MethodHelper.GetMethodName()} for Bypass OPC Server: {OpcServerAddress}");
        }
      }
    }

    /// <summary>
    /// Method to browse nodes for configuration
    /// </summary>
    /// <param name="node"></param>
    /// <param name="configurations"></param>
    protected virtual void Browse(OpcNodeInfo node, List<BypassConfigurationDto> configurations)
    {
      foreach (var configuration in configurations)
      {
        if (node?.Reference?.TypeDefinitionId?.ValueAsString?.Equals(configuration.OpcBypassParentStructureNode) == true)
        {
          var block = node.Child(configuration.OpcBypassName);
          var nodeString = block.NodeId.ToString();
          if (!BypassInstances.Any(x => x.OpcBypassNode.Equals(nodeString)))
          {
            BypassInstances.Add(new BypassInstanceDto()
            {
              FKBypassConfigurationId = configuration.BypassConfigurationId,
              BypassTypeCode = configuration.BypassTypeCode,
              OpcBypassNode = nodeString,
              OpcServerAddress = configuration.OpcServerAddress,
              OpcServerName = configuration.OpcServerName,
              BypassTypeName = configuration.BypassTypeName
            });
          }
        }
      }

      foreach (var childNode in node.Children())
        Browse(childNode, configurations);
    }

    protected virtual void StartSubscriptionUsingPolly()
    {
      if (OpcClient is not null)
      {
        OpcClient.StartSubscription(GetSubscriptions());
        NotificationController.RegisterAlarm(AlarmDefsBase.ConnectionWithOPCServerStarted,
          $"Started subscription with Bypass OPC server {OpcServerAddress}.", OpcServerAddress);
      }
      else
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
          $"Something went wrong while starting subscription for Bypass OPC Server: {OpcServerAddress}.", OpcServerAddress);
        NotificationController.Fatal($"Init method wasn't run for Bypass OPC Server: {OpcServerAddress}!!!");
        NotificationController.Fatal($"Subscription to Bypass OPC Server: {OpcServerAddress} wasn't established!!!");
      }     
    }

    protected virtual void StartWithoutSubscriptionUsingPolly()
    {
      if (OpcClient is not null)
      {
        OpcClient.Connect();

        NotificationController.RegisterAlarm(AlarmDefsBase.ConnectionWithOPCServerStarted,
          $"Started connection with Bypass OPC server {OpcServerAddress}.", OpcServerAddress);
      }
      else
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
          $"Something went wrong while starting connection for Bypass OPC Server: {OpcServerAddress}.", OpcServerAddress);
        NotificationController.Fatal($"Init method wasn't run for Bypass OPC Server: {OpcServerAddress}!!!");
        NotificationController.Fatal($"Connection to Bypass OPC Server: {OpcServerAddress} wasn't established!!!");
      }
    }
  }
}
