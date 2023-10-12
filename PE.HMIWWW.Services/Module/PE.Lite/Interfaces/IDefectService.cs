using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDefectService
  {
    VM_DefectCatalogue GetDelayCatalogue(long id);

    DataSourceResult GetDefectCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_Base> AddDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    Task<VM_Base> UpdateDefectCatalogue(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    Task<VM_Base> DeleteDefectCatalogueAsync(ModelStateDictionary modelState, VM_DefectCatalogue defectCatalogue);
    VM_DefectCatalogue GetDefectDetails(ModelStateDictionary modelState, long id);
    IList<VM_DefectCatalogueCategory> GetDefectCategoryList();
    IList<VM_DefectCatalogue> GetParentDefectCatalogues();
    Task<bool> ValidateDefectCode(string code);
    Task<bool> ValidateDefectName(string name);
  }
}
