using System;
using System.Collections.Generic;
using PE.BaseModels.DataContracts.External.TCP.Base;
using SMF.Core.DC;

namespace PE.BaseModels.DataContracts.External.TCP.Telegrams
{
  [Serializable]
  public class DCTCPTestCommunicationASCIITelegramExt : ASCIIBaseTelegram
  {
    public override int ToExternal(DataContractBase dc)
    {
      CommandsList = new List<string>();
      CommandsList.Add($"d8000{(char)13}");
      CommandsList.Add($"d7010{(char)13}");
      CommandsList.Add($"d2100.1{(char)13}");
      CommandsList.Add($"d2100.2{(char)13}");
      CommandsList.Add($"d2100.3{(char)13}");
      CommandsList.Add($"d2110.1{(char)13}");
      CommandsList.Add($"d2110.2{(char)13}");
      CommandsList.Add($"d2110.3{(char)13}");
      CommandsList.Add($"d2120.1{(char)13}");
      CommandsList.Add($"d2120.2{(char)13}");
      CommandsList.Add($"d2120.3{(char)13}");
      CommandsList.Add($"d7090{(char)13}");
      CommandsList.Add($"d7060{(char)13}");

      return 0;
    }
  }
}
