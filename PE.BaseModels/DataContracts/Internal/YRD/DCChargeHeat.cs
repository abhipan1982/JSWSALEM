using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCTransferHeatToChargingGrid : DataContractBase
  {
    [DataMember] public long HeatId { get; set; }

    [DataMember] public int MaterialsNumber { get; set; }

    [DataMember] public long WorkOrderId { get; set; }

    [DataMember] public long SourceLocationId { get; set; }
  }
}
