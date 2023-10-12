using System.Collections.Generic;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IChargingService
  {
    List<VM_RawMaterialOverview> GetQueueAreas(ModelStateDictionary modelState, List<long> materialsInAreas);
  }
}
