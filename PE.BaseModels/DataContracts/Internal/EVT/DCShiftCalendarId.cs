using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCShiftCalendarId : DataContractBase
  {
    [DataMember] public long ShiftCalendarId { get; set; }
  }
}
