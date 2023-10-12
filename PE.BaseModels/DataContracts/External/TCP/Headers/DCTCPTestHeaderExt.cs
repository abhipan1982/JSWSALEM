using System;
using System.Runtime.InteropServices;
using PE.Helpers;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.TCP.Headers
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 22)]
  public class DCTCPTestHeaderExt : BaseExternalTelegram
  {
    private const int TimeStampSize = 18;

    /// <summary>
    ///   Time stamp of sender system
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = TimeStampSize)]
    private char[] _timeStamp;

    /// <summary>
    ///   Message Counter
    /// </summary>
    public ushort Counter;

    /// <summary>
    ///   Unique message id
    /// </summary>
    public ushort MessageId;

    public string TimeStamp
    {
      get
      {
        string result = "";
        for (int i = 0; i < _timeStamp.Length && _timeStamp[i] != 0; i++)
        {
          result += _timeStamp[i];
        }

        return result.ToUtf8();
      }
      set => _timeStamp = value.Substring(0, value.Length > TimeStampSize ? TimeStampSize : value.Length)
        .PadRight(TimeStampSize, '\0').ToCharArray(0, TimeStampSize);
    }
  }
}
