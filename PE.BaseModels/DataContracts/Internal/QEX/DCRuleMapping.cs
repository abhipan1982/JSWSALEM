using System.Collections.Generic;
using System.Runtime.Serialization;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCRuleMapping : DataContractBase
  {
    [DataMember] public List<DCMappingEntry> RuleMappings { get; set; }
  }
  public class DCMappingEntry
  {
    [DataMember] public string Identifier { get; set; }
    [DataMember] public string RulesIdentifier { get; set; }
    [DataMember] public QEDirection Direction { get; set; }
    [DataMember] public QEParamType Type { get; set; }
  }
}
