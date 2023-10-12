using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCCompensationTrigger : DataContractBase
  {
    [DataMember] public long CompensationId { get; set; }
    [DataMember] public long RatingId { get; set; }
    [DataMember] public bool IsChosen { get; set; }
  }
}
