using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DbEntity.PEContext;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementAnalysis;
using SMF.Module.UnitConverter;
using System;
using Kendo.Mvc.UI;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementsSummary;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MeasurementAnalysisService : BaseService, IMeasurementAnalysisService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public MeasurementAnalysisService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetFeaturesByType(ModelStateDictionary modelState, DataSourceRequest request, int areaCode, bool lengthRelated)
    {
      DataSourceResult result = null;

      result = _hmiContext.V_Features
      .Where(x => x.AreaCode == areaCode && x.IsActive && x.IsLengthRelated == lengthRelated)
      .ToDataSourceLocalResult(request, modelState, data => new VM_FeatureMap(data));

      return result;
    }

    public async Task<VM_RawMaterialMeasurement> GeMaterialMeasurement(ModelStateDictionary modelState, long rawMaterialId, long featureId)
    {
      MVHMeasurement measurement = await _peContext.MVHMeasurements
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKFeature)
        .Include(x => x.MVHSamples)
        .FirstOrDefaultAsync(x => x.FKFeatureId == featureId && x.FKRawMaterialId == rawMaterialId);

      var result = PrepareMeasurement(measurement);
      return result;
    }

    public async Task<VM_RawMaterialMeasurementBundle> GetMeasurementComparison(long[] rawMaterialIds, long[] featureIds, bool timeNormalization)
    {
      var measurementsList = await _peContext.MVHMeasurements
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKFeature)
        .Include(x => x.MVHSamples)
        .Where(x => x.FKRawMaterialId.HasValue && rawMaterialIds.Contains(x.FKRawMaterialId.Value) && featureIds.Contains(x.FKFeatureId))
        .ToListAsync();

      var measurements = measurementsList.Select(x => PrepareMeasurement(x, !timeNormalization)).ToList();

      return new VM_RawMaterialMeasurementBundle { Measurements = measurements };
    }

    private VM_RawMaterialMeasurement PrepareMeasurement(MVHMeasurement measurement, bool offsetBaseDate = true)
    {
      if (measurement == null)
        return null;

      var unit = UOMHelper.UOMCatalogue.Find(x => x.UnitId == measurement.FKFeature.FKUnitOfMeasureId);

      VM_RawMaterialMeasurement result = new VM_RawMaterialMeasurement
      {
        Feature = new VM_RawMaterialMeasurementFeature
        {
          FeatureId = measurement.FKFeatureId,
          FeatureName = measurement.FKFeature.FeatureName,
          IsLengthRelated = measurement.FKFeature.IsLengthRelated,
          IsSampledFeature = measurement.FKFeature.IsSampledFeature,
          Unit = unit.UnitSymbol,
        },
        Material = new VM_RawMaterialMeasurementMaterial
        {
          RawmMaterialId = measurement.FKRawMaterialId.Value,
          RawmMaterialName = measurement.FKRawMaterial.RawMaterialName
        },
        Measurement = new VM_RawMaterialMeasurementMeasurement
        {
          FirstMeasurementTs = measurement.FirstMeasurementTs,
          LastMeasurementTs = measurement.LastMeasurementTs,
          IsValid = measurement.IsValid,
          ActualLength = measurement.ActualLength ?? 0,
          Min = measurement.ValueMin,
          Max = measurement.ValueMax,
          Avg = measurement.ValueAvg,
        }
      };

      if (measurement.FKFeature.IsLengthRelated && measurement.FKFeature.IsSampledFeature)
      {
        result.Measurement.Samples = measurement.MVHSamples.Select(x => new VM_RawMaterialMeasurementSample
        {
          Length = x.OffsetFromHead,
          Value = x.SampleValue
        }).ToList();
      }
      else if (!measurement.FKFeature.IsLengthRelated && measurement.FKFeature.IsSampledFeature)
      {
        var baseDate = offsetBaseDate ? measurement.FirstMeasurementTs ?? DateTime.MinValue : DateTime.MinValue;

        result.Measurement.Samples = measurement.MVHSamples
          .Where(x => x.OffsetFromHead > 0)
          .Select(x => new VM_RawMaterialMeasurementSample
          {
            Date = baseDate.AddSeconds(x.OffsetFromHead),
            Value = x.SampleValue
          }).ToList();
      }

      return result;
    }
  }
}
