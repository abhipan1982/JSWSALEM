using System;
using System.Runtime.Serialization;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DcSample
  {
    /// <summary>
    ///   Sample Value
    /// </summary>
    [DataMember]
    public double Value { get; set; }

    /// <summary>
    ///   Sample head offset / sample number in case when not material realted
    ///   Unit: [m]
    /// </summary>
    [DataMember]
    public double HeadOffset { get; set; }

    /// <summary>
    ///   1 in case when measured value is valid
    ///   default: 0
    /// </summary>
    [DataMember]
    public bool IsValid { get; set; }
  }
}
