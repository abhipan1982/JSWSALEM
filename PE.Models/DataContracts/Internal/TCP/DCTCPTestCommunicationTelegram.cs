using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.Models.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCTCPTestCommunicationTelegram : DataContractBase
  {
    [DataMember] public ushort TestShortField { get; set; }

    [DataMember] public string TestStringField { get; set; }

    [DataMember] public int TestIntegerField { get; set; }
  }
}
