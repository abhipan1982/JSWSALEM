using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCAppendRawMaterialsToLayerRequest : DataContractBase
  {
    [DataMember] public long LayerId { get; set; }

    [DataMember] public List<long> MaterialIds { get; set; }
  }
}
