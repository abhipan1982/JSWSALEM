using System;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.Internal.TCP
{
  [DataContract]
  [Serializable]
  public class DCTCPTestCommunicationTelegram : DataContractBase
  {
    [DataMember] public ushort TestShortField { get; set; }

    [DataMember] public string TestStringField { get; set; }
  }
}
