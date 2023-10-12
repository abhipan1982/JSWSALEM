using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventDetails : VM_Base
  {
    //TODOMN - exclude this
    public VM_EventDetails(EVTEvent model)
    {
      //TODOMN - refactor this
      MillEventId = model.EventId;
      EventTypeName = model.FKEventType.EventTypeName;
      DelayStart = model.EventStartTs;
      DelayEnd = model.EventEndTs;
      WorkOrderName = model.FKWorkOrder?.WorkOrderName;
      RawMaterialName = model.FKRawMaterial?.RawMaterialName;
      AssetName = model.FKAsset?.AssetName;

      if (model.EventEndTs is not null)
      {
        TimeSpan? duration = model.EventEndTs - model.EventEndTs;
        int durationInSeconds = (int)Math.Round(duration?.TotalSeconds ?? 0);
        TimeSpan time = TimeSpan.FromSeconds(durationInSeconds);
        DelayDuration = time.ToString(@"hh\:mm\:ss");
      }

      UserComment = model.UserComment;
    }

    public long MillEventId { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "EventCode", "NAME_EventCode")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventCode { get; set; }

    [SmfDisplay(typeof(VM_EventDetails), "EventTypeName", "NAME_EventTypeName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string EventTypeName { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "DelayStart", "NAME_EventStart")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? DelayStart { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "DelayEnd", "NAME_EventEnd")]
    [SmfDateTime(DateTimeDisplayFormat.ShortDateTime)]
    public DateTime? DelayEnd { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "Duration", "NAME_Duration")]
    public string DelayDuration { get; set; }

    [SmfDisplay(typeof(VM_EventDetails), "WorkOrderName", "NAME_PendingChange_WorkOrderMoveOrClosed")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string WorkOrderName { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "RawMaterialName", "NAME_MaterialName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string RawMaterialName { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "AssetName", "NAME_AssetName")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string AssetName { get; private set; }

    public DateTime EventCreatedTs { get; private set; }

    [SmfDisplay(typeof(VM_EventDetails), "UserComment", "NAME_UserComment")]
    [DisplayFormat(NullDisplayText = "-", HtmlEncode = false)]
    public string UserComment { get; private set; }
  }
}
