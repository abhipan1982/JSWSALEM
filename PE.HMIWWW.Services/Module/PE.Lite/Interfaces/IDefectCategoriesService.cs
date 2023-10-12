using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDefectCatalogueCategoriesService
  {
    VM_DefectCatalogueCategory GetDefectCatalogueCategory(ModelStateDictionary modelState, long id);

    DataSourceResult GetDefectCatalogueCategoriesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_Base> AddDefectCatalogueCategoryAsync(ModelStateDictionary modelState, VM_DefectCatalogueCategory vm);
    Task<VM_Base> UpdateDefectCatalogueCategoriesAsync(ModelStateDictionary modelState, VM_DefectCatalogueCategory vm);
    Task<VM_Base> DeleteDefectCatalogueCategoryAsync(ModelStateDictionary modelState, VM_DefectCatalogueCategory vm);
    IList<VM_DefectCatalogueCategory> GetDefectCatalogueCategories();
    IList<VM_DefectGroupsCatalogue> GetDefectGroups();
    Task<bool> ValidateDefectCategoriesCode(string code);
    Task<bool> ValidateDefectCategoriesName(string name);
    SelectList GetEnumAssignmentTypeList();
  }
}
