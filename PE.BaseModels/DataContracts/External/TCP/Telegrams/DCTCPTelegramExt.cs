using System;
using System.Runtime.InteropServices;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.TCP.Telegrams
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCTCPTelegramExt : BaseExternalTelegram
  {
    public DCHeaderExt Header { get; set; }
  }
}
