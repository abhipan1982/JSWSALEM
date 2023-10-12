using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.DBA
{
  public class DCWorkOrderStatus : DataContractBase
  {
    [DataMember] public long Counter { get; set; }

    [DataMember] public CommStatus Status { get; set; }

    [DataMember] public string BackMessage { get; set; }
  }
}
