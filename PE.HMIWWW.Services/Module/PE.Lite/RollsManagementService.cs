using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollsManagement;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollsManagementService : BaseService, IRollsManagementService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public RollsManagementService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetRollsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_Roll> list = _hmiContext.V_Rolls.AsQueryable();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollsWithTypes(data));

      return result;
    }

    public VM_RollsWithTypes GetRoll(ModelStateDictionary modelState, long id)
    {
      VM_RollsWithTypes returnValueVm = null;

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
      V_Roll roll = _hmiContext.V_Rolls.Single(z => z.RollId == id);
      if (roll != null)
      {
        returnValueVm = new VM_RollsWithTypes(roll);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData
      {
        ActualDiameter = viewModel.ActualDiameter,
        Description = viewModel.RollDescription,
        DiameterMax = viewModel.DiameterMax,
        DiameterMin = viewModel.DiameterMin,
        GroovesNumber = viewModel.GroovesNumber,
        InitialDiameter = viewModel.InitialDiameter,
        Length = viewModel.RollLength,
        MinimumDiameter = viewModel.MinimumDiameter,
        RollName = viewModel.RollName,
        RollSetBottom = viewModel.RollSetBottom,
        RollSetName = viewModel.RollSetName,
        RollSetUpper = viewModel.RollSetUpper,
        RollSetThird = viewModel.RollSetThird,
        RollTypeDescription = viewModel.RollTypeDescription,
        RollTypeId = viewModel.RollTypeId,
        RollTypeName = viewModel.RollTypeName,
        RoughnessMax = viewModel.RoughnessMax,
        RoughnessMin = viewModel.RoughnessMin,
        ScrapDate = viewModel.ScrapTime,
        ScrapReason = RollScrapReason.GetValue(viewModel.ScrapReason ?? 0),
        Status = RollStatus.New,
        StatusName = viewModel.StatusName,
        SteelgradeRoll = viewModel.RollSteelgrade,
        Supplier = viewModel.Supplier,
        YieldStrengthRef = viewModel.YieldStrengthRef
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData
      {
        ActualDiameter = viewModel.ActualDiameter,
        Description = viewModel.RollDescription,
        DiameterMax = viewModel.DiameterMax,
        DiameterMin = viewModel.DiameterMin,
        GroovesNumber = viewModel.GroovesNumber,
        InitialDiameter = viewModel.InitialDiameter,
        Length = viewModel.RollLength,
        MinimumDiameter = viewModel.MinimumDiameter,
        RollName = viewModel.RollName,
        RollSetBottom = viewModel.RollSetBottom,
        RollSetName = viewModel.RollSetName,
        RollSetUpper = viewModel.RollSetUpper,
        RollSetThird = viewModel.RollSetThird,
        RollTypeDescription = viewModel.RollTypeDescription,
        RollTypeId = viewModel.RollTypeId,
        RollTypeName = viewModel.RollTypeName,
        RoughnessMax = viewModel.RoughnessMax,
        RoughnessMin = viewModel.RoughnessMin,
        ScrapDate = viewModel.ScrapTime,
        ScrapReason = RollScrapReason.GetValue(viewModel.ScrapReason ?? 0),
        Status = RollStatus.GetValue(viewModel.EnumRollStatus),
        StatusName = viewModel.StatusName,
        SteelgradeRoll = viewModel.RollSteelgrade,
        Supplier = viewModel.Supplier,
        YieldStrengthRef = viewModel.YieldStrengthRef,
        Id = viewModel.RollId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> ScrapRoll(ModelStateDictionary modelState, VM_RollsWithTypes viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData
      {
        Id = viewModel.RollId,
        RollName = viewModel.RollName,
        RollTypeId = viewModel.RollTypeId,
        ScrapDate = viewModel.ScrapTime,
        ScrapReason = RollScrapReason.GetValue(viewModel.ScrapReason ?? 0),
        Status = RollStatus.GetValue(viewModel.EnumRollStatus)
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ScrapRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteRoll(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollData entryDataContract = new DCRollData
      {
        Id = viewModel.Id
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<List<VM_RollTypeDiameterLimit>> GetRollLimits()
    {
      List<RLSRollType> limits = await _peContext.RLSRollTypes.ToListAsync();
      List<VM_RollTypeDiameterLimit> result = limits
        .Select(x => new VM_RollTypeDiameterLimit(x.RollTypeId, x.DiameterMin ?? 0, x.DiameterMax ?? 0))
        .ToList();

      return result;
    }
  }
}
