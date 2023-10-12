using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.RLS
{
  public class DCRollDiameters : DataContractBase
  {
    [DataMember] public List<DCRollDiameter> RollDiameters { get; set; }
  }

  public class DCRollDiameter
  {
    [DataMember] public string StandName { get; set; }

    [DataMember] public long? AssetId { get; set; }

    [DataMember] public double? ActualDiameter { get; set; }

  }
}
