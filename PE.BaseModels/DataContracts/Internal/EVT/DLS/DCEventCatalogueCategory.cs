using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCEventCatalogueCategory : DataContractBase
  {
    [DataMember] public long EventCatalogueCategoryId { get; set; }

    [DataMember] public string EventCatalogueCategoryCode { get; set; }

    [DataMember] public string EventCatalogueCategoryName { get; set; }

    [DataMember] public string EventCatalogueCategoryDescription { get; set; }

    [DataMember] public bool IsDefault { get; set; }

    [DataMember] public long? FKEventCategoryGroupId { get; set; }

    [DataMember] public AssignmentType EnumAssignmentType { get; set; }

    [DataMember] public long EventTypeId { get; set; }
  }
}
