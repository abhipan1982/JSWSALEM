using System;
using System.Runtime.InteropServices;
using SMF.Core.DC;
using PE.BaseModels.DataContracts.External;

namespace PE.Models.DataContracts.External.TCP.Telegrams
{
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public class DCTCPTelegramExt : BaseExternalTelegram
  {
    public DCHeaderExt Header { get; set; }
  }
}
