using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCAreaRawMaterial : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }
    [DataMember] public long AssetId { get; set; }
  }
}
