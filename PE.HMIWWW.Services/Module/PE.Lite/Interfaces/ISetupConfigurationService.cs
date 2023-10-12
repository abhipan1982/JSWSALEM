using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Setup;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface ISetupConfigurationService
  {
    DataSourceResult GetConfigurationSearchList(ModelStateDictionary modelState, DataSourceRequest request);
    Task<VM_SetupConfiguration> FindConfigurationAsync(long configurationId);
    DataSourceResult GetSetupConfigurationDetails(ModelStateDictionary modelState, DataSourceRequest request, long configurationId);
    DataSourceResult GetSetupConfigurationsSearchGridData(ModelStateDictionary modelState, DataSourceRequest request, long configurationId);
    Task<long> GetSetupType(long setupId);
    List<string> GetListOfFiltersNameForSetup(long setupTypeId);
    DataSourceResult GetSetupParametersGridData(ModelStateDictionary modelState, DataSourceRequest request, long setupId, long setupConfigurationId);
    DataSourceResult GetSetupInstructions(ModelStateDictionary modelState, DataSourceRequest request, long setupId);
    VM_SetupConfiguration GetEmptyConfiguration();
    Task<VM_Base> CreateSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model);
    VM_SetupConfiguration GetConfiguration(long configurationId, bool includeSetups);
    Task<VM_Base> EditSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model);
    Task<VM_Base> DeleteSetupConfigurationAsync(ModelStateDictionary modelState, long configurationId);
    Task<VM_Base> SendSetupConfigurationAsync(ModelStateDictionary modelState, long configurationId, bool steelgradeRelated);
    Task<VM_Base> CloneSetupConfigurationAsync(ModelStateDictionary modelState, VM_SetupConfiguration model);
    Task<VM_Base> CreateSetupConfigurationVersionAsync(ModelStateDictionary modelState, long configurationId);
  }
}
