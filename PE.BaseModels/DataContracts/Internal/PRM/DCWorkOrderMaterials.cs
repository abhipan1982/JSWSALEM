using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrderMaterials : DataContractBase
  {
    [DataMember] public long WorkOrderId { get; set; }

    [DataMember] public short MaterialsNumber { get; set; }
  }
}
