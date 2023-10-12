using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCCrewElement: DataContractBase
  {
    [DataMember] public long CrewId { get; set; }
    [DataMember] public string LeaderName { get; set; }
    [DataMember] public string CrewName { get; set; }
    [DataMember] public string CrewDescription { get; set; }
  }
}
