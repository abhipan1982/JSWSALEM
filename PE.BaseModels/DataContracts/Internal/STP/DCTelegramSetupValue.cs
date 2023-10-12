using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCTelegramSetupValue : DataContractBase
  {
    [DataMember] public long TelegramId { get; set; }

    [DataMember] public string Value { get; set; }

    [DataMember] public string TelegramStructureIndex { get; set; }
  }
}
