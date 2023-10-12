using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCDelaysToMerge : DataContractBase
  {
    [DataMember] public long FirstDelayId { get; set; }

    [DataMember] public long SecondDelayId { get; set; }
  }
}
