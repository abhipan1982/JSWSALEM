using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.BaseModels.Structures.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite.Interfaces
{
  public class RollsetToCassetteService : BaseService, IRollsetToCassetteService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public RollsetToCassetteService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }


    #region interface IRollsetToCassetteService
    public DataSourceResult GetAvailableCassettesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_CassettesOverview> list = _hmiContext.V_CassettesOverviews
        .Where(f => (f.IsInterCassette == false) && (f.EnumCassetteStatus == CassetteStatus.New.Value
          || f.EnumCassetteStatus == CassetteStatus.Empty.Value
          || (f.EnumCassetteStatus == CassetteStatus.RollSetInside.Value && f.RollsetsNumber < f.NumberOfPositions)
          || (f.EnumCassetteStatus == CassetteStatus.AssembleIncomplete.Value && f.RollsetsNumber < f.NumberOfPositions)))
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_CassetteOverview(data));

      return result;
    }
    public async Task<VM_Base> EditGroovesForRollSet(ModelStateDictionary modelState, VM_RollsetDisplay actualVm)
    {
      VM_Base returnVm = new VM_Base();

      UnitConverterHelper.ConvertToSi(ref actualVm);

      //VALIDATE ENTRY PARAMETERS
      if (actualVm.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return returnVm;
      }
      //END OF VALIDATION

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup
      {
        Id = actualVm.RollSetId
      };
      List<SingleRollDataInfo> rollDataInfoList = new List<SingleRollDataInfo>();
      //add Rolls to list
      if (actualVm.UpperRollId.HasValue)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.UpperRollId ?? 0,
          Diameter = actualVm.UpperActualDiameter ?? 0.0
        };

        rollDataInfoList.Add(s);
      }
      if (actualVm.BottomRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.BottomRollId ?? 0,
          Diameter = actualVm.BottomActualDiameter ?? 0.0
        };
        rollDataInfoList.Add(s);
      }
      entryDataContract.RollDataInfoList = rollDataInfoList;
      List<SingleGrooveSetup> grooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVm.GrooveActualRollUpper != null)
      {
        for (int i = 0; i < actualVm.GrooveActualRollUpper.Count; i++)
        {
          if (actualVm.GrooveActualRollUpper[i].GrooveTemplateId != 0)
          {
            grooveSetupList.Add(new SingleGrooveSetup
            {
              GrooveTemplate = (long)actualVm.GrooveActualRollUpper[i].GrooveTemplateId,
              GrooveCondition = actualVm.GrooveActualRollUpper[i].EnumGrooveCondition ?? 1,
              GrooveRemark = actualVm.GrooveActualRollUpper[i].GrooveRemark,
              GrooveIdx = grooveIdx,
              AccBilletCnt = actualVm.GrooveActualRollUpper[i].AccBilletCnt,
              AccWeight = actualVm.GrooveActualRollUpper[i].AccWeight * 1000,
              AccWeightWithCoeff = actualVm.GrooveActualRollUpper[i].AccWeightWithCoeff * 1000
            });
          }
          else
          {
            grooveSetupList.Add(new SingleGrooveSetup
            {
              GrooveTemplate = (long)actualVm.GrooveActualRollUpper[i].GrooveTemplateId,
              GrooveIdx = grooveIdx,
              GrooveCondition = actualVm.GrooveActualRollUpper[i].EnumGrooveCondition ?? 1,
              AccBilletCnt = actualVm.GrooveActualRollUpper[i].AccBilletCnt,
              AccWeight = actualVm.GrooveActualRollUpper[i].AccWeight * 1000,
              AccWeightWithCoeff = actualVm.GrooveActualRollUpper[i].AccWeightWithCoeff * 1000
            });
          }
          grooveIdx++;
        }
      }
      entryDataContract.GrooveSetupList = grooveSetupList;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesDataToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return returnVm;
    }

    public DataSourceResult GetAvailableInterCassettesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_CassettesOverview> list = _hmiContext.V_CassettesOverviews
        .Where(f => (f.IsInterCassette == true) && (f.EnumCassetteStatus == CassetteStatus.New.Value
          || f.EnumCassetteStatus == CassetteStatus.Empty.Value
          || (f.EnumCassetteStatus == CassetteStatus.RollSetInside.Value && f.RollsetsNumber < f.NumberOfPositions)
          || (f.EnumCassetteStatus == CassetteStatus.AssembleIncomplete.Value && f.RollsetsNumber < f.NumberOfPositions)))
        .ToList();

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_CassetteOverview(data));

      return result;
    }

    public DataSourceResult GetAvailableRollSetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.EnumRollSetStatus == RollSetStatus.Ready.Value)
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));

      return result;
    }

    public DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.CassetteId.HasValue && (x.EnumRollSetStatus == RollSetStatus.ScheduledForCassette.Value))
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));

      return result;
    }

    public DataSourceResult GetReadyRollSetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      IQueryable<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.CassetteId.HasValue && (x.EnumRollSetStatus == RollSetStatus.ReadyForMounting.Value));
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));

      return result;
    }

    public async Task<VM_Base> ConfirmRsReadyForMounting(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetToCassetteAction dataToSend = new DCRollSetToCassetteAction
      {
        RollSetId = viewModel.Id,
        Action = RollSetCassetteAction.ConfirmRollSet
      };
      RLSRollSet rollSet = _peContext.RLSRollSets.FirstOrDefault(z => z.RollSetId == viewModel.Id);
      V_RollSetOverview view = _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.IsLastOne && z.RollSetId == rollSet.RollSetId);
      dataToSend.CassetteId = view.CassetteId;
      //END OF DB OPERATION

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_CassetteOverviewWithPositions GetCassetteOverviewWithPositions(ModelStateDictionary modelState, long id)
    {
      VM_CassetteOverviewWithPositions returnValueVm = null;

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
      V_CassettesOverview cassette = _hmiContext.V_CassettesOverviews.FirstOrDefault(z => z.CassetteId == id && z.EnumCassetteStatus != CassetteStatus.History.Value);

      if (cassette != null)
      { 
        IList<V_RollSetOverview> availableRollSets = _hmiContext.V_RollSetOverviews
          .Where(x => x.IsLastOne && x.EnumRollSetStatus == RollSetStatus.Ready.Value)
          .ToList();

        returnValueVm = new VM_CassetteOverviewWithPositions(cassette, availableRollSets);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> AssembleRollSetAndCassette(ModelStateDictionary modelState, VM_CassetteOverviewWithPositions viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction
      {
        CassetteId = viewModel.Id
      };
      if (viewModel.RollSetss.Count > 1)
      {
        entryDataContract.RollSetIdList = new List<long?>();
        foreach (VM_RollSetShort rollSetElement in viewModel.RollSetss)
        {
          entryDataContract.RollSetIdList.Add(rollSetElement.RollSetId);
        }
      }
      else
      {
        foreach (VM_RollSetShort rollSetElement in viewModel.RollSetss)
        {
          entryDataContract.RollSetId = rollSetElement.RollSetId;
          entryDataContract.Postion = 1;
        }
      }

      entryDataContract.CassetteId = viewModel.Id;
      entryDataContract.Action = RollSetCassetteAction.PlanRollSet;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> UnloadRollSet(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction
      {
        RollSetId = viewModel.Id
      };


      V_RollSetOverview rollSet = _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.IsLastOne && z.RollSetId == viewModel.Id && z.CassetteId.HasValue);
      if (rollSet != null)
      {
        entryDataContract.CassetteId = rollSet.CassetteId;
      }

      entryDataContract.Action = RollSetCassetteAction.RemoveRollSet;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CancelPlan(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.Id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetToCassetteAction entryDataContract = new DCRollSetToCassetteAction
      {
        RollSetId = viewModel.Id
      };

      V_RollSetOverview rollSet = _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.RollSetId == viewModel.Id && z.CassetteId.HasValue);
      if (rollSet != null)
      {
        entryDataContract.CassetteId = rollSet.CassetteId;
      }

      entryDataContract.Action = RollSetCassetteAction.CancelPlan;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollSetToCassetteAction(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_RollsetDisplay GetRollSetDisplay(ModelStateDictionary modelState, long id)
    {
      VM_RollsetDisplay returnValueVm = null;

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
      V_RollSetOverview rsOverview = _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.IsLastOne && z.RollSetId == id);
      if (rsOverview != null)
      {
        IList<V_RollHistory> rollHistoryUpper = _hmiContext.V_RollHistories
          .Where(z => z.RollId == rsOverview.UpperRollId && z.RollSetHistoryId == rsOverview.RollSetHistoryId)
          .ToList();
        returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper);
      }
      foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollBottom)
      {
        gr.GrooveConfirmed = false;
      }
      foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollUpper)
      {
        gr.GrooveConfirmed = false;
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_CassetteOverview GetCassette(ModelStateDictionary modelState, long id)
    {
      VM_CassetteOverview returnValueVm = null;

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
      V_CassettesOverview cassette = _hmiContext.V_CassettesOverviews.FirstOrDefault(z => z.CassetteId == id && z.EnumCassetteStatus != CassetteStatus.History.Value);
      if (cassette != null)
      {
        returnValueVm = new VM_CassetteOverview(cassette);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollSetOverview GetRollSet(ModelStateDictionary modelState, long id)
    {
      VM_RollSetOverview returnValueVm = null;

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
      V_RollSetOverview rollSet = _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.IsLastOne && z.RollSetId == id);
      if (rollSet != null)
      {
        returnValueVm = new VM_RollSetOverview(rollSet);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    #endregion

    public SelectList GetCassetteRSWithRollsList(long cassetteid)
    {
      List<V_RollSetOverview> list = null;
      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      RLSCassette cassette = _peContext.RLSCassettes.FirstOrDefault(x => x.CassetteId == cassetteid && x.EnumCassetteStatus != CassetteStatus.History);
      RLSCassetteType cassType = _peContext.RLSCassetteTypes.FirstOrDefault(z => z.CassetteTypeId == cassette.FKCassetteTypeId);
      list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.EnumRollSetStatus == RollSetStatus.Ready.Value)
        .ToList();

      foreach (V_RollSetOverview rl in list)
      {
        resultDictionary.Add(Convert.ToInt64(rl.RollSetId), rl.RollSetName);
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetCassetteRSWith3RollsList(long cassetteType)
    {
      using PEContext peContext = new PEContext();
      using HmiContext hmiCtx = new HmiContext();
      List<V_RollSetOverview> list = null;
      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      RLSCassetteType cassType = peContext.RLSCassetteTypes.FirstOrDefault(z => z.CassetteTypeId == cassetteType);
      list = hmiCtx.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.EnumRollSetStatus == RollSetStatus.Ready.Value)
        .ToList();

      foreach (V_RollSetOverview rl in list)
      {
        resultDictionary.Add(Convert.ToInt64(rl.RollSetId), rl.RollSetName);
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public IList<VM_RollSetShort> GetRollSetListByText(string text, long cassetteTypeId)
    {
      IList<VM_RollSetShort> result = new List<VM_RollSetShort>();

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && x.CassetteTypeId == cassetteTypeId && x.EnumRollSetStatus == RollSetStatus.Ready)
        .ToList();
      if (!string.IsNullOrEmpty(text))
      {
        result = _hmiContext.V_RollSetOverviews
          .Where(x => x.IsLastOne && x.EnumRollSetStatus == RollSetStatus.Ready.Value && x.RollSetName.Contains(text))
          .AsEnumerable()
          .Select(rollset => new VM_RollSetShort(rollset))
          .ToList();
      }

      return result;
    }
  }
}
