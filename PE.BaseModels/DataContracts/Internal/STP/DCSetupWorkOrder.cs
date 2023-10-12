using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupWorkOrder : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }
  }
}
