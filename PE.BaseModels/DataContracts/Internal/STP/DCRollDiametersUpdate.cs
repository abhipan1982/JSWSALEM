using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCRollDiametersUpdate : DataContractBase
  {
    [DataMember] public List<DCRollDiameter> RollDiameters { get; set; }
  }
}
