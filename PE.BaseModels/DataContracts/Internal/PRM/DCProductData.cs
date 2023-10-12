using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCProductData : DataContractBase
  {
    [DataMember] public long ProductId { get; set; }

    [DataMember] public long RawMaterialId { get; set; }
  }
}
