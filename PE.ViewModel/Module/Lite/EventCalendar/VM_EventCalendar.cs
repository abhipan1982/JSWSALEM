using System;
using System.ComponentModel.DataAnnotations;
using PE.DbEntity.HmiModels;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventCalendar : VM_Base
  {
    public VM_EventCalendar(V_Event entity)
    {
      EventTypeDescription = entity.EventTypeDescription;
      EventTypeId = entity.EventTypeId;
      EventTypeName = entity.EventTypeName;
      EventStartTs = entity.EventStartTs;
      EventEndTs = entity.EventEndTs ?? DateTime.Now;
      EventId = entity.EventId;
      HMIColor = entity.HMIColor;
      AssetName = entity.AssetName;
    }

    public string EventTypeDescription { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "EventStartTs", "NAME_EventStart")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime EventStartTs { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "EventEndTs", "NAME_EventEnd")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime EventEndTs { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "EventTypeId", "NAME_EventType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public long? EventTypeId { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "EventTypeName", "NAME_EventType")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventTypeName { get; set; }

    public long? EventId { get; set; }

    public string HMIColor { get; set; }

    [SmfDisplay(typeof(VM_EventCalendar), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; set; }
  }
}
