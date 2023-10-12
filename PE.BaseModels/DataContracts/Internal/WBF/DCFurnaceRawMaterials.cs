using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.WBF
{
  public class DCFurnaceRawMaterials : DataContractBase
  {
    [DataMember] public List<FurnaceRawMaterial> RawMaterials { get; set; } = new List<FurnaceRawMaterial>();
  }

  public class FurnaceRawMaterial
  {
    // TODOMN: Fill in tracking
    [DataMember]
    public DateTime ChargingTime { get; set; }

    /// <summary>
    ///   Material Id
    /// </summary>
    [DataMember]
    public long RawMaterialId { get; set; }

    /// <summary>
    ///   Material position in furnace
    /// </summary>
    [DataMember]
    public short Position { get; set; }
  }
}
