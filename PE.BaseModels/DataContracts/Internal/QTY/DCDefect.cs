using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCDefect : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string DefectName { get; set; }

    [DataMember] public long? DefectCatalogueId { get; set; }

    [DataMember] public long? AssetId { get; set; }

    [DataMember] public long? RawMaterialId { get; set; }

    [DataMember] public short? DefectPosition { get; set; }

    [DataMember] public string DefectDescription { get; set; }
  }
}
