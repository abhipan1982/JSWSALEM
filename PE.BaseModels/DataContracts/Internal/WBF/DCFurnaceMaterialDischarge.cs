using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.WBF
{
  public class DCFurnaceMaterialDischarge : DataContractBase
  {
    // TODOMN: Fill in tracking
    [DataMember]
    public DateTime DischargingTime { get; set; }

    /// <summary>
    ///   Material Id
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }
  }
}
