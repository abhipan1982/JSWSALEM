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
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollSetManagementService : BaseService, IRollSetManagementService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public RollSetManagementService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    #region interface IRollSetManagementService
    public DataSourceResult GetRollSetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;
      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
      .Where(z => z.IsLastOne && z.EnumRollSetStatus != RollSetStatus.ScheduledForAssemble.Value)
      .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverviewFull(data));

      return result;
    }

    public DataSourceResult GetScheduledRollSetList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      List<V_RollSetOverview> list = _hmiContext.V_RollSetOverviews
        .Where(z => z.IsLastOne && z.EnumRollSetStatus == RollSetStatus.ScheduledForAssemble.Value)
        .ToList();
      result = list.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverviewFull(data));

      return result;
    }

    public VM_RollSetOverviewFull GetRollSet(ModelStateDictionary modelState, long id)
    {
      VM_RollSetOverviewFull returnValueVm = null;

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
      V_RollSetOverview rollSet = _hmiContext.V_RollSetOverviews.FirstOrDefault(x => x.IsLastOne && x.RollSetId == id);
      if (rollSet != null)
      {
        returnValueVm = new VM_RollSetOverviewFull(rollSet);
      }
      //END OF DB OPERATION

      return returnValueVm;
    }

    public async Task<VM_Base> InsertRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        RollSetName = viewModel.RollSetName,
        RollSetType = RollSetType.GetValue(viewModel.RollSetType),
        RollSetStatus = RollSetStatus.Empty,
        Description = viewModel.RollSetDescription
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.InsertRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> AssembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.RollSetId,
        RollSetName = viewModel.RollSetName,
        BottomRollId = viewModel.BottomRollId,
        UpperRollId = viewModel.UpperRollId,
        ThirdRollId = viewModel.ThirdRollId,
        RollSetType = viewModel.RollSetTypeEnum,
        RollSetStatus = RollSetStatus.ScheduledForAssemble
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AssembleRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }
    public async Task<VM_Base> UpdateRollSetStatus(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.RollSetId,
        RollSetStatus = RollSetStatus.GetValue(viewModel.EnumRollSetStatus),
        Description = viewModel.RollSetDescription
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> ConfirmRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id,
        RollSetStatus = RollSetStatus.Ready
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.ConfirmRollSetStatusAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CancelRollSetStatus(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_Base returnVm = new VM_Base();

      ////VALIDATE ENTRY PARAMETERS
      //if (!modelState.IsValid)
      //{
      //  return returnVM;
      //}
      ////END OF VALIDATION

      //DCRollSetData entryDataContract = new DCRollSetData();
      //ModuleController.InitDataContract(entryDataContract);
      //entryDataContract.Id = viewModel.Id;
      //entryDataContract.RollSetStatus = (short)PE.Core.Constants.RollSetStatus.Empty;
      //// entryDataContract.IsThirdRoll = (short)PE.Core.Constants.NumberOfActiveRoll.twoActiveRollsRM; // default = 0

      //Task<SendOfficeResult<bool>> taskOnRemoteModule = HmiSendOffice.CancelRollSetStatus(entryDataContract);

      //if (await Task.WhenAny(taskOnRemoteModule) == taskOnRemoteModule)
      //{
      //  //task completed within timeout
      //  //use the result from "SendOffice"
      //  if (taskOnRemoteModule.Result != null)
      //  {
      //    //check communication
      //    if (taskOnRemoteModule.Result.OperationSuccess)
      //    {
      //      //check result of request
      //      if (taskOnRemoteModule.Result.DataConctract)
      //      {
      //        ModuleController.Logger.Info("ProdManager operation: \"UpdateRollSet\" result: OK");
      //      }
      //      else
      //      {
      //        ModuleController.Logger.Error("Error in ProdManager during operation:  \"UpdateRollSet\"");
      //        throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("ErrorInModuleProdManager"));
      //      }
      //    }
      //    else
      //      throw new Exception(ResourceController.GetErrorText("NoCommunication"));
      //  }
      //}
      //else
      //{
      //  // timeout logic
      //  throw new Exception(PE.HMIWWW.Core.Resources.ResourceController.GetErrorText("TimeoutInModuleProdManager"));
      //}

      await Task.CompletedTask;
      return returnVm;
    }

    public async Task<VM_Base> DisassembleRollSet(ModelStateDictionary modelState, VM_RollSetOverviewFull viewModel)
    {
      VM_Base result = new VM_Base();

      //VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.RollSetId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DisassembleRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> DeleteRollSet(ModelStateDictionary modelState, VM_LongId viewModel)
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

      DCRollSetData entryDataContract = new DCRollSetData
      {
        Id = viewModel.Id
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteRollSetAsync(entryDataContract);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public static List<RLSRollSet> GetEmptyRollsetList()
    {
      List<RLSRollSet> rollSet = null;
      using PEContext ctx = new PEContext();
      rollSet = ctx.RLSRollSets
      .Where(r => r.EnumRollSetStatus == RollSetStatus.Empty)
      .ToList();

      return rollSet;
    }

    public static SelectList GetRolls(string rollSetIdString)  // filtr for Rolls (upperRoll). The same RollType in AssembleRollSetDilog & AssembleExtendedRollSetDialog
    {
      long rollSetId = -1;
      if (!String.IsNullOrEmpty(rollSetIdString))
        rollSetId = Int64.Parse(rollSetIdString);
      using PEContext ctx = new PEContext();
      using HmiContext hmiCtx = new HmiContext();
      RLSRollSet rollSet = ctx.RLSRollSets.FirstOrDefault(z => z.RollSetId == rollSetId);
      if (rollSet != null)
      {
        IList<V_Roll> rollsWithType = hmiCtx.V_Rolls
          .Where(r => (r.EnumRollStatus == RollStatus.New || r.EnumRollStatus == RollStatus.Unassigned.Value) && r.MatchingRollsetType == rollSet.RollSetType)
          .OrderBy(f => f.RollName)
          .ToList();
        Dictionary<long, string> resultDictionary = new Dictionary<long, string>();

        foreach (V_Roll rl in rollsWithType)
        {
          resultDictionary.Add(rl.RollId, rl.RollName);
        }
        SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");
        return tmpSelectList;
      }
      else
      {
        return null;
      }
    }

    public static SelectList GetGrooveTemplates()
    {
      using PEContext ctx = new PEContext();
      IList<RLSGrooveTemplate> grooveTemplates = ctx.RLSGrooveTemplates
        .Where(x => x.EnumGrooveTemplateStatus != GrooveTemplateStatus.History.Value)
        .ToList();

      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      foreach (RLSGrooveTemplate element in grooveTemplates)
      {
        resultDictionary.Add(element.GrooveTemplateId, element.GrooveTemplateName);
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    public static SelectList GetRollsWithMaterials(string uRollId)  // filtr for Rolls (bottomRoll & thirdRoll). The same RollType and SteelgradeRoll in AssembleRollSetDilog & AssembleExtendedRollSetDialog
    {
      long upperRollId = -1;
      upperRollId = long.Parse(uRollId);
      using HmiContext ctx = new HmiContext();
      V_Roll upperRoll = ctx.V_Rolls.FirstOrDefault(x => x.RollId == upperRollId);
      IList<V_Roll> rollList = ctx.V_Rolls
        .Where(r => (r.EnumRollStatus == RollStatus.New.Value || r.EnumRollStatus == RollStatus.Unassigned.Value) && r.RollTypeId == upperRoll.RollTypeId && r.RollSteelgrade == upperRoll.RollSteelgrade)
        .ToList();
      Dictionary<long, string> resultDictionary = new Dictionary<long, string>();
      foreach (V_Roll item in rollList)
      {
        resultDictionary.Add(item.RollId, item.RollName);
      }
      SelectList tmpSelectList = new SelectList(resultDictionary, "Key", "Value");

      return tmpSelectList;
    }

    #endregion
  }
}
