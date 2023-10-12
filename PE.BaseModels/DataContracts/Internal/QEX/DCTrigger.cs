using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.QEX
{
  public class DCTrigger : DataContractBase
  {
    [DataMember] public string TriggerName { get; set; }
  }
}
