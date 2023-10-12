using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.STP
{
  public class DCTelegramSetup : DataContractBase
  {
    [DataMember] public byte[] DataStream { get; set; }

    [DataMember] public int TelegramId { get; set; }

    [DataMember] public short Port { get; set; }

    [DataMember] public string IpAddress { get; set; }
  }
}
