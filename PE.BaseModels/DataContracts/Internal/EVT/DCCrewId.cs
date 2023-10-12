using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCCrewId : DataContractBase
  {
    [DataMember] public long CrewId { get; set; }
  }
}
