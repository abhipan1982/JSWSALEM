using System.Threading.Tasks;
using SMF.Core.DC;

namespace PE.Interfaces.SendOffices.TCP
{
  public interface ITcpProxySendOffice
  {
    Task SendTestTelegramResponse(DataContractBase tel);
  }
}
