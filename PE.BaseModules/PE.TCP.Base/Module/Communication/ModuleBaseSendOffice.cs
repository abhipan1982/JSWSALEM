using System;
using System.Threading.Tasks;
using PE.BaseInterfaces.SendOffices.TCP;
using SMF.Core.DC;
using SMF.Module.Core;

namespace PE.TCP.Base.Module.Communication
{
  public class ModuleBaseSendOffice : ModuleSendOfficeBase, ITcpProxyBaseSendOffice
  {
    public virtual Task SendTestTelegramResponse(DataContractBase tel)
    {
      throw new NotImplementedException();
    }
  }
}
