using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DcMeasDataSample : DcMeasData
  {
    /// <summary>
    ///   Array of measured values. Max sixe (x ) has to be agreed with L1 and be used for all messages
    /// </summary>
    [DataMember]
    public List<DcSample> Samples { get; set; }
  }
}
