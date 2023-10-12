using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCCreateMaterialInReception : DataContractBase
  {
    [DataMember] public long HeatId { get; set; }

    [DataMember] public long SteelgradeId { get; set; }

    [DataMember] public int MaterialsNumber { get; set; }

    [DataMember] public long MaterialCatalogueId { get; set; }
  }
}
