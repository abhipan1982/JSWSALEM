using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCTelegramSetupId : DataContractBase
  {
    [DataMember] public long TelegramId { get; set; }
  }
}
