using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollType;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollTypeService : BaseService, IRollTypeService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public RollTypeService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    #region interface ICustomerService

    public DataSourceResult GetRollTypeList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<RLSRollType> list = _peContext.RLSRollTypes.AsQueryable();
      List<long> typesInUse = _hmiContext.V_Rolls
        .Select(x => x.RollTypeId)
        .Distinct()
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollType(data));
      foreach (VM_RollType rollType in result.Data)
      {
        if (typesInUse.Contains((long)rollType.RollTypeId))
          rollType.IsInUse = true;
        else
          rollType.IsInUse = false;
      }

      return result;
    }

    public VM_RollType GetRollType(ModelStateDictionary modelState, long id)
    {
      VM_RollType returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION
      RLSRollType rollType = _peContext.RLSRollTypes.Single(x => x.RollTypeId == id);
      if (rollType != null)
      {
        returnValueVm = new VM_RollType(rollType);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertRollType(ModelStateDictionary modelState, VM_RollType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData
      {
        ChokeType = viewModel.ChokeType,
        DiameterMax = viewModel.DiameterMax,
        DiameterMin = viewModel.DiameterMin,
        DrawingName = viewModel.DrawingName,
        Length = viewModel.RollLength,
        RollTypeDescription = viewModel.RollTypeDescription,
        RollTypeName = viewModel.RollTypeName,
        RoughnessMax = viewModel.RoughnessMax,
        RoughnessMin = viewModel.RoughnessMin,
        SteelgradeRoll = viewModel.RollSteelgrade,
        YieldStrengthRef = viewModel.YieldStrengthRef,
        TimeStamp = DateTime.Now,
        MatchingRollSetType = viewModel.MatchingRollsetType
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UpdateRollType(ModelStateDictionary modelState, VM_RollType viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData
      {
        Id = viewModel.RollTypeId,
        ChokeType = viewModel.ChokeType,
        DiameterMax = viewModel.DiameterMax,
        DiameterMin = viewModel.DiameterMin,
        DrawingName = viewModel.DrawingName,
        Length = viewModel.RollLength,
        RollTypeDescription = viewModel.RollTypeDescription,
        RollTypeName = viewModel.RollTypeName,
        RoughnessMax = viewModel.RoughnessMax,
        RoughnessMin = viewModel.RoughnessMin,
        SteelgradeRoll = viewModel.RollSteelgrade,
        YieldStrengthRef = viewModel.YieldStrengthRef,
        TimeStamp = DateTime.Now,
        MatchingRollSetType = viewModel.MatchingRollsetType
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteRollType(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollTypeData entryDataContract = new DCRollTypeData
      {
        Id = viewModel.Id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollTypeAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    #endregion
  }
}
