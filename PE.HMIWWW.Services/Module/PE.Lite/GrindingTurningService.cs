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
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetDisplay;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class GrindingTurningService : BaseService, IGrindingTurningService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public GrindingTurningService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    #region interface IGrindingTurningService

    public DataSourceResult GetPlannedRollsetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews.Where(x => (
        x.IsLastOne &&
        x.EnumRollSetStatus == RollSetStatus.Ready.Value
          || (x.EnumRollSetStatus == RollSetStatus.Dismounted.Value)
          || (x.EnumRollSetStatus == RollSetStatus.Empty.Value))
          && (x.EnumRollSetHistoryStatus == RollSetHistoryStatus.Actual.Value)
          && x.UpperRollId != null)
      .OrderBy(g => g.RollSetName)
      .ToList();

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));

      return result;
    }

    public DataSourceResult GetScheduledRollsetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne && (x.EnumRollSetStatus == RollSetStatus.Turning.Value)
          && (x.EnumRollSetHistoryStatus == RollSetHistoryStatus.Planned.Value) && x.BottomRollId.HasValue
          && x.UpperRollId.HasValue)
        .ToList();

      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverview(data));

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
      V_RollSetOverview rsOverview = _hmiContext.V_RollSetOverviews.FirstOrDefault(x => x.IsLastOne && x.RollSetId == id);
      if (rsOverview != null)
      {
        IList<V_RollHistory> rollHistoryUpper = _hmiContext.V_RollHistories
          .Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.UpperRollId)
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        returnValueVm = new VM_RollsetDisplay(rsOverview, rollHistoryUpper);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollSetTurningHistory GetRollSetHistoryActual(ModelStateDictionary modelState, long id)
    {
      VM_RollSetTurningHistory returnValueVm = null;

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
      V_RollSetOverview rsOverview = _hmiContext.V_RollSetOverviews.FirstOrDefault(x => x.IsLastOne && x.RollSetId == id);
      if (rsOverview != null)
      {
        IList<V_RollHistory> rollHistoryBottom = _hmiContext.V_RollHistories
          .Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.BottomRollId)
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        IList<V_RollHistory> rollHistoryUpper = _hmiContext.V_RollHistories
          .Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.UpperRollId)
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        returnValueVm = new VM_RollSetTurningHistory(rsOverview, rollHistoryUpper, rollHistoryBottom);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public VM_RollSetTurningHistory GetRollSetHistoryById(ModelStateDictionary modelState, long id)
    {
      VM_RollSetTurningHistory returnValueVm = null;

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
      V_RollSetOverview rsOverview = _hmiContext.V_RollSetOverviews.FirstOrDefault(x => x.RollSetHistoryId == id);
      if (rsOverview != null)
      {
        IList<V_RollHistory> rollHistoryBottom = _hmiContext.V_RollHistories
          .Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.BottomRollId)
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        IList<V_RollHistory> rollHistoryUpper = _hmiContext.V_RollHistories
          .Where(z => z.RollSetHistoryId == rsOverview.RollSetHistoryId && z.RollId == rsOverview.UpperRollId)
          .OrderBy(g => g.GrooveNumber)
          .ToList();
        returnValueVm = new VM_RollSetTurningHistory(rsOverview, rollHistoryUpper, rollHistoryBottom);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> UpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm)
    {
      UnitConverterHelper.ConvertToSi(ref actualVm);
      VM_Base returnVm = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (actualVm.RollSetId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (actualVm.RollSetType < 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }
      if (actualVm.GrooveActualRollUpper == null || actualVm.GrooveActualRollUpper.Count == 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("InvalidGrooveCount"));
      }
      if (!modelState.IsValid)
      {
        return returnVm;
      }
      //END OF VALIDATION

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup
      {
        Id = actualVm.RollSetId,
        Action = GrindingTurningAction.Plan,
        RollSetType = actualVm.RollSetType
      };
      List<SingleRollDataInfo> rollDataInfoList = new List<SingleRollDataInfo>();
      //add Rolls to list
      if (actualVm.UpperRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.UpperRollId ?? 0,
          Diameter = actualVm.NewUpperDiameter ?? 0.0
        };
        rollDataInfoList.Add(s);
      }
      if (actualVm.BottomRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.BottomRollId ?? 0,
          Diameter = actualVm.NewBottomDiameter ?? 0.0
        };
        rollDataInfoList.Add(s);
      }
      entryDataContract.RollDataInfoList = rollDataInfoList;
      List<SingleGrooveSetup> grooveSetupList = new List<SingleGrooveSetup>();
      short grooveIdx = 1;
      if (actualVm.GrooveActualRollUpper != null)
      {
        foreach (VM_GroovesRoll rollgroove in actualVm.GrooveActualRollUpper)
        {
          if (rollgroove.GrooveTemplateId != 0 && rollgroove.GrooveTemplateId != null)
          {
            grooveSetupList.Add(new SingleGrooveSetup
            {
              GrooveTemplate = (long)rollgroove.GrooveTemplateId,
              GrooveIdx = grooveIdx,
              GrooveCondition = rollgroove.EnumGrooveCondition ?? 1,
              GrooveStatus = RollGrooveStatus.PlannedAndTurning,
              AccWeight = rollgroove.AccWeight,
              AccBilletCnt = rollgroove.AccBilletCnt
            });
            grooveIdx++;
          }
        }
      }
      entryDataContract.GrooveSetupList = grooveSetupList;

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return returnVm;
    }


    public async Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVm = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnVm;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id,
        RollSetStatus = RollSetStatus.Ready
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ConfirmRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return returnVm;
    }

    public async Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVm = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnVm;
      }
      //END OF VALIDATION

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup
      {
        Id = viewModel.Id,
        Action = GrindingTurningAction.Cancel
      };

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return returnVm;
    }

    public async Task<VM_Base> ConfirmUpdateGroovesToRollSetDisplay(ModelStateDictionary modelState, VM_RollsetDisplay actualVm)
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

      DCRollSetGrooveSetup entryDataContract = new DCRollSetGrooveSetup();

      entryDataContract.Id = actualVm.RollSetId;
      List<SingleRollDataInfo> rollDataInfoList = new List<SingleRollDataInfo>();
      //add Rolls to list
      if (actualVm.UpperRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.UpperRollId ?? 0,
          Diameter = actualVm.NewUpperDiameter ?? 0.0
        };

        rollDataInfoList.Add(s);
      }
      if (actualVm.BottomRollId != null)
      {
        SingleRollDataInfo s = new SingleRollDataInfo
        {
          RollId = actualVm.BottomRollId ?? 0,
          Diameter = actualVm.NewBottomDiameter ?? 0.0
        };
        rollDataInfoList.Add(s);
      }
      entryDataContract.RollDataInfoList = rollDataInfoList;
      entryDataContract.Action = GrindingTurningAction.Confirm;
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
              GrooveRemark = actualVm.GrooveActualRollUpper[i].GrooveRemark,
              GrooveIdx = grooveIdx,
              GrooveCondition = actualVm.GrooveActualRollUpper[i].EnumGrooveCondition ?? 1,
              GrooveStatus = RollGrooveStatus.PlannedAndTurning,
              GrooveConfirmed = (actualVm.GrooveActualRollUpper[i].GrooveConfirmed ? YesNo.Yes.Value : YesNo.No.Value),
              AccBilletCnt = actualVm.GrooveActualRollUpper[i].AccBilletCnt,
              AccWeight = actualVm.GrooveActualRollUpper[i].AccWeight * 1000,
              AccWeightWithCoeff = actualVm.GrooveActualRollUpper[i].AccWeightWithCoeff * 1000
            });
            grooveIdx++;
          }
        }
      }
      entryDataContract.GrooveSetupList = grooveSetupList;

      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateGroovesToRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return returnVm;
    }

    public SelectList GetGrooveList()
    {
      IList<RLSGrooveTemplate> equipmentgroup = _peContext.RLSGrooveTemplates
        .Where(x => x.EnumGrooveTemplateStatus != GrooveTemplateStatus.History)
        .ToList();

      SelectList tmpSelectList = new SelectList(equipmentgroup, "GrooveTemplateId", "GrooveTemplateCode");

      return tmpSelectList;
    }

    public VM_GrooveTemplate GetGrooveTemplate(long grooveTemplateId)
    {
      RLSGrooveTemplate grooveTemplate = _peContext.RLSGrooveTemplates.FirstOrDefault(x => x.GrooveTemplateId == grooveTemplateId);

      if (grooveTemplate != null)
        return new VM_GrooveTemplate(grooveTemplate);

      return new VM_GrooveTemplate();
    }

    #endregion
  }
}
