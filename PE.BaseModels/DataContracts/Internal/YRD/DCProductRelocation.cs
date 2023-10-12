using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.YRD
{
  public class DCProductRelocation : DataContractBase
  {
    [DataMember] public long TargetLocationId { get; set; }

    [DataMember] public List<ProductRelocationDetail> Products { get; set; }
  }

  public class ProductRelocationDetail
  {
    [DataMember] public long SourceLocationId { get; set; }

    [DataMember] public List<long> ProductsIds { get; set; }
  }
}
