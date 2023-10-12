using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PPL
{
  public class DCWorkOrderToSchedule : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }

    [DataMember] public long ScheduleId { get; set; }

    [DataMember] public int OrderSeq { get; set; }

    [DataMember] public int NewSequenceNumber { get; set; }
  }
}
