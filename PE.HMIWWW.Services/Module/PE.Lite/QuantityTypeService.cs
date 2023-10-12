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
  public class QuantityTypeService : BaseService, IQuantityTypeService
  {
    private readonly PEContext _peContext;
    /// <summary>
    /// Not Used
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="peContext"></param>
    public QuantityTypeService(IHttpContextAccessor httpContextAccessor, PEContext peContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
    }

    //public DataSourceResult GetQuantityTypeList(ModelStateDictionary modelState, DataSourceRequest request)
    //{
    //  DataSourceResult result = null;
    //  result = _peContext.MNTQuantityTypes.ToDataSourceLocalResult(request, modelState, data => new VM_QuantityType(data));

    //  return result;
    //}

    //public VM_QuantityType GetQuantityTypeDetails(ModelStateDictionary modelState, long quantityTypeId)
    //{
    //  VM_QuantityType result = null;

    //  if (quantityTypeId < 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  // Validate entry data
    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }

    //  if (quantityTypeId != 0)
    //  {
    //    MNTQuantityType quantityType = _peContext.MNTQuantityTypes
    //      .Include(i => i.MNTLimitCounters)
    //      .Where(x => x.QuantityTypeId == quantityTypeId)
    //      .Single();

    //    result = new VM_QuantityType(quantityType);
    //  }
    //  else
    //  {
    //    result = new VM_QuantityType();
    //  }

    //  return result;
    //}

    //public VM_QuantityType GetEmptyQuantityType()
    //{
    //  VM_QuantityType result = new VM_QuantityType();
    //  return result;
    //}

    //public VM_QuantityType GetQuantityType(long id)
    //{
    //  VM_QuantityType result = null;

    //  MNTQuantityType eq = _peContext.MNTQuantityTypes
    //    .SingleOrDefault(x => x.QuantityTypeId == id);
    //  result = eq == null ? null : new VM_QuantityType(eq);

    //  return result;
    //}

    //public async Task<VM_Base> InsertQuantityType(ModelStateDictionary modelState, VM_QuantityType viewModel)
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

    //  DCQuantityType entryDataContract = new DCQuantityType
    //  {
    //    QuantityTypeId = viewModel.QuantityTypeId ?? 0,
    //    QuantityTypeCode = viewModel.QuantityTypeCode,
    //    QuantityTypeName = viewModel.QuantityTypeName,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs,
    //    FKExtUnitCategoryId = viewModel.FkExtUnitCategoryId,
    //    FkUnitId = viewModel.FkUnitId
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendInsertQuantityType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> UpdateQuantityType(ModelStateDictionary modelState, VM_QuantityType viewModel)
    //{
    //  VM_Base result = new VM_Base();

    //  //VALIDATE ENTRY PARAMETERS
    //  if (viewModel == null)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (viewModel.QuantityTypeId <= 0)
    //  {
    //    AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
    //  }

    //  if (!modelState.IsValid)
    //  {
    //    return result;
    //  }
    //  //END OF VALIDATION

    //  DCQuantityType entryDataContract = new DCQuantityType
    //  {
    //    QuantityTypeId = viewModel.QuantityTypeId ?? 0,
    //    QuantityTypeCode = viewModel.QuantityTypeCode,
    //    FKExtUnitCategoryId = viewModel.FkExtUnitCategoryId,
    //    FkUnitId = viewModel.FkUnitId,
    //    QuantityTypeName = viewModel.QuantityTypeName,
    //    CreatedTs = viewModel.CreatedTs,
    //    LastUpdateTs = viewModel.LastUpdateTs
    //  };

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendUpdateQuantityType(entryDataContract);

    //  //handle warning information
    //  HandleWarnings(sendOfficeResult, ref modelState);

    //  //return view model
    //  return result;
    //}

    //public async Task<VM_Base> DeleteQuantityType(ModelStateDictionary modelState, VM_LongId viewModel)
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

    //  DCQuantityType entryDataContract = new DCQuantityType {QuantityTypeId = viewModel.Id};

    //  //request data from module
    //  SendOfficeResult<DataContractBase> sendOfficeResult =
    //    await HmiSendOffice.SendDeleteQuantityType(entryDataContract);

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
  }
}
