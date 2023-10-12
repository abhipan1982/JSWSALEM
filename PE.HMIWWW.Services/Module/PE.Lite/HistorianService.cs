using Microsoft.AspNetCore.Http;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.DbEntity.PEContext;
using System.Threading.Tasks;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementAnalysis;
using System;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.HMIWWW.Core.Communication;
using PE.BaseModels.DataContracts.Internal.L1A;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMF.Module.UnitConverter;
using System.Collections.Generic;
using PE.BaseDbEntity.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class HistorianService : BaseService, IHistorianService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public HistorianService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public async Task<VM_RawMaterialMeasurementBundle> GetMeasurements(ModelStateDictionary modelState, long[] featureIds, DateTime startDate, DateTime endDate)
    {
      VM_RawMaterialMeasurementBundle result = new();

      try
      {
        DcAggregatedMeasurementRequest dc = new DcAggregatedMeasurementRequest
        {
          MeasurementListToProcess = featureIds.Select(x => new DcMeasurementRequest((int)x, startDate, endDate)).ToList()
        };

        //request data from module
        SendOfficeResult<DcRawMeasurementResponse> sendOfficeResult = await HmiSendOffice.GetRawMeasurementsAsync(dc);

        HandleWarnings(sendOfficeResult, ref modelState);


        if (!modelState.IsValid)
        {
          return result;
        }

        var featuresList = await _peContext.MVHFeatures
          .Where(x => featureIds.Contains(x.FeatureId))
          .ToListAsync();

        var features = featuresList.ToDictionary(key => key.FeatureId, value => value);

        result = new VM_RawMaterialMeasurementBundle
        {
          Measurements = sendOfficeResult.DataConctract.Measurements.Select(x => new VM_RawMaterialMeasurement
          {
            Feature = CreateFeatureDescription(x.FeatureCode, features),
            Measurement = new VM_RawMaterialMeasurementMeasurement
            {
              Samples = x.MeasurementSamples.OrderBy(x => x.MeasurementDate)
              .Select(s => new VM_RawMaterialMeasurementSample
              {
                Date = s.MeasurementDate,
                Value = s.Value
              }).ToList()
            }
          }).ToList()
        };
      }
      catch (Exception ex)
      {

        throw;
      }

      //return view model
      return result;
    }

    private static VM_RawMaterialMeasurementFeature CreateFeatureDescription(long featureCode, Dictionary<long, MVHFeature> features)
    {
      if (!features.TryGetValue(featureCode, out var mvhFeature))
        return null;

      var unit = UOMHelper.UOMCatalogue.Find(u => u.UnitId == mvhFeature.FKUnitOfMeasureId);

      return new VM_RawMaterialMeasurementFeature
      {
        FeatureId = mvhFeature.FeatureId,
        FeatureName = mvhFeature.FeatureName,
        IsLengthRelated = mvhFeature.IsLengthRelated,
        IsSampledFeature = mvhFeature.IsSampledFeature,
        Unit = unit?.UnitSymbol,
      };
    }
  }
}
