using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Defect;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IDefectGroupsCatalogueService
  {
    VM_DefectGroupsCatalogue GetDefectGroup(ModelStateDictionary modelState, long id);
    DataSourceResult GetDefectGroupList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> AddDefectGroupAsync(ModelStateDictionary modelState, VM_DefectGroupsCatalogue vm);
    Task<VM_Base> UpdateDefectGroupAsync(ModelStateDictionary modelState, VM_DefectGroupsCatalogue vm);

    Task<VM_Base> DeleteDefectGroupAsync(ModelStateDictionary modelState, VM_DefectGroupsCatalogue vm);

    //IList<VM_DefectGroupsCatalogue> GetDefectGroups();
    Task<bool> ValidateDefectGroupsCode(string code);
    Task<bool> ValidateDefectGroupsName(string name);
  }
}
