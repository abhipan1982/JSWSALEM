using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCMaterialAssign : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }

    [DataMember] public long L3MaterialId { get; set; }
  }
}
