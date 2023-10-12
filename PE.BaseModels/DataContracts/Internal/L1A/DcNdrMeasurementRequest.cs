using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.L1A
{
  [DataContract]
  public class DcNdrMeasurementRequest : DataContractBase
  {
    [DataMember] public int ParentFeatureCode { get; set; }
    [DataMember] public DateTime DateToTs { get; set; }
  }

  [DataContract]
  public class DcNdrMeasurementResponse : DataContractBase
  {
    [DataMember] public int FeatureCode { get; set; }
    [DataMember] public double Value { get; set; }
  }
}
