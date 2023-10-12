using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrderStateChange : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }

    [DataMember] public bool IsBlocked { get; set; }
  }
}
