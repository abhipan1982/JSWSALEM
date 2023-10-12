using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QTY
{
  public class DCDefectCatalogueCategory : DataContractBase
  {
    [DataMember] public long DefectCatalogueCategoryId { get; set; }

    [DataMember] public string DefectCatalogueCategoryCode { get; set; }

    [DataMember] public string DefectCatalogueCategoryName { get; set; }

    [DataMember] public string DefectCatalogueCategoryDescription { get; set; }

    [DataMember] public bool IsDefault { get; set; }

    [DataMember] public long? FKDefectCategoryGroupsId { get; set; }

    [DataMember] public short EnumAssignmentType { get; set; }
  }
}
