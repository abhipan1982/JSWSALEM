using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TRK
{
  public class DCCoilData : DataContractBase
  {
    [DataMember] public long RawMaterialId { get; set; }

    [DataMember] public long? FKMaterialId { get; set; }

    [DataMember] public double OverallWeight { get; set; }

    [DataMember] public DateTime Date {  get; set; }
  }
}
