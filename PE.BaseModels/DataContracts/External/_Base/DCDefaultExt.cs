using System;
using System.Runtime.InteropServices;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCDefaultExt : BaseExternalTelegram
  {
    /// <summary>
    ///   Common telegram Header
    /// </summary>
    public DCHeaderExt Header { get; set; }

    public override int ToExternal(DataContractBase dc)
    {
      long unixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();

      Header = new DCHeaderExt
      {
        TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
        Counter = (UInt16)(unixSeconds % UInt16.MaxValue),
        MessageId = 2000
      };
      return 0;
    }
  }
}
