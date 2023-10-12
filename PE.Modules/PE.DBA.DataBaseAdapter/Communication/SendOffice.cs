using System.Threading.Tasks;
using PE.DBA.Base.Module.Communication;
using PE.Models.DataContracts.Internal.DBA;
using SMF.Core.Communication;
using PE.Core;
using PE.Interfaces.Modules;
using PE.Interfaces.SendOffices.DBA;

namespace PE.DBA.DataBaseAdapter.Communication
{
  public class SendOffice : ModuleBaseSendOffice, IDbAdapterSendOffice
  {
    //Added by AP on16082023
    public virtual Task<SendOfficeResult<DCBatchDataStatus>> SendBatchDataToAdapterAsync(
      DCL3L2BatchData dataToSend)
    {
      string targetModuleName = Constants.SmfAuthorization_Module_ProdManager;
      IProdManager client = InterfaceHelper.GetFactoryChannel<IProdManager>(targetModuleName);

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessBatchDataAsync(dataToSend));
    }
  }
}
