using System.ServiceModel;
using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.TCP
{
  public interface ITcpSenderProxyBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task Send(DataContractBase message);
  }
}
