using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCRulesTriggerEvents : DataContractBase
  {
    [DataMember] public List<string> RulesTriggerEvents { get; set; }
  }
}
