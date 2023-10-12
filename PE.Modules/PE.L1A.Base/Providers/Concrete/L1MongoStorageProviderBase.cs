using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.L1A.Base.Configuration;
using PE.L1A.Base.Handlers;
using PE.L1A.Base.Models;
using PE.L1A.Base.Models.MongoEntity;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Concrete
{
  public class L1MongoStorageProviderBase : L1MeasurementStorageProviderBase
  {
    protected IMongoCollection<MongoMeasurement> MeasurementCollection;
    protected MongoStorageProviderHandler MongoStorageProviderHandler;

    public L1MongoStorageProviderBase(IMeasurementsDatabaseInstanceSettings settings)
    {
      Init(settings);
    }

    public override async Task<double?> GetFirstSampleUntilDate(int featureCode, DateTime dateToTs)
    {
      double? result = null;
      var resultFromStacks = StackMain.ToList().Concat(StackAlternative.ToList())
        .OrderByDescending(x => x.MeasurementDateTime)
        .FirstOrDefault(x =>
          x.FeatureCode == featureCode &&
          x.MeasurementDateTime <= dateToTs);

      if (resultFromStacks != null)
        result = resultFromStacks.Value;

      if (result == null)
      {
        var dateToInUtc = dateToTs.ToUniversalTime();
        var chunk = await MeasurementCollection
          .Find(x =>
            x.FeatureCode == featureCode &&
            x.ChunkStartDate <= dateToInUtc)
          .SortByDescending(x => x.ChunkStartDate)
          .FirstOrDefaultAsync();

        if (chunk != null)
        {
          result = chunk.Samples
            .Where(x => x.TimeStamp <= dateToInUtc)
            .OrderByDescending(x => x.TimeStamp)
            .FirstOrDefault()?
            .Value;
        }
      }

      return result;
    }

    public override async Task<IList<Measurement>> GetMeasurementsWithSamplesByDates(DcGetMeasurementsCriteria criteria)
    {
      var measurementsGroupedByFeatureCodeBySearchCriteria =
        await GetMeasurementsGroupedByFeatureCodeBySearchCriteria(criteria);

      List<Measurement> result = new List<Measurement>();

      foreach (var group in measurementsGroupedByFeatureCodeBySearchCriteria)
      {
        result.Add(new Measurement()
        {
          FeatureCode = group.Key,
          Samples = group
            .Select(s => new MeasurementSample() { MeasurementDate = s.MeasurementDateTime, Value = s.Value })
            .ToList()
        });
      }

      return result;
    }

    protected override async Task GroupResultsAndSave(List<FeatureMeasurement> measurements, int retryCount = 3)
    {
      try
      {
        if (!measurements.Any())
          return;

        List<MongoMeasurement> measurementsToSave = await MongoStorageProviderHandler.GroupResultFromCache(measurements);

        await MeasurementCollection.InsertManyAsync(measurementsToSave);
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex);

        if (retryCount > 0)
          await GroupResultsAndSave(measurements, retryCount - 1);
      }
    }

    protected virtual async Task<IEnumerable<IGrouping<int, MeasurementDto>>> GetMeasurementsGroupedByFeatureCodeBySearchCriteria(DcGetMeasurementsCriteria criteria)
    {
      List<MeasurementDto> notGroupedResult = new List<MeasurementDto>();

      notGroupedResult.AddRange(base.GetMeasurementsFromCache(criteria.FeatureCodes, criteria.DateFrom, criteria.DateTo));

      notGroupedResult.AddRange(await MongoStorageProviderHandler.GetMeasurementListByCriteria(MeasurementCollection, criteria));

      notGroupedResult = FilterSamples(criteria, notGroupedResult);

      return notGroupedResult.GroupBy(x => x.FeatureCode);
    }

    protected virtual List<MeasurementDto> FilterSamples(DcGetMeasurementsCriteria criteria, List<MeasurementDto> notGroupedResult)
    {
      var result = new List<MeasurementDto>();
      var dateFrom = criteria.DateFrom.ToLocalTime();
      var dateTo = criteria.DateTo.ToLocalTime();

      foreach (var featureCode in criteria.FeatureCodes)
      {
        var outOfRangeSample = notGroupedResult
        .OrderByDescending(x => x.MeasurementDateTime)
        .FirstOrDefault(x => x.MeasurementDateTime < dateFrom &&
          x.FeatureCode == featureCode);

        if (outOfRangeSample != null)
          result.Add(outOfRangeSample);

        result.AddRange(notGroupedResult
          .Where(x => x.MeasurementDateTime >= dateFrom &&
            x.MeasurementDateTime <= dateTo &&
            x.FeatureCode == featureCode)
          .ToList());
      }

      return result;
    }

    protected virtual void Init(IMeasurementsDatabaseInstanceSettings settings)
    {
      var mongoSetting = settings.MeasurementsDatabaseSettingInstances["MongoDatabase"];

      MongoClient mongoClient = new MongoClient(mongoSetting.Server);
      IMongoDatabase mongoDatabaseInstance = mongoClient.GetDatabase(mongoSetting.DatabaseName);

      MeasurementCollection = mongoDatabaseInstance.GetCollection<MongoMeasurement>(mongoSetting.CollectionName);
      MongoStorageProviderHandler = new MongoStorageProviderHandler();
    }
  }
}
