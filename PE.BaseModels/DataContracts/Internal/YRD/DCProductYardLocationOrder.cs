using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCProductYardLocationOrder : DataContractBase
  {
    [DataMember] public long LocationId { get; set; }

    [DataMember] public short OldIndex { get; set; }

    [DataMember] public short NewIndex { get; set; }
  }
}
