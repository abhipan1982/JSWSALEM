using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCUnscrapMaterial : DataContractBase
  {
    [DataMember] public long HeatId { get; set; }

    [DataMember] public int MaterialsNumber { get; set; }
  }
}
