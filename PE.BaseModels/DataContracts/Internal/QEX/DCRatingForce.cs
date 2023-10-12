using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCRatingForce : DataContractBase
  {
    [DataMember] public long RatingId { get; set; }
    [DataMember] public int? RatingForcedValue { get; set; }
  }
}
