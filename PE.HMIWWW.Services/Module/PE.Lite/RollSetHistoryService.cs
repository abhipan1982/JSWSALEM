using System.Collections.Generic;
using System.Linq;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.PEContext;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.GrindingTurning;
using PE.HMIWWW.ViewModel.Module.Lite.RollsetManagement;
using PE.DbEntity.PEContext;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class RollSetHistoryService : BaseService, IRollSetHistoryService
  {
    private readonly HmiContext _hmiContext;

    public RollSetHistoryService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
    }

    public VM_RollSetOverviewFull GetRollSetById(long? rollSetId)
    {
      V_RollSetOverview rollSet = _hmiContext.V_RollSetOverviews.FirstOrDefault(x => x.IsLastOne && x.RollSetId == rollSetId);

      if (rollSet != null)
      {
        return new VM_RollSetOverviewFull(rollSet);
      }
      else
      {
        return null;
      }
    }

    public VM_RollSetOverviewFull GetRollSetDetails(ModelStateDictionary modelState, long id)
    {
      VM_RollSetOverviewFull returnValueVm = null;

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

    public DataSourceResult GetRollSetSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_RollSetOverview> rollSetList = _hmiContext.V_RollSetOverviews
        .Where(x => x.IsLastOne)
        .OrderByDescending(x => x.RollSetHistoryId).AsQueryable();
      return rollSetList.ToDataSourceLocalResult(request, modelState, data => new VM_RollSetOverviewFull(data));
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

  }
}
