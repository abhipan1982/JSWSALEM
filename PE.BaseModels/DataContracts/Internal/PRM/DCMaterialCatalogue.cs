using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCMaterialCatalogue : DataContractBase
  {
    [DataMember] public long MaterialCatalogueId { get; set; }

    [DataMember] public string MaterialCatalogueName { get; set; }

    [DataMember] public string ExternalMaterialCatalogueName { get; set; }

    [DataMember] public string Description { get; set; }

    [DataMember] public long MaterialCatalogueTypeId { get; set; }

    [DataMember] public long ShapeId { get; set; }

    // Details
    [DataMember] public double? LengthMin { get; set; }

    [DataMember] public double? LengthMax { get; set; }

    [DataMember] public double? WidthMin { get; set; }

    [DataMember] public double? WidthMax { get; set; }

    [DataMember] public double ThicknessMin { get; set; }

    [DataMember] public double ThicknessMax { get; set; }

    [DataMember] public double? WeightMin { get; set; }

    [DataMember] public double? WeightMax { get; set; }

    [DataMember] public long? TypeId { get; set; }
  }
}
