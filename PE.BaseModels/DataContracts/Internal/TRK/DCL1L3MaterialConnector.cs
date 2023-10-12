using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCL1L3MaterialConnector : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }

    [DataMember] public long PRMMaterialId { get; set; }
  }
}
