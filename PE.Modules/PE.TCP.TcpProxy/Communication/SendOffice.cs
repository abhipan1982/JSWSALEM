using PE.Interfaces.Modules;
using System.ServiceModel;
using System.Threading.Tasks;
using PE.Interfaces.SendOffices.TCP;
using PE.TCP.Base.Module.Communication;
using SMF.Core.DC;
using PE.Core;
using SMF.Core.Communication;

namespace PE.TCP.TcpProxy.Communication
{
  public class SendOffice : ModuleBaseSendOffice,ITcpProxySendOffice
  {
    //TODO SendOffice for replies from telegrams
    public Task SendTestTelegramResponseToAdapter(DataContractBase tel)
    {
      string targetModuleName = Constants.SmfAuthorization_Module_L1Adapter;
      IAdapter client = InterfaceHelper.GetFactoryChannel<IAdapter>(targetModuleName);

      //call method on remote module
      return HandleModuleSendMethod(targetModuleName, () => client.ProcessTestTelegramAsync(tel));   
    }
  }
}
