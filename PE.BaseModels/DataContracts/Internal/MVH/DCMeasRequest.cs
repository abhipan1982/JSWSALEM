using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DCMeasRequest : DataContractBase
  {
    /// <summary>
    ///   Feature id - defined by PE
    /// </summary>
    [DataMember]
    public List<int> FeatureCodes { get; set; }

    /// <summary>
    ///   Numer of measurements for each feature
    /// </summary>
    [DataMember]
    public int NumerOfMeasurements { get; set; }
  }
}
