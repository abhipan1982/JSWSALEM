using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.HMI
{
  [DataContract]
  public class DCPrintingLabel : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }
    [DataMember] public long ProductId { get; set; }
  }
}
