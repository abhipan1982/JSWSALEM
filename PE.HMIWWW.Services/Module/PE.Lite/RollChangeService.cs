using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.RLS;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollChange;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollChangeService : BaseService, IRollChangeService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;
    /// <summary>
    /// Not Used
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="hmiContext"></param>
    /// <param name="peContext"></param>
    public RollChangeService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    #region ViewModel Methods

    public VM_ConfirmationForRmAndIm BuildVmConfirmationForRmAndIm(short? operationType, long? rollsetId,
      long? mountedRollsetId, short? position, short? standNo)
    {
      VM_ConfirmationForRmAndIm returnModel = new VM_ConfirmationForRmAndIm
      {
        OperationType = operationType,
        RollsetId = rollsetId,
        MountedRollsetId = mountedRollsetId,
        Position = position,
        StandNo = standNo
      };
      if (rollsetId != null)
      {
        V_RollSetOverview rollset = GetRollSetDetails((long)rollsetId);
        returnModel.RollsetName = rollset.RollSetName;
      }

      if (mountedRollsetId != null)
      {
        V_RollSetOverview mountedRollset = GetRollSetDetails((long)mountedRollsetId);
        returnModel.MountedRollsetName = mountedRollset.RollSetName;
      }

      return returnModel;
    }

    #endregion

    #region Database Operations

    public DataSourceResult GetStandGridData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long firstStand, long lastStand)
    {
      List<V_CassettesInStand> list = GetStandList(firstStand, lastStand);
      return list.ToDataSourceLocalResult(request, modelState, data => new VM_StandGrid(data));
    }

    public DataSourceResult GetRollsetGridData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long standNo)
    {
      return GetRollsets(standNo).ToDataSourceLocalResult(request, modelState, data => new VM_RollsetGrid(data));
    }

    public DataSourceResult GetRollGroovesGridData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, long rollSetId, long standNo)
    {
      return GetGrooves(rollSetId, standNo)
        .ToDataSourceLocalResult(request, modelState, data => new VM_RollGrooveGrid(data));
    }

    public List<V_CassettesInStand> GetCassettesInfoRmIm()
    {
      //TODOMN - refactor this
      List<V_CassettesInStand> list = _hmiContext.V_CassettesInStands.Where(o => o.StandNo >= 1 && o.StandNo <= 10)
        .OrderBy(g => g.StandNo).ToList();
      if (list.Count() > 0)
      {
        return list;
      }

      return null;
    }

    public async Task<VM_Base> SwapRollSet(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref viewModel);


      DCRollChangeOperationData dc = new DCRollChangeOperationData();

      dc.Action = RollChangeAction.SwapRollSetOnly.Value;
      dc.MountedRollSet = viewModel.RollsetId;
      dc.DismountedRollSet = viewModel.MountedRollsetId;
      dc.StandNo = viewModel.StandNo;
      dc.Position = viewModel.Position;

      //get cassette on this standNo
      // VCassettesOverview rec = uow.Repository<VCassettesOverview>().Query(q => q.StandNo == viewModel.StandNo && q.Status == (short)PE.Core.Constants.CassetteStatus.MountedInStand).Get().FirstOrDefault();
      V_CassettesOverview mountedCassette = _hmiContext.V_CassettesOverviews
        .Where(q => q.StandNo == viewModel.StandNo && q.EnumCassetteStatus == CassetteStatus.MountedInStand.Value)
        .FirstOrDefault();
      if (mountedCassette == null)
      {
        return result;
      }

      dc.MountedCassette = mountedCassette.CassetteId;
      //get dismounted rollset Id
      //VRollsetInCassette rs = uow.Repository<VRollsetInCassette>().Query(q => q.FkCassetteId == rec.CassetteId && q.PositionInCassette == viewModel.Position).Get().FirstOrDefault();
      V_RollsetInCassette rollSet = _hmiContext.V_RollsetInCassettes.Where(q =>
        q.FKCassetteId == mountedCassette.CassetteId && q.PositionInCassette == viewModel.Position).FirstOrDefault();
      if (rollSet == null)
      {
        return result;
      }

      dc.DismountedRollSet = rollSet.RollSetId;
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> MountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCRollChangeOperationData dc = new DCRollChangeOperationData();

      dc.Action = RollChangeAction.MountRollSetOnly.Value;
      dc.MountedRollSet = viewModel.RollsetId;
      dc.StandNo = viewModel.StandNo;
      dc.Position = viewModel.Position;
      //get cassette on this standNo
      //VCassettesOverview rec = uow.Repository<VCassettesOverview>().Query(q => q.StandNo == viewModel.StandNo).Get().FirstOrDefault();
      V_CassettesOverview mountedCassette =
        _hmiContext.V_CassettesOverviews.Where(q => q.StandNo == viewModel.StandNo).FirstOrDefault();
      if (mountedCassette == null)
      {
        return result;
      }

      dc.MountedCassette = mountedCassette.CassetteId;
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DismountRollset(ModelStateDictionary modelState, VM_ConfirmationForRmAndIm viewModel)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref viewModel);


      DCRollChangeOperationData dc = new DCRollChangeOperationData();

      dc.Action = RollChangeAction.DismountRollSetOnly.Value;
      dc.DismountedRollSet = viewModel.MountedRollsetId;
      dc.StandNo = viewModel.StandNo;
      //get cassette Id from which rollset is to be dismounted
      //VRollsetInCassette rec = uow.Repository<VRollsetInCassette>().Query(q => q.RollSetId == viewModel.RollsetId).Get().FirstOrDefault();
      V_RollsetInCassette rollSet =
        _hmiContext.V_RollsetInCassettes.Where(q => q.RollSetId == viewModel.RollsetId).FirstOrDefault();
      if (rollSet == null || rollSet.FKCassetteId == null)
      {
        return result;
      }

      dc.MountedCassette = rollSet.FKCassetteId;
      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.RollChangeAction(dc);

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RollChangeActionAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public V_RollSetOverview GetRollSetDetails(long rollSetId)
    {
      //return uow.Repository<VRollsetOverviewNewest>().Query(o => o.RollSetId == rollSetId).Get().FirstOrDefault();
      return _hmiContext.V_RollSetOverviews.Where(o => o.RollSetId == rollSetId && o.IsLastOne && o.IsLastOne)
        .FirstOrDefault();
    }

    public short GetGrooveNumber(long rollId)
    {
      //var groovesHistory = uow.Repository<VRollHistoryPerGroove>().Query(o => o.RollId == rollId &&
      //                                                                            o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual).Get().ToList();

      List<V_RollHistoryPerGroove> groovesHistory = _hmiContext.V_RollHistoryPerGrooves
        .Where(o => o.RollId == rollId && o.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual.Value).ToList();

      return (short)groovesHistory.Count();
    }

    #endregion

    #region Helper Methods

    public List<V_RollSetOverview> GetAvailableRollsets(short standNo)
    {
      return GetRollsets(standNo);
    }

    public long? GetRollId(V_RollSetOverview rollSet)
    {
      if (rollSet.UpperRollId != null)
      {
        return (long)rollSet.UpperRollId;
      }

      if (rollSet.BottomRollId != null)
      {
        return (long)rollSet.BottomRollId;
      }

      return null;
    }

    #endregion

    #region Private Methods

    private List<V_RollHistoryPerGroove> GetGrooves(long rollSetId, long standNo)
    {
      //TODOMN - refactor this

      V_RollSetOverview rollSet = new V_RollSetOverview();
      if (rollSetId != 0)
      {
        rollSet = GetRollSetDetails(rollSetId);
      }
      else
      {
        List<V_RollSetOverview> rollsets = GetRollsets(standNo);

        if (rollsets.Count() > 0)
        {
          rollSet = rollsets[0];
        }
      }

      if (rollSet != null && rollSet.RollSetId != 0)
      {
        long? rollId = GetRollId(rollSet);
        if (rollId != null)
        {
          //return uow.Repository<VRollHistoryPerGroove>().Query(o => o.RollId == rollId && o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual).Get().ToList();
          return _hmiContext.V_RollHistoryPerGrooves
            .Where(o => o.RollId == rollId && o.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual.Value).ToList();
        }
      }

      return new List<V_RollHistoryPerGroove>();
    }

    private List<V_RollSetOverview> GetRollsets(long standNo)
    {
      RLSStand stand = null;
      //var stand = uow.Repository<Stand>().Query(o => o.StandNo == standNo).Get().FirstOrDefault();
      stand = _peContext.RLSStands.Where(x => x.StandNo == standNo).FirstOrDefault();
      if (stand != null)
      {
        short rollsetType = 0;
        //PROJECT SPECIFIC!!!! bad design probably
        //find matching rollSetType depending on standNo
        if (standNo >= 1 && standNo <= 6)
        {
          rollsetType = RollSetType.RMRollSet.Value;
        }
        else if (standNo >= 7 && standNo <= 10)
        {
          rollsetType = RollSetType.IMRollSet.Value;
        }
        else
        {
          rollsetType = RollSetType.Undefined.Value;
        }

        List<V_RollSetOverview> orderedList = new List<V_RollSetOverview>();
        //V_RollsetOverviewNewest mountedRollset = uow.Repository<VRollsetOverviewNewest>().Query(o => o.StandNo == standNo && o.RollSetStatus == (short)Constants.RollSetStatus.Mounted).Get().FirstOrDefault();
        V_RollSetOverview mountedRollset = _hmiContext.V_RollSetOverviews.Where(o =>
          o.StandNo == standNo && o.EnumRollSetStatus == RollSetStatus.Mounted.Value && o.IsLastOne &&
          o.IsLastOne).FirstOrDefault();

        if (mountedRollset != null)
        {
          orderedList.Add(mountedRollset);
        }

        //IList<VRollsetOverviewNewest> rollsets = uow.Repository<VRollsetOverviewNewest>()
        //  .Query(o => o.RollSetHistoryStatus == (short)Constants.RollSetHistoryStatus.Actual && o.RollSetType == rollsetType &&
        //                (o.RollSetStatus == (short)Constants.RollSetStatus.Ready || o.RollSetStatus == (short)Constants.RollSetStatus.Dismounted))
        //  .OrderBy(g => g.OrderBy(h => h.RollSetName))
        //  .Get()
        //  .ToList();

        IList<V_RollSetOverview> rollsets = _hmiContext.V_RollSetOverviews.Where(o => o.IsLastOne &&
            o.IsLastOne && o.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual.Value &&
            o.RollSetType == rollsetType &&
            (o.EnumRollSetStatus == RollSetStatus.Ready.Value || o.EnumRollSetStatus == RollSetStatus.Dismounted.Value))
          .OrderBy(h => h.RollSetName)
          .ToList();

        if (rollsets.Count > 0)
        {
          foreach (V_RollSetOverview rollset in rollsets)
          {
            orderedList.Add(rollset);
          }
        }


        return orderedList;
      }

      return new List<V_RollSetOverview>();
    }

    private List<V_CassettesInStand> GetStandList(long firstStand, long lastStand)
    {
      //return uow.Repository<VRsCassettesInStand>().Query(o => o.StandNo >= firstStand && o.StandNo <= lastStand).OrderBy(s => s.OrderBy(g => g.StandNo)).Get().ToList();
      return _hmiContext.V_CassettesInStands.Where(x => x.StandNo >= firstStand && x.StandNo <= lastStand)
        .OrderBy(o => o.StandNo).ToList();
    }

    #endregion
  }
}
