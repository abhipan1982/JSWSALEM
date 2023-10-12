using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCTransferHeatLocation : DataContractBase
  {
    [DataMember] public long HeatId { get; set; }

    [DataMember] public int MaterialsNumber { get; set; }

    [DataMember] public long SourceLocationId { get; set; }

    [DataMember] public long TargetLocationId { get; set; }

    [DataMember] public long? WorkOrderId { get; set; }
  }
}
