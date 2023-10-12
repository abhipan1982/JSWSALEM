using System.ComponentModel.DataAnnotations.Schema;
using PE.BaseDbEntity.EnumClasses;


namespace PE.BaseDbEntity.Models
{
  public partial class EVTEventType
  {
    [NotMapped]
    public EventType EventType { get { return (EventType)this.EventTypeCode; } set { this.EventTypeCode = value; } }
  }
}
