using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
  /// Not used
  /// </summary>
  public class ActionTypeService : BaseService, IActionTypeService
  {
    private readonly PEContext _peContext;

    public ActionTypeService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }


    //public DataSourceResult GetActionTypeList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTActionTypes.ToDataSourceLocalResult(request, modelState, data => new VM_ActionType(data));

    //  return result;
    //}

    //public DataSourceResult GetRecommendedActionsList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long actionTypeId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTRecomendedActions.Where(z => z.FKActionTypeId == actionTypeId)
    //    .ToDataSourceLocalResult(request, modelState, data => new VM_RecommendedAction(data));

    //  return result;
    //}
    ////public DataSourceResult GetAllIncidentsForTypeList(ModelStateDictionary modelState, DataSourceRequest request, long ActionTypeId)
    ////{
    ////	DataSourceResult result = null;

    ////	using (PEContext ctx = new PEContext())
    ////	{
    ////		result = ctx.MNTIncidents.Where(z => z.ac == ActionTypeId).ToDataSourceLocalResult<MNTIncident, VM_Incident>(request, modelState, data => new VM_Incident(data));
    ////	}

    ////	return result;
    ////}
    //public VM_ActionType GetActionTypeDetails(ModelStateDictionary modelState, long actionTypeId)
    //{
    //  VM_ActionType result = null;

    //  if (actionTypeId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (actionTypeId != 0)
    //  {
    //    MNTActionType actionType = _peContext.MNTActionTypes
    //      .Include(i => i.MNTRecomendedActions)
    //      .Include(i => i.MNTActions)
    //      .Where(x => x.ActionTypeId == actionTypeId)
    //      .Single();

    //    result = new VM_ActionType(actionType);
    //  }
    //  else
    //  {
    //    result = new VM_ActionType();
    //  }

    //  return result;
    //}

    //public VM_ActionType GetEmptyActionType()
    //{
    //  VM_ActionType result = new VM_ActionType();
    //  return result;
    //}

    //public VM_ActionType GetActionType(long id)
    //{
    //  VM_ActionType result = null;

    //  MNTActionType eq = _peContext.MNTActionTypes
    //    .SingleOrDefault(x => x.ActionTypeId == id);
    //  result = eq == null ? null : new VM_ActionType(eq);

    //  return result;
    //}

    //public VM_RecommendedAction GetEmptyRecommendedAction()
    //{
    //  VM_RecommendedAction result = new VM_RecommendedAction();
    //  MNTActionType actionType = _peContext.MNTActionTypes
    //    .OrderBy(z => z.ActionTypeId).FirstOrDefault();
    //  if (actionType != null)
    //  {
    //    result.FkActionTypeId = actionType.ActionTypeId;
    //  }

    //  actionType = _peContext.MNTActionTypes
    //    .OrderBy(z => z.ActionTypeId).FirstOrDefault();
    //  if (actionType != null)
    //  {
    //    result.FkActionTypeId = actionType.ActionTypeId;
    //  }

    //  return result;
    //}

    //public VM_RecommendedAction GetRecommendedAction(long id)
    //{
    //  VM_RecommendedAction result = null;

    //  MNTRecomendedAction eq = _peContext.MNTRecomendedActions
    //    .SingleOrDefault(x => x.RecomenedActionId == id);
    //  result = eq == null ? null : new VM_RecommendedAction(eq);

    //  return result;
    //}

    //public async Task<VM_Base> InsertActionType(ModelStateDictionary modelState, VM_ActionType viewModel)
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

    //  DCActionType entryDataContract = new DCActionType
    //  {
    //    ActionTypeId = viewModel.ActionTypeId ?? 0,
    //    ActionTypeCode = viewModel.ActionTypeCode,
    //    ActionTypeDescription = viewModel.ActionTypeDescription,
    //    ActionTypeName = viewModel.ActionTypeName,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendInsertActionType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateActionType(ModelStateDictionary modelState, VM_ActionType viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.ActionTypeId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCActionType entryDataContract = new DCActionType
    //  {
    //    ActionTypeId = viewModel.ActionTypeId ?? 0,
    //    ActionTypeCode = viewModel.ActionTypeCode,
    //    ActionTypeDescription = viewModel.ActionTypeDescription,
    //    ActionTypeName = viewModel.ActionTypeName,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendUpdateActionType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteActionType(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCActionType entryDataContract = new DCActionType { ActionTypeId = viewModel.Id };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDeleteActionType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteRecommendedAction(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCRecommendedAction entryDataContract = new DCRecommendedAction { RecommendedActionId = viewModel.Id };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendDeleteRecommendedAction(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}
  }
}
