using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.Interfaces.Managers
{
  //Added by AP on 08072023
  public interface ITcpTelegramCommunicationManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTcpTelegramSendAsync(DataContractBase message);
  }
}
