using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.MVH;

namespace PE.Module.Core.Handlers
{
  public class MeasurementsHandler
  {
    //TODOMN: use from local cache
    public async Task<long> ConvertExternalFeatureCodesToInternal(PEContext ctx, DcMeasData message)
    {
      int featureCode = message is DcMeasDataSample
        ? (message as DcMeasDataSample).FeatureCode
        : message.FeatureCode;

      long? baseFeature =
        await ctx.MVHFeatures
          .Where(w => w.FeatureCode == featureCode)
          .Select(s => s.FeatureId)
          .SingleOrDefaultAsync();

      return baseFeature ?? 0;
    }

    public Task<MVHFeature> GetFeature(PEContext ctx, int featureCode)
    {
      return ctx.MVHFeatures.Where(w => w.FeatureCode == featureCode).SingleOrDefaultAsync();
    }

    public MVHMeasurement SaveMeasurement(PEContext ctx, DcMeasData message)
    {
      long featureId = ConvertExternalFeatureCodesToInternal(ctx, message).Result;
      if (featureId > 0)
      {
        MVHMeasurement measurement = ProcessMeasurementBeforeSave(ctx, message, featureId);
        ctx.MVHMeasurements.Add(measurement);
        return measurement;
      }

      return null;
    }

    /// <summary>
    ///   Convert DCMeasDataParcel into database elements structure
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public MVHMeasurement ProcessMeasurementBeforeSave(PEContext ctx, DcMeasData message, long fkFeatureId)
    {
      MVHMeasurement measurement;
      if (message is DcMeasDataSample)
      {
        DcMeasDataSample measDataSample = message as DcMeasDataSample;
        measurement = new MVHMeasurement
        {
          FKFeatureId = /*message.BaseFeature*/fkFeatureId,
          IsValid = Convert.ToBoolean(measDataSample.Valid),
          ValueAvg = measDataSample.Avg,
          ValueMax = measDataSample.Max,
          ValueMin = measDataSample.Min,
          FirstMeasurementTs = message.FirstMeasurementTs,
          LastMeasurementTs = message.LastMeasurementTs,
          NoOfSamples = 0,
          CreatedTs = DateTime.Now,
          ActualLength = message.ActualLength,
        };

        if (measDataSample.Samples != null)
        {
          measurement.NoOfSamples = (short)measDataSample.Samples.Count;
        }
      }
      else
      {
        measurement = new MVHMeasurement
        {
          FKFeatureId = /*message.BaseFeature*/fkFeatureId,
          IsValid = Convert.ToBoolean(message.Valid),
          ValueAvg = message.Avg,
          ValueMax = message.Max,
          ValueMin = message.Min,
          FirstMeasurementTs= message.FirstMeasurementTs,
          LastMeasurementTs= message.LastMeasurementTs,
          NoOfSamples = 0,
          CreatedTs = DateTime.Now,
          ActualLength = message.ActualLength,
        };
      }

      if (message.RawMaterialId != 0) // in case if measurement is material related
      {
        measurement.FKRawMaterialId = message.RawMaterialId;
      }

      return measurement;
    }

    public void SaveMeasurementSamples(PEContext ctx, MVHMeasurement dbMeasurement, DcMeasDataSample measurement)
    {
      if (measurement.Samples == null || measurement.Samples.Count == 0 || dbMeasurement == null ||
          dbMeasurement.MeasurementId == 0)
      {
        return;
      }

      DataTable dt = new DataTable();
      dt.Columns.Add("SampleId");
      dt.Columns.Add("FKMeasurementId");
      dt.Columns.Add("IsValid");
      dt.Columns.Add("SampleValue");
      dt.Columns.Add("OffsetFromHead");

      foreach (DcSample sample in measurement.Samples)
      {
        dt.Rows.Add(0,
          dbMeasurement.MeasurementId,
          sample.IsValid,
          sample.Value,
          sample.HeadOffset);
      }

      IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

      var connectionString = configuration.GetConnectionString("PEContext");

      using (SqlBulkCopy sqlBulk =
        new SqlBulkCopy(connectionString, SqlBulkCopyOptions.Default))
      {
        sqlBulk.BatchSize = 5000;
        sqlBulk.DestinationTableName = "MVHSamples";
        sqlBulk.WriteToServer(dt);
      }
    }

    public async Task<IEnumerable<DcMeasData>> GetMeasDataAsync(PEContext ctx, int featureCode, int numerOfMeasurements)
    {
      IQueryable<MVHMeasurement> query = ctx.MVHMeasurements.Where(x => x.FKFeature.FeatureCode == featureCode)
        .OrderByDescending(x => x.MeasurementId).Take(numerOfMeasurements);
      List<DcMeasData> result = await query.Select(x => new DcMeasData
      {
        FeatureCode = x.FKFeature.FeatureCode,
        Valid = x.IsValid ?? false,
        Avg = x.ValueAvg,
        Max = x.ValueMax ?? 0,
        Min = x.ValueMin ?? 0
      }).ToListAsync();

      return result;
    }
  }
}
