using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.MNT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Maintenance;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  /// <summary>
  /// Not Used
  /// </summary>
  public class DeviceGroupService : BaseService, IDeviceGroupService
  {
    private readonly PEContext _peContext;

    public DeviceGroupService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public DataSourceResult GetDeviceGroupList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTDeviceGroups.ToDataSourceLocalResult(request, modelState, data => new VM_DeviceGroup(data));

    //  return result;
    //}

    //public VM_DeviceGroup GetEmptyDeviceGroup()
    //{
    //  VM_DeviceGroup result = new VM_DeviceGroup();
    //  return result;
    //}

    //public VM_DeviceGroup GetDeviceGroup(long id)
    //{
    //  VM_DeviceGroup result = null;

    //  MNTDeviceGroup eq = _peContext.MNTDeviceGroups
    //    .SingleOrDefault(x => x.DeviceGroupId == id);
    //  result = eq == null ? null : new VM_DeviceGroup(eq);

    //  return result;
    //}

    //public async Task<VM_Base> InsertDeviceGroup(ModelStateDictionary modelState, VM_DeviceGroup viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCDeviceGroup entryDataContract = new DCDeviceGroup
    //  {
    //    DeviceGroupId = viewModel.DeviceGroupId,
    //    DeviceGroupCode = viewModel.DeviceGroupCode,
    //    DeviceGroupDescription = viewModel.DeviceGroupDescription,
    //    DeviceGroupName = viewModel.DeviceGroupName,
    //    ParentDeviceGroupId = viewModel.FkParentDeviceGroupId,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase>
    //    sendOfficeResult = await HmiSendOffice.SendInsertDeviceGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateDeviceGroup(ModelStateDictionary modelState, VM_DeviceGroup viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.DeviceGroupId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCDeviceGroup entryDataContract = new DCDeviceGroup
    //  {
    //    DeviceGroupId = viewModel.DeviceGroupId,
    //    DeviceGroupCode = viewModel.DeviceGroupCode,
    //    DeviceGroupDescription = viewModel.DeviceGroupDescription,
    //    DeviceGroupName = viewModel.DeviceGroupName,
    //    ParentDeviceGroupId = viewModel.FkParentDeviceGroupId,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase>
    //    sendOfficeResult = await HmiSendOffice.SendUpdateDeviceGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteDeviceGroup(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCDeviceGroup entryDataContract = new DCDeviceGroup {DeviceGroupId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase>
    //    sendOfficeResult = await HmiSendOffice.SendDeleteDeviceGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public VM_DeviceGroup GetDeviceGroupDetails(ModelStateDictionary modelState, long deviceGroupId)
    //{
    //  VM_DeviceGroup result = null;

    //  if (deviceGroupId < 0)
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
    //      .Where(x => x.DeviceGroupId == deviceGroupId)
    //      .Single();

    //    result = new VM_DeviceGroup(deviceGroup);
    //  }
    //  else
    //  {
    //    result = new VM_DeviceGroup();
    //  }

    //  return result;
    //}
  }
}
