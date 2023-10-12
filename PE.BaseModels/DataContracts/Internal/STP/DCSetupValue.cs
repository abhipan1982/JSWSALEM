using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCSetupValue : DataContractBase
  {
    [DataMember] public long SetupInstructionId { get; set; }

    [DataMember] public string Value { get; set; }
  }
}
