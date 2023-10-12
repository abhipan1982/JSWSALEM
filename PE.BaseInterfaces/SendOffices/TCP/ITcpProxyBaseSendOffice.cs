using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.TCP
{
  public interface ITcpProxyBaseSendOffice
  {
    Task SendTestTelegramResponse(DataContractBase tel);
  }
}
