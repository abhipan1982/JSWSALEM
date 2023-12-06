using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.DBA
{
  public class DCBatchDataStatus : DataContractBase
  {
    [DataMember] public long Counter { get; set; }

    [DataMember] public CommStatus Status { get; set; }

    [DataMember] public string BackMessage { get; set; }
  }
}
