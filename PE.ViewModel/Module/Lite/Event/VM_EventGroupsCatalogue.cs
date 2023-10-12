using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;

namespace PE.HMIWWW.ViewModel.Module.Lite.Event
{
  public class VM_EventGroupsCatalogue : VM_Base
  {
    public VM_EventGroupsCatalogue()
    {
    }

    public VM_EventGroupsCatalogue(EVTEventCategoryGroup entity)
    {
      EventCategoryGroupId = entity.EventCategoryGroupId;
      EventCategoryGroupCode = entity.EventCategoryGroupCode;
      EventCategoryGroupName = entity.EventCategoryGroupName;
      UnitConverterHelper.ConvertToLocal(this);
    }

    public long EventCategoryGroupId { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EventCategoryGroupName", "NAME_EventCategoryGroup")]
    public string EventCategoryGroupName { get; set; }

    [SmfDisplay(typeof(VM_EventGroupsCatalogue), "EventCategoryGroupCode", "NAME_EventCategoryGroupCode")]
    public string EventCategoryGroupCode { get; set; }
  }
}
