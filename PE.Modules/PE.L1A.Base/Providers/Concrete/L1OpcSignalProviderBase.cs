using System;
using System.Collections.Generic;
using System.Linq;
using Opc.UaFx.Client;
using PE.Helpers;
using PE.L1A.Base.Managers;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Concrete
{
  public class L1OpcSignalProviderBase : OPCSignalProviderBase
  {
    protected readonly List<OpcConfigurationPoint> ConfigurationPoints;
    protected readonly Action<List<TrackingPointSignalBase>> ProcessTrackingPointSignal;
    protected readonly Action<double, DateTime, ExtendedConfigurationPointBase> ProcessMeasurementSignal;

    public L1OpcSignalProviderBase(string opcServerAddress, List<OpcConfigurationPoint> configurationPoints,
      Action<List<TrackingPointSignalBase>> processTrackingPointSignal,
      Action<double, DateTime, ExtendedConfigurationPointBase> processMeasurementSignal)
    : base(opcServerAddress)
    {
      ConfigurationPoints = configurationPoints;
      ProcessTrackingPointSignal = processTrackingPointSignal;
      ProcessMeasurementSignal = processMeasurementSignal;
    }

    protected override Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>> GetSubscriptions()
    {
      var result = new Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>>();

      try
      {
        var configurationPointGroups = ConfigurationPoints.GroupBy(x => x.TagUrl);

        foreach (var configurationPoint in configurationPointGroups)
        {
          var configurationPointType = configurationPoint.First().ConfigurationPointType;
          switch (configurationPointType)
          {
            case ConfigurationPointType.TrackingPoint:
              result.Add(configurationPoint.Key, HandleTrackingPoint);
              break;
            case ConfigurationPointType.MeasurementPoint:
              result.Add(configurationPoint.Key, HandleMeasurementPoint);
              break;
            default:
              throw new InternalModuleException($"Configuration point type {configurationPointType} has not handler for OPC server: {OpcServerAddress}.",
                AlarmDefsBase.CannotGetSubscriptionForOPCServer, OpcServerAddress);
          }
        }
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization("L1Adapter", MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization("L1Adapter", MethodHelper.GetMethodName(), AlarmDefsBase.CannotGetSubscriptionForOPCServer,
          $"Something went wrong while getting subscriptions from configuration for OPC server {OpcServerAddress}");
      }

      return result;

    }

    protected virtual void HandleTrackingPoint(object sender, OpcDataChangeReceivedEventArgs eventArgs)
    {
      try
      {
        OpcMonitoredItem item = (OpcMonitoredItem)sender;
        string tagName = item.NodeId.ToString();
        NotificationController.Debug($"Received TrackingPoint: {tagName}, Value: {eventArgs.Item.Value.Value}");
        
        var sourceTimestamp = eventArgs.Item.Value.SourceTimestamp?.ToLocalTime() ?? DateTime.Now.ExcludeMiliseconds();
        var value = Convert.ToInt32(eventArgs.Item.Value.Value);
        var processExpertTimestamp = DateTime.Now;

        var trackingPointSignals = ConfigurationPoints
          .Where(x => x.TagUrl.Equals(tagName))
          .Select(x => new TrackingPointSignalBase(x.FeatureCode, value, sourceTimestamp, processExpertTimestamp, x.IsResendEnabled))
          .ToList();

        ProcessTrackingPointSignal(trackingPointSignals);
      }
      catch (Exception e)
      {
        if (e is FormatException || e is InvalidCastException || e is FormatException)
        {
          NotificationController.LogException(e,
            $"Something went wrong while {MethodHelper.GetMethodName()} for OPC Server: {OpcServerAddress} -> " +
            $"cannot convert {eventArgs.Item?.Value?.Value} to int");
        }
        else
        {
          NotificationController.LogException(e,
          $"Something went wrong while {MethodHelper.GetMethodName()} for OPC Server: {OpcServerAddress}");
        }
      }
    }

    protected virtual void HandleMeasurementPoint(object sender, OpcDataChangeReceivedEventArgs eventArgs)
    {
      try
      {
        OpcMonitoredItem item = (OpcMonitoredItem)sender;

        NotificationController.Debug($"Received MeasurementPoint: {item.NodeId}, Value: {eventArgs.Item.Value.Value}");

        OpcConfigurationPoint configurationPoint = ConfigurationPoints.First(x => x.TagUrl.Equals(item.NodeId.ToString()));
        var sourceTimestamp = eventArgs.Item.Value.SourceTimestamp?.ToLocalTime() ?? DateTime.Now;
        var value = (double)Convert.ToDecimal(eventArgs.Item.Value.Value);

        ProcessMeasurementSignal(value, sourceTimestamp, configurationPoint);
      }
      catch (Exception e)
      {
        if (e is FormatException || e is InvalidCastException || e is FormatException)
        {
          NotificationController.LogException(e,
            $"Something went wrong while HandleMeasurementPoint for OPC Server: {OpcServerAddress} -> " +
            $"cannot convert {eventArgs.Item?.Value?.Value} to double");
        }
        else
        {
          NotificationController.LogException(e,
            $"Something went wrong while HandleMeasurementPoint for OPC Server: {OpcServerAddress}");
        }
      }
    }
  }
}
