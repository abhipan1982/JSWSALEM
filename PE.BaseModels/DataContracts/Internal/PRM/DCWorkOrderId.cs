using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrderId : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }
  }
}
