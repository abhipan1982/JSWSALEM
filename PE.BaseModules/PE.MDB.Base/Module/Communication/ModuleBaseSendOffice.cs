using PE.Core;
using PE.Interfaces.Modules;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;
using SMF.Core.Communication;
using SMF.Module.Core;

namespace PE.MDB.Base.Module.Communication
{
  public class ModuleBaseSendOffice : ModuleSendOfficeBase
  {
    public virtual Task<SendOfficeResult<DCAckMessage>> SendHello(DCHelloMessage message)
    {
      string targetModuleName = Modules.ModuleA.Name;
      IModuleABase client = InterfaceHelper.GetFactoryChannel<IModuleABase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.Hello(message));
    }
  }
}
