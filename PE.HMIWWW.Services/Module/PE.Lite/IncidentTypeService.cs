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
  public class IncidentTypeService : BaseService, IIncidentTypeService
  {
    private readonly PEContext _peContext;
    /// <summary>
    /// Not Used
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="peContext"></param>
    public IncidentTypeService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public DataSourceResult GetIncidentTypeList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTIncidentTypes.ToDataSourceLocalResult(request, modelState, data => new VM_IncidentType(data));

    //  return result;
    //}

    //public DataSourceResult GetRecommendedActionsList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentTypeId)
    //{
    //  DataSourceResult result = null;

    //  result = _peContext.MNTRecomendedActions.Where(z => z.FKIncidentTypeId == incidentTypeId)
    //    .ToDataSourceLocalResult(request, modelState, data => new VM_RecommendedAction(data));

    //  return result;
    //}

    //public VM_IncidentType GetIncidentTypeDetails(ModelStateDictionary modelState, long incidentTypeId)
    //{
    //  VM_IncidentType result = null;

    //  if (incidentTypeId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (incidentTypeId != 0)
    //  {
    //      MNTIncidentType incidentType = _peContext.MNTIncidentTypes
    //        .Include(i => i.MNTRecomendedActions)
    //        .Include(i => i.MNTIncidents)
    //        .Where(x => x.IncidentTypeId == incidentTypeId)
    //        .Single();

    //      result = new VM_IncidentType(incidentType);
    //  }
    //  else
    //  {
    //    result = new VM_IncidentType();
    //  }

    //  return result;
    //}

    //public VM_IncidentType GetEmptyIncidentType()
    //{
    //  VM_IncidentType result = new VM_IncidentType();
    //  return result;
    //}

    //public VM_IncidentType GetIncidentType(long id)
    //{
    //  VM_IncidentType result = null;

    //  MNTIncidentType eq = _peContext.MNTIncidentTypes
    //    .SingleOrDefault(x => x.IncidentTypeId == id);
    //  result = eq == null ? null : new VM_IncidentType(eq);

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

    //  MNTIncidentType incidentType = _peContext.MNTIncidentTypes
    //    .OrderBy(z => z.IncidentTypeId).FirstOrDefault();
    //  if (incidentType != null)
    //  {
    //    result.FkIncidentTypeId = incidentType.IncidentTypeId;
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

    //public async Task<VM_Base> InsertIncidentType(ModelStateDictionary modelState, VM_IncidentType viewModel)
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

    //  DCIncidentType entryDataContract = new DCIncidentType
    //  {
    //    IncidentTypeId = viewModel.IncidentTypeId ?? 0,
    //    IncidentTypeCode = viewModel.IncidentTypeCode,
    //    IncidentTypeDescription = viewModel.IncidentTypeDescription,
    //    IncidentTypeName = viewModel.IncidentTypeName,
    //    DefaultEnumSeverity = viewModel.EnumSeverity,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendInsertIncidentType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateIncidentType(ModelStateDictionary modelState, VM_IncidentType viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.IncidentTypeId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCIncidentType entryDataContract = new DCIncidentType
    //  {
    //    IncidentTypeId = viewModel.IncidentTypeId ?? 0,
    //    IncidentTypeCode = viewModel.IncidentTypeCode,
    //    IncidentTypeDescription = viewModel.IncidentTypeDescription,
    //    IncidentTypeName = viewModel.IncidentTypeName,
    //    DefaultEnumSeverity = viewModel.EnumSeverity,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendUpdateIncidentType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteIncidentType(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCIncidentType entryDataContract = new DCIncidentType {IncidentTypeId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendDeleteIncidentType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public DataSourceResult GetAllIncidentsForTypeList(ModelStateDictionary modelState, DataSourceRequest request,
    //  long incidentTypeId)
    //{
    //  DataSourceResult result = null;

    //    result = _peContext.MNTIncidents.Where(z => z.FKIncidentTypeId == incidentTypeId)
    //      .ToDataSourceLocalResult(request, modelState, data => new VM_Incident(data));

    //  return result;
    //}
  }
}
