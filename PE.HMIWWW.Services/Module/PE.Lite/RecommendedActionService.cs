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
  public class RecommendedActionService : BaseService, IRecommendedActionService
  {
    private readonly PEContext _peContext;
    /// <summary>
    /// Not Used
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="peContext"></param>
    public RecommendedActionService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public DataSourceResult GetRecommendedActionsList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTRecomendedActions.Include(z => z.FKIncidentType).Include(z => z.FKActionType)
    //    .ToDataSourceLocalResult(request, modelState, data => new VM_RecommendedAction(data));

    //  return result;
    //}

    //public VM_RecommendedAction GetRecommendedActionDetails(ModelStateDictionary modelState, long recommendedActionId)
    //{
    //  VM_RecommendedAction result = null;

    //  if (recommendedActionId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (recommendedActionId != 0)
    //  {
    //    MNTRecomendedAction recommendedAction = _peContext.MNTRecomendedActions
    //      .Include(i => i.FKActionType)
    //      .Include(i => i.FKIncidentType)
    //      .Where(x => x.RecomenedActionId == recommendedActionId)
    //      .Single();

    //    result = new VM_RecommendedAction(recommendedAction);
    //  }
    //  else
    //  {
    //    result = new VM_RecommendedAction();
    //  }

    //  return result;
    //}

    //public VM_RecommendedAction GetEmptyRecommendedAction()
    //{
    //  VM_RecommendedAction result = new VM_RecommendedAction();
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

    //public async Task<VM_Base> InsertRecommendedAction(ModelStateDictionary modelState, VM_RecommendedAction viewModel)
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

    //  DCRecommendedAction entryDataContract = new DCRecommendedAction
    //  {
    //    RecommendedActionId = viewModel.RecommendedActionId ?? 0,
    //    ActionDescription = viewModel.ActionDescription,
    //    FkActionTypeId = viewModel.FkActionTypeId,
    //    FkIncidentTypeId = viewModel.FkIncidentTypeId
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendInsertRecommendedAction(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateRecommendedAction(ModelStateDictionary modelState, VM_RecommendedAction viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.RecommendedActionId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCRecommendedAction entryDataContract = new DCRecommendedAction
    //  {
    //    RecommendedActionId = viewModel.RecommendedActionId ?? 0,
    //    ActionDescription = viewModel.ActionDescription,
    //    FkActionTypeId = viewModel.FkActionTypeId,
    //    FkIncidentTypeId = viewModel.FkIncidentTypeId
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendUpdateRecommendedAction(entryDataContract);

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

    //  DCRecommendedAction entryDataContract = new DCRecommendedAction {RecommendedActionId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendDeleteRecommendedAction(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public DataSourceResult GetRecommendedActionsList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long recommendedActionId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTRecomendedActions.Where(z => z.RecomenedActionId == recommendedActionId)
    //    .ToDataSourceLocalResult(request, modelState, data => new VM_RecommendedAction(data));

    //  return result;
    //}
  }
}
