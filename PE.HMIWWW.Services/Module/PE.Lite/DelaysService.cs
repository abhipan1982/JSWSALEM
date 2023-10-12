using System;
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
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.HMIWWW.Core.Communication;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Service;
using PE.HMIWWW.Core.ViewModel;
using PE.HMIWWW.Services.Common;
using PE.HMIWWW.Services.Module.PE.Lite.Interfaces;
using PE.HMIWWW.ViewModel.Module.Lite.Asset;
using PE.HMIWWW.ViewModel.Module.Lite.Delay;
using PE.HMIWWW.ViewModel.Module.Lite.Event;
using PE.HMIWWW.ViewModel.Module.Lite.Shift;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.HMIWWW.UnitConverter;
using PE.DbEntity.PEContext;
using PE.Helpers;

namespace PE.HMIWWW.Services.Module.PE.Lite
{
  public class DelaysService : BaseService, IDelaysService
  {
    private readonly PEContext _peContext;
    private readonly HmiContext _hmiContext;
    public DelaysService(IHttpContextAccessor httpContextAccessor, PEContext peContext, HmiContext hmiContext) : base(httpContextAccessor)
    {
      _peContext = peContext;
      _hmiContext = hmiContext;
    }

    public Dictionary<string, int>[] GetDelaysSummary(DateTime startDateTime, DateTime endDateTime)
    {
      var currentDateTime = DateTime.Now.ExcludeMiliseconds();
      if (startDateTime > currentDateTime)
        startDateTime = currentDateTime;

      if (endDateTime > currentDateTime)
        endDateTime = currentDateTime;

      int durationTotalTime = (int)((endDateTime - startDateTime).TotalSeconds);

      Dictionary<string, int> eventsDurationSummary = new Dictionary<string, int>()
      {
        {"InOperation", 0},
        {"Delays", 0},
        {"Maintenance", 0},
      };

      Dictionary<string, int> eventsMicroDurationSummary = new Dictionary<string, int>()
      {
        {"InOperation", 0},
        {"MicroDelays", 0},
      };

      Dictionary<string, int> eventsCategorySummary = new Dictionary<string, int>();
      Dictionary<string, int> eventsCategoryId = new Dictionary<string, int>();

      List<VM_Delay> delayList = new List<VM_Delay>();
      List<VM_Delay> delayMicroList = new List<VM_Delay>();

      List<EVTShiftCalendar> inactiveShifts = _peContext.EVTShiftCalendars
        .Where(x => !(x.IsActive.Value) &&
                  ((x.PlannedStartTime >= startDateTime && x.PlannedStartTime <= endDateTime) || // start in range
                   (x.PlannedEndTime >= startDateTime && x.PlannedEndTime <= endDateTime) || // end in range

                   (x.PlannedStartTime <= startDateTime && x.PlannedEndTime >= endDateTime))) // delay begins before range and ends after
        .OrderBy(x => x.PlannedStartTime)
        .ToList();

      if (inactiveShifts.Any())
      {
        if (inactiveShifts.First().PlannedStartTime < startDateTime)
          inactiveShifts.First().PlannedStartTime = startDateTime;
        if (inactiveShifts.Last().PlannedEndTime > endDateTime)
          inactiveShifts.Last().PlannedEndTime = endDateTime;
      }

      // decrease total duration by inactive shifts time
      int inactiveShiftsTime = (int)inactiveShifts.Sum(x => (x.PlannedEndTime - x.PlannedStartTime).TotalSeconds);
      durationTotalTime -= inactiveShiftsTime;

      List<EVTEvent> dbList = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.EventStartTs >= startDateTime &&
                    (x.EventEndTs <= endDateTime || !x.EventEndTs.HasValue) &&
                     x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .ToList();

      List<EVTEvent> dbMicroList = dbList
        .Where(x => x.FKEventType.EventTypeCode == (short)ChildMillEventType.Checkpoint1DelayMicroStop)
        .ToList();

      dbList = dbList
        .Where(x => x.FKEventType.EventTypeCode == (short)ChildMillEventType.Checkpoint1Delay)
        .ToList();

      foreach (EVTEvent item in dbList)
      {
        delayList.Add(new VM_Delay(item, currentDateTime));
      }

      foreach (EVTEvent item in dbMicroList)
      {
        delayMicroList.Add(new VM_Delay(item, currentDateTime));
      }

      foreach (VM_Delay item in delayList)
      {
        string eventCatalogueCategory = item.CatalogueCategoryName;

        if (!string.IsNullOrEmpty(eventCatalogueCategory))
        {
          if (!eventsCategorySummary.ContainsKey(eventCatalogueCategory))
          {
            eventsCategorySummary.Add(eventCatalogueCategory, item.DurationInSeconds);
            eventsCategoryId.Add(eventCatalogueCategory, (int)item.CatalogueCategoryId.Value);
          }
          else
            eventsCategorySummary[eventCatalogueCategory] += item.DurationInSeconds;
        }

        if (item.DelayCatalogueIsPlanned || item.IsPlanned)
          eventsDurationSummary["Maintenance"] += item.DurationInSeconds;
        else
          eventsDurationSummary["Delays"] += (item.DurationInSeconds);
      }

      foreach (VM_Delay item in delayMicroList)
      {
        eventsMicroDurationSummary["MicroDelays"] += (item.DurationInSeconds);
      }

      eventsDurationSummary["InOperation"] = durationTotalTime - eventsDurationSummary["Maintenance"] - eventsDurationSummary["Delays"];
      eventsMicroDurationSummary["InOperation"] = eventsDurationSummary["InOperation"] - eventsMicroDurationSummary["MicroDelays"];

      if (eventsMicroDurationSummary["InOperation"] < 0)
        eventsMicroDurationSummary["InOperation"] = 0;

      Dictionary<string, int>[] result = new Dictionary<string, int>[]
      {
          eventsDurationSummary,
          eventsCategorySummary,
          eventsCategoryId,
          eventsMicroDurationSummary
      };

      return result;
    }












    public VM_EventCatalogue GetEventCatalogue(ModelStateDictionary modelState, long id)
    {
      VM_EventCatalogue result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }

      EVTEventCatalogue eventCatalogue = _peContext.EVTEventCatalogues
        .Include(x => x.FKParentEventCatalogue)
        .Include(x => x.FKEventCatalogueCategory)
          .ThenInclude(x => x.FKEventType)
        .Include(x => x.FKEventCatalogueCategory)
          .ThenInclude(x => x.FKEventCategoryGroup)
        .SingleOrDefault(x => x.EventCatalogueId == id);
      result = eventCatalogue == null ? null : new VM_EventCatalogue(eventCatalogue);

      return result;
    }

    public Task<DataSourceResult> GetEventCatalogueList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.EVTEventCatalogues
        .Include(x => x.FKEventCatalogueCategory)
        .Include(x => x.FKParentEventCatalogue)
        .ToDataSourceLocalResult<EVTEventCatalogue, VM_EventCatalogue>(request, modelState, x => new VM_EventCatalogue(x));


      return Task.FromResult(result);
    }

    public Task<DataSourceResult> GetEventCatalogueListByEventData(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, short eventTypeCode, string eventCategoryGroupCode,
      string eventCatalogueCategoryCode)
    {
      DataSourceResult result = null;

      var queryable = _peContext.EVTEventCatalogues
        .Include(x => x.FKEventCatalogueCategory)
        .Include(x => x.FKEventCatalogueCategory.FKEventType)
        .Include(x => x.FKEventCatalogueCategory.FKEventCategoryGroup)
        .Include(x => x.FKParentEventCatalogue)
        .Where(x => x.FKEventCatalogueCategory.FKEventType.EventTypeCode == eventTypeCode);

      if (!string.IsNullOrEmpty(eventCatalogueCategoryCode))
      {
        queryable = queryable.Where(x =>
          x.FKEventCatalogueCategory.EventCatalogueCategoryCode.Trim().Equals(eventCatalogueCategoryCode.Trim()));

        if (!string.IsNullOrEmpty(eventCategoryGroupCode))
          queryable = queryable.Where(x =>
            x.FKEventCatalogueCategory.FKEventCategoryGroupId.HasValue &&
            x.FKEventCatalogueCategory.FKEventCategoryGroup.EventCategoryGroupCode.Trim()
              .Equals(eventCatalogueCategoryCode.Trim()));
      }

      result = queryable
        .ToDataSourceLocalResult<EVTEventCatalogue, VM_EventCatalogue>(request, modelState, x => new VM_EventCatalogue(x));


      return Task.FromResult(result);
    }

    public IList<VM_EventCatalogueCategory> GetEventCategories()
    {
      List<VM_EventCatalogueCategory> result = new List<VM_EventCatalogueCategory>();


      List<EVTEventCatalogueCategory> dbList = _peContext.EVTEventCatalogueCategories
        .ToList();

      foreach (EVTEventCatalogueCategory item in dbList)
      {
        result.Add(new VM_EventCatalogueCategory(item));
      }


      return result;
    }

    public IList<VM_EventCatalogue> GetEventCataloguesForParentSelector()
    {
      List<VM_EventCatalogue> result = new List<VM_EventCatalogue>();


      List<EVTEventCatalogue> dbList = _peContext.EVTEventCatalogues
        .ToList();

      foreach (EVTEventCatalogue item in dbList)
      {
        result.Add(new VM_EventCatalogue(item));
      }


      return result;
    }

    public async Task<VM_Base> UpdateEventCatalogueAsync(ModelStateDictionary modelState,
      VM_EventCatalogue eventCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventCatalogue);

      DCEventCatalogue dcDelayCatalogue = new DCEventCatalogue
      {
        Id = eventCatalogue.Id,
        EventCatalogueName = eventCatalogue.EventCatalogueName,
        StdDelayTime = eventCatalogue.StdEventTime,
        EventCatalogueDescription = eventCatalogue.EventDescription,
        EventCatalogueCode = eventCatalogue.EventCatalogueCode,
        IsActive = eventCatalogue.IsActive,
        IsDefault = eventCatalogue.IsDefault,
        IsPlanned = eventCatalogue.IsPlanned,
        FKEventCategoryId = eventCatalogue.EventCatalogueCategoryId,
        ParentEventCatalogueId = eventCatalogue.ParentEventCatalogueId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.UpdateEventCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> AddEventCatalogueAsync(ModelStateDictionary modelState, VM_EventCatalogue eventCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventCatalogue);

      DCEventCatalogue dcDelayCatalogue = new DCEventCatalogue
      {
        Id = eventCatalogue.Id,
        EventCatalogueName = eventCatalogue.EventCatalogueName,
        StdDelayTime = eventCatalogue.StdEventTime,
        EventCatalogueDescription = eventCatalogue.EventDescription,
        EventCatalogueCode = eventCatalogue.EventCatalogueCode,
        IsActive = eventCatalogue.IsActive,
        IsPlanned = eventCatalogue.IsPlanned,
        IsDefault = eventCatalogue.IsDefault,
        FKEventCategoryId = eventCatalogue.EventCatalogueCategoryId,
        ParentEventCatalogueId = eventCatalogue.ParentEventCatalogueId
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.AddEventCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public async Task<VM_Base> DeleteEventCatalogueAsync(ModelStateDictionary modelState,
      VM_EventCatalogue eventCatalogue)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref eventCatalogue);

      DCEventCatalogue dcDelayCatalogue = new DCEventCatalogue { Id = eventCatalogue.Id };
      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult =
        await HmiSendOffice.DeleteEventCatalogueAsync(dcDelayCatalogue);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      //return view model
      return result;
    }

    public VM_Delay GetDelay(ModelStateDictionary modelState, long id)
    {
      VM_Delay result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }


      EVTEvent delay = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .SingleOrDefault(x => x.EventId == id);
      result = delay == null ? null : new VM_Delay(delay);


      return result;
    }

    public VM_DelayDivision GetDelayDivision(ModelStateDictionary modelState, long id)
    {
      VM_DelayDivision result = null;

      //VALIDATE ENTRY PARAMETERS
      if (id <= 0)
      {
        AddModelStateError(modelState, ResourceController.GetErrorText("BadRequestParameters"));
      }

      if (!modelState.IsValid)
      {
        return result;
      }


      EVTEvent delay = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .SingleOrDefault(x => x.EventId == id);
      result = delay == null ? null : new VM_DelayDivision(delay);


      return result;
    }

    public DataSourceResult GetDelayList(ModelStateDictionary modelState, [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      result = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventType)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x =>
          x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .OrderBy(o => o.EventStartTs)
        .ToDataSourceLocalResult<EVTEvent, VM_Delay>(request, modelState, x => new VM_Delay(x));


      return result;
    }


    public async Task<VM_Base> UpdateDelayAsync(ModelStateDictionary modelState, VM_Delay delay)
    {
      VM_Base result = new VM_Base();

      EVTEventCatalogue delayCatalogue = null;

      delayCatalogue = _peContext.EVTEventCatalogues
        .Include(x => x.FKEventCatalogueCategory)
        .First(x => x.EventCatalogueId == delay.FkEventCatalogueId);


      if (delay.EventEndTs <= delay.EventStartTs)
      {
        AddModelStateError(modelState, "End date cannot be before Start date");
      }

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delay);

      DCDelay dcDelay = new DCDelay()
      {
        Id = delay.Id,
        DelayStart = delay.EventStartTs,
        DelayEnd = delay.EventEndTs,
        IsPlanned = delay.DelayCatalogueIsPlanned || delay.IsPlanned,
        Comment = delay.UserComment,
        FkEventCatalogueId = delay.FkEventCatalogueId,
        FkAssetId = delay.FkAssetId,
        FkWorkOrderId = delay.FkWorkOrderId,
        FkUserId = delay.FkUserId,
      };


      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.SendDelayAsync(dcDelay);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public async Task<VM_Base> CreateDelayAsync(ModelStateDictionary modelState, VM_Delay delay)
    {
      VM_Base result = new VM_Base();

      if (delay.EventEndTs <= delay.EventStartTs)
        modelState.AddModelError("EventEndTs", ResourceController.GetErrorText("IncorrectDateRange"));

      if (!modelState.IsValid)
        return result;

      UnitConverterHelper.ConvertToSi(ref delay);

      DCDelay dcDelay = new DCDelay
      {
        DelayStart = delay.EventStartTs,
        DelayEnd = delay.EventEndTs,
        IsPlanned = true,
        Comment = delay.UserComment,
        FkEventCatalogueId = delay.FkEventCatalogueId,
        FkAssetId = delay.FkAssetId,
        FkWorkOrderId = delay.FkWorkOrderId,
        FkUserId = delay.FkUserId
      };

      InitDataContract(dcDelay);

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.CreateDelayAsync(dcDelay);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public IList<VM_EventCatalogue> GetEventCatalogue()
    {
      List<VM_EventCatalogue> result = new List<VM_EventCatalogue>();

      List<EVTEventCatalogue> dbList = _peContext.EVTEventCatalogues.ToList();
      foreach (EVTEventCatalogue item in dbList)
      {
        result.Add(new VM_EventCatalogue(item));
      }


      return result;
    }

    public IList<VM_EventCatalogueCategory> GetEventCatalogueCategory()
    {
      List<VM_EventCatalogueCategory> result = new List<VM_EventCatalogueCategory>();

      List<EVTEventCatalogueCategory> dbList = _peContext.EVTEventCatalogueCategories.ToList();
      foreach (EVTEventCatalogueCategory item in dbList)
      {
        result.Add(new VM_EventCatalogueCategory(item));
      }


      return result;
    }

    public IList<VM_Asset> GetAssets()
    {
      List<VM_Asset> result = new List<VM_Asset>();

      var assets = _hmiContext.V_Assets
        .Where(x => !x.IsArea)
        .ToList();

      foreach (V_Asset item in assets)
      {
        result.Add(new VM_Asset(item));
      }


      return result;
    }

    public IList<VM_ShiftWorkOrderSimpleData> GetWorkOrders()
    {
      List<VM_ShiftWorkOrderSimpleData> result = new List<VM_ShiftWorkOrderSimpleData>();

      foreach (PRMWorkOrder item in _peContext.PRMWorkOrders.AsQueryable())
      {
        result.Add(new VM_ShiftWorkOrderSimpleData(item));
      }


      return result;
    }

    public async Task<VM_Base> DivideDelayAsync(ModelStateDictionary modelState, VM_DelayDivision delay)
    {
      VM_Base result = new VM_Base();

      if (!modelState.IsValid)
      {
        return result;
      }

      UnitConverterHelper.ConvertToSi(ref delay);

      DCDelayToDivide dcDelayToDivide = new DCDelayToDivide
      {
        DelayId = delay.Id,
        DurationOfNewDelay = (int)delay.NewDelayLength
      };

      //request data from module
      SendOfficeResult<DataContractBase> sendOfficeResult = await HmiSendOffice.DivideDelayAsync(dcDelayToDivide);

      //handle warning information
      HandleWarnings(sendOfficeResult, ref modelState);

      return result;
    }

    public DataSourceResult GetPlannedDelays(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {
      DataSourceResult result = null;

      DateTime currentDateTime = DateTime.Now;


      result = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.IsPlanned == true &&
                    x.EventStartTs >= currentDateTime &&
                     x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .OrderBy(o => o.EventStartTs)
        .ToDataSourceLocalResult<EVTEvent, VM_Delay>(request, modelState, x => new VM_Delay(x));


      return result;
    }

    public async Task<IList<VM_Delay>> GetUpcomingPlannedDelays(ModelStateDictionary modelState)
    {
      IList<VM_Delay> result = new List<VM_Delay>();

      var currentDateTime = DateTime.Now;

      if (!modelState.IsValid)
      {
        return result;
      }

      var delays = await _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.IsPlanned && x.EventStartTs >= currentDateTime &&
                     x.FKEventTypeId == EventType.GetValue(EventType.Checkpoint1Delay))
        .OrderBy(o => o.EventStartTs)
        .Take(2)
        .ToListAsync();

      foreach (var item in delays)
      {
        result.Add(new VM_Delay(item));
      }

      return result;
    }

    public DataSourceResult GetDelayBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result = null;

      result = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.EventStartTs >= startDateTime &&
                    x.EventEndTs <= endDateTime &&
                    x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .OrderBy(o => o.EventStartTs)
        .ToDataSourceLocalResult<EVTEvent, VM_Delay>(request, modelState, x => new VM_Delay(x));


      return result;
    }

    public DataSourceResult GetDelaysPlannedBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result = null;

      result = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.IsPlanned == true &&
                    x.EventStartTs >= startDateTime &&
                    x.EventEndTs <= endDateTime &&
                    x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .OrderBy(o => o.EventStartTs)
                  .ToDataSourceLocalResult<EVTEvent, VM_Delay>(request, modelState, x => new VM_Delay(x));


      return result;
    }

    public DataSourceResult GetDelaysUnplannedBetweenDatesList(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request, DateTime startDateTime, DateTime endDateTime)
    {
      DataSourceResult result = null;

      result = _peContext.EVTEvents
        .Include(x => x.FKRawMaterial)
        .Include(x => x.FKEventCatalogue)
        .Include(x => x.FKEventType)
        .Include(x => x.FKAsset)
        .Include(x => x.FKWorkOrder)
        .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
        .Where(x => x.IsPlanned == false &&
                    x.EventStartTs >= startDateTime &&
                    x.EventEndTs <= endDateTime &&
                    x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay)
        .OrderByDescending(o => o.EventStartTs)
                  .ToDataSourceLocalResult<EVTEvent, VM_Delay>(request, modelState, x => new VM_Delay(x));


      return result;
    }



    public async Task<bool> ValidateEventCode(string code)
    {
      bool exists = false;


      if (!string.IsNullOrEmpty(code))
      {
        exists = await _peContext.EVTEvents
          .Include(x => x.FKEventCatalogue)
          .AnyAsync(p => p.FKEventCatalogue.EventCatalogueCode == code);
      }

      return exists;
    }

    public async Task<bool> ValidateEventName(string name)
    {
      bool exists = false;

      if (!string.IsNullOrEmpty(name))
      {
        exists = await _peContext.EVTEvents
          .Include(x => x.FKEventCatalogue)
          .AnyAsync(p => p.FKEventCatalogue.EventCatalogueName == name);
      }

      return exists;
    }

    public DataSourceResult GetDelaysOverviewByShiftIdAndWorkOrderId(ModelStateDictionary modelState,
      DataSourceRequest request, VM_ShiftWorkOrderModel model)
    {

      IQueryable<EVTEvent> delaysOfShift = _peContext.EVTEvents
       .Include(x => x.FKEventType)
       .Include(x => x.FKEventCatalogue)
       .Include(x => x.FKEventCatalogue.FKEventCatalogueCategory)
       .Where(x => !x.IsPlanned &&
                   x.FKShiftCalendarId == model.ShiftId &&
                   x.FKEventType.FKParentEvenTypeId == (short)ParentMillEventType.LineDelay &&
                   (!x.FKWorkOrderId.HasValue ||
        (x.FKWorkOrderId.HasValue && model.WorkOrderId.HasValue && x.FKWorkOrderId == model.WorkOrderId)));

      return delaysOfShift.ToDataSourceLocalResult<EVTEvent, VM_DelayOverview>(request, modelState, data => new VM_DelayOverview(data));

    }

    public DataSourceResult GetActiveDelay(ModelStateDictionary modelState,
      [DataSourceRequest] DataSourceRequest request)
    {

      IQueryable<V_DelayOverview> delayList = _hmiContext.V_DelayOverviews.Where(x => x.EventEndTs == null);

      return delayList.ToDataSourceLocalResult<V_DelayOverview, VM_DelayOverview>(request, modelState, data => new VM_DelayOverview(data));

    }

    public async Task<IList<VM_EventCatalogue>> FilterEventCatalogue(long? eventCatalogueCategoryId)
    {
      List<VM_EventCatalogue> result = new List<VM_EventCatalogue>();

      if (eventCatalogueCategoryId.HasValue)
      {
        var catalogues = await _peContext.EVTEventCatalogues
          .Where(x => x.FKEventCatalogueCategoryId == eventCatalogueCategoryId).ToListAsync();

        foreach (EVTEventCatalogue catalogue in catalogues)
        {
          result.Add(new VM_EventCatalogue(catalogue));
        }
      }
      return result;
    }

    public DataSourceResult GetEventCatalogueSearchList(ModelStateDictionary modelState, DataSourceRequest request)
    {

      IQueryable<V_EventCatalogueSearchGrid> eventsStructureList = _hmiContext.V_EventCatalogueSearchGrids.AsQueryable();
      return eventsStructureList.ToDataSourceLocalResult(request, modelState,
       data => new VM_EventCatalogueSearchGrid(data));

    }

    public List<short> GetParentEventCodes()
    {
      List<short> parentEventsCodes = new List<short>();

      parentEventsCodes = _peContext.EVTEventTypes.Where(x => x.FKParentEvenTypeId == null).Select(x => x.EventTypeCode)
        .ToList();


      return parentEventsCodes;
    }
  }
}
