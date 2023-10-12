using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCDelay : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public DateTime DelayStart { get; set; }

    [DataMember] public DateTime? DelayEnd { get; set; }

    [DataMember] public string Comment { get; set; }

    [DataMember] public bool IsPlanned { get; set; }

    [DataMember] public long? FkEventCatalogueId { get; set; }

    [DataMember] public long? FkAssetId { get; set; }

    [DataMember] public long? FkWorkOrderId { get; set; }

    [DataMember] public long? FkUserId { get; set; }
  }
}
