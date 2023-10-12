using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.L1A.Base.Models;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Abstract
{
  public abstract class L1MeasurementStorageProviderBase : IL1MeasurementStorageProviderBase
  {
    protected readonly ConcurrentStack<FeatureMeasurement> StackMain = new ConcurrentStack<FeatureMeasurement>();
    protected readonly ConcurrentStack<FeatureMeasurement> StackAlternative = new ConcurrentStack<FeatureMeasurement>();
    protected bool UseStackMain = true;
    protected readonly object Locker = new object();

    #region Abstract methods
    public abstract Task<double?> GetFirstSampleUntilDate(int featureCode, DateTime dateToTs);

    public abstract Task<IList<Measurement>> GetMeasurementsWithSamplesByDates(DcGetMeasurementsCriteria criteria);

    protected abstract Task GroupResultsAndSave(List<FeatureMeasurement> measurements, int retryCount = 3);

    #endregion

    #region Virtual methods

    public virtual void ProcessMeasurement(FeatureMeasurement measurement)
    {
      lock (Locker)
      {
        if (UseStackMain)
          StackMain.Push(measurement);
        else
          StackAlternative.Push(measurement); 
      }
    }

    public virtual async Task ProcessSaveMeasurements()
    {
      List<FeatureMeasurement> measurements;

      lock (Locker)
      {
        UseStackMain = !UseStackMain;

        if (UseStackMain)
        {
          measurements = StackAlternative.ToList();
          StackAlternative.Clear();
        }
        else
        {
          measurements = StackMain.ToList();
          StackMain.Clear();
        }
      }

      if (measurements.Any())
      {
        await GroupResultsAndSave(measurements);
      } 
    }

    protected virtual IList<MeasurementDto> GetMeasurementsFromCache(List<int> featureCodes, DateTime start,
      DateTime end)
    {
      var collectionToCheck = StackMain.ToList().Concat(StackAlternative.ToList());

      if(featureCodes.Contains(5100310))
      {
        var tempCheck = collectionToCheck.Where(x => x.FeatureCode == 5100310)
          .Select(x => (MeasurementDto)x)
        .ToList();

        if(tempCheck.Any())
          NotificationController.Debug("STD1 TQ_ACT values from cache: {@TQMeas} Start: {@Start}, End:{@End}", tempCheck, start, end);
      }

      var result = collectionToCheck
        .Where(x =>
          featureCodes.Contains(x.FeatureCode) &&
          x.MeasurementDateTime >= start &&
          x.MeasurementDateTime <= end)
        .Select(x => (MeasurementDto)x)
        .ToList();

      AddPreviousSamplesOutOfRange(featureCodes, start, collectionToCheck, result);

      return result;
    }

    protected virtual void AddPreviousSamplesOutOfRange(List<int> featureCodes, DateTime start, IEnumerable<FeatureMeasurement> collectionToCheck, List<MeasurementDto> result)
    {
      foreach (var featureCode in featureCodes)
      {
        MeasurementDto previousSample = collectionToCheck
          .OrderByDescending(x => x.MeasurementDateTime)
          .FirstOrDefault(x => x.FeatureCode == featureCode && x.MeasurementDateTime < start);

        if (previousSample != null)
          result.Add(previousSample);
      }
    }

    #endregion
  }
}
