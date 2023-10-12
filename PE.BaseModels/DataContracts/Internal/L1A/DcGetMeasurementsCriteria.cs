using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  public class DcGetMeasurementsCriteria : DataContractBase
  {
    [DataMember] public List<int> FeatureCodes { get; set; } = new List<int>();
    [DataMember] public DateTime DateFrom { get; set; }
    [DataMember] public DateTime DateTo { get; set; }    
  }
}
