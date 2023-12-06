using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.PPL
{
  public class DCScheduleWorkOrder : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }
  }
}
