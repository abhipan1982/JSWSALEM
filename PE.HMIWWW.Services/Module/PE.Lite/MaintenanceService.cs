using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using Kendo.Mvc.Extensions;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class MaintenanceService : BaseService, IMaintenanceService
  {
    private readonly PEContext _peContext;
    /// <summary>
    /// Not Used
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="peContext"></param>
    public MaintenanceService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public IEnumerable<TreeElement> GetAssetTreeData(bool getDevices = true)
    //{
    //  List<TreeElement> treeElements = new List<TreeElement>();

    //  IEnumerable<MVHAsset> assets = _peContext.MVHAssets.Where(z => z.IsActive == true).Include(z => z.FKParentAsset)
    //    .Include(z => z.MNTDeviceOnAssets).ToList();
    //  if (assets != null)
    //  {
    //    BuildTree(assets, ref treeElements, getDevices);
    //  }
    //  return treeElements;
    //}
    //public List<VM_ComponentsGroupsTreeListEl> GetComponentTreeListData(long deviceId)
    //{
    //  List<VM_ComponentsGroupsTreeListEl> resultFinal = new List<VM_ComponentsGroupsTreeListEl>();
    //  List<VM_Component> result = new List<VM_Component>();

    //  //Get all components from the device
    //  MNTDevice mNtDevice = _peContext.MNTDevices.Where(z => z.DeviceId == deviceId)
    //    .Include(z => z.MNTDeviceComponents)
    //    .Include(z => z.FKSupplier)
    //    .Include(z => z.FKDeviceGroup)
    //    .FirstOrDefault();
    //  if (mNtDevice != null)
    //  {
    //    VM_Component device = new VM_Component();

    //    device.ComponentCode = mNtDevice.DeviceCode;
    //    device.ComponentId = mNtDevice.DeviceId;
    //    device.ComponentType = ResourceController.GetResourceTextByResourceKey("NAME_Device");
    //    device.ComponentName = mNtDevice.DeviceName;
    //    device.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //    device.IsActive = true;
    //    if (mNtDevice.FKSupplier != null)
    //    {
    //      device.Supplier = mNtDevice.FKSupplier.SupplierName ?? " - ";
    //    }
    //    else
    //    {
    //      device.Supplier = " - ";
    //    }

    //    if (mNtDevice.FKDeviceGroup != null)
    //    {
    //      device.GroupCode = mNtDevice.FKDeviceGroup.DeviceGroupCode ?? "-";
    //    }
    //    else
    //    {
    //      device.GroupCode = "-";
    //    }

    //    device.ColorScheme = SelectColourByType(device.ComponentType);
    //    result.Add(device);

    //    int i = 0;
    //    foreach (MNTDeviceComponent element in mNtDevice.MNTDeviceComponents)
    //    {
    //      IList<MNTComponent> componentsInDevice = _peContext.MNTComponents
    //        .Where(z => z.ComponentId == element.FKComponentId)
    //        .Include(z => z.FKComponentGroup.FKParentComponentGroup).ToList();
    //      foreach (MNTComponent innerComponentElement in componentsInDevice)
    //      {
    //        VM_Component component = new VM_Component();
    //        component.ComponentCode = innerComponentElement.ComponentCode;
    //        component.ComponentName = innerComponentElement.ComponentName;
    //        component.ComponentType = "Component";
    //        component.GroupCode = "-";
    //        component.ColorScheme = SelectColourByType(device.ComponentType);
    //        component.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //        component.IsActive = true;
    //        i = 1;
    //        GoToNextLevel(innerComponentElement, ref device);
    //      }
    //    }
    //  }

    //  foreach (VM_Component element in result)
    //  {
    //    //Get the node
    //    VM_ComponentsGroupsTreeListEl el = new VM_ComponentsGroupsTreeListEl(null, (long)element.ComponentId,
    //      element.ComponentName, true, element.ComponentId, false, false);
    //    resultFinal.Add(el);
    //    //Check if node has any items inside
    //    if (element.Items.Count > 0)
    //    {
    //      CheckData(element, ref resultFinal);
    //    }
    //  }


    //  return resultFinal;

    //  //List<VM_AssetTreeListEl> treeElements = new List<VM_AssetTreeListEl>();
    //  //using (PEContext ctx = new PEContext())
    //  //{
    //  //	IEnumerable<MVHAsset> assets = ctx.MVHAssets.Where(z => z.IsActive == true).Include(z => z.MVHAssets1).Include(z => z.MNTDeviceOnAssets).ToList();
    //  //	if (assets != null)
    //  //	{
    //  //		foreach (MVHAsset asset in assets)
    //  //		{
    //  //			VM_AssetTreeListEl el = new VM_AssetTreeListEl(asset.ParentAssetId, asset.AssetId, asset.AssetName + " - " + asset.AssetDescription, false, null, asset.IsArea);

    //  //			treeElements.Add(el);
    //  //			if (asset.MNTDeviceOnAssets.Count > 0)
    //  //			{
    //  //				foreach (MNTDeviceOnAsset element in asset.MNTDeviceOnAssets)
    //  //				{
    //  //					MNTDeviceComponent MNTDeviceComponent = ctx.MNTDeviceComponents.Where(z => z.DeviceComponentId == element.DeviceOnAssetId).Include(z => z.MNTDevice).FirstOrDefault();

    //  //					VM_AssetTreeListEl device = new VM_AssetTreeListEl(asset.AssetId, MNTDeviceComponent.MNTDevice.DeviceId + 8000, MNTDeviceComponent.MNTDevice.DeviceName, true, MNTDeviceComponent.MNTDevice.DeviceId, false);
    //  //					treeElements.Add(device);
    //  //				}
    //  //			}
    //  //		}
    //  //	}
    //  //}
    //  //return treeElements;
    //}

    //public List<VM_AssetTreeListEl> GetAssetTreeListData()
    //{
    //  List<VM_AssetTreeListEl> treeElements = new List<VM_AssetTreeListEl>();

    //  IEnumerable<MVHAsset> assets = _peContext.MVHAssets.Where(z => z.IsActive == true).Include(z => z.FKParentAsset)
    //    .Include(z => z.MNTDeviceOnAssets).ToList();
    //  if (assets != null)
    //  {
    //    foreach (MVHAsset asset in assets)
    //    {
    //      VM_AssetTreeListEl el = new VM_AssetTreeListEl(asset.FKParentAssetId, asset.AssetId,
    //        asset.AssetName + " - " + asset.AssetDescription, false, null, asset.IsArea);

    //      treeElements.Add(el);
    //      if (asset.MNTDeviceOnAssets.Count > 0)
    //      {
    //        foreach (MNTDeviceOnAsset element in asset.MNTDeviceOnAssets)
    //        {
    //          if (element.OnLineTs != null && element.OffLineTs == null)
    //          {
    //            MNTDeviceComponent mntDeviceComponent = _peContext.MNTDeviceComponents
    //              .Where(z => z.DeviceComponentId == element.DeviceOnAssetId).Include(z => z.FKDevice)
    //              .FirstOrDefault();

    //            VM_AssetTreeListEl device = new VM_AssetTreeListEl(asset.AssetId,
    //              mntDeviceComponent.FKDevice.DeviceId + 8000, mntDeviceComponent.FKDevice.DeviceName, true,
    //              mntDeviceComponent.FKDevice.DeviceId, false);
    //            treeElements.Add(device);
    //          }
    //        }
    //      }
    //    }
    //  }

    //  return treeElements;
    //}

    //public List<VM_AssetTreeListEl> GetAssetOnlyTreeListData()
    //{
    //  List<VM_AssetTreeListEl> treeElements = new List<VM_AssetTreeListEl>();
    //  IEnumerable<MVHAsset> assets = _peContext.MVHAssets.Where(z => z.IsActive == true).Include(z => z.InverseFKParentAsset)
    //    .Include(z => z.MNTDeviceOnAssets).ToList();
    //  if (assets != null)
    //  {
    //    foreach (MVHAsset asset in assets)
    //    {
    //      VM_AssetTreeListEl el = new VM_AssetTreeListEl(asset.FKParentAssetId, asset.AssetId,
    //        asset.AssetName + " - " + asset.AssetDescription, false, null, asset.IsArea);
    //      treeElements.Add(el);
    //    }
    //  }

    //  return treeElements;
    //}

    //public DataSourceResult GetDeviceOverviewList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTDevices.Where(z => z.DeviceId > 0 && z.MNTDeviceOnAssets.Count() == 0)
    //    .Include(z => z.MNTDeviceOnAssets).ToDataSourceLocalResult(request, modelState, data => new VM_Device(data));

    //  return result;
    //}

    //public VM_Device GetMaintenanceDetails(ModelStateDictionary modelState, long elementId)
    //{
    //  VM_Device result = null;

    //  if (elementId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (elementId != 0)
    //  {
    //    MNTDevice device = _peContext.MNTDevices
    //      .Include(i => i.MNTDeviceComponents)
    //      .Include(i => i.FKSupplier)
    //      .Include(i => i.FKDeviceGroup)
    //      .Where(x => x.DeviceId == elementId)
    //      .Single();

    //    result = new VM_Device(device);
    //  }
    //  else
    //  {
    //    result = new VM_Device();
    //  }

    //  return result;
    //}

    //public VM_Component GetComponentDetails(ModelStateDictionary modelState, long componentId)
    //{
    //  VM_Component result = null;

    //  if (componentId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (componentId != 0)
    //  {
    //    MNTComponent component = _peContext.MNTComponents
    //      .Include(i => i.MNTDeviceComponents).Include(i => i.FKDefaultSupplier)
    //      .Where(x => x.ComponentId == componentId)
    //      .Single();

    //    result = new VM_Component(component);
    //  }

    //  return result;
    //}

    //public VM_DeviceIncident GetIncidentDetails(ModelStateDictionary modelState, long incidentId)
    //{
    //  VM_DeviceIncident result = null;

    //  if (incidentId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (incidentId != 0)
    //  {
    //    MNTIncident incident = _peContext.MNTIncidents
    //      .Include(x => x.FKIncidentType)
    //      .Where(x => x.IncidentId == incidentId)
    //      .Single();

    //    result = new VM_DeviceIncident(incident);
    //  }

    //  return result;
    //}

    //public VM_DeviceGroup GetDeviceGroupDetails(ModelStateDictionary modelState, string deviceGroupName)
    //{
    //  VM_DeviceGroup result = null;

    //  if (String.IsNullOrEmpty(deviceGroupName))
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (deviceGroupName != null)
    //  {
    //    MNTDeviceGroup deviceGroup = _peContext.MNTDeviceGroups
    //      .Include(x => x.FKParentDeviceGroup)
    //      .Where(x => x.DeviceGroupCode == deviceGroupName)
    //      .FirstOrDefault();

    //    result = new VM_DeviceGroup(deviceGroup);
    //  }

    //  return result;
    //}

    //public VM_DeviceGroup GetDeviceGroupDetailsById(ModelStateDictionary modelState, long deviceGroupId)
    //{
    //  VM_DeviceGroup result = null;

    //  if (deviceGroupId == 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (deviceGroupId != 0)
    //  {
    //    MNTDeviceGroup deviceGroup = _peContext.MNTDeviceGroups
    //      .Include(x => x.FKParentDeviceGroup)
    //      .Where(x => x.DeviceGroupId == deviceGroupId)
    //      .FirstOrDefault();

    //    result = new VM_DeviceGroup(deviceGroup);
    //  }

    //  return result;
    //}

    //public DataSourceResult GetIncidentsBetweenDatesList(ModelStateDictionary modelState,
    //  [DataSourceRequest] DataSourceRequest request, long deviceId, DateTime startDateTime, DateTime endDateTime)
    //{
    //  IQueryable<MNTIncident> incidents = _peContext.MNTIncidents.Include(x => x.FKDeviceComponent)
    //    .Where(x => x.StartTime >= startDateTime && x.EndTime <= endDateTime &&
    //                x.FKDeviceComponent.FKDeviceId == deviceId)
    //    .OrderBy(o => o.StartTime).AsQueryable();
    //  return incidents.ToDataSourceLocalResult(request, modelState, data => new VM_DeviceIncident(data));
    //}
    //public List<VM_Component> ComponentsInDevice(long deviceId)
    //{
    //  List<VM_Component> result = new List<VM_Component>();

    //  //Get all components from the device
    //  MNTDevice mNtDevice = _peContext.MNTDevices.Where(z => z.DeviceId == deviceId)
    //    .Include(z => z.MNTDeviceComponents)
    //    .Include(z => z.FKSupplier)
    //    .Include(z => z.FKDeviceGroup)
    //    .FirstOrDefault();
    //  if (mNtDevice != null)
    //  {
    //    VM_Component device = new VM_Component();

    //    device.ComponentCode = mNtDevice.DeviceCode;
    //    device.ComponentId = mNtDevice.DeviceId;
    //    device.ComponentType = ResourceController.GetResourceTextByResourceKey("NAME_Device");
    //    device.ComponentName = mNtDevice.DeviceName;
    //    device.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //    device.IsActive = true;
    //    if (mNtDevice.FKSupplier != null)
    //    {
    //      device.Supplier = mNtDevice.FKSupplier.SupplierName ?? " - ";
    //    }
    //    else
    //    {
    //      device.Supplier = " - ";
    //    }

    //    if (mNtDevice.FKDeviceGroup != null)
    //    {
    //      device.GroupCode = mNtDevice.FKDeviceGroup.DeviceGroupCode ?? "-";
    //    }
    //    else
    //    {
    //      device.GroupCode = "-";
    //    }

    //    device.ColorScheme = SelectColourByType(device.ComponentType);
    //    result.Add(device);

    //    int i = 0;
    //    foreach (MNTDeviceComponent element in mNtDevice.MNTDeviceComponents)
    //    {
    //      IList<MNTComponent> componentsInDevice = _peContext.MNTComponents
    //        .Where(z => z.ComponentId == element.FKComponentId)
    //        .Include(z => z.FKComponentGroup.InverseFKParentComponentGroup).ToList();
    //      foreach (MNTComponent innerComponentElement in componentsInDevice)
    //      {
    //        VM_Component component = new VM_Component();
    //        component.ComponentCode = innerComponentElement.ComponentCode;
    //        component.ComponentName = innerComponentElement.ComponentName;
    //        component.ComponentType = "Component";
    //        component.GroupCode = "-";
    //        component.ColorScheme = SelectColourByType(device.ComponentType);
    //        component.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //        component.IsActive = true;
    //        i = 1;
    //        GoToNextLevel(innerComponentElement, ref device);
    //      }
    //    }
    //  }

    //  return result;
    //}


    //public DataSourceResult GetSubComponentsForComponent(ModelStateDictionary modelState, DataSourceRequest request,
    //  long componentId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTComponents.Where(z => z.ComponentId == componentId).Include(i => i.MNTDeviceComponents)
    //    .Include(i => i.FKDefaultSupplier)
    //    .ToDataSourceLocalResult(request, modelState, x => new VM_Component(x));

    //  return result;
    //}

    //public DataSourceResult GetIncidentsForDevice(ModelStateDictionary modelState, DataSourceRequest request,
    //  long deviceId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTIncidents
    //    .Where(z => z.FKDeviceComponent.FKDeviceId == deviceId || z.FKDeviceComponent.FKDeviceId == deviceId)
    //    .Include(i => i.FKDeviceComponent.FKComponent)
    //    .Include(i => i.FKIncidentType)
    //    .ToDataSourceLocalResult(request, modelState, x => new VM_DeviceIncident(x));

    //  return result;
    //}

    //public DataSourceResult GetAllComponentsForDevice(ModelStateDictionary modelState, DataSourceRequest request,
    //  long deviceId)
    //{
    //  DataSourceResult result = null;

    //  IList<MNTDeviceComponent> mNtDeviceComponents =
    //    _peContext.MNTDeviceComponents.Where(z => z.FKDeviceId == deviceId).ToList();
    //  IList<MNTComponent> mNtComponents = new List<MNTComponent>();
    //  foreach (MNTDeviceComponent element in mNtDeviceComponents)
    //  {
    //    mNtComponents.AddRange(_peContext.MNTComponents.Where(z => z.ComponentId == element.FKComponentId)
    //      .Include(i => i.FKComponentGroup)
    //      .Include(i => i.FKDefaultSupplier)
    //      .ToList());

    //    result = mNtComponents.ToDataSourceLocalResult(request, modelState, x => new VM_Component(x));
    //  }

    //  return result;
    //}

    //public DataSourceResult GetSubDeviceGroups(ModelStateDictionary modelState, DataSourceRequest request,
    //  long deviceGroupId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTDeviceGroups.Where(z => z.FKParentDeviceGroupId == deviceGroupId)
    //    .ToDataSourceLocalResult(request, modelState, x => new VM_DeviceGroup(x));

    //  return result;
    //}

    //public VM_Incident GetEmptyIncident()
    //{
    //  VM_Incident result = new VM_Incident();
    //  return result;
    //}

    //public IList<VM_IncidentType> GetIncidentsTypeList()
    //{
    //  List<VM_IncidentType> result = new List<VM_IncidentType>();

    //  IQueryable<MNTIncidentType> dbList = _peContext.MNTIncidentTypes.AsQueryable();
    //  foreach (MNTIncidentType item in dbList)
    //  {
    //    result.Add(new VM_IncidentType(item));
    //  }

    //  return result;
    //}

    //public VM_Incident GetIncident(long id)
    //{
    //  VM_Incident result = null;

    //  MNTIncident eq = _peContext.MNTIncidents
    //    .Include(i => i.FKDeviceComponent.FKComponent)
    //    .Include(i => i.FKIncidentType)
    //    .Include(i => i.MNTActions)
    //    .SingleOrDefault(x => x.IncidentId == id);
    //  result = eq == null ? null : new VM_Incident(eq);

    //  return result;
    //}

    //public VM_Device GetEmptyDevice()
    //{
    //  VM_Device result = new VM_Device();
    //  return result;
    //}

    //public VM_Device GetDevice(long id)
    //{
    //  VM_Device result = null;

    //  MNTDevice eq = _peContext.MNTDevices
    //    .Include(i => i.MNTDeviceComponents)
    //    .Include(i => i.FKDeviceGroup)
    //    .Include(i => i.MNTDeviceOnAssets)
    //    .Include(i => i.FKSupplier)
    //    .Include(i => i.MNTLimitCounters)
    //    .SingleOrDefault(x => x.DeviceId == id);
    //  result = eq == null ? null : new VM_Device(eq);

    //  return result;
    //}

    //public VM_Action GetEmptyAction()
    //{
    //  VM_Action result = new VM_Action();
    //  return result;
    //}

    //public VM_Action GetAction(long id)
    //{
    //  VM_Action result = null;

    //  MNTAction eq = _peContext.MNTActions
    //    .Include(i => i.FKActionType)
    //    .Include(i => i.FKIncident)
    //    .Include(i => i.FKMember)
    //    .SingleOrDefault(x => x.ActionId == id);
    //  result = eq == null ? null : new VM_Action(eq);

    //  return result;
    //}

    //public DataSourceResult GetIncidentTypeDetails(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentTypeId)
    //{
    //  IQueryable<MNTIncidentType> incidents = _peContext.MNTIncidentTypes
    //    .Where(x => x.IncidentTypeId == incidentTypeId);
    //  return incidents.ToDataSourceLocalResult(request, modelState, data => new VM_IncidentType(data));
    //}

    //public VM_Component GetEmptyComponent()
    //{
    //  VM_Component result = new VM_Component();
    //  return result;
    //}

    //public VM_Component GetComponent(long id)
    //{
    //  VM_Component result = null;

    //  MNTComponent eq = _peContext.MNTComponents.Include(i => i.FKComponentGroup).Include(i => i.FKDefaultSupplier)
    //    .Include(i => i.MNTDeviceComponents)
    //    .SingleOrDefault(x => x.ComponentId == id);
    //  result = eq == null ? null : new VM_Component(eq);

    //  return result;
    //}

    //public DataSourceResult GetAllActionsForIncidentList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTActions
    //    .Where(z => z.FKIncidentId == incidentId)
    //    .Include(i => i.FKActionType)
    //    .Include(i => i.FKMember)
    //    .Include(i => i.FKIncident)
    //    .ToDataSourceLocalResult(request, modelState, x => new VM_Action(x));

    //  return result;
    //}

    //public DataSourceResult GetAllIncidentsForComponentList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long componentId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTIncidents
    //    .Where(z => z.FKDeviceComponent.FKComponentId == componentId)
    //    .Include(i => i.MNTActions)
    //    .Include(i => i.FKDeviceComponent)
    //    .Include(i => i.FKIncidentType)
    //    .ToDataSourceLocalResult(request, modelState, x => new VM_Incident(x));

    //  return result;
    //}

    //public async Task<VM_Base> DeleteDevice(ModelStateDictionary modelState, VM_LongId viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.Id <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCDevice entryDataContract = new DCDevice {DeviceId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteDevice(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteAction(ModelStateDictionary modelState, VM_LongId viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.Id <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCAction entryDataContract = new DCAction {ActionId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteAction(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteComponent(ModelStateDictionary modelState, VM_LongId viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.Id <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCComponent entryDataContract = new DCComponent {ComponentId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteComponent(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteIncident(ModelStateDictionary modelState, VM_LongId viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.Id <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCIncident entryDataContract = new DCIncident {IncidentId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteIncident(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public VM_Equipment GetEquipment(long id)
    //{
    //  VM_Equipment result = null;

    //  using (PEContext ctx = new PEContext())
    //  {
    //    //MNTEquipment eq = ctx.MNTEquipments.Include(i => i.MNTEquipmentGroup)
    //    //  .SingleOrDefault(x => x.EquipmentId == id);
    //    //result = eq == null ? null : new VM_Equipment(eq);
    //  }

    //  return result;
    //}

    //public VM_EquipmentStatus GetEquipmentStatus(long id)
    //{
    //  VM_EquipmentStatus result = null;

    //  using (PEContext ctx = new PEContext())
    //  {
    //    //MNTEquipment eq = ctx.MNTEquipments
    //    //  .SingleOrDefault(x => x.EquipmentId == id);
    //    //result = eq == null ? null : new VM_EquipmentStatus(eq);
    //  }

    //  return result;
    //}

    //public VM_Equipment GetEmptyEquipment()
    //{
    //  VM_Equipment result = new VM_Equipment();
    //  return result;
    //}

    //public DataSourceResult GetEquipmentList(ModelStateDictionary modelState,
    //  [DataSourceRequest] DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  using (PEContext ctx = new PEContext())
    //  {
    //    //result = ctx.MNTEquipments.Include(i => i.MNTEquipmentGroup)
    //    //  .ToDataSourceLocalResult<MNTEquipment, VM_Equipment>(request, modelState, x => new VM_Equipment(x));
    //  }

    //  return result;
    //}

    //public async Task<VM_Base> CreateEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    //{
    //  VM_Base result = new VM_Base();

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  UnitConverterHelper.ConvertToSi(ref vm);

    //  DCEquipment dc = new DCEquipment();
    //  dc.AlarmValue = vm.AlarmValue;
    //  dc.EquipmentCode = vm.EquipmentCode;
    //  dc.EquipmentDescription = vm.EquipmentDescription;
    //  dc.EquipmentGroupId = vm.EquipmentGroupId;
    //  dc.EquipmentId = vm.EquipmentId;
    //  dc.EquipmentName = vm.EquipmentName;
    //  dc.ServiceExpires = vm.ServiceExpires;
    //  dc.WarningValue = vm.WarningValue;

    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCreateRequest(dc);

    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  return result;
    //}

    //public async Task<VM_Base> UpdateEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    //{
    //  VM_Base result = new VM_Base();
    //  ValidateEditEquipment(modelState, vm);

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  UnitConverterHelper.ConvertToSi(ref vm);

    //  DCEquipment dc = new DCEquipment();
    //  dc.AlarmValue = vm.AlarmValue;
    //  dc.EquipmentCode = vm.EquipmentCode;
    //  dc.EquipmentDescription = vm.EquipmentDescription;
    //  dc.EquipmentGroupId = vm.EquipmentGroupId;
    //  dc.EquipmentId = vm.EquipmentId;
    //  dc.EquipmentName = vm.EquipmentName;
    //  dc.ServiceExpires = vm.ServiceExpires;
    //  dc.WarningValue = vm.WarningValue;
    //  dc.EqStatus = EquipmentStatus.GetValue(vm.EquipmentStatus ?? 0);
    //  dc.EqServiceType = ServiceType.GetValue(vm.EquipmentServiceType ?? 0);
    //  dc.Remark = vm.Remark;

    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentUpdateRequest(dc);

    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  return result;
    //}

    //public async Task<VM_Base> UpdateEquipmentStatus(ModelStateDictionary modelState, VM_EquipmentStatus vm)
    //{
    //  VM_Base result = new VM_Base();
    //  ValidateEditEquipment(modelState, vm);

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  UnitConverterHelper.ConvertToSi(ref vm);

    //  DCEquipment dc = new DCEquipment();
    //  dc.AlarmValue = vm.AlarmValue;
    //  dc.EquipmentId = vm.EquipmentId;
    //  dc.ServiceExpires = vm.ServiceExpires;
    //  dc.WarningValue = vm.WarningValue;
    //  dc.EqStatus = EquipmentStatus.GetValue(vm.EquipmentStatus ?? 0);
    //  dc.EqServiceType = ServiceType.GetValue(vm.EquipmentServiceType ?? 0);
    //  dc.Remark = vm.Remark;

    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentStatusUpdateRequest(dc);

    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  return result;
    //}

    //public async Task<VM_Base> DeleteEquipment(long id)
    //{
    //  VM_Base result = new VM_Base();

    //  DCEquipment dc = new DCEquipment();
    //  dc.EquipmentId = id;

    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentDeleteRequest(dc);

    //  //HandleWarnings(sendOfficeResult, ref modelState);

    //  return result;
    //}

    //public async Task<VM_Base> CloneEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    //{
    //  VM_Base result = new VM_Base();
    //  UnitConverterHelper.ConvertToSi(ref vm);
    //  DCEquipment dc = new DCEquipment();
    //  dc.EquipmentCode = vm.EquipmentCode;
    //  dc.EquipmentId = vm.EquipmentId;
    //  dc.EquipmentName = vm.EquipmentName;
    //  dc.EquipmentDescription = vm.EquipmentDescription;
    //  dc.WarningValue = vm.WarningValue;
    //  dc.AlarmValue = vm.AlarmValue;

    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendEquipmentCloneRequest(dc);

    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  return result;
    //}

    //public DataSourceResult GetEquipmentHistoryList(long id, ModelStateDictionary modelState,
    //  [DataSourceRequest] DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  using (PEContext ctx = new PEContext())
    //  {
    //    //result = ctx.MNTEquipmentHistories.Where(w => w.FKEquipmentId == id).OrderBy(o => o.CreatedTs)
    //    //  .ToDataSourceLocalResult<MNTEquipmentHistory, VM_EquipmentHistory>(request, modelState, x => new VM_EquipmentHistory(x));
    //  }

    //  return result;
    //}

    //public IList<VM_EquipmentGroup> GetEquipmentGroupList()
    //{
    //  List<VM_EquipmentGroup> result = new List<VM_EquipmentGroup>();
    //  using (PEContext ctx = new PEContext())
    //  {
    //    //IQueryable<MNTEquipmentGroup> dbList = ctx.MNTEquipmentGroups.OrderBy(o=>o.EquipmentGroupCode).AsQueryable();
    //    //foreach (MNTEquipmentGroup item in dbList)
    //    //{
    //    //  result.Add(new VM_EquipmentGroup(item));
    //    //}
    //  }

    //  return result;
    //}

    //public VM_LongId GetEquipmentHistory(long id)
    //{
    //  VM_LongId vm = new VM_LongId();
    //  vm.Id = id;
    //  return vm;
    //}

    //private void ValidateEditEquipment(ModelStateDictionary modelState, VM_Equipment vm)
    //{
    //  if (vm.AlarmValue.HasValue && vm.WarningValue.HasValue && vm.AlarmValue.Value < vm.WarningValue.Value)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("AlarmValueCannotBeLessThanWarningValue"));
    //  }

    //  if (vm.ServiceExpires.HasValue && vm.ServiceExpires.Value < DateTime.Now)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("ServiceExpiresCannotBeBeforeToday"));
    //  }

    //  if (vm.EquipmentStatus == EquipmentStatus.InOperation.Value &&
    //      vm.EquipmentServiceType == ServiceType.Undefined.Value)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("InOperationStatusCannotHaveUndefinedServiceType"));
    //  }

    //  if (vm.EquipmentServiceType == ServiceType.WeightRelated.Value && !vm.AlarmValue.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("ForWeightRelatedServiceTypeAlarmValueIsRequired"));
    //  }

    //  if (vm.EquipmentServiceType == ServiceType.DateRelated.Value && !vm.ServiceExpires.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("ForDateRelatedServiceTypeServiceExpiresIsRequired"));
    //  }

    //  if ((vm.EquipmentServiceType == ServiceType.Both.Value && !vm.ServiceExpires.HasValue) || !vm.AlarmValue.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText(
    //        "ForDateRelatedAndWeightRelatedServiceTypeServiceExpiresAndAlarmValueAreRequired"));
    //  }
    //}

    //private void ValidateEditEquipment(ModelStateDictionary modelState, VM_EquipmentStatus vm)
    //{
    //  if (vm.AlarmValue.HasValue && vm.WarningValue.HasValue && vm.AlarmValue.Value < vm.WarningValue.Value)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("AlarmValueCannotBeLessThanWarningValue"));
    //  }

    //  if (vm.ServiceExpires.HasValue && vm.ServiceExpires.Value < DateTime.Now)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("ServiceExpiresCannotBeBeforeToday"));
    //  }

    //  if (vm.EquipmentStatus == EquipmentStatus.InOperation.Value &&
    //      vm.EquipmentServiceType == ServiceType.Undefined.Value)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("InOperationStatusCannotHaveUndefinedServiceType"));
    //  }

    //  if (vm.EquipmentServiceType == ServiceType.WeightRelated.Value && !vm.AlarmValue.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("ForWeightRelatedServiceTypeAlarmValueIsRequired"));
    //  }

    //  if (vm.EquipmentServiceType == ServiceType.DateRelated.Value && !vm.ServiceExpires.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText("ForDateRelatedServiceTypeServiceExpiresIsRequired"));
    //  }

    //  if ((vm.EquipmentServiceType == ServiceType.Both.Value && !vm.ServiceExpires.HasValue) || !vm.AlarmValue.HasValue)
    //  {
    //    AddModelStateError(modelState,
    //      ResourceController.GetErrorText(
    //        "ForDateRelatedAndWeightRelatedServiceTypeServiceExpiresAndAlarmValueAreRequired"));
    //  }
    //}

    //public void CheckData(VM_Component inputData, ref List<VM_ComponentsGroupsTreeListEl> outputData)
    //{
    //  if (inputData.Items.Count > 0)
    //  {
    //    foreach (VM_Component subElement in inputData.Items)
    //    {
    //      if (subElement.ComponentType == "Group")
    //      {
    //        VM_ComponentsGroupsTreeListEl el = new VM_ComponentsGroupsTreeListEl(inputData.ComponentId,
    //          (long)subElement.ComponentId, subElement.ComponentName, false, subElement.ComponentId, false, true);

    //        outputData.Add(el);
    //      }

    //      if (subElement.ComponentType == "Component")
    //      {
    //        VM_ComponentsGroupsTreeListEl el = new VM_ComponentsGroupsTreeListEl(inputData.ComponentId,
    //          (long)subElement.ComponentId, subElement.ComponentName, false, subElement.ComponentId, true, false);
    //        outputData.Add(el);
    //        IList<VM_Counter> counters = GetCounterForComponent(subElement);
    //        if (counters != null)
    //        {
    //          foreach (VM_Counter elementCounter in counters)
    //          {
    //            el = new VM_ComponentsGroupsTreeListEl((long)subElement.ComponentId, (long)elementCounter.CounterId,
    //              elementCounter.QuantityTypeName, false,
    //              null, false,
    //              false, true, elementCounter.Unit, elementCounter.Value);

    //            //el.ValueMax = elementCounter.ValueMax;
    //            //el.ValueWarning = elementCounter.ValueWarning;
    //            //el.ValueAlarm = elementCounter.ValueAlarm;
    //            outputData.Add(el);
    //          }
    //        }
    //      }

    //      CheckData(subElement, ref outputData);
    //    }
    //  }
    //}

    //public IList<VM_Counter> GetCounterForComponent(VM_Component component)
    //{
    //  IList<VM_Counter> result = new List<VM_Counter>();
    //  //TODOMN - exclude this
    //  //using (PEContext ctx = new PEContext())
    //  //{
    //  //  //Get counters
    //  //  IEnumerable<MNTCounter> countersForComponent = ctx.MNTCounters.Where(z => z.MNTDeviceComponent.FKComponentId == component.ComponentId).Include(z => z.MNTDeviceComponent).ToList();

    //  //  foreach(MNTCounter element in countersForComponent)
    //  //  {
    //  //    //Get quantity type
    //  //    MNTQuantityType mNTQuantityType = ctx.MNTQuantityTypes.Where(z => z.QuantityTypeId == element.FKQuantityTypeId).FirstOrDefault();

    //  //    //Get unit
    //  //    V_UnitOfMeasure Unit = ctx.V_UnitOfMeasure.Where(z => z.UOMSIId == mNTQuantityType.FKUnitId).FirstOrDefault();

    //  //    //Get limits
    //  //    MNTLimit mNTLimit = ctx.MNTLimits.Where(z => z.FKQuantityTypeId == element.FKQuantityTypeId).FirstOrDefault();

    //  //    VM_Counter vm = new VM_Counter();
    //  //    vm.CounterId = element.CounterId;
    //  //    vm.LastUpdateTs = element.LastUpdateTs;
    //  //    vm.CreatedTs = element.CreatedTs;
    //  //    vm.Value = element.Value;

    //  //    if (mNTQuantityType != null)
    //  //    {
    //  //      vm.QuantityTypeCode = mNTQuantityType.QuantityTypeCode;
    //  //      vm.QuantityTypeId = mNTQuantityType.QuantityTypeId;
    //  //      vm.QuantityTypeName = mNTQuantityType.QuantityTypeName;
    //  //    }

    //  //    if (mNTLimit != null)
    //  //    {
    //  //      vm.ValueAlarm = mNTLimit.ValueAlarm;
    //  //      vm.ValueMax = mNTLimit.ValueMax;
    //  //      vm.ValueWarning = mNTLimit.ValueWarning;
    //  //    }

    //  //    if (Unit != null)
    //  //    {
    //  //      vm.Unit = Unit.UOMSymbol;
    //  //    }
    //  //    result.Add(vm);
    //  //  }

    //  //}
    //  return result;
    //}

    //private void BuildTree(IEnumerable<MVHAsset> assets, ref List<TreeElement> treeElements, bool getDevices = true)
    //{
    //  //Get main root element
    //  MVHAsset rootAsset = assets.Where(z => z.FKParentAssetId == null).FirstOrDefault();
    //  if (rootAsset != null)
    //  {
    //    long id = rootAsset.AssetId;
    //    TreeElement rootTreeElement =
    //      new TreeElement(-1, 0, rootAsset.AssetName + " - " + rootAsset.AssetDescription, "");
    //    treeElements.Add(rootTreeElement);
    //    CreateTree(id, assets, ref rootTreeElement, ref id, getDevices);
    //  }
    //}

    //private void CreateTree(long rootId, IEnumerable<MVHAsset> assets, ref TreeElement parentElement, ref long id,
    //  bool getDevices = true)
    //{
    //  //Get all zones
    //  IEnumerable<MVHAsset> assetZones = assets.Where(z => z.FKParentAssetId == rootId).ToList();
    //  foreach (MVHAsset elementZone in assetZones)
    //  {
    //    TreeElement treeElementZone = new TreeElement(0, 0,
    //      elementZone.AssetName + " - " + elementZone.AssetDescription, elementZone.AssetName);
    //    if (elementZone.InverseFKParentAsset.Count > 0)
    //    {
    //      parentElement.HasChildren = true;
    //    }
    //    else
    //    {
    //      parentElement.HasChildren = false;
    //    }

    //    parentElement.Items.Add(treeElementZone);

    //    if (getDevices)
    //    {
    //      AddDevicesToAssetTree(elementZone, ref treeElementZone);
    //    }

    //    //Get all areas
    //    foreach (MVHAsset elementArea in elementZone.InverseFKParentAsset)
    //    {
    //      TreeElement treeElementArea = new TreeElement(0, 0,
    //        elementArea.AssetName + " - " + elementArea.AssetDescription, elementArea.AssetName);
    //      if (elementArea.InverseFKParentAsset.Count > 0)
    //      {
    //        treeElementZone.HasChildren = true;
    //      }
    //      else
    //      {
    //        treeElementZone.HasChildren = false;
    //      }

    //      treeElementZone.Items.Add(treeElementArea);

    //      if (getDevices)
    //      {
    //        AddDevicesToAssetTree(elementArea, ref treeElementArea);
    //      }

    //      //Get all assets in areas
    //      foreach (MVHAsset elementAssetInArea in elementArea.InverseFKParentAsset)
    //      {
    //        TreeElement treeElementAssetInArea = new TreeElement(0, 0,
    //          elementAssetInArea.AssetName + " - " + elementAssetInArea.AssetDescription, elementAssetInArea.AssetName);
    //        if (elementAssetInArea.InverseFKParentAsset.Count > 0)
    //        {
    //          treeElementAssetInArea.HasChildren = true;
    //        }
    //        else
    //        {
    //          treeElementAssetInArea.HasChildren = false;
    //        }

    //        treeElementArea.Items.Add(treeElementAssetInArea);

    //        if (getDevices)
    //        {
    //          AddDevicesToAssetTree(elementAssetInArea, ref treeElementAssetInArea);
    //        }
    //      }
    //    }
    //  }
    //}

    //private void AddDevicesToAssetTree(MVHAsset asset, ref TreeElement treeElement)
    //{
    //  if (asset.MNTDeviceOnAssets.Count > 0)
    //  {
    //    foreach (MNTDeviceOnAsset element in asset.MNTDeviceOnAssets)
    //    {
    //      MNTDeviceComponent mntDeviceComponent = _peContext.MNTDeviceComponents
    //        .Where(z => z.DeviceComponentId == element.DeviceOnAssetId).Include(z => z.FKDevice).FirstOrDefault();
    //      TreeElement treeDeviceElement = new TreeElement(0, mntDeviceComponent.FKDevice.DeviceId,
    //        mntDeviceComponent.FKDevice.DeviceName, mntDeviceComponent.FKDevice.DeviceName, true);
    //      treeElement.HasChildren = true;
    //      treeDeviceElement.HasChildren = false;

    //      treeElement.Items.Add(treeDeviceElement);
    //    }
    //  }
    //}

    //private static void GoToNextComponentGroupLevel(MNTComponentGroup componentGroup, ref VM_Component parent,
    //  ref PEContext ctx, ref MNTComponent originComponent, ref VM_Component device)
    //{
    //  if (ctx != null)
    //  {
    //    long? parentComponentGroupId = componentGroup.FKParentComponentGroupId;
    //    if (parentComponentGroupId != null)
    //    {
    //      MNTComponentGroup mNtComponentGroup = ctx.MNTComponentGroups
    //        .Where(z => z.ComponentGroupId == parentComponentGroupId)
    //        .Include(z => z.InverseFKParentComponentGroup).FirstOrDefault();
    //      if (mNtComponentGroup != null)
    //      {
    //        VM_Component vMComponentGroup = new VM_Component();
    //        vMComponentGroup.ComponentCode = mNtComponentGroup.ComponentGroupCode;
    //        vMComponentGroup.ComponentId = mNtComponentGroup.ComponentGroupId;
    //        vMComponentGroup.ComponentType = "Group";
    //        vMComponentGroup.ComponentName = mNtComponentGroup.ComponentGroupName;
    //        vMComponentGroup.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //        vMComponentGroup.IsActive = true;
    //        vMComponentGroup.Supplier = "-";
    //        vMComponentGroup.GroupCode = "-";
    //        vMComponentGroup.ColorScheme = SelectColourByType(vMComponentGroup.ComponentType);
    //        vMComponentGroup.Items.Add(parent);
    //        if (mNtComponentGroup.InverseFKParentComponentGroup != null)
    //        {
    //          GoToNextComponentGroupLevel(mNtComponentGroup, ref vMComponentGroup, ref ctx, ref originComponent,
    //            ref device);
    //        }
    //        else
    //        {
    //          device.Items.Add(vMComponentGroup);
    //        }
    //      }
    //    }
    //  }
    //}


    //private static void GoToNextLevel(MNTComponent component, ref VM_Component parent)
    //{
    //  PEContext ctx2 = new PEContext();
    //  using (PEContext ctx = new PEContext())
    //  {
    //    MNTComponentGroup mNtComponentGroup = component.FKComponentGroup;
    //    if (mNtComponentGroup != null)
    //    {
    //      //Create one common container for all types
    //      VM_Component vMComponentGroup = new VM_Component();
    //      vMComponentGroup.ComponentCode = mNtComponentGroup.ComponentGroupCode;
    //      vMComponentGroup.Description = mNtComponentGroup.ComponentGroupDescription;
    //      vMComponentGroup.ComponentId = mNtComponentGroup.ComponentGroupId + 10000;
    //      vMComponentGroup.ComponentType = "Group";
    //      vMComponentGroup.ComponentName = mNtComponentGroup.ComponentGroupName;
    //      vMComponentGroup.GroupCode = "-";
    //      vMComponentGroup.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //      vMComponentGroup.IsActive = true;
    //      vMComponentGroup.Supplier = "-";
    //      vMComponentGroup.ColorScheme = SelectColourByType(vMComponentGroup.ComponentType);

    //      if (mNtComponentGroup.FKParentComponentGroupId != null)
    //      {
    //        GoToNextComponentGroupLevel(mNtComponentGroup, ref vMComponentGroup, ref ctx2, ref component, ref parent);
    //        //Add last element to the group
    //        VM_Component finalComponent = new VM_Component();
    //        finalComponent.ComponentCode = component.ComponentCode;
    //        finalComponent.Description = component.ComponentDescription;
    //        finalComponent.ComponentId = component.ComponentId;
    //        finalComponent.ComponentName = component.ComponentName;
    //        finalComponent.ComponentType = "Component";
    //        finalComponent.ColorScheme = SelectColourByType(finalComponent.ComponentType);
    //        finalComponent.GroupCode = "-";
    //        finalComponent.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //        finalComponent.IsActive = true;
    //        finalComponent.Supplier = "-";
    //        vMComponentGroup.Items.Add(finalComponent);
    //      }

    //      else
    //      {
    //        parent.Items.Add(vMComponentGroup);
    //        //Add last element to the group
    //        VM_Component finalComponent = new VM_Component();
    //        finalComponent.ComponentCode = component.ComponentCode;
    //        finalComponent.Description = component.ComponentDescription;
    //        finalComponent.ComponentId = component.ComponentId;
    //        finalComponent.ComponentType = "Component";
    //        finalComponent.ComponentName = component.ComponentName;
    //        finalComponent.ColorScheme = SelectColourByType(finalComponent.ComponentType);
    //        finalComponent.IsActiveLabel = ResourceController.GetResourceTextByResourceKey("NAME_IsActive");
    //        finalComponent.GroupCode = "-";
    //        finalComponent.IsActive = true;
    //        finalComponent.Supplier = "-";
    //        vMComponentGroup.Items.Add(finalComponent);
    //      }
    //    }
    //  }
    //}

    //private static string SelectColour(int indicator)
    //{
    //  string retVal = "";
    //  switch (indicator)
    //  {
    //    case 1:
    //      retVal = "#9ae309";
    //      break;
    //    case 2:
    //      retVal = "#e31e00";
    //      break;
    //    case 3:
    //      retVal = "#425563";
    //      break;
    //    case 4:
    //      retVal = "#425563";
    //      break;
    //    case 5:
    //      retVal = "#425563";
    //      break;
    //    case 6:
    //      retVal = "#425563";
    //      break;
    //    case 7:
    //      retVal = "#425563";
    //      break;
    //    default:
    //      retVal = "#425563";
    //      break;
    //  }

    //  return retVal;
    //}

    //private static string SelectColourByType(string type)
    //{
    //  string retVal = "";
    //  switch (type)
    //  {
    //    case "Group":
    //      retVal = "#9ae309";
    //      break;
    //    case "Component":
    //      retVal = "#e31e00";
    //      break;
    //    case "Device":
    //      retVal = "#425563";
    //      break;
    //    default:
    //      retVal = "#425563";
    //      break;
    //  }

    //  return retVal;
    //}
  }
}
