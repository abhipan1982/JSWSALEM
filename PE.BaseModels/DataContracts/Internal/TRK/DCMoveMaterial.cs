using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  [DataContract]
  public class DCMoveMaterial : DataContractBase
  {
    [DataMember] public int DropAssetCode { get; set; }

    [DataMember] public int DropOrderSeq { get; set; }

    [DataMember] public long RawMaterialId { get; set; }

    [DataMember] public int DragAssetCode { get; set; }

    [DataMember] public int DragOrderSeq { get; set; }

    [DataMember] public bool DragFromVirtualPosition { get; set; }
  }
}
