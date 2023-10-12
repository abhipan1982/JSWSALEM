using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCMaterialProductionEnd : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }
  }
}
