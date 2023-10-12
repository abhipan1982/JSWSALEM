using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCDefectCatalogue : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string DefectCatalogueName { get; set; }

    [DataMember] public string DefectCatalogueCode { get; set; }

    [DataMember] public string DefectCatalogueCategory { get; set; }

    [DataMember] public string DefectCatalogueDescription { get; set; }

    [DataMember] public bool IsDefault { get; set; }

    [DataMember] public bool? IsActive { get; set; }

    [DataMember] public long FkDelayCatalogueCategoryId { get; set; }

    [DataMember] public long? ParentDefectCatalogueId { get; set; }
  }
}
