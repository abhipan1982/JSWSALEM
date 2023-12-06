using System.Threading.Tasks;
using PE.DBA.Base.Module.Communication;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.Communication;
using PE.Core;
using PE.Interfaces.Modules;
using PE.Interfaces.SendOffices.DBA;
using PE.Interfaces.Managers.PRM;

namespace PE.DBA.DataBaseAdapter.Communication
{
  public class SendOffice : ModuleBaseSendOffice, IDbAdapterSendOffice
  {
    //Added by AP on 07122023
    public virtual Task<SendOfficeResult<DCBatchDataStatus>> SendBatchDataToAdapterAsync(
      DCL3L2BatchDataDefinition dataToSend)
    {
      string targetModuleName = Constants.SmfAuthorization_Module_ProdManager;
      IWorkOrderManager client = InterfaceHelper.GetFactoryChannel<IWorkOrderManager>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessBatchDataAsync(dataToSend));
    }
  }
}
