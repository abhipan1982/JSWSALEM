using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseInterfaces.SendOffices.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using PE.L1A.Base.Providers.Concrete;
using SMF.Core.DC;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.L1A.Base.Managers
{
  public class L1SignalManagerBase : IL1SignalManagerBase, IL1OPCSignalManagerBase
  {
    protected readonly IL1MeasurementStorageProviderBase StorageProvider;
    protected readonly IL1SignalSendOfficeBase L1SignalSendOffice;
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;

    protected readonly ConcurrentDictionary<int, DateTime> LastMeasuredDictionary = new ConcurrentDictionary<int, DateTime>();
    protected readonly ConcurrentDictionary<int, TrackingPointSignalBase> TrackingPointSignalsCache = new ConcurrentDictionary<int, TrackingPointSignalBase>();

    public L1SignalManagerBase(IL1MeasurementStorageProviderBase storageProvider,
      IL1SignalSendOfficeBase l1SignalSendOffice,
      IConfigurationStorageProviderBase configurationStorageProvider)
    {
      StorageProvider = storageProvider;
      L1SignalSendOffice = l1SignalSendOffice;
      ConfigurationStorageProvider = configurationStorageProvider;
    }

    public virtual async Task<DataContractBase> ResendTrackingPointSignals()
    {
      try
      {
        NotificationController.Warn("Resending tracking point signals");

        if (!TrackingPointSignalsCache.Values.Any())
          NotificationController.Warn("There is nothing to resend");

        var trackingPointsToResend = TrackingPointSignalsCache.Values
          .Where(x => x.IsResendEnabled)
          .Select(cp => new
          {
            SourceTimestamp = cp.SourceTimestamp,
            ProcessExpertTimestamp = cp.ProcessExpertTimestamp,
            Value = cp.Value,
            FeatureCode = cp.FeatureCode
          })
          .ToList();

        if (!trackingPointsToResend.Any())
          NotificationController.Warn("There is no tracking points with IsResendEnabled=true available");

        var request = new DcAggregatedTrackingPointSignal();

        foreach (var tp in trackingPointsToResend)
        {
          request.TrackingPointSignals.Add(new DcTrackingPointSignal()
          {
            OperationDate = tp.SourceTimestamp,
            ProcessExpertOperationDate = tp.ProcessExpertTimestamp,
            FeatureCode = tp.FeatureCode,
            Value = tp.Value
          });
        }

        var sendOfficeResult = await L1SignalSendOffice.SendAggregatedL1TrackingPointToTrackingAsync(request);

        var featureCodes = string.Join(", ", trackingPointsToResend.Select(x => x.FeatureCode));
        if (sendOfficeResult.OperationSuccess)
          NotificationController.Debug($"Successfully resent tracking points: {featureCodes} " +
                                        $"to tracking");
        else
          NotificationController.Error($"Something went wrong while resending tracking points: {featureCodes} " +
                                        $"to tracking");
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
        $"Something went wrong while {MethodHelper.GetMethodName()}");
      }

      return new DataContractBase();
    }

    public virtual void Stop()
    {
      foreach (var l1SignalProvider in ConfigurationStorageProvider.L1SignalProviders)
      {
        l1SignalProvider.Value.Stop();
      }
    }   

    public void Init()
    {
      var opcConfigurationPoints = new List<OpcConfigurationPoint>();

      foreach (var l1ConfigurationPoint in ConfigurationStorageProvider.L1ConfigurationPoints.Values)
      {
        if (l1ConfigurationPoint is OpcConfigurationPoint l1OpcConfigurationPoint)
        {
          opcConfigurationPoints.Add(l1OpcConfigurationPoint);
        }
      }

      InitOpcConfigurationPoints(opcConfigurationPoints);
    }

    protected virtual void InitOpcConfigurationPoints(List<OpcConfigurationPoint> opcConfigurationPoints)
    {
      if (opcConfigurationPoints.Any())
      {
        var configurationPointsGroupedByOpcServer = opcConfigurationPoints
          .Where(cp => !string.IsNullOrEmpty(cp.OpcServer))
          .GroupBy(cp => cp.OpcServer);

        foreach (var opcServerGroup in configurationPointsGroupedByOpcServer)
        {
          try
          {
            var opcProvider = new L1OpcSignalProviderBase(opcServerGroup.Key,
              opcServerGroup.ToList(),
              ProcessTrackingPointSignals,
              ProcessMeasurementSignal);

            opcProvider.Init();
            opcProvider.Start();
            ConfigurationStorageProvider.L1SignalProviders.Add(opcServerGroup.Key, opcProvider);
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex, $"Something went wrong while Run initialization for Opc Server: {opcServerGroup.Key}");
            throw;
          }
        }
      }
    }

    protected virtual void ProcessMeasurementSignal(double value, DateTime sourceTimestamp, ExtendedConfigurationPointBase configurationPoint)
    {
      try
      {
        LastMeasuredDictionary.TryGetValue(configurationPoint.FeatureCode, out DateTime lastMeasure);

        if ((long)(sourceTimestamp - lastMeasure).TotalMilliseconds >= configurationPoint.TimeOffsetOfSamples)
        {
          LastMeasuredDictionary.AddOrUpdate(configurationPoint.FeatureCode, sourceTimestamp, (key, lastDate) => sourceTimestamp);

          StorageProvider.ProcessMeasurement(new FeatureMeasurement(configurationPoint.FeatureCode,
            sourceTimestamp.AddSeconds(configurationPoint.RetentionFactor), sourceTimestamp, value));
        }
      }
      catch (Exception e)
      {
        NotificationController.LogException(e,
          $"Something went wrong while {MethodHelper.GetMethodName()} FeatureCode: {configurationPoint.FeatureCode}, Value: {value}, Ts: {sourceTimestamp.ToLongTimeString()}");
      }
    }

    protected virtual void ProcessTrackingPointSignals(List<TrackingPointSignalBase> trackingPointSignals)
    {
      try
      {
        if (trackingPointSignals.Any())
        {
          var now = DateTime.Now;
          var request = new DcAggregatedTrackingPointSignal();

          ProcessNotCalculatedSignals(trackingPointSignals, request);
          ProcessCalculatedSignals(trackingPointSignals, request);

          var sendOfficeResult = L1SignalSendOffice.SendAggregatedL1TrackingPointToTrackingAsync(request).GetAwaiter().GetResult();

          var featureCodes = string.Join(", ", trackingPointSignals.Select(x => x.FeatureCode));
          if (sendOfficeResult.OperationSuccess)
            NotificationController.Debug($"Successfully sent tracking point: {featureCodes} to tracking");
          else
            NotificationController.Error($"Something went wrong while sending tracking point: {featureCodes} to tracking");
        }
      }
      catch (Exception e)
      {
        NotificationController.LogException(e,
        $"Something went wrong while {MethodHelper.GetMethodName()} ");
      }
    }

    protected virtual void ProcessNotCalculatedSignals(List<TrackingPointSignalBase> trackingPointSignals, DcAggregatedTrackingPointSignal request)
    {
      foreach (var cp in trackingPointSignals)
      {
        TrackingPointSignalsCache[cp.FeatureCode] = cp;

        request.TrackingPointSignals.Add(new DcTrackingPointSignal()
        {
          OperationDate = cp.SourceTimestamp,
          ProcessExpertOperationDate = cp.ProcessExpertTimestamp,
          FeatureCode = cp.FeatureCode,
          Value = cp.Value
        });
      }
    }

    protected virtual void ProcessCalculatedSignals(List<TrackingPointSignalBase> trackingPointSignals, DcAggregatedTrackingPointSignal request)
    {
      List<TrackingPointSignalBase> calculatedTrackingPointSignals = new List<TrackingPointSignalBase>();

      foreach (var cp in trackingPointSignals)
      {
        var calculatedFeaturesGroups = GetCalculatedFeaturesGroups(cp);

        if (!calculatedFeaturesGroups.Any())
          continue;

        foreach (var calculatedFeatureGroup in calculatedFeaturesGroups)
        {
          foreach (var calculatedFeature in calculatedFeatureGroup.ToList())
          {
            if (calculatedTrackingPointSignals.Any(x => x.FeatureCode == calculatedFeature.CalculatedFeatureCode))
            {
              NotificationController.Warn($"Calculated trackingPoint signal ignored because same calculated point feature was already processed: {calculatedFeature.CalculatedFeatureCode}");

              continue;
            }

            ProcessSingleCalculatedFeature(request, calculatedTrackingPointSignals, cp, calculatedFeature);
          }
        }
      }

      if (calculatedTrackingPointSignals.Any())
        ProcessCalculatedSignals(calculatedTrackingPointSignals, request);
    }

    protected virtual List<IGrouping<int, FeatureCalculatedPointBase>> GetCalculatedFeaturesGroups(TrackingPointSignalBase cp)
    {
      return ConfigurationStorageProvider.CalculatedFeatures
        .Where(x => x.FeatureCode_1 == cp.FeatureCode || x.FeatureCode_2 == cp.FeatureCode)
        .GroupBy(x => x.CalculatedFeatureCode, y => y)
        .ToList();
    }

    protected virtual void ProcessSingleCalculatedFeature(DcAggregatedTrackingPointSignal request, List<TrackingPointSignalBase> calculatedTrackingPointSignals, TrackingPointSignalBase cp, FeatureCalculatedPointBase calculatedFeature)
    {
      bool shouldBeProcessed = CalculatedFeatureConditionsPassed(calculatedFeature.CalculatedFeatureCode);

      if (shouldBeProcessed)
      {
        var calculatedTrackingPoint = new TrackingPointSignalBase(calculatedFeature.CalculatedFeatureCode, calculatedFeature.CalculatedValue,
          cp.SourceTimestamp, cp.ProcessExpertTimestamp, calculatedFeature.IsResendEnabled);

        calculatedTrackingPointSignals.Add(calculatedTrackingPoint);

        TrackingPointSignalsCache[calculatedFeature.CalculatedFeatureCode] = calculatedTrackingPoint;

        if (!calculatedFeature.IsVirtual)
        {
          request.TrackingPointSignals.Add(new DcTrackingPointSignal()
          {
            OperationDate = calculatedTrackingPoint.SourceTimestamp,
            ProcessExpertOperationDate = calculatedTrackingPoint.ProcessExpertTimestamp,
            FeatureCode = calculatedTrackingPoint.FeatureCode,
            Value = calculatedTrackingPoint.Value
          });
        }
      }
    }

    protected virtual bool CalculatedFeatureConditionsPassed(int calculatedFeatureCode)
    {
      FeatureCalculatedPointBase[] conditions = ConfigurationStorageProvider.CalculatedFeatures
        .Where(x => x.CalculatedFeatureCode == calculatedFeatureCode)
        .OrderBy(x => x.Seq)
        .ToArray();

      int startingIndex = 1;

      return VerifyCalculatedFeatureConditionsRecurrently(startingIndex, conditions);
    }

    protected virtual bool VerifyCalculatedFeatureConditionsRecurrently(int index, FeatureCalculatedPointBase[] conditions)
    {
      var condition = conditions[index - 1];

      bool conditionResult = VerifyCalculatedFeatureAggregatedCondition(condition);

      if (index == conditions.Length)
        return conditionResult;

      switch (condition.EnumLogicalOperator_ForNextSequence)
      {
        case var logicalOperator when logicalOperator == LogicalOperator.And:
          return conditionResult && VerifyCalculatedFeatureConditionsRecurrently(index + 1, conditions);
        case var logicalOperator when logicalOperator == LogicalOperator.Or:
          return conditionResult || VerifyCalculatedFeatureConditionsRecurrently(index + 1, conditions);
        case var logicalOperator when logicalOperator == LogicalOperator.None:
          {
            NotificationController.Error($"Unreachable condition at: FeatureCode: {condition.CalculatedFeatureCode}, Seq: {condition.Seq}");
            return conditionResult;
          }
        default:
          throw new ArgumentException($"{condition.EnumLogicalOperator_ForNextSequence.Name} is out of range");
      }
    }


    protected virtual bool VerifyCalculatedFeatureAggregatedCondition(FeatureCalculatedPointBase aggregatedCondition)
    {
      if (!TrackingPointSignalsCache.TryGetValue(aggregatedCondition.FeatureCode_1, out var trackingPointSignal1))
      {
        NotificationController.Warn($"No registered signal with FeatureCode: {aggregatedCondition.FeatureCode_1}");

        return false;
      }

      bool condition1Result = ProcessCompareOperator(aggregatedCondition.FeatureCode_1, aggregatedCondition.EnumCompareOperator_1,
        trackingPointSignal1.Value, aggregatedCondition.Value_1,
        aggregatedCondition.Seq, aggregatedCondition.CalculatedFeatureCode);

      if (aggregatedCondition.EnumLogicalOperator == LogicalOperator.None)
      {
        if (aggregatedCondition.FeatureCode_2.HasValue)
          NotificationController.Error($"Unreachable condition at: CalculatedFeatureCode: {aggregatedCondition.CalculatedFeatureCode}, " +
            $"FeatureCode: {aggregatedCondition.FeatureCode_2}, Seq: {aggregatedCondition.Seq}");

        return condition1Result;
      }

      // To increase performance and do not check second condition
      if (aggregatedCondition.EnumLogicalOperator == LogicalOperator.And && condition1Result == false)
        return false;

      if (!aggregatedCondition.FeatureCode_2.HasValue || !aggregatedCondition.Value_2.HasValue)
      {
        NotificationController.Error($"FeatureCode_2 and Value_2 for CalculatedFeatureCode: {aggregatedCondition.CalculatedFeatureCode}, Seq: {aggregatedCondition.Seq} cannot be null");
        return false;
      }

      if (!TrackingPointSignalsCache.TryGetValue(aggregatedCondition.FeatureCode_2.Value, out var trackingPointSignal2))
      {
        NotificationController.Warn($"No registered signal with FeatureCode: {aggregatedCondition.FeatureCode_1}");

        return false;
      }

      bool condition2Result = ProcessCompareOperator(aggregatedCondition.FeatureCode_2.Value, aggregatedCondition.EnumCompareOperator_2,
        trackingPointSignal2.Value, aggregatedCondition.Value_2.Value,
        aggregatedCondition.Seq, aggregatedCondition.CalculatedFeatureCode);


      switch (aggregatedCondition.EnumLogicalOperator)
      {
        case var logicalOperator when logicalOperator == LogicalOperator.And:
          return condition1Result && condition2Result && TimeFilterConditionPassed(aggregatedCondition.TimeFilter, trackingPointSignal1, trackingPointSignal2);
        case var logicalOperator when logicalOperator == LogicalOperator.Or:
          return condition1Result || condition2Result;
        default:
          throw new ArgumentException($"{aggregatedCondition.EnumLogicalOperator.Name} is out of range");
      }
    }

    protected virtual bool TimeFilterConditionPassed(double? timeFilter, TrackingPointSignalBase trackingPointSignal1, TrackingPointSignalBase trackingPointSignal2)
    {
      if (!timeFilter.HasValue)
        return true;

      var timeFilterConditionVerificationResult = Math.Abs((trackingPointSignal1.ProcessExpertTimestamp - trackingPointSignal2.ProcessExpertTimestamp).TotalMilliseconds) <= timeFilter.Value;

      if (!timeFilterConditionVerificationResult)
        NotificationController.Warn($"TimeFilterCondition not passed: " +
          $"trackingPointSignal1FeatureCode: {trackingPointSignal1.FeatureCode}, " +
          $"trackingPointSignal2FeatureCode: {trackingPointSignal2.FeatureCode}, " +
          $"trackingPointSignal1Value: {trackingPointSignal1.Value}, " +
          $"trackingPointSignal2Value: {trackingPointSignal2.Value}, " +
          $"Signal1Ts: {trackingPointSignal1.ProcessExpertTimestamp.ToLongTimeString()}, " +
          $"Signal2Ts: {trackingPointSignal2.ProcessExpertTimestamp.ToLongTimeString()}, " +
          $"timeFilter: {timeFilter}");

      return timeFilterConditionVerificationResult;
    }

    protected virtual bool ProcessCompareOperator(int featureCode, CompareOperator compareOperator,
    int signalValue, int conditionValue,
    int seq, int calculatedFeatureCode)
    {
      if (compareOperator == CompareOperator.None)
      {
        NotificationController.Error($"Could not process operator: {compareOperator.Name} " +
          $"for FeatureCode: {calculatedFeatureCode} and Seq: {seq}");

        return false;
      }

      bool value;

      switch (compareOperator)
      {
        case var co when co == CompareOperator.Equal:
          value = signalValue == conditionValue;
          break;
        case var co when co == CompareOperator.NonEqual:
          value = signalValue != conditionValue;
          break;
        case var co when co == CompareOperator.LessOrEqual:
          value = signalValue <= conditionValue;
          break;
        case var co when co == CompareOperator.GreaterOrEqual:
          value = signalValue >= conditionValue;
          break;
        case var co when co == CompareOperator.Less:
          value = signalValue < conditionValue;
          break;
        case var co when co == CompareOperator.Greater:
          value = signalValue > conditionValue;
          break;
        default:
          throw new ArgumentException($"{compareOperator.Name} is out of range " +
            $"for FeatureCode: {calculatedFeatureCode} and Seq: {seq} ");
      }

      return value;
    }    
  }
}
