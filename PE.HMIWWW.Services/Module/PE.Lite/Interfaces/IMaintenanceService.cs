using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public interface IMaintenanceService
  {
    //IEnumerable<TreeElement> GetAssetTreeData(bool getDevices = true);
    //VM_Device GetEmptyDevice();
    //VM_Device GetDevice(long id);
    //VM_Incident GetEmptyIncident();
    //VM_Incident GetIncident(long id);
    //VM_Component GetEmptyComponent();
    //VM_Component GetComponent(long id);
    //VM_Action GetEmptyAction();
    //VM_Action GetAction(long id);

    //DataSourceResult GetIncidentTypeDetails(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentTypeId);

    //VM_Device GetMaintenanceDetails(ModelStateDictionary modelState, long elementId);
    //VM_Component GetComponentDetails(ModelStateDictionary modelState, long componentId);
    //VM_DeviceIncident GetIncidentDetails(ModelStateDictionary modelState, long componentId);
    //VM_DeviceGroup GetDeviceGroupDetails(ModelStateDictionary modelState, string deviceGroupName);
    //VM_DeviceGroup GetDeviceGroupDetailsById(ModelStateDictionary modelState, long deviceGroupId);
    //DataSourceResult GetDeviceOverviewList(ModelStateDictionary modelState, DataSourceRequest request);

    //DataSourceResult GetSubComponentsForComponent(ModelStateDictionary modelState, DataSourceRequest request,
    //  long componentId);

    //DataSourceResult GetIncidentsForDevice(ModelStateDictionary modelState, DataSourceRequest request, long deviceId);

    //DataSourceResult GetAllActionsForIncidentList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentId);

    //DataSourceResult GetAllIncidentsForComponentList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long componentId);

    //DataSourceResult GetAllComponentsForDevice(ModelStateDictionary modelState, DataSourceRequest request,
    //  long deviceId);

    //DataSourceResult GetSubDeviceGroups(ModelStateDictionary modelState, DataSourceRequest request, long deviceGroupId);

    //DataSourceResult GetIncidentsBetweenDatesList(ModelStateDictionary modelState,
    //  [DataSourceRequest] DataSourceRequest request, long deviceId, DateTime startDateTime, DateTime endDateTime);

    //List<VM_Component> ComponentsInDevice(long deviceId);
    //IList<VM_IncidentType> GetIncidentsTypeList();
    //Task<VM_Base> DeleteDevice(ModelStateDictionary modelState, VM_LongId viewModel);
    //Task<VM_Base> DeleteComponent(ModelStateDictionary modelState, VM_LongId viewModel);
    //Task<VM_Base> DeleteIncident(ModelStateDictionary modelState, VM_LongId viewModel);
    //Task<VM_Base> DeleteAction(ModelStateDictionary modelState, VM_LongId viewModel);
    //List<VM_AssetTreeListEl> GetAssetTreeListData();
    //List<VM_AssetTreeListEl> GetAssetOnlyTreeListData();
    //List<VM_ComponentsGroupsTreeListEl> GetComponentTreeListData(long deviceId);
  }
}
