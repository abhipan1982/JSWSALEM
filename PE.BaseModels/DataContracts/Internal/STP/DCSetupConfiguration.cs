using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupConfiguration : DataContractBase
  {
    [DataMember] public long SetupConfigurationId { get; set; }

    [DataMember] public List<long> Setups { get; set; }

    [DataMember] public string SetupConfigurationName { get; set; }

    [DataMember] public short SetupConfigurationVersion { get; set; }

    [DataMember] public bool IsSteelgradeRelated { get; set; }
  }
}
