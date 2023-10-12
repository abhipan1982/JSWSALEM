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
  /// not Used
  /// </summary>
  public class ComponentGroupService : BaseService, IComponentGroupService
  {
    private readonly PEContext _peContext;

    public ComponentGroupService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public DataSourceResult GetComponentGroupList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTComponentGroups.ToDataSourceLocalResult(request, modelState, data => new VM_ComponentGroup(data));

    //  return result;
    //}

    //public VM_ComponentGroup GetEmptyComponentGroup()
    //{
    //  VM_ComponentGroup result = new VM_ComponentGroup();
    //  return result;
    //}

    //public VM_ComponentGroup GetComponentGroup(long id)
    //{
    //  VM_ComponentGroup result = null;

    //  MNTComponentGroup eq = _peContext.MNTComponentGroups
    //    .SingleOrDefault(x => x.ComponentGroupId == id);
    //  result = eq == null ? null : new VM_ComponentGroup(eq);

    //  return result;
    //}

    //public async Task<VM_Base> InsertComponentGroup(ModelStateDictionary modelState, VM_ComponentGroup viewModel)
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

    //  DCComponentGroup entryDataContract = new DCComponentGroup
    //  {
    //    ComponentGroupId = viewModel.ComponentGroupId ?? 0,
    //    ComponentGroupCode = viewModel.ComponentGroupCode,
    //    ComponentGroupDescription = viewModel.ComponentGroupDescription,
    //    ComponentGroupName = viewModel.ComponentGroupName,
    //    ParentComponentGroupId = viewModel.FkParentComponentGroupId,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendInsertComponentGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateComponentGroup(ModelStateDictionary modelState, VM_ComponentGroup viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.ComponentGroupId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCComponentGroup entryDataContract = new DCComponentGroup
    //  {
    //    ComponentGroupId = viewModel.ComponentGroupId ?? 0,
    //    ComponentGroupCode = viewModel.ComponentGroupCode,
    //    ComponentGroupDescription = viewModel.ComponentGroupDescription,
    //    ComponentGroupName = viewModel.ComponentGroupName,
    //    ParentComponentGroupId = viewModel.FkParentComponentGroupId,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendUpdateComponentGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteComponentGroup(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCComponentGroup entryDataContract = new DCComponentGroup { ComponentGroupId = viewModel.Id };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendDeleteComponentGroup(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public VM_ComponentGroup GetComponentGroupDetails(ModelStateDictionary modelState, long componentGroupId)
    //{
    //  VM_ComponentGroup result = null;

    //  if (componentGroupId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (componentGroupId != 0)
    //  {
    //    MNTComponentGroup componentGroup = _peContext.MNTComponentGroups
    //      .Where(x => x.ComponentGroupId == componentGroupId)
    //      .Single();

    //    result = new VM_ComponentGroup(componentGroup);
    //  }
    //  else
    //  {
    //    result = new VM_ComponentGroup();
    //  }

    //  return result;
    //}
  }
}
