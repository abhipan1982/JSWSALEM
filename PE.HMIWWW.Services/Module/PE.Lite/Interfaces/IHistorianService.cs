using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.MeasurementAnalysis;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IHistorianService
  {
    Task<VM_RawMaterialMeasurementBundle> GetMeasurements(ModelStateDictionary modelState, 
      long[] featureIds, DateTime startDate, DateTime endDate);
  }
}
