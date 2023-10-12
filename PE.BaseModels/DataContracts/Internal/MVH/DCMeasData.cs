using System;
using System.Runtime.Serialization;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DcMeasData : DCMeasDataBase
  {
    /// <summary>
    ///   1 in case when measured value is valid
    ///   default 0
    /// </summary>
    [DataMember]
    public bool Valid { get; set; }
        
    /// <summary>
    ///   Min value
    /// </summary>
    [DataMember]
    public double Min { get; set; }

    /// <summary>
    ///   Avg value
    /// </summary>
    [DataMember]
    public double Avg { get; set; }

    /// <summary>
    ///   Max value
    /// </summary>
    [DataMember]
    public double Max { get; set; }

    /// <summary>
    ///   Event code defined in PE
    /// </summary>
    [DataMember]
    public int FeatureCode { get; set; }

    [DataMember]
    public bool ShouldCached { get; set; }

    [DataMember]
    public DateTime FirstMeasurementTs { get; set; }

    [DataMember]
    public DateTime LastMeasurementTs { get; set; }

    [DataMember]
    public double ActualLength { get; set; }
  }
}
