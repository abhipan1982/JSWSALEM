using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using PE.Models.DataContracts.External.TCP.Headers;
using PE.Models.DataContracts.Internal.TCP;
using PE.Helpers;
using SMF.Core.DC;

namespace PE.Models.DataContracts.External.TCP.Telegrams
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCTCPChargingBedTelegramExt : BaseExternalTelegram
  {
    //private const int TestStringFieldSize = 3;

    //[DataMember] [MarshalAs(UnmanagedType.ByValArray, SizeConst = TestStringFieldSize)]
    //public string TestStringField;
    //public char[] _testStringField;

    [DataMember] public ushort[] _shortField;
    [DataMember] public int[] _intField;

    [DataMember] public DCTCPTestHeaderExt Header { get; set; }

    //public string TestStringField
    //{
    //  get
    //  {
    //    string result = "";
    //    for (int i = 0; i < _testStringField.Length && _testStringField[i] != 0; i++)
    //    {
    //      result += _testStringField[i];
    //    }

    //    return result.ToUtf8();
    //  }
    //  set => _testStringField =
    //    value.Substring(0, value.Length > TestStringFieldSize ? TestStringFieldSize : value.Length)
    //      .PadRight(TestStringFieldSize, '\0').ToCharArray(0, TestStringFieldSize);
    //}

    public override int ToExternal(DataContractBase dc)
    {
      DCTCPChargingBedTelegram telegram = dc as DCTCPChargingBedTelegram;
      Header = new DCTCPTestHeaderExt
      {
        TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
        MessageId = (ushort)telegram.TelId,
        Counter = 15 //to be filled by sender
      };

     // _shortField = telegram.MEAS_TEMP_ACT;
      //TestStringField = telegram.TestStringField;

      for (int i = 0; i < _intField.Length; i++)
      {
        if (i == 0)
          _intField[i] = telegram.New;
        if (i == 1)
          _intField[i] = telegram.Forward_OCC;
        if (i == 2)
          _intField[i] = telegram.Backward_OCC;
        if (i == 3)
          _intField[i] = telegram.PYRO_OCC;
        if (i == 4)
          _intField[i] = telegram.BIT01;
        if (i == 5)
          _intField[i] = telegram.BIT02;
        if (i == 6)
          _intField[i] = telegram.BIT03;
        if (i == 7)
          _intField[i] = telegram.BIT04;
        if (i == 8)
          _intField[i] = telegram.BIT05;
        if (i == 9)
          _intField[i] = telegram.BIT06;
        if (i == 10)
          _intField[i] = telegram.BIT07;

      }

      for (ushort i = 0; i < _shortField.Length; i++)
      {
        if (i == 0)
          _shortField[i] = telegram.MEAS_TEMP_ACT;
        if (i == 1)
          _shortField[i] = telegram.MEAS_VEL_ACT;
      }

        return 0;
    }
  }
}
