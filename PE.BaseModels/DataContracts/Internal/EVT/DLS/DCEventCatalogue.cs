using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCEventCatalogue : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string EventCatalogueCode { get; set; }

    [DataMember] public string EventCatalogueName { get; set; }

    [DataMember] public string EventCatalogueDescription { get; set; }

    [DataMember] public bool IsActive { get; set; }

    [DataMember] public bool IsDefault { get; set; }

    [DataMember] public bool IsPlanned { get; set; }

    [DataMember] public long? FKEventCategoryId { get; set; }

    [DataMember] public long? ParentEventCatalogueId { get; set; }

    [DataMember] public double? StdDelayTime { get; set; }
  }
}
