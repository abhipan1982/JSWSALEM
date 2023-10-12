using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.PRM
{
  public class DCScrapGroup : DataContractBase
  {
    [DataMember] public long ScrapGroupId { get; set; }

    [DataMember] public string ScrapGroupCode { get; set; }

    [DataMember] public string ScrapGroupName { get; set; }

    [DataMember] public string ScrapGroupDescription { get; set; }
  }
}
