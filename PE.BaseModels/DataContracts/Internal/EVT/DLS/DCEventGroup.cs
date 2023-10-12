using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.EVT
{
  public class DCEventGroup : DataContractBase
  {
    [DataMember] public long Id { get; set; }

    [DataMember] public string GroupName { get; set; }

    [DataMember] public string GroupCode { get; set; }
  }
}
