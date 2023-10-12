using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;

namespace PE.HMIWWW.ViewModel.Module.Lite.EventCalendar
{
  public class VM_EventList : VM_Base
  {
    public VM_EventList(EVTEventType entity)
    {
      EventType = entity.EventTypeId;
      EventName = entity.EventTypeName;
      EventCode = entity.EventTypeCode;
      EventColor = entity.HMIColor;
      EventIcon = entity.HMIIcon;
      EditLink = entity.HMILink;
      Editable = entity.HMILink != "" ? true : false;
    }

    public long EventType { get; set; }
    public short EventCode { get; set; }
    public string EventName { get; set; }
    public string EventColor { get; set; }
    public string EventIcon { get; set; }
    public bool Editable { get; set; }
    public string EditLink { get; set; }
  }
}
