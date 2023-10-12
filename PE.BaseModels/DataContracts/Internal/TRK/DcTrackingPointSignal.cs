using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DcTrackingPointSignal : DataContractBase
  {
    [DataMember]
    public int Value { get; set; }
    
    [DataMember]
    public DateTime OperationDate { get; set; }
    
    [DataMember]
    public DateTime ProcessExpertOperationDate { get; set; }
        
    [DataMember]
    public int FeatureCode { get; set; }
  }

  public class DcAggregatedTrackingPointSignal : DataContractBase
  {
    [DataMember] public List<DcTrackingPointSignal> TrackingPointSignals { get; set; } = new List<DcTrackingPointSignal>();
  }

}
