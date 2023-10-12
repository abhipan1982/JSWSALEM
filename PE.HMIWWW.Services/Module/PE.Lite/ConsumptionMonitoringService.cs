using System;
using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring;
using PE.DbEntity.PEContext;
using Kendo.Mvc.Extensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  /// <summary>
  /// Not Used
  /// </summary>
  public class ConsumptionMonitoringService : BaseService, IConsumptionMonitoringService
  {
    private readonly HmiContext _hmiContext;

    public ConsumptionMonitoringService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
    }

    public DataSourceResult GetFeaturesList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_Feature> features = _hmiContext.V_Features
        .Where(x => x.IsConsumptionPoint)
        .AsQueryable();
      return features.ToDataSourceLocalResult(request, modelState, data => new VM_Feature(data));
    }

    public VM_Feature GetFeatureDetails(ModelStateDictionary modelState, long featureId)
    {
      VM_Feature result = null;

      if (featureId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      // Validate entry data
      if (!modelState.IsValid)
      {
        return result;
      }

      V_Feature feature = _hmiContext.V_Features
        .Where(x => x.FeatureId == featureId)
        .Single();

      result = new VM_Feature(feature);

      return result;
    }

    public DataSourceResult GetMeasurementData(ModelStateDictionary modelState, DataSourceRequest request,
      long featureId, DateTime dateFrom, DateTime dateTo)
    {
      int maxPointsConst = 500;

      VM_Measurement[] resultArray = new VM_Measurement[maxPointsConst];

      List<V_Measurement> measurements = _hmiContext.V_Measurements
        .Where(x => x.FeatureId == featureId && x.MeasurementTime >= dateFrom && x.MeasurementTime <= dateTo &&
                    x.IsValid)
        .OrderBy(x => x.MeasurementTime)
        .ToList();

      int measurementsCount = measurements.Count();

      if (measurementsCount > maxPointsConst)
      {
        double samplingRatio = measurementsCount / (double)maxPointsConst;

        double skipMeasurementsCounter = 0;
        double groupSize = 0;
        for (int groupCounter = 0; groupCounter < maxPointsConst; skipMeasurementsCounter += groupSize, groupCounter++)
        {
          groupSize = Math.Max((samplingRatio * (groupCounter + 1)) - Math.Floor(skipMeasurementsCounter), 1);

          List<V_Measurement> group = measurements.Skip((int)Math.Floor(skipMeasurementsCounter))
            .Take((int)Math.Floor(groupSize)).ToList();
          if (group.Any())
          {
            resultArray[groupCounter] = new VM_Measurement(new V_Measurement
            {
              MeasurementValueAvg = Math.Round(group.Select(x => x.MeasurementValueAvg).Average(), 2),
              MeasurementTime = new DateTime((long)Math.Round(
                new[] { group.First().MeasurementTime.Ticks, group.Last().MeasurementTime.Ticks }.Average()))
            });
          }
          else
          {

          }
        }
      }
      else
      {
        resultArray = measurements.Select(x => new VM_Measurement(x))
          .ToArray();
      }

      return resultArray.ToDataSourceLocalResult(request, (x) => x);
    }
  }
}
