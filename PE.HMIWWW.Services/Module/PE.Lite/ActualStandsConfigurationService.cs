using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.ActualStandConfiguration;
using PE.HMIWWW.ViewModel.Module.Lite.Cassette;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.Module.Lite.RollSetToCassette;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ActualStandsConfigurationService : BaseService, IActualStandsConfigurationService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public ActualStandsConfigurationService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor) 
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    #region interface IActualStandsConfigurationService
    public DataSourceResult GetStandConfigurationList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_ActualStandConfiguration> list =
        _hmiContext.V_ActualStandConfigurations
        .Where(x => x.StandId != 0)
        .OrderBy(o => o.Position)
        .ThenBy(o => o.StandNo).ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_StandConfiguration(data));

      return result;
    }

    public DataSourceResult GetPassChangeActualList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = new DataSourceResult();

      IList<V_PassChangeDataActual> list = _hmiContext.V_PassChangeDataActuals.Where(p => p.EnumRollGrooveStatus == RollGrooveStatus.Active.Value).ToList();

      if (request.Sorts.Count == 0) //default sorting
      {
        List<VM_PassChangeDataActual> allVm = new List<VM_PassChangeDataActual>();
        foreach (V_PassChangeDataActual el in list)
        {
          VM_PassChangeDataActual vm = new VM_PassChangeDataActual(el);
          allVm.Add(vm);
        }

        //sort by accWeightRatio
        result.Data = allVm.OrderByDescending(o => o.AccWeightRatio)
          .Skip((request.Page - 1) * request.PageSize)
          .Take(request.PageSize)
          .ToList();
        result.Total = allVm.Count;
      }
      else
      {
        result = list.ToDataSourceLocalResult(request, modelState, data => new VM_PassChangeDataActual(data));
      }

      return result;
    }
    public DataSourceResult GetCassetteRollSetsList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long? cassetteId, short? standStatus)
    {
      DataSourceResult result = null;

      if (cassetteId == null)
      {
        cassetteId = 0;
      }

      RLSCassette cassette = _peContext.RLSCassettes.FirstOrDefault(z => z.CassetteId == cassetteId && z.EnumCassetteStatus != CassetteStatus.History);
      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(p => p.IsLastOne && p.CassetteId == cassetteId && p.EnumRollSetStatus == RollSetStatus.Mounted.Value)
        .ToList();
      List<short?> listOfPositions = list
        .Select(x => x.PositionInCassette)
        .ToList();
      if (list == null)
      {
        list = new List<V_RollSetOverview>();
      }
      if (cassette != null)
      {
        for (int i = 1; i <= cassette.NumberOfPositions; i++)
        {
          if (list.Count < cassette.NumberOfPositions && !listOfPositions.Contains((short?)i))
          {
            V_RollSetOverview blankRecord = new V_RollSetOverview
            {
              PositionInCassette = (short?)i,
              RollSetName = ""
            };
            list.Add(blankRecord);
          }
        }
      }
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverviewRollChange(data));

      return result;
    }
    public DataSourceResult GetRollSetInCassetteList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long cassetteId)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(p => p.IsLastOne && p.CassetteId == cassetteId)
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));
      return result;
    }

    public DataSourceResult GetRollSetGroovesList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request, long rollsetHistoryId, short? standStatus)
    {
      DataSourceResult result = null;

      IEnumerable<V_RollHistory> returnList = new List<V_RollHistory>();
      List<V_RollHistory> list = _hmiContext.V_RollHistories
        .Where(p => p.RollSetHistoryId == rollsetHistoryId).ToList();
      if (list.Count() > 0)
      {
        string rollName = list[0].RollName;
        returnList = list.Where(o => o.RollName == rollName).OrderBy(o => o.GrooveNumber);
      }
      result = returnList.ToDataSourceLocalResult(request, modelState, data => new VM_PassChangeGroovesRoll(data));

      return result;
    }

    public static SelectList GetCassetteToStandList(long standId)
    {
      using PEContext ctx = new PEContext();
      using HmiContext hmiContext = new HmiContext();
      RLSStand stand = ctx.RLSStands.FirstOrDefault(z => z.StandId == standId);
      List<V_CassettesOverview> list = hmiContext.V_CassettesOverviews
        .Where(p => p.EnumCassetteStatus == CassetteStatus.RollSetInside.Value)
        .ToList();
      List<long> rollSetIds = list
        .Where(x => x.RollSetId.HasValue)
        .GroupBy(x => x.RollSetId)
        .Select(y => y.Key.Value)
        .ToList();
      Dictionary<long, string> rollSets = ctx.RLSRollSets
        .Where(x => rollSetIds.Contains(x.RollSetId))
        .ToDictionary(x => x.RollSetId, y => y.RollSetName);

      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      foreach (V_CassettesOverview cs in list)
      {
        if (cs.RollSetId.HasValue)
          resultDictionary.Add(cs.CassetteId, cs.CassetteName + $"({rollSets[cs.RollSetId.Value]})");
        else
          resultDictionary.Add(cs.CassetteId, cs.CassetteName + $"(N/A)");
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetEmptyCassetteToStandList(long standId)
    {
      using PEContext ctx = new PEContext();
      using HmiContext hmiContext = new HmiContext();
      RLSStand stand = ctx.RLSStands
        .FirstOrDefault(z => z.StandId == standId);
      List<V_CassettesOverview> list = hmiContext.V_CassettesOverviews
        .Where(p => p.EnumCassetteStatus == CassetteStatus.Empty.Value || p.EnumCassetteStatus == CassetteStatus.New.Value)
        .ToList();
      List<long> rollSetIds = list
        .Where(x => x.RollSetId.HasValue)
        .GroupBy(x => x.RollSetId)
        .Select(y => y.Key.Value)
        .ToList();
      Dictionary<long, string> rollSets = ctx.RLSRollSets
        .Where(x => rollSetIds.Contains(x.RollSetId))
        .ToDictionary(x => x.RollSetId, y => y.RollSetName);
      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      foreach (V_CassettesOverview cs in list)
      {
        if (cs.RollSetId.HasValue)
          resultDictionary.Add(cs.CassetteId, cs.CassetteName + $"({rollSets[cs.RollSetId.Value]})");
        else
          resultDictionary.Add(cs.CassetteId, cs.CassetteName + $"(N/A)");
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public VM_StandConfiguration GetStandConfiguration(ModelStateDictionary modelState, long id)
    {
      VM_StandConfiguration returnValueVm = null;

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
      V_ActualStandConfiguration actualStandConfig = _hmiContext.V_ActualStandConfigurations.FirstOrDefault(z => z.StandId == id);
      if (actualStandConfig != null)
      {
        returnValueVm = new VM_StandConfiguration(actualStandConfig);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollChangeOperation GetRollChangeOperation(ModelStateDictionary modelState, long id)
    {
      VM_RollChangeOperation returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      //END OF VALIDATION

      //DB OPERATION
      long rollsetToDismountId = 0;
      V_RollSetOverview rollsetToDismount = new V_RollSetOverview();
      RLSCassette cassette = _peContext.RLSCassettes
        .Include(z => z.FKStand)
        .Include(x => x.FKCassetteType)
        .FirstOrDefault(z => z.CassetteId == id && z.EnumCassetteStatus != CassetteStatus.History);
      RLSRollSetHistory rollsetToDismountFromHistories = _peContext.RLSRollSetHistories
        .FirstOrDefault(x => x.FKCassetteId == cassette.CassetteId && x.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual);

      if (rollsetToDismountFromHistories != null)
      {
        rollsetToDismountId = rollsetToDismountFromHistories.FKRollSetId;
        rollsetToDismount = _hmiContext.V_RollSetOverviews
          .FirstOrDefault(x => x.IsLastOne && x.RollSetId == rollsetToDismountId && x.EnumRollSetHistoryStatus == 1);
      }
      if (cassette != null)
      {
        returnValueVm = new VM_RollChangeOperation(cassette, rollsetToDismount, null, RollChangeAction.MountRollSetOnly);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollChangeOperation GetRollChangeOperationForRollSet(ModelStateDictionary modelState, long id, short? positionId)
    {
      VM_RollChangeOperation returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      //DB OPERATION
      V_RollSetOverview rollSet =
        _hmiContext.V_RollSetOverviews.FirstOrDefault(z => z.IsLastOne && z.RollSetId == id);
      RLSCassette cassette = _peContext.RLSCassettes
        .Include(z => z.FKStand)
        .Include(z => z.FKCassetteType)
        .FirstOrDefault(z => z.CassetteId == rollSet.CassetteId && z.EnumCassetteStatus != CassetteStatus.History);
      if (cassette != null)
      {
        returnValueVm = new VM_RollChangeOperation(cassette, rollSet, null, RollChangeAction.MountRollSetOnly);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public VM_SwappingCassettes GetSwapCassette(ModelStateDictionary modelState, long id)
    {
      VM_SwappingCassettes returnValueVm = null;

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
      RLSCassette cassette = _peContext.RLSCassettes
        .Include(z => z.FKStand)
        .Include(z => z.FKCassetteType)
        .FirstOrDefault(z => z.CassetteId == id && z.EnumCassetteStatus != CassetteStatus.History);
      if (cassette != null)
      {
        IList<V_RollSetOverview> rollSetList = _hmiContext.V_RollSetOverviews
          .Where(x => x.IsLastOne)
          .ToList();

        if (rollSetList.Count > 0)
          returnValueVm = new VM_SwappingCassettes(cassette, rollSetList);
      }
      //END OF DB OPERATION

      return returnValueVm;
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
        IList<V_RollHistory> rollHistoryUpper =
          _hmiContext.V_RollHistories
          .Where(z => z.RollSetId == id && z.RollId == rsOverview.UpperRollId && (z.EnumRollGrooveStatus == RollGrooveStatus.Actual.Value || z.EnumRollGrooveStatus == RollGrooveStatus.Active.Value))
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper);
      }
      foreach (VM_GroovesRoll gr in returnValueVm.GrooveActualRollUpper)
      {
        gr.GrooveConfirmed = (gr.EnumRollGrooveStatus == RollGrooveStatus.Active);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_GroovesRoll GetGrooveDetails(ModelStateDictionary modelState, long id)
    {
      VM_GroovesRoll returnValueVm = null;

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
      RLSRollGroovesHistory grooveDetails = _peContext.RLSRollGroovesHistories.FirstOrDefault(z => z.RollGrooveHistoryId == id);
      returnValueVm = new VM_GroovesRoll(grooveDetails);
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
    public VM_CassetteOverviewWithPositions GetCassetteWithPositions(ModelStateDictionary modelState, long id, long standId)
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
        IList<V_RollSetOverview> rollSets = _hmiContext.V_RollSetOverviews
          .Where(f => f.IsLastOne && f.CassetteId == id)
          .OrderBy(p => p.PositionInCassette)
          .ToList();
        returnValueVm = new VM_CassetteOverviewWithPositions(cassette, rollSets);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }
    public async Task<VM_Base> MountCassette(ModelStateDictionary modelState, VM_StandConfiguration viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData
      {
        StandId = (short)viewModel.StandId,
        MountedCassette = viewModel.CassetteId,
        StandNo = viewModel.StandNo,
        StandStatus = StandStatus.GetValue(viewModel.EnumStandStatus),
        Action = RollChangeAction.MountWithCassette,
        Arrangement = CassetteArrangement.Undefined,
        Mounted = DateTime.Now
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> DismountCassette(ModelStateDictionary modelState, VM_StandConfiguration viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData
      {
        StandId = (short)viewModel.StandId,
        StandNo = viewModel.StandNo,
        DismountedCassette = viewModel.CassetteId,
        MountedCassette = viewModel.CassetteId,
        Dismounted = DateTime.Now,
        StandStatus = StandStatus.GetValue(0),
        Action = RollChangeAction.DismountWithCassette,
        Arrangement = CassetteArrangement.GetValue(0)
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DismountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData
      {
        StandId = (short)viewModel.StandId,
        StandNo = viewModel.StandNo,
        DismountedCassette = viewModel.CassetteId,
        MountedCassette = viewModel.CassetteId,
        Dismounted = DateTime.Now,
        StandStatus = StandStatus.GetValue(0),
        Action = RollChangeAction.DismountRollSetOnly,
        Arrangement = CassetteArrangement.GetValue(0),
        DismountedRollSet = viewModel.RollsetToBeDismountedId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData
      {
        StandId = (short)viewModel.StandId,
        StandNo = (short)viewModel.StandNo,
        MountedCassette = viewModel.CassetteId,
        Dismounted = DateTime.Now,
        StandStatus = StandStatus.GetValue(viewModel.EnumStandStatus ?? 0),
        Action = RollChangeAction.SwapRollSetOnly,
        Arrangement = CassetteArrangement.Undefined,
        DismountedRollSet = viewModel.RollsetToBeDismountedId,
        MountedRollSet = viewModel.RollsetToBeMountedId,
        Position = viewModel.PositionInCassette
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (actualVm.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }

      for (int i = 0; i < actualVm.GrooveActualRollUpper.Count; i++)
      {
        VM_GroovesRoll gr = actualVm.GrooveActualRollUpper[i];
        UnitConverterHelper.ConvertToSi(ref gr);
        actualVm.GrooveActualRollUpper[i] = gr;
      }
      UnitConverterHelper.ConvertToSi(ref actualVm);

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup
      {
        Id = actualVm.RollSetId
      };
      List<SingleGrooveSetup> grooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVm.GrooveActualRollUpper != null)
      {
        for (int i = 0; i < actualVm.GrooveActualRollUpper.Count; i++)
        {
          if (actualVm.GrooveActualRollUpper[i].GrooveConfirmed == true)
          {
            grooveSetupList.Add(
              new SingleGrooveSetup
              {
                GrooveTemplate = (long)actualVm.GrooveActualRollUpper[i].GrooveTemplateId,
                GrooveIdx = grooveIdx,
                GrooveStatus = RollGrooveStatus.Active,
                GrooveCondition = (short)actualVm.GrooveActualRollUpper[i].EnumGrooveCondition,
                GrooveRemark = actualVm.GrooveActualRollUpper[i].GrooveRemark,
                AccWeightWithCoeff = actualVm.GrooveActualRollUpper[i].AccWeightWithCoeff,
                AccWeight = actualVm.GrooveActualRollUpper[i].AccWeight,
                AccBilletCnt = actualVm.GrooveActualRollUpper[i].AccBilletCnt,
              });
          }
          else
          {
            grooveSetupList.Add(
              new SingleGrooveSetup
              {
                GrooveTemplate = (long)actualVm.GrooveActualRollUpper[i].GrooveTemplateId,
                GrooveIdx = grooveIdx,
                GrooveStatus = RollGrooveStatus.Actual,
                GrooveCondition = (short)actualVm.GrooveActualRollUpper[i].EnumGrooveCondition,
                GrooveRemark = actualVm.GrooveActualRollUpper[i].GrooveRemark,
                AccWeightWithCoeff = actualVm.GrooveActualRollUpper[i].AccWeightWithCoeff,
                AccWeight = actualVm.GrooveActualRollUpper[i].AccWeight,
                AccBilletCnt = actualVm.GrooveActualRollUpper[i].AccBilletCnt,
              });

          }
          grooveIdx++;
        }
      }
      entryDataContract.GrooveSetupList = grooveSetupList;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesStatusesAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
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
    public async Task<VM_Base> MountRollSet(ModelStateDictionary modelState, VM_RollChangeOperation viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (viewModel.StandId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollChangeOperationData entryDataContract = new DCRollChangeOperationData
      {
        StandId = (short)viewModel.StandId,
        MountedCassette = viewModel.CassetteId,
        StandNo = (short)viewModel.StandNo,
        StandStatus = StandStatus.GetValue(viewModel.EnumStandStatus ?? 0),
        Action = RollChangeAction.MountRollSetOnly,
        Arrangement = CassetteArrangement.Undefined,
        MountedRollSet = viewModel.RollsetToBeMountedId,
        Mounted = DateTime.Now,
        Position = viewModel.PositionInCassette
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }


    public async Task<VM_Base> UpdateStandConfiguration(ModelStateDictionary modelState, VM_StandConfiguration actualVm)
    {
      VM_Base result = new VM_Base();
      if (actualVm == null)
      {
        return result;
      }

      DCStandConfigurationData entryDataContract = new DCStandConfigurationData
      {
        Id = actualVm.StandId,
        IsCalibrated = actualVm.IsCalibrated,
        NumberOfRolls = actualVm.NumberOfRolls,
        StandNo = actualVm.StandNo,
        Status = actualVm.EnumStandStatus,
        Arrangement = actualVm.Arrangement
      }; // InStand

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateStandConfigurationAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    #endregion
  }
}
