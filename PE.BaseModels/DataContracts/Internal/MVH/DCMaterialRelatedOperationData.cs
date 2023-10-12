using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DCMaterialRelatedOperationData : DataContractBase
  {
    /// <summary>
    ///   Triggerd material Id
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }
  }
}
