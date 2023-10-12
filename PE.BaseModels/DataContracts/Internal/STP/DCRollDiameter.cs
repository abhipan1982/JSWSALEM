using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCRollDiameter : DataContractBase
  {
    [DataMember] public long RollDiameterId { get; set; }

    [DataMember] public string RollDiameterValue { get; set; }
  }
}
