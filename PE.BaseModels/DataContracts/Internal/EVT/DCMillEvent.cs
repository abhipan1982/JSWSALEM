using System;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCMillEvent : DataContractBase
  {
    [DataMember] public ChildMillEventType EventType { get; set; }

    [DataMember] public long? ShiftCalendarId { get; set; }

    [DataMember] public long? WorkOrderId { get; set; }

    [DataMember] public long? RawMaterialId { get; set; }

    [DataMember] public long? AssetId { get; set; }

    [DataMember] public long? DelayId { get; set; }

    [DataMember] public DateTime DateStart { get; set; }

    [DataMember] public DateTime? DateEnd { get; set; }

    [DataMember] public string UserId { get; set; }
  }
}
