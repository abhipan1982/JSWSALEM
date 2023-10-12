using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.TCP.Base
{
  [Serializable]
  public class ASCIIBaseTelegram : BaseExternalTelegram
  {
    [DataMember] public string TelegramString { get; set; }

    [DataMember] public List<string> CommandsList { get; set; }
  }
}
