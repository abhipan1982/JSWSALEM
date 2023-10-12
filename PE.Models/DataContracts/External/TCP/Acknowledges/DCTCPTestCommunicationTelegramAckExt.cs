using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using PE.BaseModels.DataContracts.External.TCP.Headers;
using PE.BaseModels.DataContracts.Internal.TCP;
using PE.Helpers;
using SMF.Core.DC;

namespace PE.Models.DataContracts.External.TCP.Acknowledges
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCTCPTestCommunicationTelegramAckExt : BaseExternalTelegram
  {
    private const int TestStringFieldSize = 3;

    [DataMember] [MarshalAs(UnmanagedType.ByValArray, SizeConst = TestStringFieldSize)]
    //public string TestStringField;
    public char[] _testStringField;

    [DataMember] public ushort TestShortField;

    [DataMember] public DCTCPTestHeaderExt Header { get; set; }

    public string TestStringField
    {
      get
      {
        string result = "";
        for (int i = 0; i < _testStringField.Length && _testStringField[i] != 0; i++)
        {
          result += _testStringField[i];
        }

        return result.ToUtf8();
      }
      set => _testStringField =
        value.Substring(0, value.Length > TestStringFieldSize ? TestStringFieldSize : value.Length)
          .PadRight(TestStringFieldSize, '\0').ToCharArray(0, TestStringFieldSize);
    }

    public override DataContractBase ToInternal(int? telId = null)
    {
      DCTCPTestCommunicationTelegram telegram = new DCTCPTestCommunicationTelegram();

      telegram.TestShortField = TestShortField;
      telegram.TestStringField = TestStringField;
      telegram.TelId = Header.MessageId;

      return telegram;
    }
  }
}
