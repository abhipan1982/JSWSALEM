using System;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventCalendar_Scheduler : VM_Base, ISchedulerEvent
  {
    public VM_EventCalendar_Scheduler(V_Event entity)
    {
      Title = "";
      Start = entity.EventStartTs;
      End = entity.EventEndTs ?? DateTime.Now;
      IsOngoing = entity.EventEndTs == null ? true : false;
      StartTimezone = null;
      EndTimezone = null;
      Description = "";
      IsAllDay = false;
      RecurrenceRule = null;
      EventTypeId = entity.EventTypeId;
      RecurrenceException = null;
      RecurrenceID = null;
      EventId = entity.EventId;
      AssetName = entity.AssetName;
      Link = "//Delays//DelayEditPopup";
      Id = 24297;
    }



    public bool IsOngoing { get; set; }
    public long EventTypeId { get; set; }
    public long EventId { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "EventTypeName", "NAME_EventType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventTypeName { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
    public string Link { get; set; }
    public long Id { get; set; }


    // --- ISchedulerEvent implementation ---
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string StartTimezone { get; set; }
    public string EndTimezone { get; set; }
    public bool IsAllDay { get; set; }
    public int? RecurrenceID { get; set; }
    public string RecurrenceRule { get; set; }
    public string RecurrenceException { get; set; }
  }
}
