using System.Runtime.Serialization;
using SMF.Core.DC;
using System;

namespace PE.BaseModels.DataContracts.Internal.PRF
{
  [DataContract]
  public class DCCalculateKPI : DataContractBase
  {
    [DataMember] public long? WorkOrderId { get; set; }

    [DataMember] public long? L3MaterialId { get; set; }

    [DataMember] public long?  ShiftCalendarId { get; set; }

    [DataMember] public DateTime? TimeFrom { get; set; }

    [DataMember] public DateTime? TimeTo { get; set; }

    [DataMember] public bool? IsEndOfShift { get; set; }
  }
}
