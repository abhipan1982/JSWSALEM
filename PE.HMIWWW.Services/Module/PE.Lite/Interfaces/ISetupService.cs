using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ISetupService
  {
    DataSourceResult GetSetupSearchGridData(ModelStateDictionary modelState, DataSourceRequest request, long setupType);
    string GetSetupNameByType(long setupType);
    Task<long> GetSetupType(long setupId);
    VM_ListOfFilters GetFiltersListForSetupWithValues(ModelStateDictionary modelState, long setupId);
    List<string> GetListOfFiltersNameForSetup(long setupType);
    List<VM_Filters> GetValueOfFiltersForSetup(long setupId);
    VM_ListOfFilters GetFiltersListForSetupType(ModelStateDictionary modelState, long setupType);
    Dictionary<long, string> GetFilteringData(string tableName, string columnId, string columnName);
    Task<VM_Base> CreateSetup(ModelStateDictionary modelState, VM_ListOfFilters model);
    Task<VM_Base> UpdateSetupParameters(ModelStateDictionary modelState, VM_ListOfFilters model);
    Task<VM_Base> UpdateSetupValue(ModelStateDictionary modelState, VM_SetupValues model);
    Task<VM_Base> CopySetup(ModelStateDictionary modelState, VM_ListOfFilters model);
    Task<VM_Base> DeleteSetup(ModelStateDictionary modelState, long setupId);
    Task<VM_Base> SendSetupsToL1(ModelStateDictionary modelState, long telegramId);
    Task<VM_Base> CalculateSetup(ModelStateDictionary modelState, long telegramId);
    DataSourceResult GetSetupInstructions(ModelStateDictionary modelState, DataSourceRequest request, long setupId);
  }
}
