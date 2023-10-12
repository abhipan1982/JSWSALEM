using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCCreateMaterialWithHeatInReception : DataContractBase
  {
    [DataMember] public string HeatName { get; set; }

    [DataMember] public long? HeatSupplierId { get; set; }

    [DataMember] public double? HeatWeight { get; set; }

    [DataMember] public bool? IsDummy { get; set; }

    [DataMember] public long? SteelgradeId { get; set; }

    [DataMember] public int MaterialsNumber { get; set; }

    [DataMember] public long MaterialCatalogueId { get; set; }
  }
}
