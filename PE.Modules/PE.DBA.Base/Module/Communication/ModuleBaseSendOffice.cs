using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseInterfaces.SendOffices.DBA;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Core;
using SMF.Core.Communication;
using SMF.Module.Core;

namespace PE.DBA.Base.Module.Communication
{
  public class ModuleBaseSendOffice : ModuleSendOfficeBase, IDbAdapterBaseSendOffice
  {
    public virtual Task<SendOfficeResult<DCWorkOrderStatus>> SendWorkOrderDataToAdapterAsync(
      DCL3L2WorkOrderDefinition dataToSend)
    {
      string targetModuleName = Constants.SmfAuthorization_Module_ProdManager;
      IProdManagerBase client = InterfaceHelper.GetFactoryChannel<IProdManagerBase>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessWorkOrderDataAsync(dataToSend));
    }
  }
}
