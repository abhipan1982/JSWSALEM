using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCShiftCalendarElement : DataContractBase
  {
    [DataMember] public long CrewId { get; set; }

    [DataMember] public long? ShiftCalendarId { get; set; }

    [DataMember] public long ShiftDefinitionId { get; set; }

    [DataMember] public DateTime Start { get; set; }

    [DataMember] public DateTime End { get; set; }

    [DataMember] public bool IsActive { get; set; }
  }
}
