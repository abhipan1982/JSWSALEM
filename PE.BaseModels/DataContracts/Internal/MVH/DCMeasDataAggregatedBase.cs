using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PE.BaseModels.DataContracts.Internal.MVH
{
  public class DCMeasDataAggregatedBase<T> : DCMeasDataBase
  {
    public DCMeasDataAggregatedBase()
    {
      Measurements = new List<T>();
    }

    /// <summary>
    ///   List of measurements
    /// </summary>
    [DataMember]
    public List<T> Measurements { get; set; }
  }
}
