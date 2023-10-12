using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using PE.Module.Core.Managers;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Interfaces;
using SMF.Core.Notification;
using SMF.Module.UnitConverter;

namespace PE.L1A.Base.Managers
{
  public class MeasurementStorageManagerBase : MeasurementProcessingBaseManager, IMeasurementStorageManagerBase
  {
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;
    protected readonly IL1MeasurementStorageProviderBase L1MeasurementStorageProvider;

    public MeasurementStorageManagerBase(IModuleInfo moduleInfo, IConfigurationStorageProviderBase configurationStorageProvider,
      IL1MeasurementStorageProviderBase l1MeasurementStorageProvider) : base(moduleInfo)
    {
      ConfigurationStorageProvider = configurationStorageProvider;
      L1MeasurementStorageProvider = l1MeasurementStorageProvider;
    }

    public virtual async Task ProcessMeasurementsAsync(DcRelatedToMaterialMeasurementRequest data)
    {
      try
      {
        var featureConfigurationPoints = GetFeatureConfigurationPointsByParentFeatureCode(data.FeatureCode);

        if (featureConfigurationPoints.Any())
        {
          var measurements = await GetMeasurementsWithSamplesByDates(data, featureConfigurationPoints);

          await ProcessMeasurements(data, featureConfigurationPoints, measurements);
        }
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.ErrorDuringProcessingMeasurement,
          $"Something went wrong while ProcessMeasurements for feature {data.FeatureCode} with material [{data.MaterialId}]", data.FeatureCode);
        NotificationController.LogException(ex, $"Something went wrong while ProcessMeasurements" +
                                                $" for FeatureCode: {data.FeatureCode} MaterialId: {data.MaterialId}");
      }
    }

    public virtual async Task<DataContractBase> ProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data)
    {
      var options = new ParallelOptions { MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.5) * 2.0)) };

      NotificationController.Debug("ProcessAggregatedMeasurementRequestAsync {@DcAggregatedMeasurementRequest}", data);

      await Parallel.ForEachAsync(data.MeasurementListToProcess, options, async (rq, ct) =>
      {
        await ProcessMeasurementsAsync(new DcRelatedToMaterialMeasurementRequest(data.MaterialId, rq));
      });

      return new DataContractBase();
    }

    public virtual async Task<DcNdrMeasurementResponse> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest data)
    {
      var featureConfiguration = GetFeatureConfigurationPointsByParentFeatureCode(data.ParentFeatureCode)
        .FirstOrDefault();

      if (featureConfiguration != null)
      {
        double? value = await L1MeasurementStorageProvider.GetFirstSampleUntilDate(featureConfiguration.FeatureCode, data.DateToTs);

        if (value != null)
        {
          return new DcNdrMeasurementResponse()
          {
            FeatureCode = featureConfiguration.FeatureCode,
            Value = value != 0d ? UOMHelper.Local2SI(value, featureConfiguration.ExtUnitOfMeasureId) ?? 0d : 0d
          };
        }
      }

      return new DcNdrMeasurementResponse()
      {
        FeatureCode = 0,
        Value = 0d
      };
    }

    public virtual async Task<DcMeasurementResponse> ProcessGetMeasurementValueAsync(DcMeasurementRequest dc)
    {
      try
      {
        var request = new DcGetMeasurementsCriteria()
        {
          DateFrom = dc.MeasurementFromDate,
          DateTo = dc.MeasurementToDate,
          FeatureCodes = new List<int> { dc.FeatureCode }
        };
        var measurementWithSamples = await L1MeasurementStorageProvider.GetMeasurementsWithSamplesByDates(request);

        if (!measurementWithSamples.Any(x => x.Samples.Any()))
          throw new InternalModuleException($"Cannot find measurements for feature {dc.FeatureCode}",
            AlarmDefsBase.MeasurementsNotFoundForPoint, dc.FeatureCode);

        var featureConfiguration = ConfigurationStorageProvider.FeatureConfigurationPointDictionary[dc.FeatureCode];

        var measurement = measurementWithSamples.First();

        AddMissingSamples(dc.MeasurementFromDate, dc.MeasurementToDate, featureConfiguration, measurement);

        double min, max, avg = 0;

        ConvertValuesToSiUnit(featureConfiguration, measurement);
        AggregateMeasurements(featureConfiguration, measurement, out min, out max, out avg);

        return new DcMeasurementResponse()
        {
          Min = min,
          Avg = avg,
          Max = max
        };
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
        throw;
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.UnexpectedError,
          $"Unexpected error while getting measurments for point with code {dc.FeatureCode}.");
        throw;
      }
    }

    public virtual async Task<DcRawMeasurementResponse> ProcessGetRawMeasurementsAsync(DcAggregatedMeasurementRequest data)
    {
      var configurationPoints = data.MeasurementListToProcess.Select(x => new ConfigurationPointBase(ConfigurationPointType.MeasurementPoint, x.FeatureCode));
      var firstPoint = data.MeasurementListToProcess.First();
      var measurements = await GetMeasurementsWithSamplesByDates(
        new DcMeasurementRequest(0, firstPoint.MeasurementFromDate, firstPoint.MeasurementToDate),
        configurationPoints.ToList());

      var response = new DcRawMeasurementResponse()
      {
        Measurements = measurements.Select(x => new DcMeasurement
        {
          FeatureCode = x.FeatureCode,
          MeasurementSamples = x.Samples.Select(s => new DcMeasurementSample
          {
            Value = s.Value,
            IsValid = s.IsValid,
            MeasurementDate = s.MeasurementDate
          }).ToList()
        }).ToList()
      };

      return response;
    }

    protected virtual async Task ProcessMeasurements(DcRelatedToMaterialMeasurementRequest data,
      List<FeatureConfigurationPointBase> featureConfigurationPoints, IList<Measurement> measurements)
    {
      double totalSeconds =
        (data.MeasurementToDate - data.MeasurementFromDate).TotalMilliseconds / 1000; // to have floating point [s]
      double avgVelocity = 0;
      double materialLength = 0;
      foreach (var featureConfiguration in featureConfigurationPoints.OrderBy(x => x.IsVelocity))
      {
        var measurement = measurements.FirstOrDefault(x => x.FeatureCode == featureConfiguration.FeatureCode);

        if (measurement == null || measurement.Samples.Count == 0)
          continue;

        AddMissingSamples(data.MeasurementFromDate, data.MeasurementToDate, featureConfiguration, measurement);

        double min, max, avg = 0;

        ConvertValuesToSiUnit(featureConfiguration, measurement);
        AggregateMeasurements(featureConfiguration, measurement, out min, out max, out avg);

        if (featureConfiguration.IsVelocity)
        {
          CalculateMaterialLength(totalSeconds, avg, data, ref materialLength);
          avgVelocity = avg;
        }

        await SaveMeasurementsToDatabase(data, avgVelocity, materialLength, featureConfiguration, measurement, min, max,
          avg);
      }
    }

    protected virtual void AddMissingSamples(DateTime measurementFromDate,
      DateTime measurementToDate,
      FeatureConfigurationPointBase featureConfiguration,
      Measurement measurement)
    {
      List<MeasurementSample> result = new List<MeasurementSample>();
      var samplesToCheck = measurement.Samples.OrderBy(x => x.MeasurementDate).ToList();
      var count = samplesToCheck.Count;

      if (count == 0)
      {
        NotificationController.Warn($"No measurements for FeatureCode: {featureConfiguration.FeatureCode}, DateFrom: {measurementFromDate}, DateTo: {measurementToDate}");
        return;
      }

      for (int i = 0; i < count; i++)
      {
        DateTime from = samplesToCheck[i].MeasurementDate < measurementFromDate
          ? measurementFromDate
          : samplesToCheck[i].MeasurementDate;

        DateTime to = (i == count - 1)
          ? measurementToDate
          : samplesToCheck[i + 1].MeasurementDate > measurementToDate
            ? measurementToDate
            : samplesToCheck[i + 1].MeasurementDate;

        while (from < to)
        {
          result.Add(new MeasurementSample()
          {
            MeasurementDate = from,
            Value = samplesToCheck[i].Value,
            IsValid = IsValid(featureConfiguration, samplesToCheck[i].Value)
          });

          from = from.AddMilliseconds(featureConfiguration.SampleOffset);
        }
      }

      measurement.Samples = result;
    }

    protected virtual void ConvertValuesToSiUnit(FeatureConfigurationPointBase featureConfiguration,
      Measurement measurement)
    {
      measurement.Samples.ForEach(x =>
        x.Value = UOMHelper.Local2SI(x.Value, featureConfiguration.ExtUnitOfMeasureId) ?? 0);
    }

    protected virtual async Task SaveMeasurementsToDatabase(DcRelatedToMaterialMeasurementRequest data, double avgVelocity,
      double materialLength, FeatureConfigurationPointBase featureConfiguration, Measurement measurement, double min,
      double max, double avg)
    {
      if (featureConfiguration.StoreSamples)
      {
        await ProcessSingleMeasurementAsync(new DcMeasDataSample()
        {
          RawMaterialId = data.MaterialId,
          FeatureCode = measurement.FeatureCode,
          Valid = !measurement.Samples.Any(x => !x.IsValid),
          ActualLength = materialLength,
          FirstMeasurementTs = data.MeasurementFromDate,
          LastMeasurementTs = data.MeasurementToDate,
          Min = min,
          Max = max,
          Avg = featureConfiguration.IsVelocity ? avgVelocity : avg,
          Samples = measurement.Samples.Select(x => new DcSample()
          {
            IsValid = x.IsValid,
            HeadOffset = featureConfiguration.IsLengthRelated
            ? x.MeasurementDate < data.MeasurementFromDate
              ? (data.MeasurementToDate - data.MeasurementFromDate).TotalMilliseconds
                / 1000 /* to have floating point [s] */
                * avgVelocity
              : (x.MeasurementDate - data.MeasurementFromDate).TotalMilliseconds
                / 1000 /* to have floating point [s] */
                * avgVelocity
            : x.MeasurementDate < data.MeasurementFromDate
              ? 0
              : (x.MeasurementDate - data.MeasurementFromDate).TotalMilliseconds
                / 1000, /* to have floating point [s] */
            Value = x.Value
          }).ToList()
        });
      }
      else
      {
        await ProcessSingleMeasurementAsync(new DcMeasData()
        {
          RawMaterialId = data.MaterialId,
          FeatureCode = measurement.FeatureCode,
          Valid = IsValid(featureConfiguration, avg),
          ActualLength = materialLength,
          FirstMeasurementTs = data.MeasurementFromDate,
          LastMeasurementTs = data.MeasurementToDate,
          Min = min,
          Max = max,
          Avg = featureConfiguration.IsVelocity ? avgVelocity : avg
        });
      }
    }

    protected virtual void CalculateMaterialLength(double totalSeconds,
      double avgVelocity,
      DcRelatedToMaterialMeasurementRequest data,
      ref double materialLength)
    {
      materialLength = totalSeconds * avgVelocity;
    }

    protected virtual void AggregateMeasurements(FeatureConfigurationPointBase featureConfiguration,
      Measurement measurement,
      out double min,
      out double max,
      out double avg)
    {
      switch (featureConfiguration.AggregationStrategy)
      {
        case var value when value == AggregationStrategy.FloatingPoint:
          {
            min = measurement.Samples.Where(x => x.IsValid).Min(x => x.Value);
            max = measurement.Samples.Where(x => x.IsValid).Max(x => x.Value);
            avg = measurement.Samples.Where(x => x.IsValid).Average(x => x.Value);
            break;
          }
        case var value when value == AggregationStrategy.Logical:
          {
            min = measurement.Samples.Where(x => x.IsValid).Min(x => x.Value);
            max = measurement.Samples.Where(x => x.IsValid).Max(x => x.Value);
            avg = (int)Math.Round(measurement.Samples.Where(x => x.IsValid).Average(x => x.Value));
            break;
          }
        default:
          throw new ArgumentException(
            $"AggregationStrategy {featureConfiguration.AggregationStrategy.Name} has not been implemented yet");
      }
    }

    protected virtual bool IsValid(FeatureConfigurationPointBase featureConfiguration, double value)
    {
      if (featureConfiguration.MinValue.HasValue && value < featureConfiguration.MinValue.Value)
        return false;

      if (featureConfiguration.MaxValue.HasValue && value > featureConfiguration.MaxValue.Value)
        return false;

      return true;
    }

    protected virtual List<FeatureConfigurationPointBase> GetFeatureConfigurationPointsByParentFeatureCode(
      int parentFeatureCode)
    {
      if (!ConfigurationStorageProvider.FeatureConfigurationPointsByParentFeatureCode.ContainsKey(parentFeatureCode))
        return new List<FeatureConfigurationPointBase>();

      var result = ConfigurationStorageProvider.FeatureConfigurationPointsByParentFeatureCode[parentFeatureCode];

      if (!result.Any())
        return new List<FeatureConfigurationPointBase>();

      return result;
    }

    protected virtual async Task<IList<Measurement>> GetMeasurementsWithSamplesByDates<T>(
      DcMeasurementRequest data,
      List<T> featureConfigurationPoints)
      where T : ConfigurationPointBase
    {
      var measurements = await L1MeasurementStorageProvider.GetMeasurementsWithSamplesByDates(
        new DcGetMeasurementsCriteria()
        {
          DateFrom = data.MeasurementFromDate,
          DateTo = data.MeasurementToDate,
          FeatureCodes = featureConfigurationPoints.Select(x => x.FeatureCode).ToList()
        });

      return measurements;
    }
  }
}
