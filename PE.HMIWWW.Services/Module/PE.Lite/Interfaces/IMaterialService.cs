using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.ViewModel.Module.Lite.Material;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IMaterialService
  {
    DataSourceResult GetMaterialSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    VM_MaterialOverview GetMaterialById(long? materialId);
    VM_MaterialOverview GetMaterialDetails(ModelStateDictionary modelState, long materialId);
    DataSourceResult GetNotAssignedMaterials(ModelStateDictionary modelState, DataSourceRequest request);
  }
}
