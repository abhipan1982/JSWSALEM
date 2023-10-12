using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCDelayToDivide : DataContractBase
  {
    [DataMember] public long DelayId { get; set; }

    [DataMember] public int DurationOfNewDelay { get; set; }
  }
}
