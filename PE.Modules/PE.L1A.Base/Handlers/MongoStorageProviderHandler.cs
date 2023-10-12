using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.L1A.Base.Models;
using PE.L1A.Base.Models.MongoEntity;

namespace PE.L1A.Base.Handlers
{
  public class MongoStorageProviderHandler
  {
    public virtual Task<List<MongoMeasurement>> GroupResultFromCache(List<FeatureMeasurement> measurements)
    {
      List<MongoMeasurement> measurementsToSave = new List<MongoMeasurement>();
      
      var groupedMeasurements = measurements.GroupBy(x => x.FeatureCode);

      foreach (var group in groupedMeasurements)
      {
        measurementsToSave.Add(new MongoMeasurement()
        {
          FeatureCode = group.Key,
          ChunkStartDate = group.LastOrDefault()?.MeasurementDateTime,
          ExpiryDate = group.FirstOrDefault()?.ExpiryDateTime,
          Samples = group.Reverse().Select(x => new MongoSample() { TimeStamp = x.MeasurementDateTime, Value = x.Value })
        });
      }

      return Task.FromResult(measurementsToSave);
    }

    public virtual async Task<List<MeasurementDto>> GetMeasurementListByCriteria(MongoDB.Driver.IMongoCollection<MongoMeasurement> measurementCollection, DcGetMeasurementsCriteria criteria)
    {
      var dateFrom = criteria.DateFrom.ToUniversalTime();
      var dateTo = criteria.DateTo.ToUniversalTime();

      // Get all measurements which dates are in range of ChunkStartDate
      var resultToAnalyse = (await measurementCollection
        .Find(measurement =>
          criteria.FeatureCodes.Contains(measurement.FeatureCode) &&
          measurement.ChunkStartDate.HasValue &&
          measurement.ChunkStartDate >= dateFrom &&
          measurement.ChunkStartDate <= dateTo)
        .ToListAsync())
        .SelectMany(x => x.Samples
          .Select(s => new MeasurementDto(x.FeatureCode, s.TimeStamp.ToLocalTime(), s.Value)))
        .ToList();

      // Additional logic to add first outofrange sample and measurements from previous chunk
      foreach (var featureCode in criteria.FeatureCodes)
      {
        // In case if there are missing measurements from the previous chunk or there is missing previous sample
        if(!resultToAnalyse.Any(x => x.MeasurementDateTime < dateFrom &&
          x.FeatureCode == featureCode))
        {
          // Get previous chunk from Mongo
          var outOfRangeChunk = await measurementCollection
          .Find(measurement =>
            measurement.FeatureCode == featureCode &&
            measurement.ChunkStartDate.HasValue &&
            measurement.ChunkStartDate < dateFrom)
          .SortByDescending(measurement => measurement.ChunkStartDate)
          .Limit(1)
          .FirstOrDefaultAsync();

          if (outOfRangeChunk != null)
          {
            // Add missing samples from the previous chunk
            resultToAnalyse.AddRange(outOfRangeChunk
              .Samples
                .Where(s => s.TimeStamp >= dateFrom &&
                  s.TimeStamp <= dateTo)
                .Select(s => new MeasurementDto(outOfRangeChunk.FeatureCode, s.TimeStamp.ToLocalTime(), s.Value)));

            // Add previous sample
            resultToAnalyse.Add(
              outOfRangeChunk.Samples
              .OrderByDescending(x => x.TimeStamp)
              .Where(x => x.TimeStamp < dateFrom)
              .Select(x => new MeasurementDto(outOfRangeChunk.FeatureCode, x.TimeStamp.ToLocalTime(), x.Value))
              .First());
          }
        }
      }

      return resultToAnalyse;
    }
  }
}
