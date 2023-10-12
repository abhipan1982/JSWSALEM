using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupListOfParameres : DataContractBase
  {
    [DataMember] public string SetupName { get; set; }

    [DataMember] public long SetupId { get; set; }

    [DataMember] public long? SetupType { get; set; }

    [DataMember] public Dictionary<long, long> ListOfParametres { get; set; }
  }
}
