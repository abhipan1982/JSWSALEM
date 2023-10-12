using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCWorkOrdersList : DataContractBase
  {
    [DataMember] public List<long> WorkOrdersList { get; set; }

    [DataMember] public long RawMaterialId { get; set; }
  }
}
