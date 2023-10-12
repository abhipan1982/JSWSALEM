using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PE.BaseDbEntity.EnumClasses;
using PE.DbEntity.HmiModels;
using PE.BaseDbEntity.Models;
using PE.BaseDbEntity.PEContext;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.HMIWWW.Core;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.HtmlHelpers;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.ViewModel;
using PE.HMIWWW.ViewModel.System;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.DbEntity.ExceptionHelpers;
using PE.DbEntity.PEContext;
using Kendo.Mvc.Extensions;
using PE.HMIWWW.Core.Extensions;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;
using PE.DbEntity.SPModels;
using PE.DbEntity.EFCoreExtensions;

namespace PE.HMIWWW.Services.System
{
  public interface IShiftCalendarService
  {
    DataSourceResult GetShiftCalendarsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request);

    Task<VM_ShiftCalendarElement> UpdateShiftCalendarElement(ModelStateDictionary modelState,
      VM_ShiftCalendarElement viewModel);

    Task<VM_LongId> DeleteShiftCalendarElement(ModelStateDictionary modelState, VM_LongId viewModel);
    Task<VM_Base> GenerateShiftCalendarForNextWeek(ModelStateDictionary modelState);
    VM_ShiftCalendarElement GetShiftCalendarElement(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel);
    Task<VM_LongId> InsertShiftCalendar(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel);
    DataSourceResult PrepareCrewData(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request);
    Task<VM_Base> GenerateShifts(ModelStateDictionary modelState, IList<VM_ShiftDay> list);
  }

  public class ShiftCalendarService : BaseService, IShiftCalendarService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;

    public ShiftCalendarService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    #region public methods

    public static List<EVTShiftDefinition> GetShiftDefinitionsList()
    {
      List<EVTShiftDefinition> shiftDefinitions = null;
      try
      {
        using (PEContext ctx = new PEContext())
        {
          shiftDefinitions = ctx.EVTShiftDefinitions.ToList();
          foreach (EVTShiftDefinition element in shiftDefinitions)
          {
            element.ShiftCode = ResxHelper.GetResxByKey((ShiftTime)element.ShiftDefinitionId);
          }
        }
      }
      catch (Exception ex)
      {
        DbExceptionResult result =
          DbExceptionHelper.ProcessException(ex, "GetShiftDefinitionsList::Database operation failed!", null);
      }

      return shiftDefinitions;
    }

    #endregion

    #region interface IShiftCalendarService

    public DataSourceResult GetShiftCalendarsList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnValue = null;

      // VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return returnValue;
      }
      //END OF VALIDATION

      //DB OPERATION 
      VM_ShiftCalendar vmodel = new VM_ShiftCalendar();
      List<VM_ShiftCalendarElement> vm = new List<VM_ShiftCalendarElement>();

        IList<V_ShiftCalendar> scCollection = _hmiContext.V_ShiftCalendars.ToList();
        foreach (V_ShiftCalendar rec in scCollection)
        {
          vm.Add(new VM_ShiftCalendarElement(rec));
        }

        vmodel.vmShiftCalendarList = vm;

      returnValue = vmodel.vmShiftCalendarList.ToDataSourceLocalResult(request, modelState, (x) => x);
      //END OF DB OPERATION

      return returnValue;
    }

    public async Task<VM_ShiftCalendarElement> UpdateShiftCalendarElement(ModelStateDictionary modelState,
      VM_ShiftCalendarElement viewModel)
    {
      VM_ShiftCalendarElement returnValueVm = null;

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.CrewId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      DCShiftCalendarElement dc = new DCShiftCalendarElement
      {
        CrewId = viewModel.CrewId,
        ShiftCalendarId = viewModel.ShiftCalendarId,
        ShiftDefinitionId = viewModel.ShiftDefinitionId,
        IsActive = viewModel.IsActive ?? false
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.UpdateShiftCalendarElementAsync(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return viewModel;
    }

    public async Task<VM_LongId> DeleteShiftCalendarElement(ModelStateDictionary modelState, VM_LongId viewModel)
    {
      VM_LongId returnValueVm = null;

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
        return returnValueVm;
      }
      //END OF VALIDATION

      DCShiftCalendarId dc = new DCShiftCalendarId { ShiftCalendarId = viewModel.Id };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DeleteShiftCalendarElement(dc);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      returnValueVm = new VM_LongId(viewModel.Id);

      return returnValueVm;
    }

    public async Task<VM_Base> GenerateShiftCalendarForNextWeek(ModelStateDictionary modelState)
    {
      VM_Base result = new VM_Base();

      // VALIDATE ENTRY PARAMETERS
      if (!modelState.IsValid)
      {
        return result;
      }
      //END OF VALIDATION

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.GenerateShiftCalendarForNextWeek(new DataContractBase());

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public VM_ShiftCalendarElement GetShiftCalendarElement(ModelStateDictionary modelState,
      VM_ShiftCalendarElement viewModel)
    {
      VM_ShiftCalendarElement returnValueVm = new VM_ShiftCalendarElement();

      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (viewModel.ShiftCalendarId <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION

      //DB OPERATION

      V_ShiftCalendar shiftCalendar =
        _hmiContext.V_ShiftCalendars.Where(z => z.ShiftCalendarId == viewModel.ShiftCalendarId).Single();
      if (shiftCalendar != null)
      {
        returnValueVm = new VM_ShiftCalendarElement(shiftCalendar);
        return returnValueVm;
      }

      return viewModel;
      // END OF DB OPERATION 
    }

    public async Task<VM_LongId> InsertShiftCalendar(ModelStateDictionary modelState, VM_ShiftCalendarElement viewModel)
    {
      VM_LongId returnValueVm = null;
      //VALIDATE ENTRY PARAMETERS
      if (viewModel == null)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return returnValueVm;
      }
      //END OF VALIDATION 

      DCShiftCalendarElement dc = new DCShiftCalendarElement
      {
        CrewId = viewModel.CrewId,
        ShiftCalendarId = viewModel.ShiftCalendarId,
        ShiftDefinitionId = viewModel.ShiftDefinitionId,
        IsActive = viewModel.IsActive ?? false,
        Start = viewModel.Start,
        End = viewModel.End
      };

      //request data from module
      SendOfficeResult<DCShiftCalendarId> sendOfficeResult = await HmiSendOffice.InsertShiftCalendar(dc);

      if (sendOfficeResult.OperationSuccess)
      {
        returnValueVm = new VM_LongId(sendOfficeResult.DataConctract.ShiftCalendarId);
      }

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return returnValueVm;
    }

    public DataSourceResult PrepareCrewData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult returnVm = null;
      List<VM_ScheduleItems> scheduleItems = new List<VM_ScheduleItems>();
      List<EVTCrew> crews;

      crews = _peContext.EVTCrews.ToList();
      foreach (EVTCrew c in crews)
      {
        VM_ScheduleItems tmp = new VM_ScheduleItems(c.CrewId, c.CrewName, "#" + ModuloColor.GetColor(c.CrewId));
        tmp.Name = c.CrewName;
        tmp.Id = c.CrewId;
        tmp.Color = "#" + ModuloColor.GetColor(c.CrewId);
        scheduleItems.Add(tmp);
      }

      returnVm = scheduleItems.ToDataSourceLocalResult(request, modelState, (x) => x);
      return returnVm;
    }

    public Task<VM_Base> GenerateShifts(ModelStateDictionary modelState, IList<VM_ShiftDay> list)
    {
      var request = list.Select(x => new SPDayShiftLayoutId()
      {
        DateDay = x.Date,
        ShiftLayoutId = x.ShiftLayout
      }).ToList();

      _hmiContext.ExecuteSPUpdateShiftCalendar(request);

      return Task.FromResult(new VM_Base());
    }

    #endregion
  }
}
