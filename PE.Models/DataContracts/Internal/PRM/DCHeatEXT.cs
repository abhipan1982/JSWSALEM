using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.PRM
{
  [DataContract]
  [Serializable]
  public class DCHeatEXT : DataContractBase
  {
    [DataMember] public long HeatId { get; set; }

    [DataMember] public string HeatName { get; set; }

    [DataMember] public long? FKHeatSupplierId { get; set; }

    [DataMember] public long? FKSteelgradeId { get; set; }

    [DataMember] public double? HeatWeight { get; set; }

    [DataMember] public bool? IsDummy { get; set; }
  }
}
