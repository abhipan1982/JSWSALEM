using System.ServiceModel;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.TCP
{
  public interface ITcpTelegramCommunicationManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTcpTelegramSendAsync(DataContractBase message);
  }
}
