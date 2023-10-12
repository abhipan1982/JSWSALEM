using System.Linq;
using System.Threading.Tasks;
using PE.HMIWWW.Core.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Schedule;
using PE.HMIWWW.ViewModel.Module.Lite.WorkOrder;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.HMIWWW.Services.System;
using PE.HMIWWW.Core.Parameter;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class ScheduleService : BaseService, IScheduleService
  {
    private readonly HmiContext _hmiContext;
    private readonly PEContext _peContext;

    public ScheduleService(IHttpContextAccessor httpContextAccessor, HmiContext hmiContext, PEContext peContext) : base(httpContextAccessor)
    {
      _hmiContext = hmiContext;
      _peContext = peContext;
    }

    public DataSourceResult GetSchedule(ModelStateDictionary modelState, DataSourceRequest request)
    {
      IQueryable<V_ScheduleSummary> scheduleList = _hmiContext.V_ScheduleSummaries
        .OrderByDescending(x => x.ScheduleOrderSeq)
        .AsQueryable();

      return scheduleList.ToDataSourceLocalResult(request, modelState, data => new VM_ScheduleSummary(data));
    }

    public async Task<VM_Base> AddWorkOrderToSchedule(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule { WorkOrderId = workOrderId };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddWorkOrderToScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> RemoveItemFromSchedule(ModelStateDictionary modelState, long scheduleId,
      long workOrderId, short orderSeq)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule
      {
        WorkOrderId = workOrderId,
        ScheduleId = scheduleId,
        OrderSeq = orderSeq
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.RemoveItemFromScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> EndOfWorkOrder(ModelStateDictionary modelState, long workOrderId)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      WorkOrderId dataToSend = new WorkOrderId
      {
        Id = workOrderId
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.EndOfWorkOrderAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> MoveItemInSchedule(ModelStateDictionary modelState, long scheduleId, long workOrderId,
      int seqId, int newIndex)
    {
      VM_Base result = new VM_Base();

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      DCWorkOrderToSchedule dataToSend = new DCWorkOrderToSchedule
      {
        ScheduleId = scheduleId,
        WorkOrderId = workOrderId,
        NewSequenceNumber = newIndex,
        OrderSeq = seqId
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.MoveItemInScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Schedule GetScheduleData(ModelStateDictionary modelState, long scheduleId)
    {
      VM_Schedule result = null;

      if (scheduleId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      PPLSchedule schedule = _peContext.PPLSchedules.Where(w => w.ScheduleId == scheduleId)
        .Include(i => i.FKWorkOrder.FKHeat)
        .Include(i => i.FKWorkOrder.FKSteelgrade)
        .Include(l => l.FKWorkOrder.PRMMaterials)
        .Include(l => l.FKWorkOrder.FKMaterialCatalogue)
        .Include(l => l.FKWorkOrder.FKProductCatalogue).FirstOrDefault();

      result = new VM_Schedule(schedule);
      result.PlannedWeight = schedule.FKWorkOrder.TargetOrderWeight;
      result.NoOfmaterials = schedule.FKWorkOrder.PRMMaterials.Count;

      return result;
    }

    public async Task<VM_Base> CreateTestSchedule(ModelStateDictionary modelState, VM_Schedule viewModel)
    {
      VM_Base result = new VM_Base();

      int? maxMaterialsCount = ParameterController.GetParameter("ScheduleMaxMaterialsCount").ValueInt.GetValueOrDefault();

      if (viewModel.NoOfmaterials > maxMaterialsCount)
      {
        modelState.AddModelError(nameof(viewModel.NoOfmaterials), ResourceController.GetErrorText("IncorrectNumberOfMaterials"));
        return result;
      }

      //verify model state
      if (!modelState.IsValid)
      {
        return result;
      }

      //prepare data to send
      UnitConverterHelper.ConvertToSi(ref viewModel);

      DCTestSchedule dataToSend = new DCTestSchedule
      {
        ScheduleId = viewModel.ScheduleId,
        OrderSeq = viewModel.OrderSeq,
        ScheduleStatus = viewModel.ScheduleStatus,
        PlannedWeight = viewModel.PlannedWeight,
        //Todo PlannedTime = viewModel.PlannedTime?.Ticks,
        StartTime = viewModel.StartTime,
        EndTime = viewModel.EndTime,
        PlannedStartTime = viewModel.PlannedStartTime,
        PlannedEndTime = viewModel.PlannedEndTime,
        NoOfmaterials = viewModel.NoOfmaterials,
        WorkOrderId = viewModel.FKWorkOrderId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.AddTestScheduleAsync(dataToSend);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_WorkOrderSummary PreparePratialForSchedule(ModelStateDictionary modelState)
    {
      VM_WorkOrderSummary result = null;
      return result;
    }
  }
}
