using System;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.ConsumptionMonitoring;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IConsumptionMonitoringService
  {
    DataSourceResult GetFeaturesList(ModelStateDictionary modelState, DataSourceRequest request);

    VM_Feature GetFeatureDetails(ModelStateDictionary modelState, long featureId);

    DataSourceResult GetMeasurementData(ModelStateDictionary modelState, DataSourceRequest request, long featureId,
      DateTime dateFrom, DateTime dateTo);
  }
}
